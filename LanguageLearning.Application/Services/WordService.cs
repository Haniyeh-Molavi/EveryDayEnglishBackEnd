public class WordService : IWordService
{
    private readonly IWordService _services;

    public WordService(IWordService services)
    {
        _services = services;
    }

    public async Task CreateAsync(WordDetailsDto request)
    {
        var word = new WordDetailsDto
        {
            Word = request.Word,
            Translation = request.Translation,
            Definition = request.Definition,
            ExampleSentence = request.ExampleSentence,
            Pronunciation = request.Pronunciation
        };

        await _services.CreateAsync(word);
    }
}