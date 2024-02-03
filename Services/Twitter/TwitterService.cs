using static GoogleTwitterOauth.Services.Twitter.TwitterService;
using Tweetinvi.Credentials.Models;
using Tweetinvi.Models;
using Tweetinvi;
using System.Text.Json;
using System.Web;

namespace GoogleTwitterOauth.Services.Twitter
{


		public class TwitterService : ITwitterService
		{

			private readonly TwitterClient _twitterClient;

			public TwitterService(TwitterClient twitterClient)
			{
				_twitterClient = twitterClient;
			}
			public async Task<IAuthenticationRequest> GetRequestTokenAsync(string callbackUrl)
			{
				var authenticationRequest = await _twitterClient.Auth.RequestAuthenticationUrlAsync(callbackUrl);
				return authenticationRequest;
			}

			public async Task<TwitterUser> GetAccessTokenAsync(IAuthenticationRequest authRequest, string oauthVerifier)
			{

				try
				{
					var userCredentials = await _twitterClient.Auth.RequestCredentialsFromVerifierCodeAsync(oauthVerifier, authRequest);
					if (userCredentials != null)
					{
						var userClient = new TwitterClient(userCredentials);

						var user = await userClient.Users.GetAuthenticatedUserAsync();
						if (user != null)
						{
							return new TwitterUser
							{
								Username = user.ScreenName,
								ProfileImageUrl = user.ProfileImageUrl,
								Description = user.Description,
								id = user.Id,
							};
						}
						else
						{
							return null;
						}
					}
					else
					{

						return null;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"An error occurred: {ex.Message}");
					throw ex;
				}
			}
			public string GetAuthorizationUrl(IAuthenticationRequest requestToken)
			{
				return requestToken.AuthorizationURL;
			}
			public string ExtractOAuthToken(string authUrl)
			{
				var uri = new Uri(authUrl);
				var query = HttpUtility.ParseQueryString(uri.Query);
				return query["oauth_token"];
			}
			public string SerializeAuthRequest(Serialize authRequest)
			{
				return JsonSerializer.Serialize(authRequest);
			}

			public IAuthenticationRequest DeserializeAuthRequest(string jsonString)
			{
				return JsonSerializer.Deserialize<AuthenticationRequest>(jsonString);
			}
		}

		public class TwitterUser
		{
			public string Username { get; set; }
			public string Email { get; set; }
			public string ProfileImageUrl { get; set; }

			public string Description { get; set; }

			public Int64 id { get; set; }
		}
	}

