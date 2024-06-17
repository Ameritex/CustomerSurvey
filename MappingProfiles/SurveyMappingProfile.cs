using AutoMapper;
using CustomerSurvey.Models;

namespace CustomerSurvey.MappingProfiles
{
    public class SurveyMappingProfile : Profile
    {
        public SurveyMappingProfile()
        {
            CreateMap<CustomerSurveyViewModel, Data.Entities.CustomerSurvey>().ReverseMap();
            CreateMap<CustomerSurveyQuestionViewModel, Data.Entities.CustomerSurveyQuestion>().ReverseMap();
            CreateMap<UnsubscribeEmailViewModel, Data.Entities.UnsubscribeEmail>().ReverseMap();
        }
    }
}
