using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Application.Dtos.FeedbackDtos
{
    public class FeedbackFilterRequestDto
    {
        public Guid QuestionId { get; set; }          // ID
        public string? SearchTerm { get; set; }       // Ime korisnika ili komentar
        public string? SortField { get; set; }        // "Rating", "UserFullName", "CreatedAt"
        public string? SortOrder { get; set; }        // "ASC" ili "DESC"
        public string? TimeRange { get; set; }        // "LastWeek", "LastMonth", "Last3Months", "LastYear"
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;

    }
}
