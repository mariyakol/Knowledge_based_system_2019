using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageHandler.FactoryModel
{
    class Package
    {
        public bool IsPressed { get; set; }
        public bool IsDamaged { get; set; }
        public Barcode Barcode { get; set; }

        public Package()
        {
            var prob = 60;

            if (Form1.rand.Next(0, 100) <= prob)
            {
                this.Barcode = new Barcode();
            }
        }
    }
}
