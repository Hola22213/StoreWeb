using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MinimalAPIERP.Data;
using ERP;

namespace MinimalAPIERP.Api
{
    internal static class StoreApi
    {
        public static RouteGroupBuilder MapStoreApi(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/erp").WithTags("Store Api");

            // Endpoint para obtener todos los stores
            group.MapGet("/stores", async (AppDbContext db) =>
            {
                var stores = await db.Stores.ToListAsync();
                return Results.Ok(stores);
            })
            .WithOpenApi();

            // Endpoint para obtener un store por su ID
            group.MapGet("/store/{storeid:int}", async (int storeid, AppDbContext db) =>
            {
                var store = await db.Stores.FindAsync(storeid);
                return store != null ? Results.Ok(store) : Results.NotFound();
            })
            .WithOpenApi();

            // Endpoint para crear un nuevo store
            group.MapPost("/store", async (Store newStore, AppDbContext db) =>
            {
                db.Stores.Add(newStore);
                await db.SaveChangesAsync();

                var response = new
                {
                    newStore.StoreId,
                    newStore.name,
                    newStore.code
                };
                return Results.Created($"/erp/store/{newStore.StoreId}", response);
            })
            .Produces<object>()
            .WithOpenApi();

            // Endpoint para actualizar un store existente
            group.MapPut("/store/{storeid:int}", async (int storeid, Store updatedStore, AppDbContext db) =>
            {
                var existingStore = await db.Stores.FindAsync(storeid);
                if (existingStore == null)
                    return Results.NotFound();

                existingStore.name = updatedStore.name; // Actualizar otros campos según sea necesario
                await db.SaveChangesAsync();
                return Results.NoContent();
            })
            .WithOpenApi();

            // Endpoint para eliminar un store por su ID
            group.MapDelete("/store/{storeid:int}", async (int storeid, AppDbContext db) =>
            {
                var existingStore = await db.Stores.FindAsync(storeid);
                if (existingStore == null)
                    return Results.NotFound();

                db.Stores.Remove(existingStore);
                await db.SaveChangesAsync();
                return Results.NoContent();
            })
            .WithOpenApi();

            return group;
        }
    }
}
