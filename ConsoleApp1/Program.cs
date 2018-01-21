using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Models;
using ConsoleApp1.Controllers;
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp1 {
 
    class Program {
        
        static void Main(string[] args) {

            var bounds = new Bounds(volume: 40);
           
            var tank = new TankController(bounds);

            var day = 24;
            var incom = 0.08f;

            var data = new FilingData(day * 20, incom, true);

            var controller = new FillingController(data, tank);

            var boundsq = new Bounds(volume: 40);

            var tankq = new TankController(boundsq);
            var dataq = new FilingData(day * 20, incom, false);
            var controllerq = new FillingController(dataq, tankq);

            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workBook;
            Excel.Worksheet workSheet;

            workBook = excelApp.Workbooks.Add();
            workSheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);

            printToExcel(workSheet, controller, 1);
            printToExcel(workSheet, controllerq, 8);

            excelApp.Visible = true;
            excelApp.UserControl = true;

        }

        static void printToExcel(Excel.Worksheet workSheet, FillingController controller, int startPos) {
            startPos++;
            var first = controller.controller.pumps.value.First();
            var second = controller.controller.pumps.value.Last();

            var x = 3;

            workSheet.Cells[2, startPos] = "Время";

            workSheet.Cells[1, startPos + 1] = "Литры";

            workSheet.Cells[1, startPos + 3] = "Время работы насоса";

            workSheet.Cells[2, startPos + 1] = "Общий";

            workSheet.Cells[2, startPos + 2] = "Первый";
            workSheet.Cells[2, startPos + 3] = "Второй"; // Открываем созданный excel-файл

            var prevValue1 = 0f;

            var prevValue2 = 0f;

            Bounds.TimeHanlder timeHandler = delegate (float time) {
                var dayTime = "";

                if (GlobalTime.dayTime() == DayTime.half) {
                    dayTime = "Half";
                }
                if (GlobalTime.dayTime() == DayTime.hight)
                {
                    dayTime = "High";
                }
                if (GlobalTime.dayTime() == DayTime.night)
                {
                    dayTime = "Night";
            }
                switch (GlobalTime.dayTime())
                {
                    case DayTime.behindHight:
                        dayTime = "behindHight";
                        break;
                    case DayTime.hight:
                        dayTime = "hight";
                        break;
                    case DayTime.night:
                        dayTime = "night";
                        break;
                    case DayTime.afterNight:
                        dayTime = "afterNight";
                        break;
                    case DayTime.half:
                        dayTime = "half";
                        break;
                }
            workSheet.Cells[x, 1] = dayTime;

                var value = time == 0 ? 24 : time;
                
                
                /*
                Console.WriteLine("==========" + value + "==========");

                Console.WriteLine("Hours Pump1: " + first.time.hour);
                Console.WriteLine("Hours Pump2: " + second.time.hour);
                Console.WriteLine("Water: " + controller.controller.water);
                Console.WriteLine();
                */

                workSheet.Cells[x, startPos] = value;

                workSheet.Cells[x, startPos + 1] = controller.controller.water;

                var checker = x;

                workSheet.Cells[x, startPos + 2] = (prevValue1 <= 0
                     ? first.time.minutes
                     : first.time.minutes - prevValue1) / 60;                    
                
                prevValue1 = first.time.minutes;

                workSheet.Cells[x, startPos + 3] = (prevValue1 <= 0
                     ? second.time.minutes
                     : second.time.minutes - prevValue2) / 60;
                
                prevValue2 = second.time.minutes;

                x++;
            };

            controller.controller.bounds.timeHanlder = timeHandler;

            Bounds.MidnightHanlder mindanightHandler = delegate ()
            {

            };

            controller.controller.bounds.midnightHanlder = mindanightHandler;

            controller.processFilling();

            workSheet.Cells[x + 1, startPos + 1] = "Ночь";
            workSheet.Cells[x + 1, startPos + 2] = "Полу-пик";
            workSheet.Cells[x + 1, startPos + 3] = "Пик";

            workSheet.Cells[x + 2, startPos + 1] = first.time.night + first.time.afterNight;
            workSheet.Cells[x + 3, startPos + 1] = second.time.night + second.time.afterNight;

            workSheet.Cells[x + 2, startPos + 2] = first.time.half + first.time.preHigh;
            workSheet.Cells[x + 3, startPos + 2] = second.time.half + second.time.preHigh;

            workSheet.Cells[x + 2, startPos + 3] = first.time.high;
            workSheet.Cells[x + 3, startPos + 3] = second.time.high;
        }
        /*

        Console.WriteLine("__________FIRST_Pump_____________");

                
                Console.WriteLine(first.night23_7);
                Console.WriteLine(first.half7_8);
                Console.WriteLine(first.high8_11);
                Console.WriteLine(first.half11_20);
                Console.WriteLine(first.high20_22);
                Console.WriteLine(first.half22_23);


                Console.WriteLine("__________SECOND_Pump_____________");

                Console.WriteLine(second.night23_7);
                Console.WriteLine(second.half7_8);
                Console.WriteLine(second.high8_11);
                Console.WriteLine(second.half11_20);
                Console.WriteLine(second.high20_22);
                Console.WriteLine(second.half22_23);

                Console.WriteLine("=====================");

                Console.WriteLine();
               */

    }
}
