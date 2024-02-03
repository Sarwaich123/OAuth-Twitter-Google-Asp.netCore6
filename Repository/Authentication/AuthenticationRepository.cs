using GoogleTwitterOauth.Models;
using GoogleTwitterOauth.Models.DBcontext;

namespace GoogleTwitterOauth.Repository.Authentication
{
	public class AuthenticationRepository : IAuthenticationRepository
	{
		private readonly DBContext _dbContext;
		public AuthenticationRepository(DBContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<Boolean> AuthToken(TwitterAuthtoken input)
		{
			_dbContext.TwitterAuth.Add(input);
			await _dbContext.SaveChangesAsync();
			return true;
		}

		public TwitterAuthtoken GetAuthToken(string oauthtoken1)
		{
			return _dbContext.TwitterAuth.Where(c => c.oauthtoken == oauthtoken1).FirstOrDefault();
		}

	}
	}
