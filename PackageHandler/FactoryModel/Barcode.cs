using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageHandler.FactoryModel
{
    class Barcode
    {
        public double PackageWeight { get; set; }
        public double Cost { get; set; }
        public bool MustPress { get; set; }

        public Barcode()
        {
            PackageWeight = Form1.rand.NextDouble() * (20 - 1) + 1;
            Cost = Form1.rand.Next(0, 10000);

            var prob = 80;
            MustPress = Form1.rand.Next(0, 100) <= prob;
        }
    }
}
