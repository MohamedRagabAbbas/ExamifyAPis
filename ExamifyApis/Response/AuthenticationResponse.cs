namespace ExamifyApis.Response
{
    public class AuthenticationResponse
    {
        public string Token { get; set; } = "";
        public bool IsAuthenticated { get; set; } = false;
        public string Role { get; set; } = "";
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Message { get; set; } = "";
    }
}
