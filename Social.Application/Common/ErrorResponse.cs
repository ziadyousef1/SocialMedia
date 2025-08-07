namespace Social.Application.Common;

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string StatusPhrase { get; set; }
    public DateTime Timestamp { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
    
    
}