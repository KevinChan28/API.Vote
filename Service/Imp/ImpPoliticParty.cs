using API.UsersVote.DTO;
using API.UsersVote.Models;
using API.UsersVote.Repository;

namespace API.UsersVote.Service.Imp
{
	public class ImpPoliticParty : IPoliticParty
	{
		private readonly IPartyPoliticRepository _partyPoliticRepository;

		public ImpPoliticParty(IPartyPoliticRepository partyPoliticRepository)
		{
			_partyPoliticRepository = partyPoliticRepository;
		}

		public async Task<List<PoliticParty>> GetPoliticParties()
		{
			return await _partyPoliticRepository.GetPoliticParties();
		}

		public async Task<int> Register(RegisterParty politicParty)
		{
			PoliticParty politicPartyNew = new PoliticParty
			{
				Name = politicParty.Name,
				Image = politicParty.Image,
				Applicant = politicParty.Applicant,
			};

			int idParty = await _partyPoliticRepository.Register(politicPartyNew);

			return idParty;
		}
	}
}
