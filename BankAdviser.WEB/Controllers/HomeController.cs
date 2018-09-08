using AutoMapper;
using BankAdviser.BLL.DTO;
using BankAdviser.BLL.Infrastructure;
using BankAdviser.BLL.Interfaces;
using BankAdviser.WEB.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BankAdviser.WEB.Controllers
{
    public class HomeController : Controller
    {        
        IInquiryManager inquiryManager;
        IReplyEntryManager replyManager;

        public HomeController (IInquiryManager inquiryManager, IReplyEntryManager replyManager)
        {
            this.inquiryManager = inquiryManager;
            this.replyManager = replyManager;
        }

        public ActionResult Index()
        {
            InquiryVM inquiryVM = new InquiryVM();
            return View(inquiryVM);
        }
        
        [HttpPost]
        public ActionResult MakeInquiry(InquiryVM inquiryVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    

                    return RedirectToAction("ShowReply", "Home", inquiryVM);
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }

            return View(inquiryVM);
        }

        public ActionResult ShowReply(InquiryVM inquiryVM)
        {
            inquiryVM.UserIP = Request.UserHostAddress;            
            string ip = GetIP();
            var browser = HttpContext.Request.Browser;


            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<InquiryVM, InquiryDTO>()).CreateMapper();
            var inquiryDTO = mapper.Map<InquiryVM, InquiryDTO>(inquiryVM);

            int inqId = inquiryManager.SaveInquiry(inquiryDTO);

            var replyEntriesDTO = replyManager.GetReplyEntriesByInquiry(inqId);

            var mapper2 = new MapperConfiguration(cfg => cfg.CreateMap<ReplyEntryDTO, ReplyEntryVM>()).CreateMapper();            
            var replyEntriesVM = mapper2.Map<List<ReplyEntryDTO>, List<ReplyEntryVM>>(replyEntriesDTO);

            return View(replyEntriesVM);
        }

        protected string GetIP()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        protected override void Dispose(bool disposing)
        {
            inquiryManager.Dispose();
            replyManager.Dispose();
            base.Dispose(disposing);
        }        
    }
}