namespace LloydsExchange.QuotePolicies.Tests.TestDoubles
{
    using RequestRouter;

    public class QuoteProviderStub : QuoteProviderBase
    {
        public bool GetResponseCalled { get; private set; }

        protected override StandardResponseBase GetResponse(StandardRequestBase standardRequest)
        {
            this.GetResponseCalled = true;
            var request = (QuoteRequestStub)standardRequest;
            return request?.ForcedResponse;
        }
    }
}
