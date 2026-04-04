using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class User
{ 
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    
}