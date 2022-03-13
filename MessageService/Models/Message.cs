<<<<<<< HEAD
﻿namespace MessageService.Models
{
    public class Message
    {
=======
﻿using System.ComponentModel.DataAnnotations;

namespace MessageService.Models
{
    public class Message
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

        public Message()
        {
        }
        public Message(int id, int userId, string description, string image, DateTime createdDate)
        {
            Id = id;
            UserId = userId;
            Description = description;
            Image = image;
            CreatedDate = createdDate;
        }
>>>>>>> e884d9240d91d31b7bb0866a701fb38791c9aa11
    }
}
