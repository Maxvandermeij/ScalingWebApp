using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace ScalingWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrimeController : ControllerBase
    {

        [HttpGet]
        public PrimeResult Get(int searchLimit = 1000000)
        {
            var result = CalculatePrimes(searchLimit);
            return new PrimeResult
            {
                LowestPrime = result.Min(),
                HighestPrime = result.Max(),
                TotalNumberOfPrimes = result.Count(),
                SearchedFrom = 0,
                SearchedUntil = searchLimit
            };
        }

        public IEnumerable<int> CalculatePrimes(int max)
        {
            return Enumerable.Range(0, (int)Math.Floor(2.52 * Math.Sqrt(max) / Math.Log(max)))
                .Aggregate(Enumerable.Range(2, max - 1).ToList(),
                    (result, index) =>
                    {
                        var bp = result[index]; var sqr = bp * bp;
                        result.RemoveAll(i => i >= sqr && i % bp == 0);
                        return result;
                    });
        }
    }


    
    public class PrimeResult
    {
        public int SearchedFrom { get; set; }
        public int SearchedUntil { get; set; }
        public int LowestPrime { get; set; }
        public int HighestPrime { get; set; }
        public int TotalNumberOfPrimes { get; set; }
    }
}