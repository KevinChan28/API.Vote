using API.UsersVote.Commons;
using API.UsersVote.DTO;
using API.UsersVote.Models;
using API.UsersVote.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.UsersVote.Controllers
{
	[EnableCors("Cors")]
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUser _user;
		public UsersController(IUser user)
		{
			_user = user;
		}

		/// <summary>
		/// Crear una persona
		/// </summary>
		/// <returns>Id de la persona nueva</returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la verificación.</response>
		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> CreatePeople([FromBody] RegisterUser model)
		{
			ResponseBase response = new ResponseBase();
			try
			{
				int idAccount = await _user.RegisterPeople(model);

				if (idAccount > 0)
				{
					response.Success = true;
					response.Message = "People register";
					response.Data = new { IdAccount = idAccount };
				}
				else
				{
					return BadRequest(response.Message = "Error creating account");
				}
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					response.Message = ex.Message); ;
			}
			return Ok(response);
		}

		/// <summary>
		/// Crear un funcionario
		/// </summary>
		/// <returns>Id de la persona nueva</returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la verificación.</response>
		[HttpPost("officer")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> CreateFuncionario([FromBody] RegisterUser model)
		{
			ResponseBase response = new ResponseBase();
			try
			{
				int idAccount = await _user.RegisterFuncionario(model);

				if (idAccount > 0)
				{
					response.Success = true;
					response.Message = "Funcionario register";
					response.Data = new { IdAccount = idAccount };
				}
				else
				{
					return BadRequest(response.Message = "Error creating account");
				}
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					response.Message = ex.Message); ;
			}
			return Ok(response);
		}

		/// <summary>
		/// Crear un administrador
		/// </summary>
		/// <returns>Id de la persona nueva</returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la verificación.</response>
		[HttpPost("admin")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> CreateAdmin([FromBody] RegisterUser model)
		{
			ResponseBase response = new ResponseBase();
			try
			{
				int idAccount = await _user.RegisterAdmin(model);

				if (idAccount > 0)
				{
					response.Success = true;
					response.Message = "Admin register";
					response.Data = new { IdAccount = idAccount };
				}
				else
				{
					return BadRequest(response.Message = "Error creating account");
				}
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					response.Message = ex.Message); ;
			}
			return Ok(response);
		}

		/// <summary>
		/// Obtener todas los usuarios
		/// </summary>
		/// <returns>Lista de personas</returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la verificación.</response>
		[HttpGet]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> GetAllUsers()
		{
			ResponseBase response = new ResponseBase();
			try
			{
				List<User> users = await _user.GetAllUsers();
				response.Success = true;
				response.Message = "Search succesffully";
				response.Data = users;

			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					response.Message = ex.Message); ;
			}
			return Ok(response);
		}

		/// <summary>
		/// Autenticar usuario
		/// </summary>
		/// <returns>Lista de personas</returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la verificación.</response>
		[HttpPost("auth")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> Authenticate([FromBody] Login login)
		{
			ResponseBase response = new ResponseBase();
			try
			{
				ResponseLogin responseLogin = await _user.Auth(login);

				if (responseLogin.UserTokens == null)
				{
					response.Success = false;
					response.Message = responseLogin.Message;

					return Ok(response);
				}

				response.Success = true;
				response.Message = responseLogin.Message;
				response.Data = responseLogin.UserTokens;

			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					response.Message = ex.Message); ;
			}
			return Ok(response);
		}
	}
}
