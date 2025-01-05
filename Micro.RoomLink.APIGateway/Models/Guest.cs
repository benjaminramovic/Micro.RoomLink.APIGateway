
namespace Micro.RoomLink.APIGateway.Models;

public class Guest
{
    public int id { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string email { get; set; }
    public DateTime dateOfBirth { get; set; }
}