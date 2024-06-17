using CustomerSurvey.Data.Entities;

namespace CustomerSurvey.Models
{
    public class CustomerSurveyViewModel
    {
        public int Id { get; set; }
        public int QuoteId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public IEnumerable<CustomerSurveyAnswer>? CustomerSurveyAnswers { get; set; }
    }
}
