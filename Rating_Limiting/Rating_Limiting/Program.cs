using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region Temelde Nasýl Uygulanýr?
//builder.Services.AddRateLimiter(options =>
//{
//    options.AddFixedWindowLimiter("Basic", _options =>
//    {
//        _options.Window = TimeSpan.FromSeconds(12);
//        _options.PermitLimit = 4;
//        _options.QueueLimit = 2;
//        _options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
//    });
//});
#endregion
#region Rate Limiter Algoritmalarý Nelerdir?

#region Fixed Window
//builder.Services.AddRateLimiter(options =>
//{
//    options.AddFixedWindowLimiter("Fixed", _options =>
//    {
//        _options.Window = TimeSpan.FromSeconds(12);
//        _options.PermitLimit = 4;
//        _options.QueueLimit = 2;
//        _options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
//    });
//});
#endregion
#region Sliding Window
//builder.Services.AddRateLimiter(options =>
//{
//    options.AddSlidingWindowLimiter("Sliding", _options =>
//    {
//        _options.Window = TimeSpan.FromSeconds(12);
//        _options.PermitLimit = 4;
//        _options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
//        _options.QueueLimit = 2;
//        _options.SegmentsPerWindow = 2;
//    });
//});
#endregion
#region Token Bucket
//builder.Services.AddRateLimiter(options =>
//{
//    options.AddTokenBucketLimiter("Token", _options =>
//    {
//        _options.TokenLimit = 4;
//        _options.TokensPerPeriod = 4;
//        _options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
//        _options.QueueLimit = 2;
//        _options.ReplenishmentPeriod = TimeSpan.FromSeconds(12);
//    });
//});
#endregion
#region Concurrency
//builder.Services.AddRateLimiter(options =>
//{
//    options.AddConcurrencyLimiter("Conccurency", _options =>
//    {
//        _options.PermitLimit = 4;
//        _options.QueueLimit = 2;
//        _options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
//    });
//});
#endregion

#endregion

#region OnRejected Property'si
//builder.Services.AddRateLimiter(options =>
//{
//    options.AddFixedWindowLimiter("Basic", _options =>
//    {
//        _options.Window = TimeSpan.FromSeconds(12);
//        _options.PermitLimit = 4;
//        _options.QueueLimit = 2;
//        _options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
//    });

//    options.OnRejected = (context, cancellationToken) =>
//    {
//        //Log...
//        return new();
//    };
//});
#endregion

#region Özelleþtirilmiþ Rate Limit Policy Oluþturma
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy<string, CustomRateLimitPolicy>("CustomPolicy");
});
#endregion

var app = builder.Build();

app.MapGet("/", () =>
{

}).RequireRateLimiting("...");

app.UseRateLimiter();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


