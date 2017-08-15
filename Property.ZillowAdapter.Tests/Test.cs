using NUnit.Framework;
using System;
using zillowlib;
namespace zillowlib.Tests
{
    [TestFixture()]
    public class Test
    {
        IPropertyEngine pricingEngine = new ZillowAdapter();

        public IPropertyEngine PricingEngine { get => pricingEngine; set => pricingEngine = value; }

        [Test()]
        public void SearchPropertyByAddress_returns_correctProperty()
        {
            var result =  PricingEngine.SearchProperty("2309 Aimee Ln", "60194");
            Assert.IsNotNull(result); 
            Assert.IsTrue(result.zpid > 0);

        }

        [Test()]
		public void GetZestimate_returns_estimateValue()
		{
			var result = PricingEngine.SearchProperty("2309 Aimee Ln", "60194");
            var price = PricingEngine.GetZestimate(result.zpid);

            Console.Write(price);
			Assert.IsNotNull(price);
			Assert.IsTrue(price > 0);

		}
    }
}
