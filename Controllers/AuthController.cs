using Microsoft.AspNetCore.Mvc;
using GoogleTwitterOauth.Models;
using GoogleTwitterOauth.Services;
using GoogleTwitterOauth.Services.Twitter;
using GoogleTwitterOauth.Services.AuthenticationService;

namespace GoogleTwitterOauth.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : Controller
	{
		private readonly IAuthenticateService _auth;
		private readonly ITwitterService _twitterService;

		public AuthController(IAuthenticateService auth, ITwitterService twitterService1)
		{
			_auth = auth;
			_twitterService = twitterService1;
		}
		[HttpGet("auth/twitter")]
		public async Task<IActionResult> AuthenticateWithTwitter()
		{
			var _authRequest = await _twitterService.GetRequestTokenAsync("http://www.localhost.com:3000/login");
			var authUrl = _twitterService.GetAuthorizationUrl(_authRequest);
			var serializableAuthRequest = new Serialize(_authRequest);
			string _authreq = _twitterService.SerializeAuthRequest(serializableAuthRequest);
			string oauthToken = _twitterService.ExtractOAuthToken(authUrl);

			TwitterAuthtoken user = new TwitterAuthtoken();
			user.oauthtoken = oauthToken;
			user.authrequest = _authreq;
			bool ans = await _auth.AuthToken(user);// This part Add Auth token into Database.which can be used in Callback Function
			return Redirect(authUrl);
		}

		[HttpPost("twitter/callback")]
		public async Task<ActionResult<string>> TwitterCallback([FromBody] TwitterRequest oauth_verifier1)
		{
			var _authreq = _auth.GetAuthToken(oauth_verifier1.oauth_token);
			var authii = _twitterService.DeserializeAuthRequest(_authreq.authrequest);
			var accessToken = await _twitterService.GetAccessTokenAsync(authii, oauth_verifier1.oauth_verifier);
			//accessToken.id is twitter id response from Server.

			if (accessToken.id != null)
			{
				return Ok("User authenticated with Twitter.");
			}
			else
			{
				return NotFound("No user data found");
			}
			

		}


		[HttpGet("auth/google")]
		public async Task<IActionResult> AuthenticateWithGoogle()
		{
			var googleOAuthUrl = $"https://accounts.google.com/o/oauth2/v2/auth?client_id={"693631694815-1t7n5ugt9pmisrlmtlhcn1sfdud0unqe.apps.googleusercontent.com"}&response_type=code&redirect_uri=http://www.localhost.com:3000/login&scope=openid%20email%20profile";
			return Redirect(googleOAuthUrl);
		}

		[HttpPost("callback")]
		public async Task<ActionResult<string>> RegisterWithGoogle([FromBody] CodeRequest request)
		{
			var tokenResponse = await _auth.ExchangeCodeForTokens(request.Code);

			if (tokenResponse == null)
			{
				return BadRequest("Invalid Google token");
			}
			else
			{

				return Ok("User data found with Google");
			}
		}
	}
}
