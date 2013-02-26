using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssetManager.Model
{
    public class FinancialSummary
    {
        public decimal Ebit { get; set; }
        public double Roi { get; set; }
        public decimal Charges { get; set; }
        public decimal Earnings { get; set; }
    }
}
