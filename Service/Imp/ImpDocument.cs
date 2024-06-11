using API.UsersVote.DTO;
using API.UsersVote.Models;
using API.UsersVote.Repository;

namespace API.UsersVote.Service.Imp
{
	public class ImpDocument : IDocuments
	{
		private readonly IDocumentRepository _documentRepository;
		private readonly IAuthorization _authorization;
		private readonly IUserRepository _userRepository;

		public ImpDocument(IDocumentRepository document, IAuthorization authorization, IUserRepository userRepository)
		{
			_documentRepository = document;
			_authorization = authorization;
			_userRepository = userRepository;
		}

		public async Task<List<DocumentAdmin>> GetDocumentAdmins()
		{
			return await _documentRepository.GetDocumentAdmins();
		}

		public async Task<int> Register(RegisterDocument documentAdmin)
		{
			int idUser = _authorization.UserCurrentId();
			User user = await _userRepository.GetUserById(idUser);
			DocumentAdmin document = new DocumentAdmin
			{
				FileBase64 = documentAdmin.FileBase64,
				Section = user.Section,
				UserId = idUser,
			};

			int idDocument = await _documentRepository.Register(document);

			return idDocument;
		}
	}
}
