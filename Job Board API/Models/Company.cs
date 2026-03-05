using System.Text.Json.Serialization;

namespace Job_Board_API.Models;

public class Company
{
    public int Id { get; set; } 
    public string Name { get; set; }
    public string Industry { get; set; }
    public string Address { get; set; }
    
    [JsonIgnore]
    public ICollection<Job>? Jobs { get; set; } = new List<Job>();
    
}