using System;
using System.Threading;
using System.Threading.Tasks;

namespace EventTest
{
    public class MyEventArgs : EventArgs
    {
        public string EventType { get; set; }
        public int Payload { get; set; }
    }

    public class MyEventSource
    {
        public event EventHandler<MyEventArgs> MyEvent;
        private int _counter = 0;

        public void RunTask(CancellationToken token)
        {
            Task.Run(() => RunThread(token));
        }

        private void RunThread(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                Thread.Sleep(1000);
                _counter++;
                OnMyEvent(new MyEventArgs() { EventType = "inc", Payload = _counter });
            }
        }

        public void OnMyEvent(MyEventArgs args)
        {
            EventHandler<MyEventArgs> handler = MyEvent;
            handler?.Invoke(this, args);
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            static void OnMyEvent1(object sender, MyEventArgs a)
            {
                Console.WriteLine($"MyEvent1 args = {a.EventType}, {a.Payload}");
            }

            static void OnMyEvent2(object sender, MyEventArgs a)
            {
                Console.WriteLine($"MyEvent2 args = {a.EventType}, {a.Payload}");
            }

            MyEventSource mes = new MyEventSource();
            mes.MyEvent += OnMyEvent1;
            mes.MyEvent += OnMyEvent2;

            CancellationTokenSource cts = new CancellationTokenSource();
            mes.RunTask(cts.Token);

            Console.ReadLine();
            Console.WriteLine("cancelled");
            cts.Cancel();


            Console.ReadLine();

        }
    }
}
