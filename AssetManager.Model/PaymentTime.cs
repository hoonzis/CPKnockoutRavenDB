using System;
using System.Collections.Generic;



namespace AssetManager.Model
{
    public class PaymentTime
    {
        public int Year { get; set; }
        public int Month { get; set; }


        //We need methods to compare two payments times, in order to be able to use
        //payment time as key inside dictionaries. Could also implement IEquatable<T>
        //but don't have to

        public override int GetHashCode()
        {
            return Int32.Parse(Year.ToString() + Month.ToString());
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var ptime = obj as PaymentTime;
            return Year == ptime.Year && Month == ptime.Month;
        }
        public static bool operator ==(PaymentTime t1, PaymentTime t2)
        {
            return t1.Equals(t2);
        }

        public static bool operator !=(PaymentTime t1, PaymentTime t2)
        {
            return !t1.Equals(t2);
        }
    }
}