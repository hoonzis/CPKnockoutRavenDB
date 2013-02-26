using System;
using System.Collections.Generic;


namespace AssetManager.Model
{
    public class Charge : PayedObligation
    {
        public Charge() : base() { }

        public String Counterparty { get; set; }
        public String Type { get; set; }
        public Boolean Automatic { get; set; }
        public Regularity Regularity { get; set; }
    }
}