using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp1.Models
{
    class Pump {
        
        public float litters;

        public Time time;

        public float speed = 0.9f;

        public Pump() {
            this.time = new Time();
        }

        public void fillWith(float water) {
            this.litters += water;
            
            this.time.minutes ++;
        }

    }
}
