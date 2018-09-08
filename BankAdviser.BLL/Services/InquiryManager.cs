using AutoMapper;
using BankAdviser.BLL.DTO;
using BankAdviser.BLL.Infrastructure;
using BankAdviser.BLL.Interfaces;
using BankAdviser.DAL.Entities;
using BankAdviser.DAL.Interfaces;
using System;

namespace BankAdviser.BLL.Services
{
    public class InquiryManager : IInquiryManager
    {
        private IUnitOfWork uow;

        public InquiryManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public int SaveInquiry(InquiryDTO inquiryDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<InquiryDTO, Inquiry>()).CreateMapper();            
            Inquiry inquiry = mapper.Map<InquiryDTO, Inquiry>(inquiryDTO);

            inquiry.Date = DateTime.Now;

            uow.Inquiries.Create(inquiry);
            uow.Save();

            return inquiry.Id;
        }

        public InquiryDTO GetInquiry(int? inqId)
        {
            if (inqId == null)
                throw new ValidationException("Inquiry ID is not set", "");

            Inquiry inquiry = uow.Inquiries.Get(inqId.Value);

            if (inquiry == null)
                throw new ValidationException("Inquiry is not found", "");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Inquiry, InquiryDTO>()).CreateMapper();            
            InquiryDTO inquiryDTO = mapper.Map<Inquiry, InquiryDTO>(inquiry);

            return inquiryDTO;
        }

        public void Dispose()
        {
            uow.Dispose();
        }
    }
}