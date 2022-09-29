using CanWeFixItService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CanWeFixItApiUnitTests
{
    [TestClass]
    public class ApiUnitTests
    {
        const string connectionString = "Data Source=DatabaseService;Mode=Memory";
        private readonly DatabaseService databaseService;

        public ApiUnitTests()
        {
            databaseService = new DatabaseService(connectionString);
            databaseService.SetupDatabase();
        }

        [TestMethod]
        public void ValidateMarketDataToReturnCorrectJSONPayload()
        {
            var marketData = databaseService.MarketData().Result;

            Assert.IsNotNull(marketData);
            Assert.Equals(2, marketData.Count());
            Assert.IsTrue(marketData.All(x => x.Active));
            Assert.IsTrue(marketData.All(x => x.Id is 2 or 4));
            Assert.IsTrue(marketData.All(x => x.InstrumentId is 2 or 4));
        }

        [TestMethod]
        public void ValidateInstrumentToReturnCorrectJSONPayload()
        {
            var instrumentsData = databaseService.Instruments().Result;

            Assert.IsNotNull(instrumentsData);
            Assert.Equals(4, instrumentsData.Count());
            Assert.IsTrue(instrumentsData.All(x => x.Active));
            Assert.IsTrue(instrumentsData.All(x => x.Id is 2 or 4 or 6 or 8));
        }

        [TestMethod]
        public void ValidateMarketValuationToReturnCorrectJSONPayload()
        {
            var marketValuationData = databaseService.MarketValuation().Result;

            Assert.IsNotNull(marketValuationData);
            Assert.Equals(13332, marketValuationData.FirstOrDefault().Total);
            Assert.Equals("DataValueTotal", marketValuationData.FirstOrDefault().Name);
        }
    }
}