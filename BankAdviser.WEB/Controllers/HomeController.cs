using AutoMapper;
using BankAdviser.BLL.DTO;
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
        InquiryVM inquiryVM;

        public HomeController (IInquiryManager inquiryManager, IReplyEntryManager replyManager)
        {
            this.inquiryManager = inquiryManager;
            this.replyManager = replyManager;

            inquiryVM = new InquiryVM();
            inquiryVM.Sum = 1000;
            inquiryVM.Term = 12;
        }

        public ActionResult Index()
        {            
            return View(inquiryVM);
        }       

        public ActionResult ShowReply(InquiryVM inqVM)
        {
            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<InquiryVM, InquiryDTO>()).CreateMapper();
                var inquiryDTO = mapper.Map<InquiryVM, InquiryDTO>(inqVM);

                int inqId = inquiryManager.SaveInquiry(inquiryDTO);

                var replyEntriesDTO = replyManager.GetReplyEntries(inqId);

                var mapper2 = new MapperConfiguration(cfg => cfg.CreateMap<ReplyEntryDTO, ReplyEntryVM>()).CreateMapper();
                var replyEntriesVM = mapper2.Map<List<ReplyEntryDTO>, List<ReplyEntryVM>>(replyEntriesDTO);

                ViewBag.Inquiry = inqVM;

                return View(replyEntriesVM);
            }
            else
            {
                inqVM.ValidationErrors = new Dictionary<string, string>();

                if (inqVM.Sum < 0)
                {
                    inqVM.ValidationErrors["Sum"] = "Сумма не должна быть отрицательной";                    
                }
                else if (ModelState["Sum"].Errors.Count > 0)
                {
                    inqVM.ValidationErrors["Sum"] = "Введите корректную сумму";
                }

                if (inqVM.Term < 1 || inqVM.Term > 36)
                {
                    inqVM.ValidationErrors["Term"] = "Срок должен быть от 1 до 36 мес.";
                }
                else if (ModelState["Term"].Errors.Count > 0)
                {
                    inqVM.ValidationErrors["Term"] = "Введите корректные данные по сроку";
                }
            }

            return View("Index", inqVM);
        }
     

        protected override void Dispose(bool disposing)
        {
            inquiryManager.Dispose();
            replyManager.Dispose();
            base.Dispose(disposing);
        }
    }
}