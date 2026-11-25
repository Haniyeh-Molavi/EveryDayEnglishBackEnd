using LanguageLearning.Application.DTOs.Words;
using LanguageLearning.Domain.Entities;

namespace LanguageLearning.Application.DTOs.MyWords;

// A word in the user's personal list, with its learning progress
public class MyWordDto
{
    public WordDto Word { get; set; } = new WordDto();
    public bool IsLearned { get; set; }
    public DateTime SavedAt { get; set; }
    public DateTime? LearnedAt { get; set; }

    // The UserWord must be loaded together with its Word and the Word's Category
    public static MyWordDto FromEntity(UserWord userWord)
    {
        return new MyWordDto
        {
            Word = WordDto.FromEntity(userWord.Word),
            IsLearned = userWord.IsLearned,
            SavedAt = userWord.SavedAt,
            LearnedAt = userWord.LearnedAt
        };
    }
}
