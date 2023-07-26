using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIBank_1.DesignPatterns.SingletonPattern;
using WebAPIBank_1.Models.Context;
using WebAPIBank_1.Models.Entities;
using WebAPIBank_1.Models.RequestModels;
using WebAPIBank_1.Models.ResponseModels;

namespace WebAPIBank_1.Controllers
{
    public class PaymentController : ApiController
    {
        MyContext _db;

        public PaymentController()
        {
            _db = DBTool.DBInstance;
        }

        //Asagıdaki Action sadece development testi icindir...API canlıya cıkacagı zaman kesinlikle acık bırakılmamalıdır...

        //[HttpGet]
        //public List<PaymentResponseModel> GetAll()
        //{
        //    return _db.CardInfoes.Select(x => new PaymentResponseModel
        //    {
        //        CardExpiryMonth = x.CardExpiryMonth,
        //        CardExpiryYear = x.CardExpiryYear,
        //        CardNumber = x.CardNumber,
        //        CardUserName = x.CardUserName,
        //        SecurityNumber  = x.SecurityNumber
        //    }).ToList();
        //}


        [HttpPost]
        public IHttpActionResult ReceivePayment(PaymentRequestModel item)
        {
            CardInfo ci = _db.CardInfoes.FirstOrDefault(x => x.CardNumber == item.CardNumber && x.SecurityNumber == item.SecurityNumber && x.CardUserName == item.CardUserName && x.CardExpiryMonth == item.CardExpiryMonth && x.CardExpiryYear == item.CardExpiryYear);

            if (ci != null)
            {

                if(ci.CardExpiryYear < DateTime.Now.Year)
                {
                    return BadRequest("Tarihi gecmis(yıl)");
                }
                else if(ci.CardExpiryYear == DateTime.Now.Year)
                {
                    if(ci.CardExpiryMonth < DateTime.Now.Month)
                    {
                        return BadRequest("Tarihi gecmiş(ay)");
                    }

                    if(ci.Balance >= item.ShoppingPrice)
                    {
                        SetBalance(item, ci);
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Bakiyeniz yetersiz");
                    }
                }
                else if(ci.Balance >= item.ShoppingPrice)
                {
                    SetBalance(item, ci);
                    return Ok();
                }
                return BadRequest("Bakiyeniz getersiz");
            }


            return BadRequest("Kart bilgileri yanlıs");
        }


        public void SetBalance(PaymentRequestModel item,CardInfo ci)
        {
            ci.Balance -= item.ShoppingPrice;
            //ShoppingPrice'dan yüzdelik komisyon alınıp kalan miktar alacaklının hesabına aktarılır...
            _db.SaveChanges();
        }



    }
}
