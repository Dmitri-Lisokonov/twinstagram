using System.ComponentModel.DataAnnotations;

namespace Shared.Models.User
{
    public class Follow
    {
        [Key, Required]
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid FollowUserId { get; private set; }

        public Follow(Guid userId, Guid followUserId)
        {
            UserId = userId;
            FollowUserId = followUserId;
        }
    }
}
