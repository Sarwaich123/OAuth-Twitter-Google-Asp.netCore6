using GoogleTwitterOauth.Models;
using GoogleTwitterOauth.Repository.Authentication;
using Newtonsoft.Json;
using Tweetinvi.Core.Models;

namespace GoogleTwitterOauth.Services.AuthenticationService
{

		public class AuthenticateService : IAuthenticateService
		{
			private readonly IAuthenticationRepository _authenticationDb;

			public AuthenticateService(IAuthenticationRepository authenticationDb)
			{
				_authenticationDb = authenticationDb;
			}

			public async Task<GoogleTokenResponse> ExchangeCodeForTokens(string authorizationCode)
			{
				using (var client = new HttpClient())
				{
					var tokenRequest = new Dictionary<string, string>
					{
						["code"] = authorizationCode,
						["client_id"] = "client-id(google)",
						["client_secret"] = "client-secret(google)",
						["redirect_uri"] = "http://www.localhost.com:3000/login", // same as in your Google OAuth setup
						["grant_type"] = "authorization_code"
					};

					var requestContent = new FormUrlEncodedContent(tokenRequest);

					var response = await client.PostAsync("https://oauth2.googleapis.com/token", requestContent);


					if (response.IsSuccessStatusCode)
					{
						var responseString = await response.Content.ReadAsStringAsync();
						var googleResponse = JsonConvert.DeserializeObject<GoogleTokenResponse>(responseString);
						return googleResponse!;
					}
					else
					{
						// Handle error response
						return null;
					}
				}
			}


			public async Task<Boolean> AuthToken(TwitterAuthtoken input)
			{
				return await _authenticationDb.AuthToken(input);
			}
			public TwitterAuthtoken GetAuthToken(string oauthtoken)
			{
				var authtoken = _authenticationDb.GetAuthToken(oauthtoken);
				return authtoken;
			}


		}

		public class UserInfo
		{
			public string Email { get; set; }
			public string Name { get; set; }
			public string ProfilePicture { get; set; }
		}
	}

