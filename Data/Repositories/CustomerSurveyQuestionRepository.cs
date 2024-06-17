using CustomerSurvey.Data.Entities;
using CustomerSurvey.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CustomerSurvey.Data.Repositories
{
    public class CustomerSurveyQuestionRepository : ICustomerSurveyQuestionRepository
    {
        private readonly SurveyContext _surveyContext;
        public CustomerSurveyQuestionRepository(SurveyContext surveyContext)
        {
            _surveyContext = surveyContext;
        }

        public async Task<IEnumerable<CustomerSurveyQuestion>> GetAsync()
        {
            var questions = await _surveyContext.CustomerSurveyQuestions
                                                .Where(x => x.IsActive == true)
                                                .OrderBy(x => x.OrderNo)
                                                .ToListAsync();
            return questions;
        }
    }
}
