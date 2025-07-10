public class YoutubePlaylistResponse
{
    public List<YoutubePlaylistItem> Items { get; set; }
    public string NextPageToken { get; set; }
}

public class YoutubePlaylistItem
{
    public YoutubeSnippet Snippet { get; set; }
}

public class YoutubeSnippet
{
    public string Title { get; set; }
    public YoutubeResourceId ResourceId { get; set; }
    public YoutubeThumbnails Thumbnails { get; set; }
}

public class YoutubeResourceId
{
    public string VideoId { get; set; }
}

public class YoutubeThumbnails
{
    public YoutubeThumbnail Medium { get; set; }
}

public class YoutubeThumbnail
{
    public string Url { get; set; }
}
