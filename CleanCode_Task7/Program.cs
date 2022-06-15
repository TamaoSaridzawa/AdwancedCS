using System;

namespace CleanCode_Task7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public static void CreateNewObject()
        {
            //Создание объекта на карте
        }

        public static void SetRandomValue()
        {
            _chance = Random.Range(0, 100);
        }

        public static int GetCalculatedSalary(int hoursWorked)
        {
            return _hourlyRate * hoursWorked;
        }
    }
}
