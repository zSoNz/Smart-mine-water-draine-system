using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Models;

namespace ConsoleApp1.Controllers {

    class FilingData {

        public float time;
        public bool isGlobal;

        public float littersIncoming;

        public FilingData(float time, float income, bool isGlobal) {
            this.time = time;
            this.littersIncoming = income;
            this.isGlobal = isGlobal;
        }

    }

    class FillingController {

        public float time;
        public float litters;
        public bool isGlobal;

        public Time inWork = new Time();

        public TankController controller;

        //Костыль, ПРАСТИТЕ
        public FillingController(FilingData data, TankController controller) {
            this.time = data.time;
            this.litters = data.littersIncoming;
            this.isGlobal = data.isGlobal;

            this.controller = controller;
        }

        public void processFilling() {
            while(this.time * 60 >= this.inWork.minutes) {
                
                if (GlobalTime.minute % 60 == 0 && this.controller.bounds.timeHanlder != null)
                {
                    this.controller.bounds.timeHanlder(GlobalTime.hour);
                }

                this.inWork.minutes++;
                GlobalTime.minute++;

                if (!this.isGlobal)
                {
                    this.controller.bounds.calculateFakeBounds();
                }
                else
                {
                    this.controller.bounds.calculateBoundsWithGlobalTime();
                }

                controller.fillWith(this.litters);
            }
        }

    }

}
