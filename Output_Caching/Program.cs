using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder =>
    {
        builder.Expire(TimeSpan.FromSeconds(5));
    });

    options.AddPolicy("Custom", policy =>
    {
        policy.Expire(TimeSpan.FromSeconds(10));
    });
});

var app = builder.Build();

app.UseOutputCache();

app.MapGet("/", () =>
{
    return Results.Ok(DateTime.UtcNow);
}).CacheOutput("Custom");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



#region Output Caching

#region Output Cache'i Pipeline'a Ekleme

#endregion
#region Minimal API'larýn Cache'lenmesi
#region CacheOutput Metodu

#endregion
#region OutputCache Attribute'u

#endregion
#endregion
#region Controller'da Cache'leme

#endregion
#region Cache Politikasý Oluþturma

#endregion

#endregion