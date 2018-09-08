using AutoMapper;
using BankAdviser.BLL.DTO;
using BankAdviser.BLL.Infrastructure;
using BankAdviser.BLL.Interfaces;
using BankAdviser.DAL.Entities;
using BankAdviser.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankAdviser.BLL.Services
{
    public class DepositManager : IDepositManager
    {
        private IUnitOfWork uow;

        public DepositManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void SaveDeposit(DepositDTO depositDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DepositDTO, Deposit>()).CreateMapper();           
            Deposit deposit = mapper.Map<DepositDTO, Deposit>(depositDTO);

            deposit.SearchDate = DateTime.Now;
            if (deposit.MaxSum == 0)
                deposit.MaxSum = 100_000_000;

            uow.Deposits.Create(deposit);
            uow.Save();
        }

        public IEnumerable<DepositDTO> SelectDeposits(int? inquiryId)
        {
            if (inquiryId == null)
                throw new ValidationException("Inquiry ID is not set", "");

            Inquiry inq = uow.Inquiries.Get(inquiryId.Value);

            if (inq == null)
                throw new ValidationException("Inquiry is not found", "");

            var depositsByInquiry = uow.Deposits.GetAll()
                                    .Where(d => d.Currency == inq.Currency &&
                                    d.MinSum < inq.Sum &&
                                    d.MaxSum > inq.Sum &&
                                    d.HasTerm(inq.Term) &&
                                    d.InterestsPeriodicity == inq.InterestsPeriodicity &&
                                    d.IsAddable == inq.IsAddable &&
                                    d.IsWithdrawable == inq.IsWithdrawable &&
                                    d.IsCancellable == inq.IsCancellable); // &&
                                    //(db.Banks.Get(d.BankId).Type == BankType.Private && inq.ArePrivateBanksIncluded) &&
                                    //(db.Banks.Get(d.BankId).Type == BankType.State && inq.AreStateBanksIncluded) &&
                                    //(db.Banks.Get(d.BankId).Type == BankType.Foreign && inq.AreForeignBanksIncluded));

            IEnumerable<DepositDTO> depositsDTObyInquiry = null;
            if (depositsByInquiry != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Deposit, DepositDTO>()).CreateMapper();                
                depositsDTObyInquiry = mapper.Map<IEnumerable<Deposit>, IEnumerable<DepositDTO>>(depositsByInquiry);
            }            

            return depositsDTObyInquiry;
        }

        public void Dispose()
        {
            uow.Dispose();
        }
    }
}