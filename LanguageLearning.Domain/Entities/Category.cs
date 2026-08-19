using LanguageLearning.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<Word> Words { get; set; } = new List<Word>();
}