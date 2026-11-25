using LanguageLearning.Application.DTOs.MyWords;
using LanguageLearning.Application.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanguageLearning.API.Controllers;

// The logged-in user's personal word list. Every endpoint here needs a token.
[ApiController]
[Authorize]
[Route("api/my-words")]
public class MyWordsController : ControllerBase
{
    private readonly IMyWordService _myWordService;

    public MyWordsController(IMyWordService myWordService)
    {
        _myWordService = myWordService;
    }

    // GET api/my-words              -> all my words
    // GET api/my-words?isLearned=false -> only words I still need to learn
    [HttpGet]
    public async Task<ActionResult<List<MyWordDto>>> GetMyWords([FromQuery] bool? isLearned)
    {
        return Ok(await _myWordService.GetMyWordsAsync(GetUserId(), isLearned));
    }

    [HttpGet("progress")]
    public async Task<ActionResult<ProgressDto>> GetProgress()
    {
        return Ok(await _myWordService.GetProgressAsync(GetUserId()));
    }

    [HttpPost("{wordId:guid}")]
    public async Task<ActionResult<MyWordDto>> Add(Guid wordId)
    {
        return Ok(await _myWordService.AddAsync(GetUserId(), wordId));
    }

    // Mark a word as learned or not learned
    [HttpPut("{wordId:guid}")]
    public async Task<ActionResult<MyWordDto>> UpdateProgress(Guid wordId, UpdateProgressRequest request)
    {
        return Ok(await _myWordService.UpdateProgressAsync(GetUserId(), wordId, request.IsLearned));
    }

    [HttpDelete("{wordId:guid}")]
    public async Task<IActionResult> Remove(Guid wordId)
    {
        await _myWordService.RemoveAsync(GetUserId(), wordId);

        return NoContent();
    }

    // The user id is stored in the "sub" claim of the JWT token
    private Guid GetUserId()
    {
        var userId = User.FindFirst("sub")?.Value;

        if (userId == null)
            throw new UnauthorizedAccessException("Please log in.");

        return Guid.Parse(userId);
    }
}
