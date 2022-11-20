using Microsoft.AspNetCore.SignalR;
using SignalR_Client_Results.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddCors(policy => policy.AddDefaultPolicy(policy => policy.AllowAnyMethod()
                                  .AllowAnyHeader()
                                  .AllowCredentials()
                                  .SetIsOriginAllowed(origin => true)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<MyHub>("/myhub");

app.Run();


#region SignalR - Client Results
//Server, client'a mesaj göndermenin dýþýnda client'tan da return ile bir result isteyebilir.
//Bunun için server'da InvokeAsync ile istemciden bir sonuç döndürmesi beklenir.

#region .NET Client

#endregion
#region Typescript Client

#endregion
#region Strongly-typed Hub'lar da 

#endregion

#endregion