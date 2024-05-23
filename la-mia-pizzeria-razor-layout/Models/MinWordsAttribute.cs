using System.ComponentModel.DataAnnotations;
public class MinWordsAttribute : ValidationAttribute
{
    public int MinWords;

    public MinWordsAttribute(int minWords)
    {
        MinWords = minWords;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || !(value is string))
        {
            return new ValidationResult("Description must be a valid string.");
        }

        string description = (string)value;

        string[] words = description.Split(new[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

        if (words.Length < MinWords)
        {
            return new ValidationResult($"Description must contain at least {MinWords} words.");
        }

        return ValidationResult.Success;
    }
}

