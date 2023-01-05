using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SynergyISP.Presentation.Apps.Portal.Server.Pages;
[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ErrorModel : PageModel
{
    /// <summary>
    /// Gets or sets the request id.
    /// </summary>
    public string? RequestId { get; set; }

    /// <summary>
    /// Gets a value indicating whether show request id.
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    private readonly ILogger<ErrorModel> _logger;

    public ErrorModel(ILogger<ErrorModel> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Ons the get.
    /// </summary>
    public void OnGet()
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    }
}
