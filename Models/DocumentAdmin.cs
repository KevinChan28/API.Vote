namespace API.UsersVote.Models
{
	public partial class DocumentAdmin
	{
		public int Id { get; set; }
		public string FileBase64 { get; set; }
		public string Section { get; set; }
		public int UserId { get; set; }
	}
}
