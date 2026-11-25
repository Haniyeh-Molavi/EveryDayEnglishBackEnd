using LanguageLearning.Application.DTOs.Words;
using LanguageLearning.Application.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanguageLearning.API.Controllers;

[ApiController]
[Route("api/words")]
public class WordsController : ControllerBase
{
    private readonly IWordService _wordService;

    public WordsController(IWordService wordService)
    {
        _wordService = wordService;
    }

    // Add a new word. Gemini translates it into the other two languages.
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<WordDto>> Create(CreateWordRequest request)
    {
        return Ok(await _wordService.CreateAsync(request));
    }
}
