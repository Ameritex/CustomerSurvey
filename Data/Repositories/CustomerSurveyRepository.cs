using CustomerSurvey.Data.Entities;
using CustomerSurvey.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CustomerSurvey.Data.Repositories
{
    public class CustomerSurveyRepository : ICustomerSurveyRepository
    {
        private readonly SurveyContext _surveyContext;
        public CustomerSurveyRepository(SurveyContext surveyContext)
        {
            _surveyContext = surveyContext;
        }

        public async Task SaveAsync(Entities.CustomerSurvey survey)
        {
            await _surveyContext.AddAsync(survey);
            await _surveyContext.SaveChangesAsync();
        }

        public async Task<Entities.CustomerSurvey> GetByQuoteIdAsync(long quoteId)
        {
            var customerSurvey =  _surveyContext.CustomerSurveys.Where(x => x.QuoteId == quoteId)
                .Include(x => x.CustomerSurveyAnswers);
            return await customerSurvey
                .FirstOrDefaultAsync();
        }

        public async Task UnsubscribeAddAsync(string email)
        {
            var existingUnsubscribedEmail = _surveyContext.UnsubscribeEmails.FirstOrDefault(x => x.Email == email);
            if (existingUnsubscribedEmail == null)
            {
                var unsubscribeEmail = new UnsubscribeEmail
                {
                    Email = email,
                    CreatedDate = DateTime.Now,
                    IsUnsubscribed = true
                };
                await _surveyContext.UnsubscribeEmails.AddAsync(unsubscribeEmail);
            }
            else
            {
                existingUnsubscribedEmail.UpdatedDate = DateTime.Now;
                existingUnsubscribedEmail.IsUnsubscribed = true;

                _surveyContext.Update(existingUnsubscribedEmail);
            }
            await _surveyContext.SaveChangesAsync();
        }
        
        public async Task<bool> SaveEmailSettingAsync(UnsubscribeEmail emailSetting)
        {
            var existingSetting = await _surveyContext.UnsubscribeEmails.FirstOrDefaultAsync(x => x.Email == emailSetting.Email);
            try
            {
                if (existingSetting != null)
                {
                    existingSetting.UpdatedDate = DateTime.Now;
                    existingSetting.IsUnsubscribed = emailSetting.IsUnsubscribed;
                    existingSetting.IsOnceADay = emailSetting.IsOnceADay;
                }
                else
                {
                    await _surveyContext.UnsubscribeEmails.AddAsync(emailSetting);
                }
                await _surveyContext.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UnsubscribeEmail?> GetByEmailAsync( string email)
        {
            var existingSetting = await _surveyContext.UnsubscribeEmails.FirstOrDefaultAsync(x => x.Email == email);
            return existingSetting;
        }
    }
}
