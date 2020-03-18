namespace LloydsExchange.Quotes.Tests
{
    using Lloyds.Exchange.Quotes;
    using RequestRouter;
    using Xunit;

    public class RequestQuoteTests : RequestQuote
    {
        [Fact]
        public void RequestQuoteInheritsRequest()
        {
            Assert.True(typeof(RequestBase).IsAssignableFrom(typeof(RequestQuote)));
        }
    }
}
