using System;
using System.Security.Cryptography;
using System.Text;

namespace PaymentSystems
{
    class Program
    {
        static void Main(string[] args)
        {
            Order order = new Order(10, 12000);

            IPaymentSystem paymentSystemMd5 = new PaymentSystem1();
            Console.WriteLine(paymentSystemMd5.GetPayingLink(order));

            IPaymentSystem paymentSystem2 = new PaymentSystem2();
            Console.WriteLine(paymentSystem2.GetPayingLink(order));

            IPaymentSystem paymentSystem3 = new PaymentSystem3();
            Console.WriteLine(paymentSystem3.GetPayingLink(order));

            Console.ReadKey();

        }
    }

    public class Order
    {
        public readonly int Id;
        public readonly int Amount;

        public Order(int id, int amount) => (Id, Amount) = (id, amount);
    }

    public interface IPaymentSystem
    {
        public abstract string GetPayingLink(Order order);
    }

    public class PaymentSystem1 : IPaymentSystem
    {
        public virtual string GetPayingLink(Order order)
        {
            return GetHashMd5(order);
        }

       private string GetHashMd5(Order order)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(order.Id.ToString()));

            return Convert.ToBase64String(hash);
        }
    }

    public class PaymentSystem2 : PaymentSystem1
    {
        public override string GetPayingLink(Order order)
        {
            return base.GetPayingLink(order) + order.Amount;
        }
    }

    public class PaymentSystem3 : IPaymentSystem
    {
        private float _secretKey = 114;

        public string GetPayingLink(Order order)
        {
           return GetHashSha1(order) + order.Id + _secretKey;
        }

        private string GetHashSha1(Order order)
        {
            SHA1 sha1 = SHA1.Create();
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(order.Amount.ToString()));
            return Convert.ToBase64String(hash);
        }
    }
}
