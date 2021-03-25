﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentCarContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<Car, bool>> filter = null)
        {
            
                using (RentCarContext context = new RentCarContext())
                {
                    var result = from c in filter is null ? context.Cars : context.Cars.Where(filter)
                                 join b in context.Brands on c.BrandId equals b.BrandId
                                 join co in context.Colors on c.ColorId equals co.ColorId
                                 select new CarDetailDto
                                 {
                                     CarId = c.CarId,
                                     CarName = c.CarName,
                                     BrandName = b.BrandName,
                                     ColorName = co.ColorName,
                                     DailyPrice = c.DailyPrice,
                                     ModelYear = c.ModelYear,
                                     Description= c.Description,
                                     ImagePath = context.CarImages.Where(x => x.CarId == c.CarId).FirstOrDefault().ImagePath
                                 };

                return result.ToList();

            }

            }
    }
}
