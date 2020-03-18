namespace Lloyds.Exchange.Quotes
{
    using System;
    using RequestRouter;

    public abstract class QuoteBase : StandardResponseBase
    {
        public decimal Premium { get; protected set; }

        public decimal Tax { get; protected set; }

        public decimal Commission { get; protected set; }

        public DateTime ExpiryDateUtc { get; protected set; }

        protected void Validate()
        {
            ValidateCurrency(this.Premium, nameof(this.Premium));
            ValidateCurrency(this.Tax, nameof(this.Tax));
            ValidateCurrency(this.Commission, nameof(this.Commission));
            this.ValidatePremium();
            this.ValidateExpiryDate();
        }

        private static void ValidateCurrency(decimal value, string name)
        {
            if (value < 0)
                throw new ArgumentException($"{name} should not be less than zero.");
            if (value % 0.01m > 0)
                throw new ArgumentException($"{name} should not have more than 2 decimal places.");
        }

        private void ValidatePremium()
        {
            if (this.Premium < 0.01m)
                throw new ArgumentException($"{nameof(this.Premium)} should not be zero.");
        }

        private void ValidateExpiryDate()
        {
            if (this.ExpiryDateUtc.Date == DateTime.UtcNow.Date)
                throw new ArgumentException("Quotes should not expire on the same day they are issued.");
            if (this.ExpiryDateUtc.Date < DateTime.UtcNow.Date)
                throw new ArgumentException($"{nameof(this.ExpiryDateUtc)} should not be in the past.");
        }
    }
}
