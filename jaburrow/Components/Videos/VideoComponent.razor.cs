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

        try
        {
            var uri = new Uri(Url);

            // Handle youtu.be short links
            if (uri.Host.Contains("youtu.be"))
            {
                var id = uri.AbsolutePath.Trim('/');
                return $"https://www.youtube.com/embed/{id}?rel=0&vq=hd1080";
            }

            // Handle full YouTube watch URLs
            if (uri.Host.Contains("youtube.com") && uri.Query.Contains("v="))
            {
                var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
                var id = query["v"]; // <-- THIS is the correct extraction

                return $"https://www.youtube.com/embed/{id}?rel=0&vq=hd1080";
            }

            // Already an embed URL
            if (uri.AbsolutePath.Contains("/embed/"))
            {
                var id = uri.AbsolutePath.Split("/embed/").Last();
                return $"https://www.youtube.com/embed/{id}?rel=0&vq=hd1080";
            }
        }
        catch
        {
            // fallback
        }

        return Url;
    }
}