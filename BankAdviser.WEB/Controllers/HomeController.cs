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
        IInquiryService inquiryService;

        public HomeController (IInquiryService service)
        {
            inquiryService = service;
        }

        public ActionResult Index()
        {
            //IEnumerable<DialogDTO> dialogDTOs = inquiryService.GetDialogs();
            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DialogDTO, DialogVM>()).CreateMapper();
            //var dialogs = mapper.Map<IEnumerable<DialogDTO>, List<DialogVM>>(dialogDTOs);
            //return View(dialogs);

            InquiryVM inquiryVM = new InquiryVM();
            return View(inquiryVM);
        }

        public ActionResult PutInquiry(int? id)
        {
            try
            {
                DialogDTO dialog = inquiryService.GetDialog(id);
                var request = new InquiryVM
                {
                    Id = dialog.Id
                };

                return View(request);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult PutInquiry(InquiryVM inquiryVM)
        {
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<InquiryVM, InquiryDTO>());
                var inquiryDTO = Mapper.Map<InquiryVM, InquiryDTO>(inquiryVM);

                inquiryService.SaveInquiry(inquiryDTO);

                return Content("<h2>Ваш запрос принят в работу. Ответ будет выслан на указанный e-mail.</h2>");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(inquiryVM);
        }
        protected override void Dispose(bool disposing)
        {
            inquiryService.Dispose();
            base.Dispose(disposing);
        }
    }
}