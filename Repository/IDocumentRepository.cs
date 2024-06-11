using API.UsersVote.Models;

namespace API.UsersVote.Repository
{
	public interface IDocumentRepository
	{
		Task<int> Register(DocumentAdmin documentAdmin);
		Task<List<DocumentAdmin>> GetDocumentAdmins();
	}
}
