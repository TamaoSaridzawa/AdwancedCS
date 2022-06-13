using System;
using System.IO;

namespace Logger
{
    class Program
    {
        static void Main(string[] args)
        {
            Pathfinder pathfinder = new Pathfinder(new ConsoleLogWritter());
            Pathfinder pathfinderTwo = new Pathfinder(new SecureConsoleLogWritter());
            Pathfinder pathfinderFree = new Pathfinder(new FileLogWritter());
            Pathfinder pathfinderFour = new Pathfinder(new SecureFileLogWritter());
            Pathfinder pathfinderFive = new Pathfinder(new ConsoleLogWritterAndSecureFile());

            pathfinder.Find("Error");
            pathfinderTwo.Find("Error");
            pathfinderFree.Find("Error");
            pathfinderFour.Find("Error");
            pathfinderFive.Find("Error");

            Console.ReadKey();
        }
    }

    class RecordDateChecker
    {
        public bool IsRecord()
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
        protected RecordDateChecker RecordDateChecker = new RecordDateChecker();

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
            if (RecordDateChecker.IsRecord())
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
            if (RecordDateChecker.IsRecord())
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

            if (RecordDateChecker.IsRecord())
            {
                base.WritteFail(message);
            }
        }
    }

    class Pathfinder
    {
        private Logger _logger;

        public Pathfinder(Logger logger)
        {
            _logger = logger;
        }

        public void Find(string message)
        {
            _logger.WritteError(message);
        }
    }
}
