namespace FlagExplorer.Domain.Configuration
{
    public class SystemErrorResponse : ErrorResponse
    {
        public SystemErrorResponse() { }

        public SystemErrorResponse(Exception exception)
        {
            ErrorCode = exception.Message;
            ErrorReason = exception.InnerException?.Message;
        }
    }
}
