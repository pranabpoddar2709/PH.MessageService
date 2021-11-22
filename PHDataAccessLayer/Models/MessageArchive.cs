using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PHDataAccessLayer.Models
{
    public partial class MessageArchive
    {
        public int MessageId { get; set; }
        public DateTime Date { get; set; }
        public string Details { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Mode { get; set; }
        public string Status { get; set; }
        public int? RetryNumber { get; set; }
        public DateTime? RetryDate { get; set; }
        public string Remarks { get; set; }
    }
}
