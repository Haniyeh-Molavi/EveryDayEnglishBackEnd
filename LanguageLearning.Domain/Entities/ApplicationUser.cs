using Microsoft.AspNetCore.Identity;

namespace LanguageLearning.Domain.Entities;

// The logged-in user. Email, password hash etc. come from IdentityUser.
public class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    // Words this user saved to their personal list
    public ICollection<UserWord> UserWords { get; set; } = new List<UserWord>();
}
