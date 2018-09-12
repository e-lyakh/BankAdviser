using BankAdviser.BLL.DTO;
using System.Collections.Generic;

namespace BankAdviser.BLL.Interfaces
{
    public interface IReplyEntryManager
    {
        void SaveReplyEntry(ReplyEntryDTO replyEntryDTO);
        ReplyEntryDTO GetReplyEntry(int? id);
        List<ReplyEntryDTO> GetReplyEntries(int? inquiryId);
        void Dispose();
    }
}