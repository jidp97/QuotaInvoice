using System.Linq;
using QuotaInvoice.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using QuotaInvoice.Shared.Entities;
using QuotaInvoice.Shared.Models;
using QuotaInvoice.Server.Helpers;
using System;

namespace QuotaInvoice.Server.Services;

public class Services : Interfaces
{
    public ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Services
    (
        ApplicationDbContext context,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task Add<T>(T element)
    {
        _context.Add(entity: element);
        await _context.SaveChangesAsync();
    }

    public async Task Update<T>(T element)
    {
        _context.Entry(element).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Remove<T>(T element)
    {
        _context.Remove(element);
        await _context.SaveChangesAsync();
    }

    public async Task<ActionResult<List<CreditCard>>> GetAllCreditCards()
        => await _context.CreditCards.OrderBy(x => x.Number)
            .ToListAsync();

    public async Task<ActionResult<List<CreditCard>>> GetPaginatedListofCreditCards(PaginationModel pagination, string number)
    {
        var queryable = _context.CreditCards.AsQueryable();
        if (!string.IsNullOrEmpty(number))
        {
            queryable = queryable.Where(x => x.Number.Contains(number));
        }
        await _httpContextAccessor.HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, pagination.CantidadAMostrar);
        var result = await queryable.Paginar(pagination).ToListAsync();
        return result;
    }

    public async Task<ActionResult<CreditCard>> GetCreditCardById(Guid id)
        => await _context.CreditCards.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<ActionResult<List<MetroCard>>> GetAllMetroCards()
        => await _context.MetroCards.OrderBy(x => x.Number).
            ToListAsync();

    public async Task<ActionResult<MetroCard>> GetMetroCardById(Guid id) =>
        await _context.MetroCards
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<ActionResult<MetroCard>> GetMetroCardByNumber(string number) =>
        await _context.MetroCards
            .FirstOrDefaultAsync(x => x.Number == number);

    public async Task<ActionResult<MetroCard>> GetUserMetroCardById(Guid id, string userId) =>
        await _context.MetroCards.Where(x => x.UserId == userId).FirstOrDefaultAsync(x => x.Id == id);

    public async Task<ActionResult<List<MetroCard>>> GetPaginatedListOfUserMetroCards(PaginationModel pagination, string number, string userId)
    {
        var queryable = _context.MetroCards.Where(x=>x.UserId == userId).AsQueryable();
        if (!string.IsNullOrEmpty(number))
        {
            queryable = queryable.Where(x => x.Number.Contains(number));
        }
        await _httpContextAccessor.HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, pagination.CantidadAMostrar);
        var result = await queryable.Paginar(pagination).ToListAsync();
        return result;
    }


    public async Task<ActionResult<List<MetroCard>>> GetPaginatedListOfAllMetroCards(PaginationModel pagination, string number)
    {
        var queryable = _context.MetroCards.AsQueryable();
        if (!string.IsNullOrEmpty(number))
        {
            queryable = queryable.Where(x => x.Number.Contains(number));
        }
        await _httpContextAccessor.HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, pagination.CantidadAMostrar);
        var result = await queryable.Paginar(pagination).ToListAsync();
        return result;
    }

    public async Task<ActionResult<List<TravelHistory>>> GetAllUserTravels(string metroCard)
        => await _context.TravelsHistory.ToListAsync(); 

    public Task<ActionResult<List<TravelHistory>>> GetPaginatedListOfAllUserTravels(PaginationModel pagination, string metroCardId, string metroCard)
    {
        throw new System.NotImplementedException();
    }

    public async Task<ActionResult<List<TravelHistory>>> GetPaginatedListOfAllActiveTravels(PaginationModel pagination, bool active)
    {
        var queryable = _context.TravelsHistory.AsQueryable();
        var activeTravels = queryable.Where(x => x.Active);
        await _httpContextAccessor.HttpContext.InsertarParametrosPaginacionEnRespuesta(activeTravels, pagination.CantidadAMostrar);
        var result = await activeTravels.Paginar(pagination).ToListAsync();
        return result;
    }

    public async Task<ActionResult<TravelHistory>> GetCurrentTrip(string metroCard, bool active) =>
           await _context.TravelsHistory
                .Include(x => x.MetroCard)
                    .Where(x => x.Active)
                        .FirstOrDefaultAsync(x => x.MetroCard.Number == metroCard);
}

  