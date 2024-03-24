using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Order.Api.Models.Requests;

public class OrderRequest : IValidatableObject
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (To <= From)
        {
            yield return new ValidationResult("To parameter cannot be equal or less than From parameter.", [nameof(To)]);
        }
    }
}
