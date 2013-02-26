using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManager.Model
{
    public class ObligationYear
    {
        public int Year { get; set; }
        public String Name { get; set; }
        public List<Payment> Payments { get; set; }
        public int Factor { get; set; }

        public ObligationYear()
        {
            Payments = new List<Payment>();
        }
        public void Add(Payment p){
            Payments.Add(p);
        }
    }
}
