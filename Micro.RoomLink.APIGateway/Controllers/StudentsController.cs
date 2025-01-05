using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Micro.RoomLink.APIGateway.Controllers;

[ApiController]
[Route("[controller]")]

public class StudentsController:ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly Urls _urls;

    public StudentsController(HttpClient httpClient,IOptions<Urls> urls)
    {
        _httpClient = httpClient;
        _urls = urls.Value;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        Console.WriteLine(_urls.Students);

        try
        {
            var response = await _httpClient.GetAsync(_urls.Students + "Students");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, response.Content.Headers.ContentType?.ToString() ?? "application/json");
            }

            return StatusCode((int)response.StatusCode);
        }
        catch (Exception e){
            return StatusCode(500, e.Message);
    }
}
}