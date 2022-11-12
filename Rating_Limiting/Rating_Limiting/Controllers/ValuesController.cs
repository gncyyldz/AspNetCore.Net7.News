using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace Rating_Limiting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("CustomPolicy")]
    public class ValuesController : ControllerBase
    {
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAsync()
        {
            await Task.Delay(20000);
            return Ok();
        }
    }
}

#region Rate Limit
#region Rate Limit Nedir?

#endregion

#region Temelde Nasıl Uygulanır?

#endregion

#region Rate Limiter Algoritmaları Nelerdir?

#region Fixed Window
//Sabit bir zaman aralığı kullanarak istekleri sınırlandıran algoritmadır.
#endregion
#region Sliding Window
//Fixed Windows algoritmasına benzerlik göstermektedir. Her sabit sürede bir zamana aralığında istekleri sınırlandırmamaktadır. Lakin sürenin yaırısndan sonra diğer periyodun request kotasını harcayacak şekilde istekleri karşılar.
#endregion
#region Token Bucket
//Her periyotta işlenecek request sayısı kadar token üretilmektedir. Eğer ki bu tokenlar kullanıldıysa diğer periyottan borç alınabilir. Lakin her periyotta token üretim miktarı kadar token üretilecek ve bu şekilde rate limit uygulanacaktır. Şunu unutmamak lazımdır ki, her periyoun maximum token limiti verilen sabit sayı kadar olacaktır.
#endregion
#region Concurrency
//Asenkron requestleri sınırlanmak için kullanılan bir algoritmadır. Her istek concurrency sınırını bir azaltmakta ve bittikleri taktirde bu sınırı bir arttırmaktadırlar. Diğer algoritmalara nazaran sadece asenkron requestleri sınırlandırır. 
#endregion

#endregion

#region Attribute'lar
#region EnableRateLimiting
//Controller yahut action seviyesinde istenilen politikada rate limiti devreye sokmamızı sağlayan bir attribute'dur.
#endregion
#region DisableRateLimiting
//Controller seviyesinde devreye sokulmuş bir rate limit politikasıonın action seviyesinde pasifleştirilmesini sağlayan bir attributedur.
#endregion
#endregion

#region Minimal API'lar da Rate Limiting
//RequireRateLimiting
#endregion

#region OnRejected Property'si
//Rate limit uygulanan operasyonlarda sınırdan dolayı boşa çıkan request'lerin söz konusu olduğu durumlarda loglama vs. gibi işlemleri yapabilmek için kullandıuımız event mantığında bir properydir.
#endregion

#region Özelleştirilmiş Rate Limit Policy Oluşturma

#endregion
#endregion


class CustomRateLimitPolicy : IRateLimiterPolicy<string>
{
    public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected =>
    (context, cancellationToken) =>
        {
            //Log...
            return new();
        };

    public RateLimitPartition<string> GetPartition(HttpContext httpContext)
    {
        return RateLimitPartition.GetFixedWindowLimiter("", _ => new()
        {
            PermitLimit = 4,
            Window = TimeSpan.FromSeconds(12),
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 2
        });
    }
}