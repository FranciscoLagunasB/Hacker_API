using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class HackerNewsController : ControllerBase
{
    private readonly HackerNewsService _hackerNewsService;

    public HackerNewsController(HackerNewsService hackerNewsService)
    {
        _hackerNewsService = hackerNewsService;
    }

    [HttpGet("beststories")]
public async Task<IActionResult> GetBestStories([FromQuery] int n)
{
    if (n <= 0) return BadRequest("The number of stories must be greater than 0.");

    var stories = await _hackerNewsService.GetBestStoriesAsync(n);
    Console.WriteLine(stories);
    foreach (var story in stories)
{
    Console.WriteLine($"Title: {story.title}");
    Console.WriteLine($"URI: {story.url}");
    Console.WriteLine($"Posted By: {story.by}");
    Console.WriteLine();
}
    return Ok(stories.Select(s => new
    {
        title = s.title,
        uri = s.url,
        postedBy = s.by,
        time = DateTimeOffset.FromUnixTimeSeconds(s.time).ToString("yyyy-MM-ddTHH:mm:sszzz"),
        score = s.score,
        commentCount = s.descendants
    }));
}
}



namespace YourNamespace
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hola");
        }
    }
}
