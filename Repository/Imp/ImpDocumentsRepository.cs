using API.UsersVote.Context;
using API.UsersVote.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.UsersVote.Repository.Imp
{
	public class ImpDocumentsRepository : IDocumentRepository
	{
		private readonly VotedbContext _context;

		public ImpDocumentsRepository(VotedbContext context)
		{
			_context = context;
		}
		public async Task<List<DocumentAdmin>> GetDocumentAdmins()
		{
			return await _context.DocumentAdmins.AsNoTracking().ToListAsync();
		}

		public async Task<int> Register(DocumentAdmin documentAdmin)
		{
			EntityEntry<DocumentAdmin> entityEntry = await _context.DocumentAdmins.AddAsync(documentAdmin);
			await _context.SaveChangesAsync();

			return entityEntry.Entity.Id;
		}
	}
}
