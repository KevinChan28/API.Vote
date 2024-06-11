using System.ComponentModel.DataAnnotations;

namespace API.UsersVote.DTO
{
	public class RegisterParty
	{
		public string? Applicant { get; set; }

		public string? Name { get; set; }
		[Base64String]
		public string? Image { get; set; }
	}
}
