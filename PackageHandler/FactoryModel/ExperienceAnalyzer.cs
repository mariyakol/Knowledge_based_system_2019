using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageHandler.FactoryModel
{
    class ExperienceAnalyzer
    {
        //Обнаруживает закономерности в данных от камеры и весов
        //Анализирует прошлый опыт. 

        //
        //Приходит к выводу: прессовать/нет. 
        //

        //А если нет данных в базе знаний о том, какая
        //посылка хрупкая а какая нет, то провести эксперимент
        //
               
        public static bool CanPressIfEnoughKnowledge(double weight, Mark? mark, out bool canPress) //На вход идет что-то от нейронной сети
        {
            Form1.logger.Info("Активация анализатора опыта");
            Form1.logger.Info("Проверка хватает ли знаний");
            var pressMark = false;
            var pressWeight = false;

            var isEnough = mark != null ? KBSystem.IfEnoughMark(mark, out pressMark) : KBSystem.IfEnoughWeight(weight, out pressWeight);
     
            if (isEnough) //Да
            {                
                canPress = mark != null ? pressMark : pressWeight;
                Form1.logger.Info($"Знаний на основе {(mark.HasValue ? "маркера" : "веса" )} достаточно. Решение: {(canPress ? "прессуем" : "не прессуем")}");

                return true;

            }
            Form1.logger.Warn("Знаний недостаточно!");
            canPress = false;
            return false;            
        }

    }
}
