namespace AuthApi.Users.Configs;

public static class DBConstraints
{
    public const int PASSWORD_MIN_LENGTH = 6;
    public const int PASSWORD_MAX_LENGTH = 15;
    public const int PHONE_NUMBER_LENGTH = 10;
    public const int EMAIL_MAX_LENGTH = 100;
    public const int NAME_MIN_LENGTH = 3;
    public const int NAME_MAX_LENGTH = 50;
    public const int ADDRESS_MAX_LENGTH = 256;
}
