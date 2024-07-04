namespace MinimalAPIERP.MinimalAPIERP.Api
{
    public class CarItemAPI
    {
        public void MapEndpoints(WebApplication app)
        {
            app.MapGet("/api/caritems", GetAllCarItems);
            app.MapGet("/api/caritems/{id}", GetCarItemById);
            app.MapPost("/api/caritems", CreateCarItem);
            app.MapPut("/api/caritems/{id}", UpdateCarItem);
            app.MapDelete("/api/caritems/{id}", DeleteCarItem);
        }

        private IResult GetAllCarItems()
        {
            // Lógica para obtener todos los CarItems
            return Results.Ok(new List<CarItem>());
        }

        private IResult GetCarItemById(int id)
        {
            // Lógica para obtener un CarItem por ID
            var carItem = new CarItem { Id = id, name = "Sample Car", Description = "Sample Description" };
            return Results.Ok(carItem);
        }

        private IResult CreateCarItem(CarItem carItem)
        {
            // Lógica para crear un nuevo CarItem
            return Results.Created($"/api/caritems/{carItem.Id}", carItem);
        }

        private IResult UpdateCarItem(int id, CarItem carItem)
        {
            // Lógica para actualizar un CarItem existente
            carItem.Id = id;
            return Results.Ok(carItem);
        }

        private IResult DeleteCarItem(int id)
        {
            // Lógica para eliminar un CarItem
            return Results.NoContent();
        }
    }

    public class CarItem
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string Description { get; set; }
    }
}
