using System.ComponentModel.DataAnnotations;

namespace CustomerSurvey.Data.Entities
{
    public class CustomerSurvey
    {
        [Key]
        public int Id { get; set; }
        public long QuoteId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public virtual ICollection<CustomerSurveyAnswer>? CustomerSurveyAnswers { get; set; }
    }
}
