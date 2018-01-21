using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{

    class Bounds
    {

        delegate void Counter(float upper, float lower, float low);

        public delegate void MidnightHanlder();
        public MidnightHanlder midnightHanlder;

        public delegate void TimeHanlder(float hour);
        public TimeHanlder timeHanlder;

        public int volume;

        //Defaluts values for bounds setting to 24:00
        public float upper;
        public float lower;

        public float pumpWhile = 2;

        public Bounds(int volume)
        {
            this.volume = volume;

            this.calculateBoundsWithGlobalTime();
        }

        public void calculateBoundsWithGlobalTime()
        {
            var time = GlobalTime.dayTime();
            
            this.calculateBoundsWith(time);
        }

        public void calculateBoundsWithLocal(Time time)
        {

            this.calculateBoundsWith(time.dayTime());

        }

        public void calculateFakeBounds()
        {
            Counter calculate = delegate (float x, float y, float z)
            {
                this.upper = volume * x;
                this.lower = volume * y;

                this.pumpWhile = volume * z;
            };
            
            calculate(0.9f, 0.8f, 0.1f);

        }

        private void calculateBoundsWith(DayTime time)
        {
            Counter calculate = delegate (float x, float y, float z)
            {
                this.upper = volume * x;
                this.lower = volume * y;

                this.pumpWhile = volume * z;
            };

            switch (time)
            {
                case DayTime.behindHight:
                    calculate(0.6f, 0.11f, 0.5f);
                    break;
                case DayTime.hight:
                    calculate(0.9f, 0.8f, 0.69f);
                    break;
                case DayTime.night:
                    calculate(0.8f, 0.6f, 0.05f);
                    break;
                case DayTime.afterNight:
                    calculate(0.9f, 0.10f, 0.05f);
                    break;
                case DayTime.half:
                    calculate(0.9f, 0.7f, 0.28f);
                    break;
            }

        }

    }

}


