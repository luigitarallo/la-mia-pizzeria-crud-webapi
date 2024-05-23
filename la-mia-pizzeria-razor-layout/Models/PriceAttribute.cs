using System.ComponentModel.DataAnnotations;

public class PriceAttribute : ValidationAttribute
{
    public decimal Price { get; set; }
    public PriceAttribute() { }
    public PriceAttribute(int price)
    {
        this.Price = price;
    }
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("Insert a valid price");
        }
        decimal field = (decimal)value;
        if (field <0 )
        {
            return new ValidationResult("Insert a price > 0");
        }
        return ValidationResult.Success;
    }
}


