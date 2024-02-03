using GoogleTwitterOauth.Models;
using Tweetinvi.Core.Models;

namespace GoogleTwitterOauth.Repository.Authentication
{
	public interface IAuthenticationRepository
	{

		Task<Boolean> AuthToken(TwitterAuthtoken input);

		TwitterAuthtoken GetAuthToken(string oauthtoken1);

	}
}
