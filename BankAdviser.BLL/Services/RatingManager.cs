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
    public class RatingManager : IRatingManager
    {
        public RatingManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        private IUnitOfWork uow;

        public void SaveRating(RatingDTO ratingDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RatingDTO, Rating>()).CreateMapper();
            Rating rating = mapper.Map<RatingDTO, Rating>(ratingDTO);

            rating.SearchDate = DateTime.Now;

            uow.Ratings.Create(rating);
            uow.Save();
        }

        public RatingDTO GetRating(int? ratingId)
        {
            if (ratingId == null)
                throw new ValidationException("Rating ID is not set", "");

            Rating rating = uow.Ratings.Get(ratingId.Value);

            if (rating == null)
                throw new ValidationException("Rating is not found", "");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Rating, RatingDTO>()).CreateMapper();
            RatingDTO ratingDTO = mapper.Map<Rating, RatingDTO>(rating);

            return ratingDTO;
        }

        public IEnumerable<RatingDTO> GetAll()
        {
            var ratings = uow.Ratings.GetAll();
            
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Rating, RatingDTO>()).CreateMapper();
            var ratingsDTO = mapper.Map<IEnumerable<Rating>, IEnumerable<RatingDTO>>(ratings);

            return ratingsDTO;
        }

        public void Dispose()
        {
            uow.Dispose();
        }       
    }
}
