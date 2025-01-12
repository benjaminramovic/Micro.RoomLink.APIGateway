namespace Micro.RoomLink.APIGateway.Models;

public class Room
{
    public int roomNumber { get; set; }
    public string type { get; set; }
    public int pricePerNight { get; set; }
    public Boolean isAvailable { get; set; }
    public int capacity { get; set; }
    public string description { get; set; }
}