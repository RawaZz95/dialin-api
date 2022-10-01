using Microsoft.EntityFrameworkCore;
using DialInApi.Data;
using DialInApi.Models;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// InMemory
builder.Services.AddDbContext<DialInDbContext>(opt => 
    opt.UseInMemoryDatabase(builder.Configuration.GetConnectionString("InMemoryDbConnection")));

// REDIS
builder.Services.AddSingleton<IConnectionMultiplexer>(opt => 
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")));
    
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDialInRepository, SqlDialInRepository>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// GET Multiple
app.MapGet("api/v1/dialins", async (IDialInRepository dialInRepository) =>
{
    var dialins = await dialInRepository.GetAllDialInsAsync();

    return Results.Ok(dialins);
});

// GET Single
app.MapGet("api/v1/dialins/{dialInId}", async (IDialInRepository dialInRepository, string dialInId) =>
{
    var dialIn = await dialInRepository.GetDialInByIdAsync(dialInId);

    if (dialIn != null) 
    {
        return Results.Ok(dialIn);
    }

    return Results.NotFound();
});

// POST Create
app.MapPost("api/v1/dialins", async (IDialInRepository dialInRepository, DialIn dialIn) =>
{
    // dialIn.Id = 0;
    // dialIn.DialInId = Guid.NewGuid().ToString();
    // dialIn.TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
    // await dialInRepository.CreateDialInAsync(dialIn);
    // await dialInRepository.SaveChangesAsync();

    // return Results.Created($"/api/v1/dialins/{dialIn.DialInId}", dialIn);
    return Results.Problem();
});

// PUT Update
app.MapPut("api/v1/dialins/{dialInId}", async (IDialInRepository dialInRepository, string dialInId, DialIn dialIn) =>
{
    var originalDialIn = await dialInRepository.GetDialInByIdAsync(dialInId);

    if (originalDialIn == null) 
    {
        return Results.NotFound();
    }

    originalDialIn.BrewMethod = dialIn.BrewMethod;
    originalDialIn.CoffeeName = dialIn.CoffeeName;
    originalDialIn.CoffeeCode = dialIn.CoffeeCode;
    originalDialIn.AmountInGrams = dialIn.AmountInGrams;
    originalDialIn.YieldInGrams = dialIn.YieldInGrams;
    originalDialIn.ShotDuration = dialIn.ShotDuration;
    originalDialIn.GrindSize = dialIn.GrindSize;
    originalDialIn.GrinderName = dialIn.GrinderName;
    originalDialIn.TasteResult = dialIn.TasteResult;

    await dialInRepository.SaveChangesAsync();

    return Results.NoContent();
});

// Delete Delete
app.MapDelete("api/v1/dialins/{dialInId}", async (IDialInRepository dialInRepository, string dialInId) =>
{
    var originalDialIn = await dialInRepository.GetDialInByIdAsync(dialInId);

    if (originalDialIn == null) 
    {
        return Results.NotFound();
    }

    dialInRepository.DeleteDialInAsync(originalDialIn);
    await dialInRepository.SaveChangesAsync();

    return Results.Ok(originalDialIn);
});

app.Run();