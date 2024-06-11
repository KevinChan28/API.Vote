using API.UsersVote.Service;
using Microsoft.AspNetCore.SignalR;

namespace API.UsersVote.Hubs
{
    public class VoteHub : Hub
	{
		private readonly IVote _vote;

		public VoteHub(IVote vote)
		{
			_vote = vote;
		}

		public async Task SendVote(string applicant)
		{
			try
			{
				await Clients.All.SendAsync("ReceiveVote", applicant);
			}
			catch (Exception ex)
			{

				Console.WriteLine("Error al enviar notificacion " + ex);
			}

		}
	}
}
