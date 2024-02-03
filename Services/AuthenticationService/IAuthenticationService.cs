using GoogleTwitterOauth.Models;

namespace GoogleTwitterOauth.Services.AuthenticationService
{
	public interface IAuthenticateService
	{
		Task<GoogleTokenResponse> ExchangeCodeForTokens(string authorizationCode);

		Task<Boolean> AuthToken(TwitterAuthtoken input);

		TwitterAuthtoken GetAuthToken(string oauthtoken);
	}
	}
