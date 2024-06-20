namespace NumeroLetraAPI.Entities;

public class LoginResponse
{
    public required int IdUser { get; set; }

    public string? CompleteName { get; set; }

    public string? User { get; set; }

    public string? Token { get; set; }
}
