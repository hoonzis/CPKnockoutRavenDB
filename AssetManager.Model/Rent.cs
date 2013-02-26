using System;
using System.Collections.Generic;



namespace AssetManager.Model
{
    public class Rent : PayedObligation
    {
        public Regularity Regularity { get; set;} 
        public decimal Deposit { get; set; }
        public bool DepositPayed { get; set; }
        public bool DepositReturned { get; set; }
        
    }
}