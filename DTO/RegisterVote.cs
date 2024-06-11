namespace API.UsersVote.DTO
{
	public record RegisterVote(

	 string? Applicant,
	 DateTime? CreatedDate,

	 string? Section,
	 string? VoteLocation
		);
}
