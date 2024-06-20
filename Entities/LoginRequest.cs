namespace NumeroLetraAPI.Entities;

public class LoginRequest
{
    public required string User { get; set; }

    public required string Password { get; set; }
}
