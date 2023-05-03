using Microsoft.EntityFrameworkCore;
using PokemonAPI;
using PokemonAPI.Contexts;
using PokemonAPI.Repositories;

const string policyName = "AllowAll";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName,
                              policy =>
                              {
                                  policy.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader();
                              });
    options.AddPolicy(name: "OnlyGET",
                              policy =>
                              {
                                  policy.AllowAnyOrigin()
                                  .WithMethods("GET")
                                  .AllowAnyHeader();
                              });
});


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



bool useSql = true;
if (useSql)
{
    var optionsBuilder =
        new DbContextOptionsBuilder<PokemonContext>();
    optionsBuilder.UseSqlServer(Secrets.ConnectionString);
    PokemonContext context = 
        new PokemonContext(optionsBuilder.Options);
    builder.Services.AddSingleton<IPokemonsRepository>(
        new PokemonsRepositoryDB(context));
}
else
{
    builder.Services.AddSingleton<IPokemonsRepository>
        (new PokemonsRepository());
}

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
