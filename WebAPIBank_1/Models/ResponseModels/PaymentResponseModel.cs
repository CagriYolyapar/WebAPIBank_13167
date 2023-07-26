using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIBank_1.Models.ResponseModels
{
    public class PaymentResponseModel
    {
        public string CardUserName { get; set; }
        public string SecurityNumber { get; set; }
        public string CardNumber { get; set; }
        public int CardExpiryMonth { get; set; }
        public int CardExpiryYear { get; set; }

    }
}