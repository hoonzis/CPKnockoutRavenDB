using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AssetManager.Model;

namespace AssetManager.Dto.Wrappers
{
    public class PaymentUpdateDto
    {

        public int AssetID { get; set; }

        public int ParentID { get; set; }

        public string Type { get; set; }

        public Payment Payment { get; set; }
    }
}
