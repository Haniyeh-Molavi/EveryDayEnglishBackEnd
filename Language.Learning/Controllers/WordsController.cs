using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
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
    public async Task<IActionResult> Create(TranslationDto translationDto)
    {
        if (translationDto == null)
        {
            throw new ArgumentNullException(nameof(translationDto));
        }
        await _service.CreateAsync(translationDto);

        return Ok();
    }
}