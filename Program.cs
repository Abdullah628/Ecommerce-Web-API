var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

List<Category> categories= new List<Category>();


// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast")
// .WithOpenApi();

app.MapGet("/", ()=>{
    return "API is working Finxe";
});

// Read => Read a Category => GET : /api/categories

app.MapGet("/api/categories", ()=>{
    return Results.Ok(categories);
});

// Create => Create a category => POST : /api/categories
app.MapPost("/api/categories", ()=>{
    var newCategory = new Category{
        // CategoryId = Guid.NewGuid(),
        CategoryId = Guid.Parse("e1cdb261-9cd5-4b8b-8e81-38f611eaaf6a"),
        Name = "smartphone",
        Description = "Devices and gadgets including phones, laptops, and other electronic equipment",
        CreateAt = DateTime.UtcNow,
    };
    categories.Add(newCategory);

    return Results.Created($"/api/categories/{newCategory.CategoryId}", newCategory);
});

// Delete => Delete a Category => DELETE : /api/categories
app.MapDelete("/api/categories", ()=>{
    var foundCategory = categories.FirstOrDefault(category => category.CategoryId == Guid.Parse("e1cdb261-9cd5-4b8b-8e81-38f611eaaf6a"));
    if (foundCategory != null){
        return Results.NotFound("Category with this id doesn't exist");
    }
    categories.Remove(foundCategory);
    return Results.NoContent();
});

// Update => Update a Category => PUT : /api/Categories
app.MapPut("/api/categories", ()=>{
    var foundCategory = categories.FirstOrDefault(category => category.CategoryId == Guid.Parse("e1cdb261-9cd5-4b8b-8e81-38f611eaaf6a"));
    if (foundCategory != null){
        return Results.NotFound("Category with this id doesn't exist");
    }
    foundCategory.Name = "Classical Phone";
    foundCategory.Description = "Classical is the best phone";
    return Results.NoContent();
});

app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }


public record Category{
    public Guid CategoryId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreateAt { get; set; }
};


//CRUD



