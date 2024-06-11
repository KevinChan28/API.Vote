using System.ComponentModel.DataAnnotations;

namespace API.UsersVote.DTO
{
	public class RegisterDocument
	{
		[Base64String]
		public string FileBase64 { get; set; }
	}
}
