using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class Trxn
    {
        public string id { get; set; }
        public string nameOnCard { get; set; }
        public string cardNumber { get; set; }
        public string expDate { get; set; }
        public string createdOn { get; set; }
        public string createdBy { get; set; }
        public string CreditCardTypeName { get; set; }
        public double unitPrice { get; set; }
        public double totalPrice { get; set; }
        public int quantity { get; set; }
        public int cardCategory { get; set; }
        public int ReservationId { get; set; }
        public int UserId { get; set; }
    }
}