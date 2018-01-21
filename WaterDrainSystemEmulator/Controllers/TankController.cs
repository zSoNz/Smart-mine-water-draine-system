using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Models;
using System.Linq.Expressions;

namespace ConsoleApp1.Controllers {

    class TankController {

        public Bounds bounds;
        public Pumps pumps;
        
        private bool firstMode = false;
        private bool secondMode = false;

        public float water = 0;

        public TankController(Bounds bounds) {
            Pump[] values = { new Pump(), new Pump() };

            this.bounds = bounds;
            this.pumps = new Pumps(values);
        }

        public void fillWith(float water) {
            this.water += water;
            
            if (this.bounds.pumpWhile >= this.water) {
                this.secondMode = false;
                this.firstMode = false;
            }
            
            if (this.water >= this.bounds.upper || this.secondMode) {
                foreach (Pump pump in this.pumps.value) {
                    pump.fillWith(water: pump.speed);
                    this.water -= pump.speed;

                    this.secondMode = true;
                }
            }

            if ((this.water >= this.bounds.lower || this.firstMode) && !this.secondMode) {
                var pump = this.pumps.value.First();

                pump.fillWith(pump.speed);

                this.water -= pump.speed;

                this.firstMode = true;
            }

        }

    }

}
