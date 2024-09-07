namespace BooksmartAPI.DTOs.Auth
{
    public class TokenDTO
    {
        public string Value { get; set; }
        public DateTime Expiration { get; set; }
    }
}
