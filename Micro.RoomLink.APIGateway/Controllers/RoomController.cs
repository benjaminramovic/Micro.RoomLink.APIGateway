using System.Text;
using Micro.RoomLink.APIGateway.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Micro.RoomLink.APIGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly Urls _urls;

    public RoomController(HttpClient httpClient, IOptions<Urls> urls)
    {
        _httpClient = httpClient;
        _urls = urls.Value;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var response = await _httpClient.GetAsync(_urls.Rooms + "api/rooms");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, response.Content.Headers.ContentType?.ToString() ?? "application/json");
            }

            return StatusCode((int)response.StatusCode);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        try
        {
            var response = await _httpClient.GetAsync(_urls.Rooms + "api/rooms/" + id);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, response.Content.Headers.ContentType?.ToString() ?? "application/json");
            }

            return StatusCode((int)response.StatusCode);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Room room)
    {
        try
        {
            var content = new StringContent(JsonConvert.SerializeObject(room),Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_urls.Rooms + "api/rooms", content);
            if (response.IsSuccessStatusCode)
            {
                var contentResponse = await response.Content.ReadAsStringAsync();
                return Content(contentResponse, response.Content.Headers.ContentType?.ToString() ?? "application/json");
            }

            return StatusCode((int)response.StatusCode);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] Room room)
    {
        try
        {
            var content = new StringContent(JsonConvert.SerializeObject(room),Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(_urls.Rooms + "api/rooms/" + id, content);
            if (response.IsSuccessStatusCode)
            {
                var contentResponse = await response.Content.ReadAsStringAsync();
                return Content(contentResponse, response.Content.Headers.ContentType?.ToString() ?? "application/json");
            }
            return StatusCode((int)response.StatusCode);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync(_urls.Rooms + "api/rooms/" + id);
            if (response.IsSuccessStatusCode)
            {
                var contentResponse = await response.Content.ReadAsStringAsync();
                return Content(contentResponse, response.Content.Headers.ContentType?.ToString() ?? "application/json");
            }
            return StatusCode((int)response.StatusCode);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}