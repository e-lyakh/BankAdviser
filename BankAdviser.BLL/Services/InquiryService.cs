using AutoMapper;
using BankAdviser.BLL.DTO;
using BankAdviser.BLL.Infrastructure;
using BankAdviser.BLL.Interfaces;
using BankAdviser.DAL.Entities;
using BankAdviser.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace BankAdviser.BLL.Services
{
    public class InquiryService : IInquiryService
    {
        IUnitOfWork Db { get; set; }

        public InquiryService(IUnitOfWork uow)
        {
            Db = uow;
        }
        public void SaveInquiry(InquiryDTO inquiryDto)
        {
            //Request request = Db.Requests.Get(requestDto.Id);
            
            //if (request == null)
            //    throw new ValidationException("Request is not found", "");

            Inquiry inquiry = new Inquiry
            {
                Id = inquiryDto.Id,
                Currency = inquiryDto.Currency,
                Sum = inquiryDto.Sum,
                Term = inquiryDto.Term,
                InterestsPayout = inquiryDto.InterestsPayout,                
                IsAddable = inquiryDto.IsAddable,
                IsWithdrawable = inquiryDto.IsWithdrawable,
                NeedPrivateBanks = inquiryDto.NeedPrivateBanks,
                NeedStateBanks = inquiryDto.NeedStateBanks,
                NeedForeignBanks = inquiryDto.NeedForeignBanks,
                BanksNum = inquiryDto.BanksNum,
                Date = DateTime.Now
            };

            Db.Inquiries.Create(inquiry);
            Db.Save();

            //TODO: Start search by Selenium           
        }

        public IEnumerable<DialogDTO> GetDialogs()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Dialog, DialogDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Dialog>, List<DialogDTO>>(Db.Dialogs.GetAll());
        }

        public DialogDTO GetDialog(int? id)
        {
            if (id == null)
                throw new ValidationException("Dialog id is not set", "");
            var dialog = Db.Dialogs.Get(id.Value);
            if (dialog == null)
                throw new ValidationException("Dialog is not found", "");

            return new DialogDTO
            {
                Id = dialog.Id,
                Language = dialog.Language,
                Sum = dialog.Sum,
                Currency = dialog.Currency,
                InterestPayout = dialog.InterestPayout,
                BanksGroup = dialog.BanksGroup,
                Term = dialog.Term,
                BanksNum = dialog.BanksNum,
                Email = dialog.Email,
                Format = dialog.Format
            };
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
