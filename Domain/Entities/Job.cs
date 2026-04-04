
namespace Domain.Entities;

public class Job
{ 
    public int Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; } 
    
    public int CompanyId { get; set; }
    
    [System.Text.Json.Serialization.JsonIgnore]
    public Company? Company { get; set; } 
}