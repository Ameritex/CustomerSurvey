using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerSurvey.Data.Entities
{
    public class CustomerSurveyAnswer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string? Answer { get; set; }
        public string? AnswerText { get; set; }
        public int QuestionId { get; set; }
        public int SurveyId { get; set; }
        public int? OrderNo { get; set; }

        public Entities.CustomerSurvey CustomerSurvey { get; set; }
    }
}
