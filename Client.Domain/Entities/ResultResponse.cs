using Client.Domain.Enums;

namespace Client.Domain.Entities
{
    public class ResultResponse<T>
    {
        public ResultResponse()
        {
            ErrorMessage = string.Empty;
            IsValid = true;
            StatusCode = EStatusCode.Ok;
        }

        public void SetStatus(EStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public void SetData(T? data)
        {
            Data = data;
        }

        public void SetErrorMessage(EStatusCode statusCode, string? message)
        {
            StatusCode = statusCode;
            ErrorMessage = message;
            IsValid = false;
        }

        public EStatusCode StatusCode { get; set; }
        public bool IsValid { get; set; }
        public string? ErrorMessage { get; private set; }
        public T? Data { get; set; }
    }

}
