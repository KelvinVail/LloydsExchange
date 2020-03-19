namespace LloydsExchange.QuotePolicies.Tests.TestDoubles
{
    using RequestRouter;

    public class QuoteRequestStub : QuoteRequestBase
    {
        public StandardResponseBase ForcedResponse { get; set; }
    }
}
