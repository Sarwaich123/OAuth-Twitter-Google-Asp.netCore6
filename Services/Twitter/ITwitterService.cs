using Tweetinvi.Models;

namespace GoogleTwitterOauth.Services.Twitter
{
	public interface ITwitterService
	{
		Task<IAuthenticationRequest> GetRequestTokenAsync(string callbackUrl);
		Task<TwitterUser> GetAccessTokenAsync(IAuthenticationRequest authRequest, string oauthVerifier);

		string GetAuthorizationUrl(IAuthenticationRequest requestToken);

		string ExtractOAuthToken(string authUrl);

		IAuthenticationRequest DeserializeAuthRequest(string jsonString);

		string SerializeAuthRequest(Serialize authRequest);

	}
}
