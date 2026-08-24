using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLearning.Domain.Entities
{
    public class CommonSentence
    {
        public Guid Id { get; set; }

        public string EnglishText { get; set; }

        public string PersianText { get; set; }

        public string PortugueseText { get; set; }

        public bool IsActive { get; set; }
    }
}
