using Microsoft.AspNetCore.Mvc;

namespace ISPH.API.Responses
{
    public static class HttpResponses
    {
        public static IActionResult ServerError(this ControllerBase controller, string message)
        {
            return controller.StatusCode(500, message);
        }
    }
}