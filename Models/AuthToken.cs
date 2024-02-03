using System.ComponentModel.DataAnnotations;

namespace GoogleTwitterOauth.Models
{
	public class TwitterAuthtoken
	{
		[Key]
		public string oauthtoken { get; set; }
		public string authrequest { get; set; }
	}
}
