using System;
using System.Collections.Generic;

namespace AssetManager.Model
{
    public class Asset
    {
        public Asset()
        {
            Charges = new List<Charge>();
            Rents = new List<Rent>();
        }

        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int LastChargeId { get; set; }
        public int LastRentId { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String Country { get; set; }
        public String ZipCode {get;set;}
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double InitialCosts { get; set; }
        public List<Rent> Rents { get; set; }
        public List<Charge> Charges { get; set; }
        public double Ebit { get; set; }
        public double Size { get; set; }
        public double PMS { get; set; }
        public double Price { get; set; }
        public double IncomeTax { get; set; }
        public double InterestRate { get; set; }

        public int GenerateChargeId()
        {
            return ++LastChargeId;
        }

        public int GenerateRentId()
        {
            return ++LastRentId;
        }
    }
}