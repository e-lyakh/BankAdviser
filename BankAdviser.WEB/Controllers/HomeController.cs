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
        IEnquiryManager enquiryManager;
        IReplyEntryManager replyManager;

        public HomeController (IEnquiryManager enquiryManager, IReplyEntryManager replyManager)
        {
            this.enquiryManager = enquiryManager;
            this.replyManager = replyManager;
        }

        public ActionResult Index()
        {
            EnquiryVM enquiryVM = new EnquiryVM();
            return View(enquiryVM);
        }
        
        [HttpPost]
        public ActionResult MakeEnquiry(EnquiryVM enquiryVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EnquiryVM, EnquiryDTO>()).CreateMapper();
                    ////Mapper.Initialize(cfg => cfg.CreateMap<EnquiryVM, EnquiryDTO>());
                    //var enquiryDTO = mapper.Map<EnquiryVM, EnquiryDTO>(enquiryVM);

                    //enquiryManager.SaveEnquiry(enquiryDTO);

                    return RedirectToAction("ShowReply", "Home", enquiryVM);
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }

            return View(enquiryVM);
        }

        public ActionResult ShowReply(EnquiryVM enquiryVM)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EnquiryVM, EnquiryDTO>()).CreateMapper();            
            var enquiryDTO = mapper.Map<EnquiryVM, EnquiryDTO>(enquiryVM);

            int enqId = enquiryManager.SaveEnquiry(enquiryDTO);           

            var replyEntriesDTO = replyManager.GetReplyEntries(enqId);

            var mapper2 = new MapperConfiguration(cfg => cfg.CreateMap<ReplyEntryDTO, ReplyEntryVM>()).CreateMapper();            
            var replyEntriesVM = mapper2.Map<List<ReplyEntryDTO>, List<ReplyEntryVM>>(replyEntriesDTO);

            return View(replyEntriesVM);
        }

        protected override void Dispose(bool disposing)
        {
            enquiryManager.Dispose();
            replyManager.Dispose();
            base.Dispose(disposing);
        }
    }
}