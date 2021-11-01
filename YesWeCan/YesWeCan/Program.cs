using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace YesWeCan
{
    class Program
    {
        private static readonly HttpClient Client = new();
        
        static async Task Main(string[] args)
        {
            try
            {
                (await Client.GetStringAsync("http://localhost:5010/v1/marketdata/"))
                    .ParseJson<IList<MarketData>>()
                    .Verify(
                        d => d.Count == 2,
                        d => d.All(x => x.Active),
                        d => d.All(x => x.Id is 2 or 4) && 
                             d.All(x => x.InstrumentId is 2 or 4)
                        )
                    .ToString()
                    .Write(s => s.Contains("Failed") ? ConsoleColor.Red : ConsoleColor.Green);
                
                (await Client.GetStringAsync("http://localhost:5010/v1/instruments/"))
                    .ParseJson<IList<Instrument>>()
                    .Verify(
                        d => d.Count == 4,
                        d => d.All(x => x.Active),
                        d => d.All(x => x.Id is 2 or 4 or 6 or 8) 
                    )
                    .ToString()
                    .Write(s => s.Contains("Failed") ? ConsoleColor.Red : ConsoleColor.Green);
                

                (await Client.GetStringAsync("http://localhost:5010/v1/valuations/"))
                    .ParseJson<IList<MarketValuation>>()
                    .Verify(
                        d => d.Count == 1,
                        _ => true,
                        d => d.First().Name == "DataValueTotal" && d.First().Total == 13332
                    )
                    .ToString()
                    .Write(s => s.Contains("Failed") ? ConsoleColor.Red : ConsoleColor.Green);
            }
            catch (Exception e)
            {
                e.Message.WriteLine();
                e.StackTrace?.WriteLine();
            }
            
            "Press any key to exit...".WriteLine();
            Console.Read();
        }
    }

    public static class Core
    {
        public static void WriteLine(this string content)
        {
            Console.WriteLine(content);
        }
        
        public static void Write(this string content, Func<string, ConsoleColor> useColour)
        {
            Console.ForegroundColor = useColour(content);
            Console.Write(content);
        }

        public static T ParseJson<T>(this string content)
        {
            return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        }

        public static VerificationResult Verify<T>(this IList<T> content, 
            Func<IList<T>, bool> verifyCount,
            Func<IList<T>, bool> verifyActive,
            Func<IList<T>, bool> verifyContent)
        {
            return new VerificationResult
            {
                Active = verifyActive(content),
                Content = verifyContent(content),
                Count = verifyCount(content),
                DataType = typeof(T).Name  
            };
        } 
    }

    public class Instrument
    {
        public int Id { get; set; }
        public string Sedol { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
    
    public class MarketData
    {
        public int Id { get; set; }
        public long? DataValue { get; set; }
        public int? InstrumentId { get; set; }
        public bool Active { get; set; }
    }

    public class MarketValuation
    {
        public string Name { get; set; }
        public long? Total { get; set; }
    }
    
    public class VerificationResult
    {
        public bool Active { get; set; }
        public bool Content { get; set; }
        public bool Count { get; set; }
        public string DataType { get; set; }

        private static string ResultString(bool result) => result ? "Passed" : "Failed";

        public override string ToString()
        {
            return $"=== {DataType} Verification ===\n" +
                   $"Active Test: {ResultString(Active)}\n" +
                   $"Content Test: {ResultString(Content)}\n" +
                   $"Count Test: {ResultString(Count)}\n" +
                   "========================================\n";
        }
    }
}