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
	public class VotesController : ControllerBase
	{
		private readonly IVote _vote;

		public VotesController(IVote vote)
		{
			_vote = vote;
		}

		/// <summary>
		/// Obtener todas los VOTOS
		/// </summary>
		/// <returns>Lista de VOTOS</returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la verificación.</response>
		[HttpGet]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> GetAllVotes()
		{
			ResponseBase response = new ResponseBase();
			try
			{
				List<Vote> votes = await _vote.GetVotes();
				response.Success = true;
				response.Message = "Search succesffully";
				response.Data = votes;

			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					response.Message = ex.Message); ;
			}
			return Ok(response);
		}


		/// <summary>
		/// Registrar un voto
		/// </summary>
		/// <returns>Id de la persona nueva</returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la verificación.</response>
		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> CreateVote([FromBody] RegisterVote model)
		{
			ResponseBase response = new ResponseBase();
			try
			{
				int idVote = await _vote.AddVote(model);

				if (idVote > 0)
				{
					response.Success = true;
					response.Message = "Vote register";
					response.Data = new { IdVote = idVote };
				}
				else
				{
					return BadRequest(response.Message = "Error creating vote");
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
		/// Obtener el total de votos de cada candidato por sección
		/// </summary>
		/// <returns>Lista candidato y sus votos</returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la verificación.</response>
		[HttpGet("graphic")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> GetVotesOfApplicantBySection()
		{
			ResponseBase response = new ResponseBase();
			try
			{
				List<GraphicTotalVotes> votes = await _vote.GetTotalVotesBySection();
				response.Success = true;
				response.Message = "Search succesffully";
				response.Data = votes;

			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					response.Message = ex.Message); ;
			}
			return Ok(response);
		}

		/// <summary>
		/// Obtener el total de votos de cada candidato
		/// </summary>
		/// <returns>Lista candidato y sus votos</returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la verificación.</response>
		[HttpGet("admin/graphic")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> GetVotesOfApplicant()
		{
			ResponseBase response = new ResponseBase();
			try
			{
				List<GraphicTotalVotes> votes = await _vote.GetTotalVotes();
				response.Success = true;
				response.Message = "Search succesffully";
				response.Data = votes;

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
