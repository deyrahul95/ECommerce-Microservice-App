using System.ComponentModel.DataAnnotations;
using ProductApi.Domain.Constants;

namespace ProductApi.Application.Models;

public record CreateProductRequest(
    [Required][MaxLength(DBConstraint.PRODUCT_NAME_MAX_LENGTH)] string Name,
    [Required][Range(DBConstraint.PRODUCT_MIN_QUANTITY, DBConstraint.PRODUCT_MAX_QUANTITY)] int Quantity,
    [Required][Range((double)DBConstraint.PRODUCT_MIN_PRICE, (double)DBConstraint.PRODUCT_MAX_PRICE)] decimal Price
);
