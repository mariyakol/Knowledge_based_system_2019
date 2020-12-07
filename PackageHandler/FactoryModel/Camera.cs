using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PackageHandler.FactoryModel
{
    class Camera
    {
        public static Photo TakePhoto(Package package)
        {
            Form1.logger.Info("Активация камеры.");
            var photo = new Photo();
            //Thread.Sleep(200);
            Form1.logger.Info("Фото сделано.");

            return photo;
        }
    }
}
