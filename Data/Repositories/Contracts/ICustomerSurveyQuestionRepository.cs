using CustomerSurvey.Data.Entities;

namespace CustomerSurvey.Data.Repositories.Contracts
{
    public interface ICustomerSurveyQuestionRepository
    {
        Task<IEnumerable<CustomerSurveyQuestion>> GetAsync();
    }
}
