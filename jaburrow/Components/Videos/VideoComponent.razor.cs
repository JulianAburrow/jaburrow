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

            // youtu.be short links
            if (uri.Host.Contains("youtu.be"))
            {
                var id = uri.AbsolutePath.Trim('/');
                return $"https://www.youtube.com/embed/{id}?rel=0&vq=hd1080";
            }

            // youtube.com/watch?v=...
            if (uri.Host.Contains("youtube.com"))
            {
                var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
                var id = query["v"];

                if (!string.IsNullOrWhiteSpace(id))
                    return $"https://www.youtube.com/embed/{id}?rel=0&vq=hd1080";
            }

            // already embed
            if (uri.AbsolutePath.Contains("/embed/"))
            {
                var id = uri.AbsolutePath.Split("/embed/").Last();
                return $"https://www.youtube.com/embed/{id}?rel=0&vq=hd1080";
            }
        }
        catch
        {
            // ignore
        }

        // FINAL FALLBACK: ALWAYS convert watch URLs to embed
        if (Url.Contains("watch?v="))
        {
            var id = Url.Split("watch?v=").Last().Split('&').First();
            return $"https://www.youtube.com/embed/{id}?rel=0&vq=hd1080";
        }

        return Url;
    }
}