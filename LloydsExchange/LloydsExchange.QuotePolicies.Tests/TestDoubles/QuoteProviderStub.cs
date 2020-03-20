namespace LloydsExchange.QuotePolicies.Tests.TestDoubles
{
    using RequestRouter;

    public class QuoteProviderStub : QuoteProviderBase
    {
        protected override StandardResponseBase GetResponse(StandardRequestBase standardRequest)
        {
            var request = (QuoteRequestStub)standardRequest;
            return request.ForcedResponse;
        }
    }
}
