using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PackageHandler.FactoryModel
{
    class Press
    {
        public static void DoPress(Package package)
        {
            Form1.logger.Info("Активация пресса");
            Thread.Sleep(300);
            Form1.logger.Info("Пресс");
            package.IsPressed = true;
        }

        public static bool IsDamagedAfterPress(Package package, Mark? marks, double weight)
        {
            DoPress(package);
            Thread.Sleep(300);
            if (marks.HasValue)
            {
                var prob = GetProbability(marks);
                package.IsDamaged = Form1.rand.Next(0, 100) <= prob;
            }
            else
            {
                var prob = GetProbabilityOfWeight(weight);
                package.IsDamaged = Form1.rand.Next(0, 100) <= prob;
            }
            Form1.logger.Info($"Пресс {(package.IsDamaged ? "" : "не")} повредил посылку");

            return package.IsDamaged;
        }

        private static double GetProbability(Mark? mark)
        {
            double result = 0;

            switch (mark)
            {
                case Mark.DoNotPress:
                    result = 70;
                    break;
                case Mark.Fragile:
                    result = 80;
                    break;
                case Mark.ScaredOfMoisture:
                    result = 10;
                    break;
                case Mark.ScaredOfSun:
                    result = 10;
                    break;
                case Mark.TurnNotAllowed:
                    result = 25;
                    break;
            }

            return result;
        }

        private static double GetProbabilityOfWeight(double weight)
        {
            if  (weight < 5)
            {
                return 65;
            }
            if (weight < 10)
            {
                return 25;
            }
            if (weight < 15)
            {
                return 10;
            }
            if (weight < 20)
            {
                return 5;
            }
            return 0;
        }
    }
}
