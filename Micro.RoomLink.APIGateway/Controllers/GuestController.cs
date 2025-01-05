using System.Text;
using Micro.RoomLink.APIGateway.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Micro.RoomLink.APIGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class GuestController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly Urls _urls;

    public GuestController(HttpClient httpClient, IOptions<Urls> urls)
    {
        _httpClient = httpClient;
        _urls = urls.Value;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var response = await _httpClient.GetAsync(_urls.Guests + "api/guests");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, response.Content.Headers.ContentType?.ToString() ?? "application/json");
            }

            return StatusCode((int)response.StatusCode);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetGuest([FromRoute] int id)
    {
        try
        {
            var response = await _httpClient.GetAsync(_urls.Guests + "api/guests/" + id);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return Content(responseContent, response.Content.Headers.ContentType?.ToString() ?? "application/json");
            }
            return StatusCode((int)response.StatusCode);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Guest guest)
    {
        try
        {
            var content = new StringContent(JsonConvert.SerializeObject(guest),Encoding.UTF8, @"application/json");
            var response = await _httpClient.PostAsync(_urls.Guests + "api/guests", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return Content(responseContent, response.Content.Headers.ContentType?.ToString() ?? "application/json");
            }

            return StatusCode((int)response.StatusCode);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute]int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync(_urls.Guests + "api/guests/" + id);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return Content(responseContent, response.Content.Headers.ContentType?.ToString() ?? "application/json");
            }
            return StatusCode((int)response.StatusCode);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Guest guest)
    {
        try
        {
            var content = new StringContent(JsonConvert.SerializeObject(guest), Encoding.UTF8, @"application/json");
            var response = await _httpClient.PutAsync(_urls.Guests + "api/guests/" + id, content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return Content(responseContent, response.Content.Headers.ContentType?.ToString() ?? "application/json");
            }

            return StatusCode((int)response.StatusCode);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    

   
}