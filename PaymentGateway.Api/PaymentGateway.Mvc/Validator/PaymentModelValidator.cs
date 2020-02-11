using PaymentGateway.Mvc.Models;
using System;

namespace PaymentGateway.Mvc.Validator
{
    public static class PaymentModelValidator
    {
        public static bool IsValid(this PaymentModel model)
        {
            return !string.IsNullOrEmpty(model.CardNumber) &&
                   !string.IsNullOrEmpty(model.CVV) &&
                   !string.IsNullOrEmpty(model.Currency) &&
                   model.Amount > 0 &&
                   model.ExpiryDate > DateTime.Today;
        }
    }
}
