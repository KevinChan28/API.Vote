using API.UsersVote.Models;

namespace API.UsersVote.Repository
{
	public interface IPartyPoliticRepository
	{
		Task<int> Register(PoliticParty politicParty);
		Task<List<PoliticParty>> GetPoliticParties();
	}
}
