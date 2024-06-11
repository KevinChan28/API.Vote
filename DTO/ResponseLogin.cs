using Api.Client.MarquetStore.Security;

namespace API.UsersVote.DTO
{
	public class ResponseLogin
	{
		public string Message { get; set; }
		public UserTokens UserTokens { get; set; }
	}
}
