using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace LanguageLearning.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("API is working");
    }
}