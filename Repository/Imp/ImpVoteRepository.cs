using API.UsersVote.Context;
using API.UsersVote.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.UsersVote.Repository.Imp
{
	public class ImpVoteRepository : IVoteRepository
	{
		private readonly VotedbContext _context;

		public ImpVoteRepository(VotedbContext context)
		{
			_context = context;
		}

		public async Task<int> AddVote(Vote vote)
		{
			EntityEntry<Vote> registerVote = await _context.Votes.AddAsync(vote);
			await _context.SaveChangesAsync();

			return registerVote.Entity.Id;
		}

		public async Task<List<Vote>> GetVotes()
		{
			return await _context.Votes.AsNoTracking().ToListAsync();
		}
	}
}
