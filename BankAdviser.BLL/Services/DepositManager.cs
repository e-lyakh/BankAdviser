using AutoMapper;
using BankAdviser.BLL.BusinessModels;
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
        public DepositManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        private IUnitOfWork uow;

        public void SaveDeposit(DepositDTO depositDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DepositDTO, Deposit>()).CreateMapper();           
            Deposit deposit = mapper.Map<DepositDTO, Deposit>(depositDTO);

            deposit.SearchDate = DateTime.Now;
            if (deposit.MaxSum == 0)
                deposit.MaxSum = 100000000;

            uow.Deposits.Create(deposit);
            uow.Save();
        }

        public IEnumerable<DepositDTO> SelectDeposits(int? equiryId)
        {
            Inquiry inq = uow.Inquiries.Get(equiryId.Value);

            if (inq == null)
                throw new ValidationException("Inquiry ID is not set", "");

            IEnumerable<Deposit> depositsByInquiry;

            // Base selection:
            depositsByInquiry = uow.Deposits.GetAll()
                                .Where(d => d.Currency == inq.Currency &&
                                inq.Sum > d.MinSum &&
                                inq.Sum < d.MaxSum &&
                                d.HasTerm(inq.Term) &&
                                d.InterestsPeriodicity == inq.InterestsPeriodicity)
                                .Select(d => d);

            // Deposit conditions:
            if (inq.IsAddable)
            {
                depositsByInquiry = depositsByInquiry
                                    .Where(d => d.IsAddable == inq.IsAddable)
                                    .Select(d => d);
            }
            if (inq.IsWithdrawable)
            {
                depositsByInquiry = depositsByInquiry
                                    .Where(d => d.IsWithdrawable == inq.IsWithdrawable)
                                    .Select(d => d);
            }
            if (inq.IsCancellable)
            {
                depositsByInquiry = depositsByInquiry
                                    .Where(d => d.IsCancellable == inq.IsCancellable)
                                    .Select(d => d);
            }

            // Banks types conditions:
            if (!inq.ArePrivateBanksIncluded)
            {
                depositsByInquiry = depositsByInquiry
                                   .Where(d => uow.Banks.Get(d.BankId).Type == BankType.State ||
                                               uow.Banks.Get(d.BankId).Type == BankType.Foreign)
                                   .Select(d => d);
            }
            if (!inq.AreStateBanksIncluded)
            {
                depositsByInquiry = depositsByInquiry
                                   .Where(d => uow.Banks.Get(d.BankId).Type == BankType.Private ||
                                               uow.Banks.Get(d.BankId).Type == BankType.Foreign)
                                   .Select(d => d);
            }
            if (!inq.AreForeignBanksIncluded)
            {
                depositsByInquiry = depositsByInquiry
                                   .Where(d => uow.Banks.Get(d.BankId).Type == BankType.Private ||
                                               uow.Banks.Get(d.BankId).Type == BankType.State)
                                   .Select(d => d);
            }

            // Sort order conditions:
            if (inq.SortOrder == SortBy.Profitability)
            {
                depositsByInquiry = depositsByInquiry
                                    .OrderByDescending(d => NetIncome.Calculate(inq, d));
            }
            else if (inq.SortOrder == SortBy.BanksRating)
            {
                depositsByInquiry = depositsByInquiry
                                    .OrderBy(d => uow.Banks.Get(d.BankId).Rating);
            }
            else if (inq.SortOrder == SortBy.BanksAssets)
            {
                depositsByInquiry = depositsByInquiry
                                    .OrderBy(d => uow.Banks.Get(d.BankId).AssetsRank);
            }

            // Take first inq.BankNum results:
            if (depositsByInquiry.Count() > inq.BanksNum)
                depositsByInquiry = depositsByInquiry.Take(inq.BanksNum);

            IEnumerable <DepositDTO> depositsDTObyInquiry = null;
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
