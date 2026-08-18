using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TranslateController : ControllerBase
{
    private readonly TranslatorService _translator;

    public TranslateController(TranslatorService translator)
    {
        _translator = translator;
    }

    [HttpGet]
    public async Task<IActionResult> Translate(string text)
    {
        var result = await _translator.TranslateAsync(text, "fa");
        return Ok(result);
    }
}