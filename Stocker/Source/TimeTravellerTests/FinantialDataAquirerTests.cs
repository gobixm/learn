using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTraveller;

namespace TimeTravellerTests
{
    public class FinantialDataAquirerTests
    {
        [Test]
        [Category("Integration")]
        public void ShouldGetSNPIndices()
        {
            FinantialDataAquirer aquirer = new FinantialDataAquirer(new StandardWebClient());
            var task = aquirer.GetHistoricalData("SNP", DateTime.Parse("01.01.2014"), DateTime.Parse("01.01.2015"));
            Assert.False(task.IsCompleted);
            var prices = task.Result;
            Assert.NotNull(prices);
            Assert.Greater(prices.Count, 0);
        }
    }
}
