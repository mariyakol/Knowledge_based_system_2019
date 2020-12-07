using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PackageHandler.FactoryModel
{
    class Scales
    {
        public static double GetWeight(Package package)
        {
            Form1.logger.Info("Активация весов.");
            double weight = Form1.rand.NextDouble() * (20 - 1) + 1;
            //Thread.Sleep(300);
            Form1.logger.Info($"Посылка взвешена. Вес = {weight}");
            return weight;
        }
    }
}
