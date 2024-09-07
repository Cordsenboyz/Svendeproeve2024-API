namespace BooksmartAPI.DTOs.Auth
{
    public class LoginResponseDTO
    {
        public bool Authenticated { get; set; }
        public TokenDTO Token { get; set; }
    }
}
