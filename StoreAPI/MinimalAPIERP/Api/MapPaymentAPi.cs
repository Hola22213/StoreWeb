using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using MinimalAPIERP.Data;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ERP.Api;

internal static class PaymentApi
{
    public static RouteGroupBuilder MapPaymentApi(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/erp")
            .WithTags("Payment Api");

        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

        group.MapGet("/paymentinfo", async (AppDbContext db) =>
        {
            var paymentInfos = await db.PaymentInfos.ToListAsync();
            return paymentInfos.Count > 0
                ? Results.Json(paymentInfos, options)
                : Results.NotFound();
        })
        .WithOpenApi();

        return group;
    }
}
