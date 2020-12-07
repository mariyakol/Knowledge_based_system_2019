using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageHandler.FactoryModel
{
    class ExperimentComponent
    {
        public static bool TryToPress(Package package, Mark? marks, double weight)
        {
            Form1.logger.Info("Активация компонента экспериментов");
            return Press.IsDamagedAfterPress(package, marks, weight);
        }
    }
}
