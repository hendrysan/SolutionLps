namespace Solution.LogicalCode
{
    public class MemoryLeakSecond
    {
        public delegate void EventHandler(object sender, EventArgs e);

        public void Main()
        {
            var publisher = new EventPublisher();

            //while (true)
            //{
            var subscriber = new EventSubscriber(publisher);
            // do something with the publisher and subscriber objects
            publisher.RaiseEvent();


            //}

        }

        public class EventPublisher
        {
            public event EventHandler MyEvent;
            public void RaiseEvent()
            {
                MyEvent?.Invoke(this, EventArgs.Empty);
            }
        }
        public class EventSubscriber
        {
            public EventSubscriber(EventPublisher publisher)
            {
                publisher.MyEvent += OnMyEvent;
            }

            private void OnMyEvent(object sender, EventArgs e)
            {
                Console.WriteLine("MyEvent raised");
            }
        }
    }
}
