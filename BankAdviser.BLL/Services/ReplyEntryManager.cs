using AutoMapper;
using BankAdviser.BLL.BusinessModels;
using BankAdviser.BLL.DTO;
using BankAdviser.BLL.Infrastructure;
using BankAdviser.BLL.Interfaces;
using BankAdviser.DAL.Entities;
using BankAdviser.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace BankAdviser.BLL.Services
{
    public class ReplyEntryManager : IReplyEntryManager
    {
        public ReplyEntryManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        private IUnitOfWork uow;

        public void SaveReplyEntry(ReplyEntryDTO replyEntryDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ReplyEntryDTO, ReplyEntry>()).CreateMapper();            
            ReplyEntry replyEntry = mapper.Map<ReplyEntryDTO, ReplyEntry>(replyEntryDTO);

            replyEntry.Date = DateTime.Now;

            uow.ReplyEntries.Create(replyEntry);
            uow.Save();
        }

        public ReplyEntryDTO GetReplyEntry(int? id)
        {
            if (id == null)
                throw new ValidationException("ReplyEntry ID is not set", "");

            ReplyEntry replyEntry = uow.ReplyEntries.Get(id.Value);

            if (replyEntry == null)
                throw new ValidationException("ReplyEntry is not found", "");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ReplyEntry, ReplyEntryDTO>()).CreateMapper();            
            ReplyEntryDTO replyEntryDTO = mapper.Map<ReplyEntry, ReplyEntryDTO>(replyEntry);

            return replyEntryDTO;
        }

        public List<ReplyEntryDTO> GetReplyEntries(int? inquiryId)
        {
            if (inquiryId == null)
                throw new ValidationException("Inquiry ID id is not set", "");            

            DepositManager depositManager = new DepositManager(uow);
            var deposits = depositManager.SelectDeposits(inquiryId);

            BankManager bankManager = new BankManager(uow);

            InquiryManager inquiryManager = new InquiryManager(uow);
            InquiryDTO inquiry = inquiryManager.GetInquiry(inquiryId.Value);

            List<ReplyEntryDTO> replyEntries = new List<ReplyEntryDTO>();

            foreach (var d in deposits)
            {
                ReplyEntryDTO replyEntry = new ReplyEntryDTO
                {
                    BankName = bankManager.GetBank(d.BankId).Name,
                    DepositName = d.Name,
                    DepositRate = d.GetRateByTerm(inquiry.Term),
                    DepositBonusInfo = d.BonusInfo,
                    NetIncome = NetIncome.Calculate(inquiry, d),
                    BankRating = bankManager.GetBank(d.BankId).Rating,
                    BankAssetsRank = bankManager.GetBank(d.BankId).AssetsRank,
                    Remark = d.Remark,
                    DepositUrl = d.Url
                };
                replyEntries.Add(replyEntry);
            }           

            return replyEntries;
        }        

        public void Dispose()
        {
            uow.Dispose();
        }
    }
}