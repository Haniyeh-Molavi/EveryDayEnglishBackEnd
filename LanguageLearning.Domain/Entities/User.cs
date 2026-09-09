using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLearning.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public ICollection<UserWord> Words { get; set; }
    }
}
