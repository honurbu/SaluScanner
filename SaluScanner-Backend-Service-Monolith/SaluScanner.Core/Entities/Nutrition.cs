using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Core.Entities
{
    public class Nutrition : IEntity
    {
        public int Id { get; set; }
        
        // gram in 100 grams
        public float Carbonhydrate { get; set; }
        public float Fat { get; set; }
        public float Protein { get; set; }
        public float Calori { get; set; }

        // miligram in 100 grams
        public float VitamineA { get; set; }
        public float VitamineB { get; set; }
        public float VitamineC { get; set; }
        public float VitamineD { get; set; }
        public float VitamineE { get; set; }
        public float VitamineK { get; set; }
    }
}
