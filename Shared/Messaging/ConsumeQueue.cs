namespace Shared.Messaging
{
    public class ConsumeQueue
    {
        public QueueName Name { get; }

        public ConsumeQueue(QueueName name)
        {
            Name = name;
        }
    }
}
