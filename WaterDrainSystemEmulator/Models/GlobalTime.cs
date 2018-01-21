using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models {

    enum DayTime {
        night,
        afterNight,
        half,
        hight,
        behindHight
    }

    class GlobalTime {

        public static float hour {
            get {
                return (minute / 60) % 24;
            }
        }

        public static float minute = 0;

        public static DayTime dayTime() {

            if (hour < 6 || hour <= 24 && hour >= 23)
            {
                return DayTime.night;
            }

            if (hour >= 6 && hour < 7) {
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

