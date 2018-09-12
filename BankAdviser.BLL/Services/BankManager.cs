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

        public void Dispose()
        {
            db.Dispose();
        }       
    }
}
