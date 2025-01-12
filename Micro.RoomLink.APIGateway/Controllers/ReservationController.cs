using System.Text;
using Micro.RoomLink.APIGateway.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Micro.RoomLink.APIGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly Urls _urls;

    public ReservationController(HttpClient httpClient, IOptions<Urls> urls)
    {
        _httpClient = httpClient;
        _urls = urls.Value;
    }

    [HttpGet]
    public async Task<IActionResult> GetReservations()
    {
        try
        {
            var response = await _httpClient.GetAsync(_urls.Reservations + "api/reservations");
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
    public async Task<IActionResult> GetReservation([FromRoute] string id)
    {
        try
        {
            var response = await _httpClient.GetAsync(_urls.Reservations + "api/reservations/" + id);
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
    public async Task<IActionResult> Add([FromBody] Reservation reservation)
    {
        try
        {
            var content = new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8,
                "application/json");
            var response = await _httpClient.PostAsync(_urls.Reservations + "api/reservations", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return Content(responseContent,
                    response.Content.Headers.ContentType?.ToString() ?? "application/json");
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
            var response = await _httpClient.DeleteAsync(_urls.Reservations + "api/reservations/" + id);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return Content(responseContent, response.Content.Headers.ContentType?.ToString() ?? "application/json");
            }

            return StatusCode((int)response.StatusCode);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] Reservation reservation)
    {
        try
        {
            var content = new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8,"application/json");
            var response = await _httpClient.PutAsync(_urls.Reservations + "api/reservations/" + id, content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return Content(responseContent, response.Content.Headers.ContentType?.ToString() ?? "application/json");
            }
            return StatusCode((int)response.StatusCode);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("guests/{id}/reservations")]
    public async Task<IActionResult> GetReservations([FromRoute] string id)
    {
        try
        {
            var response = await _httpClient.GetAsync(_urls.Reservations + "api/guests/" + id + "/reservations");
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
    
    
}