using System.ComponentModel.DataAnnotations;

namespace CustomerSurvey.Data.Entities
{
    public class CustomerSurveyQuestion
    {
        [Key]
        public int Id { get; set; }
        public int? Type { get; set; } = 1;
        public int? OrderNo { get; set; } = 0;
        public string? Question { get; set; } = string.Empty;
        public bool? IsActive { get; set; } = false;
    }
}
