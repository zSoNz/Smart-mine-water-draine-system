using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models {

    class Pumps {

        public Pump[] value;

        public Pumps(Pump[] pumps) {
            this.value = pumps;
        }

        public void process(int pump, int water) {
            this.value[pump].fillWith(water);
        }

        public float workTime() {
            return this.value.First().time.minutes;
        }
        
    }

}
