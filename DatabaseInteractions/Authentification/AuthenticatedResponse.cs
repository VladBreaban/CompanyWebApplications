namespace DatabaseInteractions.Authentification;

    public class AuthenticatedResponse
    {
        public string? Token { get; set; }
        public string? Email { get; set; }
        public Guid? userId { get; set; }
    }

