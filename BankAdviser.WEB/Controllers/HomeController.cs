using AutoMapper;
using BankAdviser.BLL.BusinessModels;
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
        IRatingManager ratingManager;

        InquiryVM inquiryVM;

        public HomeController (IInquiryManager inquiryManager, IReplyEntryManager replyManager, IRatingManager ratingManager)
        {
            this.inquiryManager = inquiryManager;
            this.replyManager = replyManager;
            this.ratingManager = ratingManager;

            inquiryVM = new InquiryVM();
            inquiryVM.Sum = 1000;
            inquiryVM.Term = 12;
        }

        public ActionResult Index()
        {            
            return View();
        }

        public ActionResult AskDeposits()
        {
            return View(inquiryVM);
        }

        public ActionResult ShowDeposits(InquiryVM inqVM)
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

        public ActionResult ShowRatings()
        {
            var ratingsDTO = ratingManager.GetAll();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RatingDTO, RatingVM>()).CreateMapper();
            var ratingsVM = mapper.Map<IEnumerable<RatingDTO>, List<RatingVM>>(ratingsDTO);

            return View(ratingsVM);
        }

        public ActionResult Calculator(CalculatorVM calcVM)
        {
            if (calcVM == null)
            {
                calcVM = new CalculatorVM();
                return View(calcVM);
            }
            if (calcVM.Term == 0)
            {
                return View(calcVM);
            }
            else if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CalculatorVM, CalculatorBM>()).CreateMapper();
                var calcBM = mapper.Map<CalculatorVM, CalculatorBM>(calcVM);

                calcBM.Calculate();

                calcVM.ResultSum = calcBM.ResultSum;
                calcVM.TaxSum = calcBM.TaxSum;
                calcVM.NetSum = calcBM.NetSum;

                return View(calcVM);
            }
            else
            {
                calcVM.ValidationErrors = new Dictionary<string, string>();

                if (calcVM.Sum < 0)
                {
                    calcVM.ValidationErrors["Sum"] = "Сумма не должна быть отрицательной";
                }
                else if (ModelState["Sum"].Errors.Count > 0)
                {
                    calcVM.ValidationErrors["Sum"] = "Введите корректную сумму";
                }

                if (calcVM.Term < 1 || calcVM.Term > 36)
                {
                    calcVM.ValidationErrors["Term"] = "Срок должен быть от 1 до 36 мес.";
                }
                else if (ModelState["Term"].Errors.Count > 0)
                {
                    calcVM.ValidationErrors["Term"] = "Введите корректные данные по сроку";
                }

                if (calcVM.Rate < 0)
                {
                    calcVM.ValidationErrors["Rate"] = "Ставка не должна быть отрицательной";
                }
                else if (ModelState["Rate"].Errors.Count > 0)
                {
                    calcVM.ValidationErrors["Rate"] = "Введите корректную ставку";
                }
            }                

            return View(calcVM);
        }


        protected override void Dispose(bool disposing)
        {
            inquiryManager.Dispose();
            replyManager.Dispose();
            base.Dispose(disposing);
        }
    }
}