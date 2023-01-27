using AdDeposit.Core;
using AdDeposit.Domain.Ads;
using AdDeposit.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(typeof(IWriteRepository<>), typeof(FakeStorage<>));
builder.Services.AddSingleton(typeof(IReadRepository<>), typeof(FakeStorage<>));

builder.Services.AddScoped<AdsCreation>();
builder.Services.AddScoped<AdsPublication>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();