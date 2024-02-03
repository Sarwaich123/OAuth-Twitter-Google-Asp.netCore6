using Tweetinvi.Models;

namespace GoogleTwitterOauth.Services
{

		public class Serialize
		{
			public string AuthorizationKey { get; set; }
			public string AuthorizationSecret { get; set; }

			public string AuthorizationURL { get; set; }
			public string ConsumerKey { get; set; }
			public string ConsumerSecret { get; set; }
			//public string Id { get; set; }
			public string VerifierCode { get; set; }
			// Include other fields as needed

			public Serialize(IAuthenticationRequest authRequest)
			{
				AuthorizationKey = authRequest.AuthorizationKey;
				AuthorizationSecret = authRequest.AuthorizationSecret;
				AuthorizationURL = authRequest.AuthorizationURL;
				ConsumerKey = authRequest.ConsumerKey;
				ConsumerSecret = authRequest.ConsumerSecret;
				VerifierCode = authRequest.VerifierCode;

			}

		}
	}

