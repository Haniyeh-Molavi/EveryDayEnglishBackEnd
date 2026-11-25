namespace LanguageLearning.Domain.Entities;

// A word that a user saved to their personal list
public class UserWord
{
    public Guid UserId { get; set; }

    public ApplicationUser User { get; set; } = null!;

    public Guid WordId { get; set; }

    public Word Word { get; set; } = null!;

    public DateTime SavedAt { get; set; }

    // Learning progress
    public bool IsLearned { get; set; }

    public DateTime? LearnedAt { get; set; }
}
