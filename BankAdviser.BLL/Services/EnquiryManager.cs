using AutoMapper;
using BankAdviser.BLL.DTO;
using BankAdviser.BLL.Infrastructure;
using BankAdviser.BLL.Interfaces;
using BankAdviser.DAL.Entities;
using BankAdviser.DAL.Interfaces;
using System;

namespace BankAdviser.BLL.Services
{
    public class EnquiryManager : IEnquiryManager
    {
        private IUnitOfWork db;

        public EnquiryManager(IUnitOfWork uow)
        {
            db = uow;
        }

        public int SaveEnquiry(EnquiryDTO enquiryDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EnquiryDTO, Enquiry>()).CreateMapper();            
            Enquiry enquiry = mapper.Map<EnquiryDTO, Enquiry>(enquiryDTO);

            enquiry.Date = DateTime.Now;

            db.Enquiries.Create(enquiry);
            db.Save();

            return enquiry.Id;
        }

        public EnquiryDTO GetEnquiry(int? enqId)
        {
            if (enqId == null)
                throw new ValidationException("Enquiry ID is not set", "");

            Enquiry enquiry = db.Enquiries.Get(enqId.Value);

            if (enquiry == null)
                throw new ValidationException("Enquiry is not found", "");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Enquiry, EnquiryDTO>()).CreateMapper();            
            EnquiryDTO enquiryDTO = mapper.Map<Enquiry, EnquiryDTO>(enquiry);

            return enquiryDTO;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}