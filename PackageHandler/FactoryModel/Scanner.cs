using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PackageHandler.FactoryModel
{
    class Scanner
    {
        public static bool TryScan(Package package, out bool canPress)
        {
            Form1.logger.Info("Активация сканера...");
            //Thread.Sleep(200);
            Form1.logger.Info("Сканирование штрихкода...");
            var barcode = package.Barcode;
            //Thread.Sleep(300);
            if (barcode == null)
            {
                Form1.logger.Error("Штрихкод отсутствует");
                canPress = false;
                return false;
            }

            Form1.logger.Info($"Штрихкод отсканирован. Press = {barcode.MustPress}");
            canPress = barcode.MustPress;
            return true;
        }
    }
}
