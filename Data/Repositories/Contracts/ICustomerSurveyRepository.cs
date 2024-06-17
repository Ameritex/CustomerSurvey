using CustomerSurvey.Data.Entities;

namespace CustomerSurvey.Data.Repositories.Contracts
{
    public interface ICustomerSurveyRepository
    {
        Task SaveAsync(Entities.CustomerSurvey survey);
        Task UnsubscribeAddAsync(string email);
        Task<Entities.CustomerSurvey> GetByQuoteIdAsync(long quoteId);
        Task<bool> SaveEmailSettingAsync(UnsubscribeEmail emailSetting);
        Task<UnsubscribeEmail?> GetByEmailAsync(string email);
    }
}
