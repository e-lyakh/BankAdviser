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
        private IUnitOfWork db;

        public ReplyEntryManager(IUnitOfWork uow)
        {
            db = uow;
        }
        public void SaveReplyEntry(ReplyEntryDTO replyEntryDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ReplyEntryDTO, ReplyEntry>()).CreateMapper();            
            ReplyEntry replyEntry = mapper.Map<ReplyEntryDTO, ReplyEntry>(replyEntryDTO);

            replyEntry.Date = DateTime.Now;

            db.ReplyEntries.Create(replyEntry);
            db.Save();
        }

        public ReplyEntryDTO GetReplyEntry(int? id)
        {
            if (id == null)
                throw new ValidationException("ReplyEntry ID is not set", "");

            ReplyEntry replyEntry = db.ReplyEntries.Get(id.Value);

            if (replyEntry == null)
                throw new ValidationException("ReplyEntry is not found", "");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ReplyEntry, ReplyEntryDTO>()).CreateMapper();            
            ReplyEntryDTO replyEntryDTO = mapper.Map<ReplyEntry, ReplyEntryDTO>(replyEntry);

            return replyEntryDTO;
        }

        public List<ReplyEntryDTO> GetReplyEntries(int? enquiryId)
        {
            if (enquiryId == null)
                throw new ValidationException("Enquiry ID id is not set", "");            

            DepositManager depositManager = new DepositManager(db);
            var deposits = depositManager.SelectDeposits(enquiryId);

            BankManager bankManager = new BankManager(db);

            EnquiryManager enquiryManager = new EnquiryManager(db);
            EnquiryDTO enquiry = enquiryManager.GetEnquiry(enquiryId.Value);

            List<ReplyEntryDTO> replyEntries = new List<ReplyEntryDTO>();

            foreach (var d in deposits)
            {
                ReplyEntryDTO replyEntry = new ReplyEntryDTO
                {
                    BankName = bankManager.GetBank(d.BankId).Name,
                    DepositName = d.Name,
                    DepositRate = d.GetRateByTerm(enquiry.Term),
                    DepositBonusInfo = d.BonusInfo,
                    NetIncome = NetIncome.Calculate(enquiry, d),
                    Remark = d.Remark,
                    DepositUrl = d.Url
                };
                replyEntries.Add(replyEntry);
            }           

            return replyEntries;
        }        

        public void Dispose()
        {
            db.Dispose();
        }
    }
}