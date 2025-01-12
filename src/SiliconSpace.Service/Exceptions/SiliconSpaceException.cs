namespace SiliconSpace.Service.Exceptions
{
    public class SiliconSpaceException : Exception
    {
        public int StatusCode { get; set; }
        public SiliconSpaceException(int statusCode, string Message) : base(Message) 
        {

            StatusCode = statusCode;
        }
    }
}
