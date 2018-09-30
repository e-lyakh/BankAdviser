using BankAdviser.BLL.DTO;
using System.Collections.Generic;

namespace BankAdviser.BLL.Interfaces
{
    public interface IRatingManager
    {
        void SaveRating(RatingDTO ratingDTO);
        RatingDTO GetRating(int? ratingId);
        IEnumerable<RatingDTO> GetAll();
        void Dispose();
    }
}