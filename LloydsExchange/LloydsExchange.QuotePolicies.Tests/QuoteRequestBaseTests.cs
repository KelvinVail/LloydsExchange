namespace LloydsExchange.QuotePolicies.Tests
{
    using RequestRouter;
    using Xunit;

    public class QuoteRequestBaseTests : QuoteRequestBase
    {
        [Fact]
        public void RequestQuoteInheritsRequest()
        {
            Assert.True(typeof(StandardRequestBase).IsAssignableFrom(typeof(QuoteRequestBase)));
        }
    }
}
