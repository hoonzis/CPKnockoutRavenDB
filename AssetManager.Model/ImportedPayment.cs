using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssetManager.Model
{
    public class ImportedPayment
    {
        public String AccountNumber { get; set; }
        public DateTime Date {get;set;}
        public Decimal Amount { get; set; }
        public PaymentTime PaymentTime { get; set; }
        public String CounterpartyName { get; set; }
        public String Notes { get; set; }
    }
}
