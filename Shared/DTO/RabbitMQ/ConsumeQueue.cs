namespace Shared.DTO.RabbitMQ
{
    public class ConsumeQueue
    {
        public string Name { get; }

        public ConsumeQueue(string name)
        {
            Name = name;
        }
    }
}
