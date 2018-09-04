using AutoMapper;
using BankAdviser.BLL.DTO;
using BankAdviser.BLL.Infrastructure;
using BankAdviser.BLL.Interfaces;
using BankAdviser.DAL.Entities;
using BankAdviser.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAdviser.BLL.Services
{
    public class BankManager : IBankManager
    {
        private IUnitOfWork db;

        public BankManager(IUnitOfWork uow)
        {
            db = uow;
        }
        public void SaveBank(BankDTO bankDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BankDTO, Bank>()).CreateMapper();            
            Bank bank = mapper.Map<BankDTO, Bank>(bankDTO);

            bank.SearchDate = DateTime.Now;

            db.Banks.Create(bank);
            db.Save();
        }

        public BankDTO GetBank(int? bankId)
        {
            if (bankId == null)
                throw new ValidationException("Bank ID is not set", "");

            Bank bank = db.Banks.Get(bankId.Value);

            if (bank == null)
                throw new ValidationException("Bank is not found", "");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Bank, BankDTO>()).CreateMapper();            
            BankDTO bankDTO = mapper.Map<Bank, BankDTO>(bank);

            return bankDTO;
        }

        public Dictionary<DepositDTO, BankDTO> GetBanksByDeposits(IEnumerable<DepositDTO> deposits)
        {
            //if (deposits == null)
            //    throw new ValidationException("Deposits are not set", "");

            //var banksForDeposits = new Dictionary<Deposit, Bank>();

            //DepositManager depositManager = new DepositManager(db);
            //var depositsByEnquiry = depositManager.SelectDeposits(enquiryId);

            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ReplyEntry, ReplyEntryDTO>()).CreateMapper();
            //IEnumerable<ReplyEntryDTO> replyEntriesDTO = mapper.Map<IEnumerable<ReplyEntry>, List<ReplyEntryDTO>>(db.ReplyEntries.GetAll());

            //return replyEntriesDTO;

            throw new NotImplementedException();
        }

        public void Dispose()
        {
            db.Dispose();
        }       
    }
}
