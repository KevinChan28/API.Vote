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
	public class DocumentsController : ControllerBase
	{
		private readonly IDocuments _documents;

		public DocumentsController(IDocuments documents)
		{
			_documents = documents;
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
				List<DocumentAdmin> partys = await _documents.GetDocumentAdmins();
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
		/// Registrar un acta de casilla
		/// </summary>
		/// <returns>Id del acta nueva</returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la verificación.</response>
		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> Register([FromBody] RegisterDocument model)
		{
			ResponseBase response = new ResponseBase();
			try
			{
				int document = await _documents.Register(model);

				if (document < 1)
				{
					response.Success = false;
					response.Message = "Error al crear el documento";

					return Ok(response);
				}

				response.Success = true;
				response.Message = "Document subido exitosament";
				response.Data = new { PartidoId = document };

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
