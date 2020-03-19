namespace LloydsExchange.QuotePolicies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RequestRouter;

    public abstract class QuoteProviderBase : ResponderBase
    {
        private readonly List<Type> validTypes =
            new List<Type>
            {
                typeof(QuoteBase),
                typeof(DeclinedBase),
                typeof(ReferralBase),
            };

        public StandardResponseBase GetQuote(QuoteRequestBase quoteRequest)
        {
            var response = this.GetResponse(quoteRequest);
            return this.ValidateResponse(response);
        }

        private StandardResponseBase ValidateResponse(StandardResponseBase response)
        {
            if (!this.validTypes.Any(t => t.IsInstanceOfType(response)))
                throw new InvalidCastException("Quote providers can only return a Quote, Declined or Referral message.");

            return response;
        }
    }
}
