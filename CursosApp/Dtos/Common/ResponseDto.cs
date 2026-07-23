namespace CursosApp.Dtos.Common
{
    public class ResponseDto<T>
    {
        public int StatusCode { get; set; } 
        public string Message { get; set; } 
        public bool Status { get; set; }     
        public T Data { get; set; }  
    }
}