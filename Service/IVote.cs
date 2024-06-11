using API.UsersVote.DTO;
using API.UsersVote.Models;

namespace API.UsersVote.Service
{
	public interface IVote
	{
		Task<int> AddVote(RegisterVote vote);
		Task<List<Vote>> GetVotes();
		Task<List<GraphicTotalVotes>> GetTotalVotes();
		Task<List<GraphicTotalVotes>> GetTotalVotesBySection();
	}
}
