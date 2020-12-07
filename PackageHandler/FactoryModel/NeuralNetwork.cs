using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PackageHandler.FactoryModel
{
    class NeuralNetwork
    {
        public static Mark? RecognizeMarks(Photo photo)
        {
            Form1.logger.Info("Активация нейросети.");
            //Thread.Sleep(200);
            Form1.logger.Info("Распознавание маркеров...");
            //Thread.Sleep(200);
            Form1.logger.Info("Распознавание маркеров...");
            //Thread.Sleep(200);
            Form1.logger.Info("Распознавание маркеров...");
            //Thread.Sleep(200);
            
            if (photo.MarksOnPhoto.HasValue)
            {
                var mark = photo.MarksOnPhoto.ToString();

                Form1.logger.Info($"Маркеры распознаны: {mark}");
            }
            else
            {
                Form1.logger.Info($"Маркеры не найдены");
            }

            return photo.MarksOnPhoto;
        }
    }
}
