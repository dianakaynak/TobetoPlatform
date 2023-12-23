﻿using Entities.Concretes;

namespace Business.Dtos.Requests.DeleteRequests
{
    public class DeleteAnnouncementRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime AnnouncementDate { get; set; }

    }
}