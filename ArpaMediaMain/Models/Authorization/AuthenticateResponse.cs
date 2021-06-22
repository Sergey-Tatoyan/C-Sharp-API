namespace WebApi.Models
{
    public class AuthenticateResponse
    {
        public string Token { get; set; }
        public int Expiration { get; set; }

        public AuthenticateResponse(string token, int expiration)
        {
            Token = token;
            Expiration = expiration;
        }
    }
}