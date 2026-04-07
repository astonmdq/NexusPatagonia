namespace NexusPatagonia.Domain.Exceptions
{
    public class BusinessException : Exception
    {
        public int? ErroCode { get; }
        public BusinessException(string message) : base(message)
        { 
        }
        public BusinessException(string message, int errorCode) : base(message)
        {
            ErroCode = errorCode;
        }   
    }
}
