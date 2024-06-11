namespace API.UsersVote.Models;

public partial class User
{
	public int Id { get; set; }

	public string Curp { get; set; }

	public string Password { get; set; }

	public string Section { get; set; }

	public string Rol { get; set; }

	public string Email { get; set; }

	public string Location { get; set; }
	public string State { get; set; }

	public bool AlreadyVoted { get; set; }
}
