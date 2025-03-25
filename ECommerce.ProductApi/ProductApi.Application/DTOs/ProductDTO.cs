using System.ComponentModel.DataAnnotations;
using ProductApi.Domain.Constants;

namespace ProductApi.Application.DTOs;

public record ProductDTO(
    [Required] Guid Id,
    [Required][MaxLength(DBConstraint.PRODUCT_NAME_MAX_LENGTH)] string Name,
    [Required][Range(DBConstraint.PRODUCT_MIN_QUANTITY, DBConstraint.PRODUCT_MAX_QUANTITY)] int Quantity,
    [Required][Range(DBConstraint.PRODUCT_MIN_PRICE, DBConstraint.PRODUCT_MAX_PRICE)] decimal Price,
    DateTime? CreatedAt = null,
    DateTime? UpdatedAt = null
);