namespace jaburrow.Components;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

public partial class CookieBannerComponent : ComponentBase
{
    [Inject] protected IJSRuntime JS { get; set; } = default!;

    protected bool ShowBanner { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var hasConsented = await JS.InvokeAsync<bool>("cookieConsent.hasConsented");
            ShowBanner = !hasConsented;
            StateHasChanged();
        }
    }

    protected async Task AcceptCookies()
    {
        await JS.InvokeVoidAsync("cookieConsent.setConsented");
        ShowBanner = false;
    }
}