using Microsoft.AspNetCore.Components;

namespace jaburrow.Components.Videos;

public partial class VideoComponent
{
    [Parameter] public string Url { get; set; } = string.Empty;

    [Parameter] public string Title { get; set; } = string.Empty;
    
    [Parameter] public string Description { get; set; } = string.Empty;

    private string GetEmbedUrl()
    {
        if (string.IsNullOrWhiteSpace(Url))
            return string.Empty;

        // Handle youtu.be short links
        if (Url.Contains("youtu.be"))
        {
            var id = Url.Split('/').Last();
            return $"https://www.youtube.com/embed/{id}?rel=0&vq=hd1080";
        }

        // Handle full YouTube watch URLs
        if (Url.Contains("watch?v="))
        {
            var id = Url.Split("watch?v=").Last();
            return $"https://www.youtube.com/embed/{id}?rel=0&vq=hd1080";
        }

        // If already an embed URL, just append HD params
        if (Url.Contains("/embed/"))
        {
            return Url + "?rel=0&vq=hd1080";
        }

        // Fallback
        return Url;
    }
}
