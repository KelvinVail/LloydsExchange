using LloydsExchange.QuotePolicies.Tests.TestDoubles;

namespace LloydsExchange.QuotePolicies.Tests
{
    using System;
    using RequestRouter;
    using Xunit;

    public class QuoteBaseTests : QuoteBase
    {
        public QuoteBaseTests()
        {
            this.Premium = 1.99m;
            this.Tax = 0.99m;
            this.Commission = 1.99m;
            this.ExpiryDateUtc = DateTime.Today.AddDays(1);
            this.Validate();
        }

        [Fact]
        public void QuoteInheritsFromResponse()
        {
            Assert.True(typeof(StandardResponseBase).IsAssignableFrom(typeof(QuoteBase)));
        }

        [Fact]
        public void OnValidateThrowIfPremiumIsZero()
        {
            this.Premium = 0;
            var ex = Assert.Throws<ArgumentException>(this.Validate);
            Assert.Equal($"{nameof(this.Premium)} should not be zero.", ex.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-0.01)]
        public void OnValidateThrowIfPremiumIsLessThanZero(decimal premium)
        {
            this.Premium = premium;
            var ex = Assert.Throws<ArgumentException>(this.Validate);
            Assert.Equal($"{nameof(this.Premium)} should not be less than zero.", ex.Message);
        }

        [Theory]
        [InlineData(1.999)]
        [InlineData(1.001)]
        [InlineData(238.971)]
        [InlineData(1.000001)]
        [InlineData(82910.24902808)]
        public void OnValidateThrowIfPremiumHasMoreThanThreeDecimals(decimal premium)
        {
            this.Premium = premium;
            var ex = Assert.Throws<ArgumentException>(this.Validate);
            Assert.Equal($"{nameof(this.Premium)} should not have more than 2 decimal places.", ex.Message);
        }

        [Fact]
        public void OnValidateTaxCanBeZero()
        {
            this.Tax = 0;
            this.Validate();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-0.01)]
        public void OnValidateThrowIfTaxIsLessThanZero(decimal tax)
        {
            this.Tax = tax;
            var ex = Assert.Throws<ArgumentException>(this.Validate);
            Assert.Equal($"{nameof(this.Tax)} should not be less than zero.", ex.Message);
        }

        [Theory]
        [InlineData(1.999)]
        [InlineData(1.001)]
        [InlineData(238.971)]
        [InlineData(1.000001)]
        [InlineData(82910.24902808)]
        public void OnValidateThrowIfTaxHasMoreThanThreeDecimals(decimal tax)
        {
            this.Tax = tax;
            var ex = Assert.Throws<ArgumentException>(this.Validate);
            Assert.Equal($"{nameof(this.Tax)} should not have more than 2 decimal places.", ex.Message);
        }

        [Fact]
        public void OnValidateCommissionCanBeZero()
        {
            this.Commission = 0;
            this.Validate();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-0.01)]
        public void OnValidateThrowIfCommissionIsLessThanZero(decimal commission)
        {
            this.Commission = commission;
            var ex = Assert.Throws<ArgumentException>(this.Validate);
            Assert.Equal($"{nameof(this.Commission)} should not be less than zero.", ex.Message);
        }

        [Theory]
        [InlineData(1.999)]
        [InlineData(1.001)]
        [InlineData(238.971)]
        [InlineData(1.000001)]
        [InlineData(82910.24902808)]
        public void OnValidateThrowIfCommissionHasMoreThanThreeDecimals(decimal commission)
        {
            this.Commission = commission;
            var ex = Assert.Throws<ArgumentException>(this.Validate);
            Assert.Equal($"{nameof(this.Commission)} should not have more than 2 decimal places.", ex.Message);
        }

        [Fact]
        public void OnValidateThrowIfExpiryDateIsToday()
        {
            this.ExpiryDateUtc = DateTime.Today;
            var ex = Assert.Throws<ArgumentException>(this.Validate);
            Assert.Equal("Quotes should not expire on the same day they are issued.", ex.Message);
        }

        [Fact]
        public void OnValidateThrowIfExpiryDateHasPassed()
        {
            this.ExpiryDateUtc = DateTime.Today.AddDays(-1);
            var ex = Assert.Throws<ArgumentException>(this.Validate);
            Assert.Equal($"{nameof(this.ExpiryDateUtc)} should not be in the past.", ex.Message);
        }

        [Fact]
        public void QuoteIsExpiredIfExpiryDateIsInThePast()
        {
            this.ExpiryDateUtc = DateTime.Today.AddDays(-1);
            Assert.True(this.Expired);
        }

        [Fact]
        public void QuoteIsNotExpiredIfTheExpiryDateIsToday()
        {
            this.ExpiryDateUtc = DateTime.Today;
            Assert.False(this.Expired);
        }

        [Fact]
        public void QuoteIsNotExpiredIfTheExpiryDateIsInTheFuture()
        {
            this.ExpiryDateUtc = DateTime.Today.AddDays(1);
            Assert.False(this.Expired);
        }

        [Fact]
        public void QuoteCanBeRejected()
        {
            this.Reject();
        }
    }
}
