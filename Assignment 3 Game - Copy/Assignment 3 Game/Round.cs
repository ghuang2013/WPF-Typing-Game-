using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3_Game {
    public class Round {
        static int number = 0;
        double segundo;

        public Round(double segundo) {
            number++;
            this.segundo = segundo;
        }

        public int Number {
            get { return number; }
        }

        public double Segundo {
            get { return segundo; }
        }
    }
}
