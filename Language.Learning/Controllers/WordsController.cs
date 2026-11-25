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

    // GET api/words?search=cat&categoryId=...
    [HttpGet]
    public async Task<ActionResult<List<WordDto>>> GetAll(
        [FromQuery] string? search,
        [FromQuery] Guid? categoryId)
    {
        return Ok(await _wordService.GetAllAsync(search, categoryId));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<WordDto>> GetById(Guid id)
    {
        return Ok(await _wordService.GetByIdAsync(id));
    }

    // Add a new word. Gemini translates it into the other two languages.
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<WordDto>> Create(CreateWordRequest request)
    {
        var word = await _wordService.CreateAsync(request);

        return CreatedAtAction(nameof(GetById), new { id = word.Id }, word);
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _wordService.DeleteAsync(id);

        return NoContent();
    }
}
