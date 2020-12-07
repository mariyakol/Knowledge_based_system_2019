using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageHandler.FactoryModel
{
    class Photo
    {
        public Mark? MarksOnPhoto { get; set; }
        public Photo()
        {
            this.MarksOnPhoto = GenerateMarks();
        }

        private Mark? GenerateMarks()
        {
            var index = Form1.rand.Next(-1, 5);
            var list = new List<Mark> { Mark.DoNotPress, Mark.Fragile, 
                                         Mark.ScaredOfMoisture, Mark.ScaredOfSun,
                                         Mark.TurnNotAllowed };

            //Shuffle(list);
            if (index == -1)
            {
                return null;
            }

            return list[index];
        }

        //private void Shuffle<T>(IList<T> list)
        //{
        //    int n = list.Count;
        //    while (n > 1)
        //    {
        //        n--;
        //        int k = Form1.rand.Next(n + 1);
        //        T value = list[k];
        //        list[k] = list[n];
        //        list[n] = value;
        //    }
        //}
    }
}
