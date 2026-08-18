using Microsoft.AspNetCore.Mvc;
namespace LanguageLearning.API.Controllers;

[ApiController]
[Route("api/words")]
public class WordsController : ControllerBase
{
    private readonly IWordService _service;

    public WordsController(IWordService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(TranslationDto request)
    {
        await _service.CreateAsync(request);

        return Ok();
    }
}