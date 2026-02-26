namespace jaburrow.Services;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

public class NavigationTracker : IDisposable
{
    private readonly NavigationManager _nav;
    private readonly IJSRuntime _js;

    public NavigationTracker(NavigationManager nav, IJSRuntime js)
    {
        _nav = nav;
        _js = js;

        // Listen for route changes
        _nav.LocationChanged += OnLocationChanged;
    }

    private async void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        var uri = new Uri(e.Location);
        var path = uri.PathAndQuery;

        // Capture the old title
        var oldTitle = await _js.InvokeAsync<string>("eval", "document.title");

        // Wait until the title changes (max ~500ms)
        string newTitle = oldTitle;
        for (int i = 0; i < 10; i++)
        {
            await Task.Delay(50);
            newTitle = await _js.InvokeAsync<string>("eval", "document.title");
            if (newTitle != oldTitle)
                break;
        }

        Console.WriteLine($"[TRACKER] Sending page_path: {path}, page_title: {newTitle}");

        await _js.InvokeVoidAsync("blazorGtag", "event", "page_view", new
        {
            page_path = path,
            page_title = newTitle,
            page_location = e.Location
        });
    }

    public void Dispose()
    {
        _nav.LocationChanged -= OnLocationChanged;
    }
}
