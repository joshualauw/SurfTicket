namespace SurfTicket.Application.Enums
{
    public enum SurfErrorCode
    {
        GENERIC_ERROR = 1000,
        USER_NOT_FOUND = 2000,
        USER_WRONG_PASSWORD = 2001,
        USER_EMAIL_ALREADY_USED = 2002,
        READ_FAILED = 3000,
        INSERT_FAILED = 3001,
        UPDATE_FAILED = 3002,
        DELETE_FAILED = 3003,
        UNAUTHORIZED = 4000
    }
}
