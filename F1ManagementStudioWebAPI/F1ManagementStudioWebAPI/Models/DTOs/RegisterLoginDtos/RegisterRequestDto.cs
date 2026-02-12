namespace F1ManagementStudioWebAPI.Models.DTOs.RegisterLoginDtos
{
    public class RegisterRequestDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string[]? Roles { get; set; }

    }
    public class RegisterResponse
    {
        public string? Email { get; set; }
        public string[]? Roles { get; set; }
    }
    public class LoginRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
    public class LoginResponse
    {
        public string? Email { get; set; }
        public string Token { get; set; }
    }
}

