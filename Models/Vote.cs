namespace API.UsersVote.Models;

public partial class Vote
{
	public int Id { get; set; }

	public string? Applicant { get; set; }

	public DateTime? CreatedDate { get; set; }

	public string? Section { get; set; }

	public string? VoteLocation { get; set; }

}
