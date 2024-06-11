using Api.Client.MarquetStore.Security;
using API.UsersVote.Commons.Enums;
using API.UsersVote.DTO;
using API.UsersVote.Models;
using API.UsersVote.Repository;
using API.UsersVote.Tools;

namespace API.UsersVote.Service.Imp
{
	public class ImpUser : IUser
	{
		private readonly IUserRepository _userRepository;
		private readonly JwtSettings _jwtSettings;

		public ImpUser(IUserRepository userRepository, JwtSettings jwtSettings)
		{
			_userRepository = userRepository;
			_jwtSettings = jwtSettings;
		}

		public async Task<ResponseLogin> Auth(Login login)
		{
			bool exist = await _userRepository.ExistUserByCurpOrPassword(login.EmailOrCurp);
			UserTokens token = new UserTokens();
			ResponseLogin responseLogin = new ResponseLogin();

			if (!exist)
			{
				responseLogin.UserTokens = null;
				responseLogin.Message = "Usuario no registrado";

				return responseLogin;
			}

			User userAuth = await _userRepository.ValidateCredentials(login.EmailOrCurp, Encrypt.GetSHA256(login.password));

			if (userAuth != null)
			{
				if (userAuth.AlreadyVoted)
				{
					responseLogin.Message = "Ya haz emitido tu voto";
					responseLogin.UserTokens = null;

					return responseLogin;
				}
				UserTokens informationUser = new UserTokens
				{
					Id = userAuth.Id,
					Email = userAuth.Email,
					Rol = userAuth.Rol,
					State = userAuth.State,
					Section = userAuth.Section,
					AlreadyVoted = userAuth.AlreadyVoted,
				};

				token = JwtHelpers.GenerateToken(informationUser, _jwtSettings);

				responseLogin.UserTokens = token;
				responseLogin.Message = "Usuario autenticado";

				return responseLogin;
			}

			responseLogin.UserTokens = null;
			responseLogin.Message = "Contraseña incorrecto";

			return responseLogin;

		}

		public async Task<List<User>> GetAllUsers()
		{
			return await _userRepository.GetAllUsers();
		}

		public async Task<User> GetUserById(int idUser)
		{
			return await _userRepository.GetUserById(idUser);
		}

		public async Task<int> RegisterAdmin(RegisterUser model)
		{
			User people = new User
			{
				Curp = model.Curp,
				Location = model.Location,
				Rol = nameof(Role.Administrador),
				Password = Encrypt.GetSHA256(model.Password),
				Section = model.Section,
				Email = model.Email,
				State = model.State
			};

			int idPeople = await _userRepository.Register(people);

			return idPeople;
		}

		public async Task<int> RegisterFuncionario(RegisterUser model)
		{
			User people = new User
			{
				Curp = model.Curp,
				Location = model.Location,
				Rol = nameof(Role.Funcionario),
				Password = Encrypt.GetSHA256(model.Password),
				Section = model.Section,
				Email = model.Email,
				State = model.State
			};

			int idPeople = await _userRepository.Register(people);

			return idPeople;
		}

		public async Task<int> RegisterPeople(RegisterUser model)
		{
			User people = new User
			{
				Curp = model.Curp,
				Location = model.Location,
				Rol = nameof(Role.Persona),
				Password = Encrypt.GetSHA256(model.Password),
				Section = model.Section,
				Email = model.Email,
				State = model.State
			};

			int idPeople = await _userRepository.Register(people);

			return idPeople;
		}
	}
}
