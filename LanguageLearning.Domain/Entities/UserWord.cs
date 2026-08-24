using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLearning.Domain.Entities
{
    public class UserWord
    {
        public Guid UserId { get; set; }

        public User User { get; set; }

        public Guid WordId { get; set; }

        public Word Word { get; set; }

        public DateTime SavedAt { get; set; }
    }
}
