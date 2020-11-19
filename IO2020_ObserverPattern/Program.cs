using System;
using System.Collections.Generic;

namespace auctionObserverC
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer cus1 = new Customer("Jan");
            Customer cus2 = new Customer("Tom");
            Customer cus3 = new Customer("Ben");
            Auction auction = new Auction("obraz");

            auction.Attach(cus1);
            auction.Attach(cus2);
            auction.Attach(cus2);

            cus1.MakeOffer(auction, 150.0);
            cus2.MakeOffer(auction, 200.0);

            auction.Detach(cus2);
            auction.Detach(cus3);

            cus1.MakeOffer(auction, 300.0);
        }
    }

    public interface IObserver
    {
        void Update(Auction auction);
        //void Update(Auction auction, double price);
    }

    public abstract class Subject
    {
        protected List<IObserver> _observerList = new List<IObserver>();


        public void Attach(IObserver observer)
        {
            if (!this._observerList.Contains(observer))
                this._observerList.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            if (this._observerList.Contains(observer))
                this._observerList.Remove(observer);
        }

        abstract protected void Notify();

    }

    public class Customer : IObserver
    {
        private String _name;

        public Customer(String _name)
        {
            this._name = _name;
        }

        public void MakeOffer(Auction auction, double price)
        {
            auction.SetPrice(price);
        }

        public void Update(Auction auction)
        {
            double price = auction.GetPrice();
            Console.WriteLine("Customer " + this._name + " was notified about " + auction.GetObject() + " auction price change to: " + price);
        }
        //public void Update(Auction auction, double price)
        //{
        //    Console.WriteLine("Customer " + this._name + " was notified about " + auction.GetObject() + " auction price change to: " + price);
        //}
    }

    public class Auction : Subject
    {
        private String _object;
        private double _price;

        public Auction(String _object)
        {
            this._object = _object;
        }

        public void SetPrice(double price)
        {
            this._price = price;
            Notify();
        }

        public String GetObject()
        {
            return _object;
        }

        public double GetPrice()
        {
            return this._price;
        }

        protected override void Notify()
        {
            foreach (IObserver observer in this._observerList)
            {
                observer.Update(this);
                //observer.Update(this, this._price);
            }
        }
    }


}
