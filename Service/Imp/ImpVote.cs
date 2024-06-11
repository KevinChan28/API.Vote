using API.UsersVote.DTO;
using API.UsersVote.Models;
using API.UsersVote.Repository;

namespace API.UsersVote.Service.Imp
{
	public class ImpVote : IVote
	{
		private readonly IVoteRepository _voteRepository;
		private readonly IAuthorization _authorization;
		private readonly IUserRepository _userRepository;

		public ImpVote(IVoteRepository voteRepository, IAuthorization authorization, IUserRepository user)
		{
			_voteRepository = voteRepository;
			_authorization = authorization;
			_userRepository = user;
		}

		public async Task<int> AddVote(RegisterVote vote)
		{
			int idUser = _authorization.UserCurrentId();

			User user = await _userRepository.GetUserById(idUser);

			if (user == null | user.AlreadyVoted == true)
			{
				return 0;
			}

			Vote voteNew = new Vote
			{
				CreatedDate = DateTime.UtcNow,
				Applicant = vote.Applicant,
				Section = vote.Section,
				VoteLocation = vote.VoteLocation,
			};

			int idVote = await _voteRepository.AddVote(voteNew);

			user.AlreadyVoted = true;

			await _userRepository.Update(user);

			return idVote;
		}

		public async Task<List<GraphicTotalVotes>> GetTotalVotes()
		{
			List<Vote> votes = await _voteRepository.GetVotes();
			List<GraphicTotalVotes> graphicTotalVotes = votes.GroupBy(x => x.Applicant).Select(c => new GraphicTotalVotes
			{
				Applicant = c.Key,
				TotalVotes = c.Count()
			}).ToList();

			return graphicTotalVotes;
		}

		public async Task<List<GraphicTotalVotes>> GetTotalVotesBySection()
		{
			List<Vote> votes = await _voteRepository.GetVotes();
			int idUser = _authorization.UserCurrentId();
			User user = await _userRepository.GetUserById(idUser);

			if (user == null)
			{
				return null;
			}

			List<GraphicTotalVotes> graphicTotalVotes = votes.Where(c => c.Section == user.Section).GroupBy(x => x.Applicant).Select(c => new GraphicTotalVotes
			{
				Applicant = c.Key,
				TotalVotes = c.Count()
			}).ToList();

			return graphicTotalVotes;
		}

		public async Task<List<Vote>> GetVotes()
		{
			return await _voteRepository.GetVotes();
		}
	}
}
