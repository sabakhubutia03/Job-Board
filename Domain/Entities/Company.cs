using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Company
{
    public int Id { get; set; } 
    public string Name { get; set; }
    public string Industry { get; set; }
    public string Address { get; set; }
    
    [JsonIgnore]
    public ICollection<Job>? Jobs { get; set; } = new List<Job>();
    
}