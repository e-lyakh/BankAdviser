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
        private IUnitOfWork db;

        public DepositManager(IUnitOfWork uow)
        {
            db = uow;
        }

        public void SaveDeposit(DepositDTO depositDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DepositDTO, Deposit>()).CreateMapper();           
            Deposit deposit = mapper.Map<DepositDTO, Deposit>(depositDTO);

            deposit.SearchDate = DateTime.Now;
            if (deposit.MaxSum == 0)
                deposit.MaxSum = 100_000_000;

            db.Deposits.Create(deposit);
            db.Save();
        }

        public IEnumerable<DepositDTO> SelectDeposits(int? equiryId)
        {
            Enquiry enq = db.Enquiries.Get(equiryId.Value);

            if (enq == null)
                throw new ValidationException("Enquiry ID is not set", "");

            var depositsByEnquiry = db.Deposits.GetAll();
                                    //.Where(d => d.Currency == enq.Currency &&
                                    //d.MinSum < enq.Sum &&
                                    //d.MaxSum > enq.Sum &&
                                    //d.HasTerm(enq.Term) &&
                                    //d.InterestsPeriodicity == enq.InterestsPeriodicity &&
                                    //d.IsAddable == enq.IsAddable &&
                                    //d.IsWithdrawable == enq.IsWithdrawable &&
                                    //d.IsCancellable == enq.IsCancellable &&
                                    //(db.Banks.Get(d.BankId).Type == BankType.Private && enq.ArePrivateBanksIncluded) &&
                                    //(db.Banks.Get(d.BankId).Type == BankType.State && enq.AreStateBanksIncluded) &&
                                    //(db.Banks.Get(d.BankId).Type == BankType.Foreign && enq.AreForeignBanksIncluded));

            IEnumerable<DepositDTO> depositsDTObyEnquiry = null;
            if (depositsByEnquiry != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Deposit, DepositDTO>()).CreateMapper();                
                depositsDTObyEnquiry = mapper.Map<IEnumerable<Deposit>, IEnumerable<DepositDTO>>(depositsByEnquiry);
            }            

            return depositsDTObyEnquiry;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
