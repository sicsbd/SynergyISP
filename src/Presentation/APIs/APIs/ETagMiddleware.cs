namespace SynergyISP.Presentation.APIs;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using SynergyISP.Domain.Helpers;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

public class ETagMiddleware
{
    private readonly RequestDelegate _next;

    public ETagMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var response = context.Response;
        var originalStream = response.Body;

        using var ms = new MemoryStream();
        response.Body = ms;

        await _next(context);

        if (IsEtagSupported(response))
        {
            string checksum = CalculateChecksum(ms);

            response.Headers[HeaderNames.ETag] = checksum;
            StringValues etag;

            ExtractETags(context.Request.Headers, out (string? matchETag, string? noneMatchETag, string? modifiedSinceETag, string? unmodifiedSinceETag) etags);

            if (etags.matchETag is not null
             && !etags.matchETag.IsNullOrWhiteSpace()
             && checksum == etags.matchETag)
            {
                response.StatusCode = checksum == etags.matchETag
                                    ? StatusCodes.Status304NotModified
                                    : StatusCodes.Status412PreconditionFailed;

                response.ContentLength = 0;
                await response.BodyWriter.WriteAsync(string.Empty.GetBytes(Encoding.UTF8));
                return;
            }

            if (etags.noneMatchETag is not null
             && !etags.noneMatchETag.IsNullOrWhiteSpace()
             && checksum == etags.noneMatchETag)
            {
                response.StatusCode = checksum == etags.matchETag
                                    ? StatusCodes.Status304NotModified
                                    : StatusCodes.Status412PreconditionFailed;
                response.ContentLength = 0;
                await response.BodyWriter.WriteAsync(string.Empty.GetBytes(Encoding.UTF8));
                return;
            }

            if (etags.modifiedSinceETag is not null
             && !etags.modifiedSinceETag.IsNullOrWhiteSpace()
             && checksum == etags.modifiedSinceETag)
            {
                response.StatusCode = checksum == etags.matchETag
                                    ? StatusCodes.Status304NotModified
                                    : StatusCodes.Status412PreconditionFailed;
                response.ContentLength = 0;
                await response.BodyWriter.WriteAsync(string.Empty.GetBytes(Encoding.UTF8));
                return;
            }

            if (etags.unmodifiedSinceETag is not null
             && !etags.unmodifiedSinceETag.IsNullOrWhiteSpace()
             && checksum == etags.unmodifiedSinceETag)
            {
                response.StatusCode = checksum == etags.matchETag
                                    ? StatusCodes.Status304NotModified
                                    : StatusCodes.Status412PreconditionFailed;
                response.ContentLength = 0;
                await response.BodyWriter.WriteAsync(string.Empty.GetBytes(Encoding.UTF8));
                return;
            }
        }

        ms.Position = 0;
        await ms.CopyToAsync(originalStream);
    }

    private void ExtractETags(
        IHeaderDictionary headers,
        out (string?, string?, string?, string?) etags)
    {
        StringValues matchETag, noneMatchETag, modifiedSinceETag, unmodifiedSinceETag;
        headers.TryGetValue(HeaderNames.IfMatch, out matchETag);
        headers.TryGetValue(HeaderNames.IfNoneMatch, out noneMatchETag);
        headers.TryGetValue(HeaderNames.IfUnmodifiedSince, out modifiedSinceETag);
        headers.TryGetValue(HeaderNames.IfUnmodifiedSince, out unmodifiedSinceETag);
        etags = (matchETag, noneMatchETag, modifiedSinceETag, unmodifiedSinceETag);

    }

    private static bool IsEtagSupported(HttpResponse response)
    {
        if (response.StatusCode != StatusCodes.Status200OK)
        {
            return false;
        }

        // The 20kb length limit is not based in science. Feel free to change
        if (response.Body.Length > 20 * 1024)
        {
            return false;
        }

        return !response.Headers.ContainsKey(HeaderNames.ETag);
    }

    private static string CalculateChecksum(MemoryStream ms)
    {
        using SHA1 algo = SHA1.Create();
        ms.Position = 0;
        byte[] bytes = algo.ComputeHash(ms);
        string checksum = $"\"{WebEncoders.Base64UrlEncode(bytes)}\"";

        return checksum;
    }
}

public static class ApplicationBuilderExtensions
{
    public static void UseETag(this IApplicationBuilder app)
    {
        app.UseMiddleware<ETagMiddleware>();
    }
}