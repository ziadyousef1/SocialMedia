namespace Social.Application.Models;

public class OperationResult<T> 
{
    public T Payload { get; set; }
    public bool IsSuccess { get; set; } = true;
    public List<Error> Errors { get; set; } = new List<Error>();
    
    
}