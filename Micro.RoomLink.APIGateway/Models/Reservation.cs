namespace Micro.RoomLink.APIGateway.Models;

public class Reservation
{
    public int guest_id { get; set; }
    public string room_id { get; set; }
    public DateTime check_in { get; set; }
    public DateTime check_out { get; set; }
    public float total_price { get; set; }
    public string status { get; set; }
}