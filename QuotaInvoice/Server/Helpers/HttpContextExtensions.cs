﻿using Microsoft.EntityFrameworkCore;

namespace QuotaInvoice.Server.Helpers
{
    public static class HttpContextExtensions
    {
        public static async Task InsertarParametrosPaginacionEnRespuesta<T>(
            this HttpContext context, IQueryable<T> queryable, int cantidadRegistrosAmostrar)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            double conteo = await queryable.CountAsync();
            double totalPaginas = Math.Ceiling(conteo / cantidadRegistrosAmostrar);
            //context.Response.Headers.Add("totalPaginas", totalPaginas.ToString());
            context.Response.Headers.Add("conteo", conteo.ToString());
            context.Response.Headers.Add("totalPaginas", totalPaginas.ToString());
        }
    }
}