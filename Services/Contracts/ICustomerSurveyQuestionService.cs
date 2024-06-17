using CustomerSurvey.Models;

namespace CustomerSurvey.Services.Contracts
{
    public interface ICustomerSurveyQuestionService
    {
        Task<IReadOnlyList<CustomerSurveyQuestionViewModel>> GetAsync();
    }
}
