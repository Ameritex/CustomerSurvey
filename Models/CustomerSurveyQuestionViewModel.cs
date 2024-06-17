namespace CustomerSurvey.Models
{
    public class CustomerSurveyQuestionViewModel
    {
        public int Id { get; set; }
        public int? Type { get; set; } = 1;
        public int? OrderNo { get; set; } = 0;
        public string? Question { get; set; } = string.Empty;
        public bool? IsActive { get; set; } = false;
        
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
    }
}
