namespace LloydsExchange.QuotePolicies.Tests
{
    using System;
    using LloydsExchange.QuotePolicies.Tests.TestDoubles;
    using RequestRouter;
    using Xunit;

    public class QuoteProviderTests
    {
        private readonly QuoteProviderStub quoteProvider = new QuoteProviderStub();

        [Fact]
        public void QuoteProviderBaseInheritsResponderBase()
        {
            Assert.True(typeof(ResponderBase).IsAssignableFrom(typeof(QuoteProviderBase)));
        }

        [Fact]
        public void GivenAQuoteRequestQuoteProviderCanRespondWithAQuote()
        {
            var quoteRequest = new QuoteRequestStub { ForcedResponse = new QuoteStub() };
            var response = this.quoteProvider.GetQuote(quoteRequest);
            Assert.IsAssignableFrom<StandardResponseBase>(response);
            Assert.IsAssignableFrom<QuoteBase>(response);
        }

        [Fact]
        public void GivenAQuoteRequestQuoteProviderCanDeclineToQuote()
        {
            var quoteRequest = new QuoteRequestStub { ForcedResponse = new DeclinedStub() };
            var response = this.quoteProvider.GetQuote(quoteRequest);
            Assert.IsAssignableFrom<StandardResponseBase>(response);
            Assert.IsAssignableFrom<DeclinedBase>(response);
        }

        [Fact]
        public void GivenAQuoteRequestQuoteProviderCanOfferAReferral()
        {
            var quoteRequest = new QuoteRequestStub { ForcedResponse = new ReferralStub() };
            var response = this.quoteProvider.GetQuote(quoteRequest);
            Assert.IsAssignableFrom<StandardResponseBase>(response);
            Assert.IsAssignableFrom<ReferralBase>(response);
        }

        [Fact]
        public void QuoteProviderThrowsInResponseIsNotQuoteDeclinedOrReferral()
        {
            var quoteRequest = new QuoteRequestStub { ForcedResponse = new InvalidResponseStub() };
            var ex = Assert.Throws<InvalidCastException>(() => this.quoteProvider.GetQuote(quoteRequest));
            Assert.Equal("Quote providers can only return a Quote, Declined or Referral message.", ex.Message);
        }
    }
}
