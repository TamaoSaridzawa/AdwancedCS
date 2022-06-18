using System;
using System.Collections.Generic;

namespace CleanCode_Task10
{
    class Program
    {
        static void Main(string[] args)
        {
            var databasePayment = new DatabasePaymentSystems();

            var orderForm = new OrderForm();

            var paymentHandler = new PaymentHandler();

            string systemId = orderForm.ShowForm();

            IPaymentSystem paymentSystem = databasePayment.TryIdentifySystem(systemId);

            if (paymentSystem != null)
            {
                paymentHandler.ShowPaymentResult(paymentSystem);
            }

            Console.ReadKey();
        }
    }

    public interface IPaymentSystem
    {
        public string GetIdSystem();
        public  void Pay();
    }

    abstract public class PaymentSystem : IPaymentSystem
    {
        private string _id;

        public PaymentSystem(string id)
        {
            _id = id;
        }

        public abstract void Pay();

        public string GetIdSystem()
        {
            return _id;
        }
    }

    class PaymentSystemQiwi : PaymentSystem
    {
        public PaymentSystemQiwi(string id) : base(id) { }

        public override void Pay()
        {
            Console.WriteLine("Перевод на страницу QIWI...");
        }
    }
    class PaymentSystemWebMoney : PaymentSystem
    {
        public PaymentSystemWebMoney(string id) : base(id) { }

        public override void Pay()
        {
            Console.WriteLine("Вызов API WebMoney...");
        }
    }

    class PaymentSystemCard : PaymentSystem
    {
        public PaymentSystemCard(string id) : base(id) { }

        public override void Pay()
        {
            Console.WriteLine("Вызов API банка эмитера карты Card...");
        }
    }

    class DatabasePaymentSystems
    {
        private List<IPaymentSystem> _paymentSystems = new List<IPaymentSystem>();

        public DatabasePaymentSystems()
        {
            AddSystem();
        }

        public IPaymentSystem TryIdentifySystem(string systemId)
        {
            for (int i = 0; i < _paymentSystems.Count; i++)
            {
                if (systemId == _paymentSystems[i].GetIdSystem())
                {
                    return _paymentSystems[i];
                }
            }

            Console.WriteLine("Такая система платежей не поддерживается базой данных.");

            return null;
        }

        private void AddSystem()
        {
            _paymentSystems.Add(new PaymentSystemQiwi("QIWI"));
            _paymentSystems.Add(new PaymentSystemWebMoney("WebMoney"));
            _paymentSystems.Add(new PaymentSystemCard("Card"));
        }
    }

    public class OrderForm
    {
        public string ShowForm()
        {
            Console.WriteLine("Мы принимаем: QIWI, WebMoney, Card");

            //симуляция веб интерфейса
            Console.WriteLine("Какое системой вы хотите совершить оплату?");
            return Console.ReadLine();
        }
    }

    public class PaymentHandler
    {
        public void ShowPaymentResult(IPaymentSystem system)
        {
            system.Pay();

            Console.WriteLine("Оплата прошла успешно!");
        }
    }
}
