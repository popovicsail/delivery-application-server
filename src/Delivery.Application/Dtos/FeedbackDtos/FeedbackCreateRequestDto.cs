using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Application.Dtos.FeedbackDtos
{
    public class FeedbackCreateRequestDto
    {
        public Guid QuestionId { get; set; }
        public int Rating { get; set; } // 1–5
        public string? Comment { get; set; }
        public Guid UserId { get; set; }
    }
}
