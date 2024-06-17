using CustomerSurvey.Data.Repositories.Contracts;
using AutoMapper;
using CustomerSurvey.Models;
using CustomerSurvey.Services.Contracts;

namespace CustomerSurvey.Services
{
    public class CustomerSurveyQuestionService : ICustomerSurveyQuestionService
    {
        private readonly ICustomerSurveyQuestionRepository _surveyQuestionRepository;
        private readonly IMapper _mapper;

        public CustomerSurveyQuestionService(ICustomerSurveyQuestionRepository surveyQuestionRepository, IMapper mapper)
        {
            _surveyQuestionRepository = surveyQuestionRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CustomerSurveyQuestionViewModel>> GetAsync()
        {
            var questions = await _surveyQuestionRepository.GetAsync();
            return _mapper.Map<IReadOnlyList<CustomerSurveyQuestionViewModel>>(questions);
        }
    }
}
