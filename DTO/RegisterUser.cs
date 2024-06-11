using System.ComponentModel.DataAnnotations;

namespace API.UsersVote.DTO
{
	public class RegisterUser
	{
		[Required]
		public string Curp { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		public string Section { get; set; }
		[Required]
		public string Location { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string State { get; set; }
	}
}
