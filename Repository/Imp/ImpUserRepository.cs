using API.UsersVote.Context;
using API.UsersVote.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.UsersVote.Repository.Imp
{
	public class ImpUserRepository : IUserRepository
	{
		private readonly VotedbContext _context;

		public ImpUserRepository(VotedbContext context)
		{
			_context = context;
		}

		public async Task<bool> ExistUserByCurpOrPassword(string data)
		{
			return await _context.Users.AnyAsync(c => c.Email == data | c.Curp == data);
		}

		public async Task<List<User>> GetAllUsers()
		{
			return await _context.Users.AsNoTracking().ToListAsync();
		}

		public async Task<User> GetUserById(int idUser)
		{
			return _context.Users.Where(x => x.Id == idUser).FirstOrDefault();
		}

		public async Task<int> Register(User user)
		{
			EntityEntry<User> registerUser = await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();

			return registerUser.Entity.Id;
		}

		public async Task Update(User user)
		{
			EntityEntry<User> registerUser = _context.Users.Update(user);
			await _context.SaveChangesAsync();

		}

		public async Task<User> ValidateCredentials(string data, string password)
		{
			return _context.Users.Where(x => x.Curp == data | x.Email == data && x.Password == password).FirstOrDefault();
		}
	}
}
