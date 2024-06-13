using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

public class HackerNewsService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private const string BestStoriesUrl = "https://hacker-news.firebaseio.com/v0/beststories.json";
    private const string StoryUrlTemplate = "https://hacker-news.firebaseio.com/v0/item/{0}.json";

    public HackerNewsService(HttpClient httpClient, IMemoryCache cache)
    {
        _httpClient = httpClient;
        _cache = cache;
    }

    public async Task<List<Story>> GetBestStoriesAsync(int n)
{
    var storyIds = await GetBestStoryIdsAsync();
    var tasks = storyIds.Take(n).Select(id => GetStoryByIdAsync(id));
    var stories = await Task.WhenAll(tasks);

    return stories.OrderByDescending(s => s.score).ToList();
}


    private async Task<List<int>> GetBestStoryIdsAsync()
    {
        if (!_cache.TryGetValue("BestStories", out List<int> storyIds))
        {
            var response = await _httpClient.GetStringAsync(BestStoriesUrl);
            storyIds = JsonSerializer.Deserialize<List<int>>(response);

            var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
            _cache.Set("BestStories", storyIds, cacheEntryOptions);
        }

        return storyIds;
    }

    private async Task<Story> GetStoryByIdAsync(int id)
{
    Console.WriteLine($"Fetching story with id: {id}");
    var response = await _httpClient.GetStringAsync(string.Format(StoryUrlTemplate, id));
    var story = JsonSerializer.Deserialize<Story>(response);

    return story;
}

}
public class Story
{
    public string title { get; set; }
    public string url { get; set; }
    public string by { get; set; }
    public long time { get; set; }
    public int score { get; set; }
    public int descendants { get; set; }
}
