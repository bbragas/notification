﻿using System.Text.Json;

namespace Notification.Api.Errors;
public class ErrorDetails
{
    public ErrorDetails(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }

    public int StatusCode { get; set; }
    public string Message { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

}