﻿using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IResult Add(Rental rental);
        IResult Delete(Rental rental);
        IResult Update(Rental rental);

        IDataResult<Rental> Get(int rentalid);
        IDataResult<List<RentalDetailDto>> GetRentalByCarId(int carId);
        IDataResult<List<RentalDetailDto>> GetRentalDetails();
        IDataResult<List<Rental>> GetAll();
    }
}
