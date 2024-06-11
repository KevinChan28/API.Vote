using API.UsersVote.Models;

namespace API.UsersVote.Repository
{
	public interface IUserRepository
	{
		Task<int> Register(User user);
		Task<User> GetUserById(int idUser);
		Task<List<User>> GetAllUsers();
		Task<bool> ExistUserByCurpOrPassword(string data);
		Task<User> ValidateCredentials(string data, string password);
		Task Update(User user);
	}
}
