using System.Security.Claims;

namespace API.UsersVote.Service.Imp
{
	public class ImpAuthorization : IAuthorization
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ImpAuthorization(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public string GetSection()
		{
			ClaimsIdentity identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;

			if (identity.IsAuthenticated)
			{
				Claim claim = identity.FindFirst("Section");

				if (claim != null)
				{
					return claim.Value;

				}
			}
			return null;
		}

		public int UserCurrentId()
		{
			ClaimsIdentity Identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;

			if (Identity.IsAuthenticated != false)
			{
				int idUsuarioActual = int.Parse(Identity.FindFirst(ClaimTypes.NameIdentifier).Value);

				return idUsuarioActual;
			}

			return 0;
		}
	}
}
