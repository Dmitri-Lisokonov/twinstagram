namespace Shared.DTO.RabbitMQ
{
    public class PublishQueue
    {
        public string Name { get; }

        public PublishQueue(string name)
        {
            Name = name;
        }
    }
}
