namespace ExamifyApis.Response
{
    public class ResponseClass<T>
    {
        public string Message { get; set; } = string.Empty;
        public bool Status { get; set; } = false;
        public T? Data { get; set; }
    }
}
