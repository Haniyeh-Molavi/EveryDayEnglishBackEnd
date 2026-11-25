using LanguageLearning.Application.DTOs.Sentences;
using LanguageLearning.Application.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanguageLearning.API.Controllers;

[ApiController]
[Route("api/sentences")]
public class SentencesController : ControllerBase
{
    private readonly ISentenceService _sentenceService;

    public SentencesController(ISentenceService sentenceService)
    {
        _sentenceService = sentenceService;
    }

    [HttpGet]
    public async Task<ActionResult<List<SentenceDto>>> GetAll()
    {
        return Ok(await _sentenceService.GetAllAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SentenceDto>> GetById(Guid id)
    {
        return Ok(await _sentenceService.GetByIdAsync(id));
    }

    // Send a sentence in one language. Gemini fills in the other two.
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<SentenceDto>> Create(CreateSentenceRequest request)
    {
        var sentence = await _sentenceService.CreateAsync(request);

        return CreatedAtAction(nameof(GetById), new { id = sentence.Id }, sentence);
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<SentenceDto>> Update(Guid id, UpdateSentenceRequest request)
    {
        return Ok(await _sentenceService.UpdateAsync(id, request));
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _sentenceService.DeleteAsync(id);

        return NoContent();
    }
}
