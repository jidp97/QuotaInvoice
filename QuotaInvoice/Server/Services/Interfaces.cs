using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuotaInvoice.Shared.Entities;
using QuotaInvoice.Shared.Models;

namespace QuotaInvoice.Server.Services;

public interface Interfaces
{
    public Task Add<T>(T element);
    public Task Update<T>(T element);
    public Task Remove<T>(T element);

    //CreditCards

    Task<ActionResult<List<CreditCard>>> GetAllCreditCards();
    Task<ActionResult<List<CreditCard>>> GetPaginatedListofCreditCards(PaginationModel pagination, string number);
    Task<ActionResult<CreditCard>> GetCreditCardById(Guid id);

    //MetroCards

    Task<ActionResult<List<MetroCard>>> GetAllMetroCards();
    Task<ActionResult<MetroCard>> GetMetroCardById(Guid id);
    Task<ActionResult<MetroCard>> GetMetroCardByNumber(string number);
    Task<ActionResult<MetroCard>> GetUserMetroCardById(Guid id, string userId);
    Task<ActionResult<List<MetroCard>>> GetPaginatedListOfUserMetroCards(PaginationModel pagination, string number, string userId);
    Task<ActionResult<List<MetroCard>>> GetPaginatedListOfAllMetroCards(PaginationModel pagination, string number);

    //TravelHistory()

    Task<ActionResult<List<TravelHistory>>> GetAllUserTravels(string metroCard);
    Task<ActionResult<List<TravelHistory>>> GetPaginatedListOfAllUserTravels(PaginationModel pagination, string metroCardId , string metroCard);
    Task<ActionResult<List<TravelHistory>>> GetPaginatedListOfAllActiveTravels(PaginationModel pagination, bool active);
    Task<ActionResult<TravelHistory>> GetCurrentTrip(string metroCard, bool active);

    


}

