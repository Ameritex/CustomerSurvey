using AutoMapper;
using CustomerSurvey.Data.Entities;
using CustomerSurvey.Data.Repositories.Contracts;
using CustomerSurvey.Models;
using CustomerSurvey.Services.Contracts;

namespace CustomerSurvey.Services
{
    public class CustomerSurveyService : ICustomerSurveyService
    {
        private readonly ICustomerSurveyRepository _surveyRepository;
        private readonly IMapper _mapper;

        public CustomerSurveyService(ICustomerSurveyRepository surveyRepository, IMapper mapper)
        {
            _surveyRepository = surveyRepository;
            _mapper = mapper;
        }

        public async Task SaveAsync(CustomerSurveyViewModel surveyViewModel)
        {
            var customerSurvey = _mapper.Map<Data.Entities.CustomerSurvey>(surveyViewModel);
            await _surveyRepository.SaveAsync(customerSurvey);
        }

        public async Task<CustomerSurveyViewModel> GetByQuoteIdAsync(long quoteId)
        {
            var survey = await _surveyRepository.GetByQuoteIdAsync(quoteId);

            return _mapper.Map<CustomerSurveyViewModel>(survey);
        }

        public async Task UnsubscribeAddAsync(string email)
        {
            await _surveyRepository.UnsubscribeAddAsync(email);
        }

        public async Task<bool> SaveEmailSettingAsync(UnsubscribeEmailViewModel setting)
        {
            var emailSetting = _mapper.Map<UnsubscribeEmail>(setting);
            var result = await _surveyRepository.SaveEmailSettingAsync(emailSetting);        
            return result;
        }

        public async Task<UnsubscribeEmailViewModel> GetByEmailAsync(string email)
        {
            try
            {
                var setting = await _surveyRepository.GetByEmailAsync(email);
                if (setting != null)
                {
                    return _mapper.Map<UnsubscribeEmailViewModel>(setting);
                }
                return new UnsubscribeEmailViewModel();
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
