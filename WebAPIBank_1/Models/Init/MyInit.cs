using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebAPIBank_1.Models.Context;
using WebAPIBank_1.Models.Entities;

namespace WebAPIBank_1.Models.Init
{
    public class MyInit:CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            CardInfo ci = new CardInfo();
            ci.CardUserName = "Erdal Goksen";
            ci.CardNumber = "1111 1111 1111 1111";
            ci.CardExpiryYear = 2024;
            ci.CardExpiryMonth = 12;
            ci.SecurityNumber = "222";
            ci.Limit = 50000;
            ci.Balance = 50000;
            context.CardInfoes.Add(ci);
            context.SaveChanges();
           
        }
    }
}