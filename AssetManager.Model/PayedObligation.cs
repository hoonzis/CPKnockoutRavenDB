using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManager.Model
{
    public class PayedObligation
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public short PaymentDay { get; set; }
        public String AccountNumber { get; set; }
        public Decimal Amount { get; set; }
        public int Unit { get; set; }
        public String Notes { get; set; }
        public DateTime? End { get; set; }
        public DateTime? Start { get; set; }

        public PayedObligation()
        {
            Payments = new List<Payment>();
        }
        public int LastPaymentId { get; set; }
        
        public List<Payment> Payments { get; set; }

        public int GeneratePaymentId()
        {
            return ++LastPaymentId;
        }
    }
}
