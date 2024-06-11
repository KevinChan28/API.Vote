using API.UsersVote.DTO;
using API.UsersVote.Models;

namespace API.UsersVote.Service
{
	public interface IDocuments
	{
		Task<int> Register(RegisterDocument documentAdmin);
		Task<List<DocumentAdmin>> GetDocumentAdmins();
	}
}
