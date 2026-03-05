namespace Job_Board_API.Models;

public class ApiProblemDitaile
{
    public string Type { get; init; }   
    public string Title { get; init; }
    public int StatusCode { get; init; }
    public string Details { get; init; }

    public string Instance { get; init; }
}