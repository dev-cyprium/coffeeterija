using System;
namespace coffeterija.application.Responses
{
    public class ErrorResponse
    {
        public string Message { get; set; } = "Unfortunetly, something bad happaned. Check details below.";
        public string ErrorDetails { get; set; }
    }
}
