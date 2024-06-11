using API.UsersVote.Context;
using API.UsersVote.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.UsersVote.Repository.Imp
{
	public class ImpPoliticPartyRepository : IPartyPoliticRepository
	{
		private readonly VotedbContext _context;

		public ImpPoliticPartyRepository(VotedbContext context)
		{
			_context = context;
		}

		public async Task<List<PoliticParty>> GetPoliticParties()
		{
			return await _context.PoliticParties.AsNoTracking().ToListAsync();
		}

		public async Task<int> Register(PoliticParty politicParty)
		{
			EntityEntry<PoliticParty> registerPolitic = await _context.PoliticParties.AddAsync(politicParty);
			await _context.SaveChangesAsync();

			return registerPolitic.Entity.Id;
		}
	}
}
