using LanguageLearning.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLearning.Application.DTOs
{
    public class WordMetadataDto
    {
        public WordType WordType { get; set; }

        public Gender Gender { get; set; }
    }
}
