using DBAcess;
using DBAcess.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

[ApiController]
[Route("api/youtube")]
public class YoutubeController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "AIzaSyAn0G-wmLadr3ZoEblx7uerfrXhidcMdJQ";
    private readonly HistoryMadeSimpleContext _context;

    public YoutubeController(IHttpClientFactory httpClientFactory, HistoryMadeSimpleContext context)
    {
        _context = context;
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpGet("playlist")]
    public async Task<IActionResult> GetPlaylistVideos([FromQuery] string playlistId)
    {
        if (string.IsNullOrEmpty(playlistId))
            return BadRequest("Missing playlistId");

        var videos = new List<YoutubeVideoDto>();
        string nextPageToken = "";

        do
        {
            var apiUrl = $"https://www.googleapis.com/youtube/v3/playlistItems?part=snippet&maxResults=50&playlistId={playlistId}&key={_apiKey}&pageToken={nextPageToken}";
            var json = await _httpClient.GetStringAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<YoutubePlaylistResponse>(json);

            foreach (var item in data.Items)
            {
                videos.Add(new YoutubeVideoDto
                {
                    Title = item.Snippet.Title,
                    VideoId = item.Snippet.ResourceId.VideoId,
                    Url = $"https://www.youtube.com/embed/{item.Snippet.ResourceId.VideoId}",
                    Thumbnail = item.Snippet.Thumbnails?.Medium?.Url
                });
            }

            nextPageToken = data.NextPageToken ?? "";
        }
        while (!string.IsNullOrEmpty(nextPageToken));
        
        foreach(var item in videos)
        {
            var videoinsert = new DBAcess.Entities.Video
            {
                Title = item.Title,
                LessonMethodId = 25,
                Url = item.Url,
                Thumbnail = item.Thumbnail
            };
            _context.Videos.Add(videoinsert);
        }
         await _context.SaveChangesAsync();
        return Ok(videos);
    }
}
