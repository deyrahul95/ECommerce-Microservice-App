namespace ProductApi.Domain.Constants;

public static class DBConstraint
{
    public const int PRODUCT_NAME_MAX_LENGTH = 250;
    public const int PRODUCT_MIN_QUANTITY = 1;
    public const int PRODUCT_MAX_QUANTITY = 10000;
    public const int PRODUCT_MIN_PRICE = 1;
    public const int PRODUCT_MAX_PRICE = 1000000;
}
