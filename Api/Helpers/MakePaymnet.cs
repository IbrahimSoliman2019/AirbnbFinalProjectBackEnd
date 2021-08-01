﻿using Api.DTOS;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Helpers
{
    public class MakePayment
    {
        public async Task<string> PayAsync(PaymentDto model)
        {
            try
            {
                StripeConfiguration.ApiKey = "";

                var options = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Number = model.CardNumber,
                        ExpMonth = model.Month,
                        ExpYear = model.Year,
                        Cvc = model.CVC,
                    },
                };
                var service = new TokenService();
                Token stripeToken = await service.CreateAsync(options);

                var chargeoptions = new ChargeCreateOptions
                {
                    Amount = model.Amount,
                    Currency ="usd",
                    Description = model.Description,

                    Source = stripeToken.Id

                };
                var chargeservice = new ChargeService();
                var Charge =await chargeservice.CreateAsync(chargeoptions);
                if (Charge.Paid)
                {
                    return "success";
                }
                else
                {
                    return "failed";
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}