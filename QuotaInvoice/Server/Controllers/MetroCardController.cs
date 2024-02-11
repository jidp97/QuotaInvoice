using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuotaInvoice.Server.Services;
using QuotaInvoice.Shared.Entities;
using QuotaInvoice.Shared.Models;

namespace QuotaInvoice.Server.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class MetroCardController : Controller
	{
		private readonly Interfaces _interfaces;
		public MetroCardController
		(
			Interfaces interfaces
		)
		{
			_interfaces = interfaces;
		}

		[HttpGet("list")]
		public async Task<ActionResult<List<MetroCard>>> GetListAsync() => await _interfaces.GetAllMetroCards();

		[HttpGet("{id}/getMetroCard")]
		public async Task<ActionResult<MetroCard>> GetAsync(Guid id) => await _interfaces.GetMetroCardById(id);

		[HttpGet]
		public async Task<ActionResult<List<MetroCard>>> GetPaginatedList([FromQuery] PaginationModel pagination, [FromQuery] string number, string userId)
			=> await _interfaces.GetPaginatedListOfUserMetroCards(pagination, number, userId);

		[HttpPost]
		public async Task<ActionResult> PostAsync(MetroCard metroCard)
		{
			await _interfaces.Add(metroCard);
			return new CreatedAtRouteResult("getMetroCard", new { id = metroCard.Id }, metroCard);
		}
		[HttpPut]
		public async Task<ActionResult> PutAsync(MetroCard metroCard)
		{
			await _interfaces.Update(metroCard);
			return NoContent();
		}
		[HttpDelete("{id}"), Authorize(Policy = Policies.IsAdmin)]
		public async Task<ActionResult> DeleteAsync(Guid Id)
		{
			var metroCard = new MetroCard { Id = Id };
			await _interfaces.Remove(metroCard);
			return NoContent();
		}
	}
}

