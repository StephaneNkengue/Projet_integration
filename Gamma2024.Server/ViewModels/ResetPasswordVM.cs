namespace Gamma2024.Server.ViewModels
{
    public class ResetPasswordVM
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;
    }
}
