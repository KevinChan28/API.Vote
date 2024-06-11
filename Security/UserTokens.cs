namespace Api.Client.MarquetStore.Security
{
	public class UserTokens
	{
		public string Token { get; set; }
		public int? Id { get; set; }
		public string Email { get; set; }
		public string Rol { get; set; }
		public string Section { get; set; }
		public string State { get; set; }
		public bool AlreadyVoted { get; set; }
	}
}
