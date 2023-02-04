namespace SynergyISP.Presentation.APIs;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
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

            if (context.Request.Headers.TryGetValue(HeaderNames.IfNoneMatch, out var etag)
             && checksum == etag)
            {
                response.StatusCode = StatusCodes.Status304NotModified;
                response.ContentLength = 0;
                await response.BodyWriter.WriteAsync(string.Empty.GetBytes(Encoding.UTF8));
                return;
            }
        }

        ms.Position = 0;
        await ms.CopyToAsync(originalStream);
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