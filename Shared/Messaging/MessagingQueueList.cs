namespace Shared.Messaging
{
    public class MessagingQueueList
    {
        public List<ConsumeQueue> ConsumeQueues { get; set; }

        public List<PublishQueue> PublishQueues { get; set; }

        public MessagingQueueList()
        {
            ConsumeQueues = new List<ConsumeQueue>();
            PublishQueues = new List<PublishQueue>();
        }

        public void AddConsumeQueue(ConsumeQueue queue)
        {
            ConsumeQueues.Add(queue);
        }

        public void AddPublishQueue(PublishQueue queue)
        {
            PublishQueues.Add(queue);
        }
    }
}
