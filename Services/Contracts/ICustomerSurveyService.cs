using CustomerSurvey.Models;

namespace CustomerSurvey.Services.Contracts
{
    public interface ICustomerSurveyService
    {
        Task SaveAsync(CustomerSurveyViewModel surveyViewModel);
        Task UnsubscribeAddAsync(string encodedEmail);
        Task<CustomerSurveyViewModel> GetByQuoteIdAsync(long quoteId);
        Task<bool> SaveEmailSettingAsync(UnsubscribeEmailViewModel setting);
        Task<UnsubscribeEmailViewModel> GetByEmailAsync(string email);
    }
}
