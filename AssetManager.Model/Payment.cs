using System;
using System.Collections.Generic;


namespace AssetManager.Model
{
    public class Payment
    {
        public int Id { get; set; }
        
        //The exact date of payment
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public PaymentTime PaymentTime { get; set; }
        public bool Payed { get; set; }
    }
}