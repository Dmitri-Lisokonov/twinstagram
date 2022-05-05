namespace Shared.Messaging
{
    public class PublishQueue
    {
        public QueueName Name { get; }

        public PublishQueue(QueueName name)
        {
            Name = name;
        }
    }
}
