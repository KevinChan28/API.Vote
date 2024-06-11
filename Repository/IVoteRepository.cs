using API.UsersVote.Models;

namespace API.UsersVote.Repository
{
	public interface IVoteRepository
	{
		Task<int> AddVote(Vote vote);
		Task<List<Vote>> GetVotes();
	}
}
