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
	public class PartysController : ControllerBase
	{
		private readonly IPoliticParty _politicParty;

		public PartysController(IPoliticParty politicParty)
		{
			_politicParty = politicParty;
		}

		/// <summary>
		/// Obtener todas los partidos politicos
		/// </summary>
		/// <returns>Lista de partidos</returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la verificación.</response>
		[HttpGet]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> GetPartys()
		{
			ResponseBase response = new ResponseBase();
			try
			{
				List<PoliticParty> partys = await _politicParty.GetPoliticParties();
				response.Success = true;
				response.Message = "Search succesffully";
				response.Data = partys;

			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					response.Message = ex.Message); ;
			}
			return Ok(response);
		}

		/// <summary>
		/// Registrar un partido politivo
		/// </summary>
		/// <returns>Id del partido nuevo</returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la verificación.</response>
		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> Register([FromBody] RegisterParty model)
		{
			ResponseBase response = new ResponseBase();
			try
			{
				int partys = await _politicParty.Register(model);

				if (partys < 1)
				{
					response.Success = false;
					response.Message = "Error al crear un Partido Politico";

					return Ok(response);
				}

				response.Success = true;
				response.Message = "Partido politico creado exitosament";
				response.Data = new { PartidoId = partys };

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
