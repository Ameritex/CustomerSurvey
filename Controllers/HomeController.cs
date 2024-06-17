using CustomerSurvey.Models;
using CustomerSurvey.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CustomerSurvey.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerSurveyQuestionService _customerSurveyQuestion;
        private readonly ICustomerSurveyService _customerSurvey;

        public HomeController(ILogger<HomeController> logger,
            ICustomerSurveyQuestionService customerSurveyQuestion,
            ICustomerSurveyService customerSurvey)
        {
            _logger = logger;
            _customerSurveyQuestion = customerSurveyQuestion;
            _customerSurvey = customerSurvey;
        }

        public async Task<IActionResult> Index()
        {
            long quoteId = 0;
            int customerId = 0;

            try
            {
                quoteId = long.Parse(EncryptDecrypt.Decrypt(HttpContext.Request.Query["quoteId"].ToString()));
                customerId = int.Parse(EncryptDecrypt.Decrypt(HttpContext.Request.Query["customerId"].ToString()));
            }
            catch (Exception ex)
            {
                _logger.LogError($"EncryptDecrypt Error: {ex.Message}\r\nInnerException:{ex.InnerException?.Message}\r\nStackTrace:{ex.StackTrace}");
                return new NotFoundResult();
            }

            var questions = await _customerSurveyQuestion.GetAsync();

            try
            {
                var customerSurvey = await _customerSurvey.GetByQuoteIdAsync(quoteId);

                ViewBag.CustomerSurvey = customerSurvey;

                ViewBag.QuoteId = quoteId;
                ViewBag.CustomerId = customerId;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}\r\nInnerException:{ex.InnerException?.Message}\r\nStackTrace:{ex.StackTrace}");
            }
            return View(questions);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("api/Save")]
        public async Task<IActionResult> SaveAsync(CustomerSurveyViewModel customerSurvey)
        {
            try
            {
                await _customerSurvey.SaveAsync(customerSurvey);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Save Error: {ex.Message}\r\nInnerException:{ex.InnerException?.Message}\r\nStackTrace:{ex.StackTrace}");
            }
            return BadRequest();
        }

        [Route("api/unsubscribe/{email}")]
        [HttpGet]
        public async Task<IActionResult> UnsubscribeEmail(string email)
        {
            try
            {
                string decodedEmail = EncryptDecrypt.Decrypt(Uri.UnescapeDataString(email));

                await _customerSurvey.UnsubscribeAddAsync(decodedEmail);

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unsubscribe Error: {ex.Message}\r\nInnerException:{ex.InnerException?.Message}\r\nStackTrace:{ex.StackTrace}");
            }
            return BadRequest();
        }

        [Route("emailsetup/{email}")]
        [HttpGet]
        public async Task<IActionResult> ManageEmailSettings(string email)
        {
            try
            {
                string decodedEmail = EncryptDecrypt.Decrypt(Uri.UnescapeDataString(email));

                var setting = await _customerSurvey.GetByEmailAsync(decodedEmail);
                return View(setting);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Email Setting Error: {ex.Message}\r\nInnerException:{ex.InnerException?.Message}\r\nStackTrace:{ex.StackTrace}");
            }
            return BadRequest();
        }

        [Route("api/emailsetup/save")]
        [HttpPost]
        public async Task<IActionResult> SaveEmailSettings(UnsubscribeEmailViewModel setting)
        {
            try
            {
                setting.Email = EncryptDecrypt.Decrypt(Uri.UnescapeDataString(setting.Email));
                setting.CreatedDate = DateTime.Now;
                var result = await _customerSurvey.SaveEmailSettingAsync(setting);
                if (result)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Email Setting Error: {ex.Message}\r\nInnerException:{ex.InnerException?.Message}\r\nStackTrace:{ex.StackTrace}");
            }
            return BadRequest();
        }
    }
}