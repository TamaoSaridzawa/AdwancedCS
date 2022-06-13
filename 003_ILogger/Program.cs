using System;
using System.IO;

namespace _003_ILogger
{
    class Program
    {
        static void Main(string[] args)
        {
            Pathfinder pathfinder = new Pathfinder(new ConsoleLogWritter());
            Pathfinder pathfinderTwo = new Pathfinder(new SecureConsoleLogWritter());
            Pathfinder pathfinderFree = new Pathfinder(new FileLogWritter());
            Pathfinder pathfinderFoo = new Pathfinder(new SecureFileLogWritter());
            Pathfinder pathfinderFive = new Pathfinder(new ConsoleLogWritterAndSecureFile());

            pathfinder.Find("Пишет в консоль");
            pathfinderTwo.Find("Пишет в консоль по пятницам");
            pathfinderFree.Find("Пишет в файл");
            pathfinderFoo.Find("Пишет в файл по пятницам");
            pathfinderFive.Find("Пишет в консоль, а по пятницам еще и в файл");

            Console.ReadKey();
        }
    }

    class CheckDayRecorder
    {
        public bool ChekData()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                return true;
            }
            return false;
        }
    }

    abstract class Logger 
    {
        protected CheckDayRecorder _checkDayRecorder = new CheckDayRecorder();

        public abstract void WritteError(string message);
    }

    class ConsoleLogWritter : Logger                   
    {
        public override void WritteError(string message)
        {
            WritteConsole(message);
        }

        protected void WritteConsole(string message)
        {
            Console.WriteLine(message);
        }
    }

    class SecureConsoleLogWritter : ConsoleLogWritter        
    {
        public override void WritteError(string message)
        {
            if (_checkDayRecorder.ChekData())
            {
                base.WritteConsole(message);
            }
        }
    }

    class FileLogWritter : SecureConsoleLogWritter 
    {
        public override void WritteError(string message)
        {
            WritteFail(message);
        }

        protected void WritteFail(string message)
        {
            File.WriteAllText("log.txt", message);
        }
    }

    class SecureFileLogWritter : FileLogWritter
    {
        public override void WritteError(string message)
        {
            if (_checkDayRecorder.ChekData())
            {
                base.WritteFail(message);
            }
        }
    }

    class ConsoleLogWritterAndSecureFile : SecureFileLogWritter
    {
        public override void WritteError(string message)
        {
            base.WritteConsole(message);

            if (_checkDayRecorder.ChekData())
            {
                base.WritteFail(message);
            }
        }       
    }

    class Pathfinder
    {
        private Logger _loger;

        public Pathfinder(Logger logger)
        {
            _loger = logger;
        }

        public void Find( string message)
        {
            _loger.WritteError(message);
        }
    }
}
