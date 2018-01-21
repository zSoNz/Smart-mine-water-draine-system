using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models {

    class Time {
        public float night = 0;
        public float half = 0;
        public float high = 0;

        public float afterNight = 0;
        public float preHigh = 0;

        public float hour {
            get { return (minutes / 60)%24; }
        }

        public float minutes {
            set
            {
                _minutes = value;

                switch (GlobalTime.dayTime())
                {
                    case DayTime.behindHight:
                        this.preHigh++;
                        break;
                    case DayTime.afterNight:
                        this.afterNight++;
                        break;
                    case DayTime.half:
                        this.half++;
                        break;
                    case DayTime.hight:
                        this.high++;
                        break;
                    case DayTime.night:
                        this.night++;
                        break;
                }
            }

            get {
                return _minutes;
            }
        }

        private float _minutes;

        public DayTime dayTime() {

            if (hour < 6 || hour <= 24 && hour >= 23)
            {
                return DayTime.night;
            }

            if (hour >= 6 && hour < 7)
            {
                return DayTime.afterNight;
            }

            if (
                (hour >= 7 && hour < 8)
                    || (hour >= 19 && hour < 20)
                )
            {
                return DayTime.behindHight;
            }

            if (
                (hour >= 11 && hour < 19)
                  || (hour >= 22 && hour < 23)
                )
            {
                return DayTime.half;
            }

            if (
                (hour >= 8 && hour < 11)
                  || (hour >= 20 && hour < 22)
                )
            {
                return DayTime.hight;
            }

            return DayTime.half;
        }

    }

}
