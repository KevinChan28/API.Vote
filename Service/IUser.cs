using API.UsersVote.DTO;
using API.UsersVote.Models;

namespace API.UsersVote.Service
{
	public interface IUser
	{
		Task<ResponseLogin> Auth(Login login);
		Task<int> RegisterPeople(RegisterUser model);
		Task<User> GetUserById(int idUser);
		Task<List<User>> GetAllUsers();
		Task<int> RegisterFuncionario(RegisterUser model);
		Task<int> RegisterAdmin(RegisterUser model);
	}
}
