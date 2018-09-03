using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BotDetect.Web.Mvc;
using TDShop.Common;
using TDShop.Data.Repositories;
using TDShop.Model.Models;
using TDShop.Service;
using TDShop.Web.Mappings;
using TDShop.Web.Models;

namespace TDShop.Web.Controllers
{
    public class ContactController : Controller
    {
        public IFeedbackService _feedbackService;
        public IContactDetailService _contactDetailService;
        public ContactController(IContactDetailService contactDetailService, IFeedbackService _feedbackService)
        {
            this._contactDetailService = contactDetailService;
        }
        // GET: Contact
        public ActionResult Index()
        {
            FeedbackViewModel viewModel = new FeedbackViewModel();
            viewModel.ContactDetail = GetDetail();
            return View(viewModel);
        }
        [HttpPost]
        [CaptchaValidation("CaptchaCode", "contactCaptcha", "Mã xác nhận không đúng")]
        public ActionResult SendFeedBack(FeedbackViewModel feedbackViewModel)
        {
            if(ModelState.IsValid)
            {
                var feedback = AutoMapperConfiguration.Mapper.Map<FeedbackViewModel, Feedback>(feedbackViewModel);
                //feedback.ID = n
                //_feedbackService.Create(feedback);
                //_feedbackService.Save();

                ViewData["SuccessMsg"] = "Gửi phản hồi thành công";


                string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/template/contact_template.html"));
                content = content.Replace("{{Name}}", feedbackViewModel.Name);
                content = content.Replace("{{Email}}", feedbackViewModel.Email);
                content = content.Replace("{{Message}}", feedbackViewModel.Message);
                var adminMail = ConfigHelper.GetByKey("FromEmailAddress");
                MailHelper.SendMail(adminMail, "Thông tin liên hệ từ website", content);

                feedbackViewModel.Name = "";
                feedbackViewModel.Message = "";
                feedbackViewModel.Email = "";
            }
            feedbackViewModel.ContactDetail = GetDetail();

            return RedirectToAction("Index", feedbackViewModel);
        }

        private ContactDetailViewModel GetDetail()
        {
            var model = _contactDetailService.GetDefaultContact();
            var contactViewModel = AutoMapperConfiguration.Mapper.Map<ContactDetail, ContactDetailViewModel>(model);
            return contactViewModel;
        }
    }
}