namespace BlogApi.Api.Services.Communication
{
    public class TokenResponse : BaseResponse
    {
        public string Token { get; set; }

        public TokenResponse(bool success, string message, string token) : base(success, message)
        {
            Token = token;
        }
    }
}
