using API.UsersVote.DTO;
using API.UsersVote.Models;

namespace API.UsersVote.Service
{
	public interface IPoliticParty
	{
		Task<int> Register(RegisterParty politicParty);
		Task<List<PoliticParty>> GetPoliticParties();
	}
}
