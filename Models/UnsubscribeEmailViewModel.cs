﻿namespace CustomerSurvey.Models
{
    public class UnsubscribeEmailViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsUnsubscribed { get; set; } = false;
        public bool? IsOnceADay { get; set; } = false;
        public DateTime? UpdatedDate { get; set; }
    }
}
