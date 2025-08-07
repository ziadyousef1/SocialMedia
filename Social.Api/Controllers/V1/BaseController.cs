using Microsoft.AspNetCore.Mvc;
using Social.Application.Common;
using Social.Application.Enums;
using Social.Application.Models;

namespace Social.Api.Controllers.V1;

public class BaseController : ControllerBase
{
 
    protected IActionResult HandleErrorResponse(List<Error> errors)
    {
        if(errors.Any(e => e.Code == ErrorCode.NotFound))
        {
            var error = errors.FirstOrDefault(e => e.Code == ErrorCode.NotFound);
            var apiError = new ErrorResponse()
            {
                StatusCode = 404,
                StatusPhrase = "Not Found",
                Timestamp = DateTime.UtcNow,
                Errors = new List<string> { error.Message }
            };
            return NotFound(apiError);
                    
        }

        if (errors.Any(e => e.Code == ErrorCode.ServerError))
        {
            var error = errors.FirstOrDefault(e => e.Code == ErrorCode.ServerError);
            var apiError = new ErrorResponse()
            {
                StatusCode = 500,
                StatusPhrase = "Server Error",
                Timestamp = DateTime.UtcNow,
                Errors = new List<string> { error.Message }
            };
            return StatusCode(500,apiError);
        }
        var apiErrorResponse = new ErrorResponse
        {
            StatusCode = 400,
            StatusPhrase = "Bad Request",
            Timestamp = DateTime.UtcNow,
            Errors = errors.Select(e => e.Message).ToList()
        };
        return BadRequest(apiErrorResponse);
    }
}