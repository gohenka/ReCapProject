﻿using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            IResult result = BusinessRules.Run(RentCarReturnDateCheck(rental.CarId));
            if (result != null)
            {
                return result;
            }


            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDelete);
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdate);
        }

        public IDataResult<Rental> Get(int rentalid)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.RentalId == rentalid), Messages.RentalIdListed);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.RentalListed);
        }


        private IResult RentCarReturnDateCheck(int carId)
        {
            var result = _rentalDal.GetAll(r => r.CarId == carId);
            if (result.Count > 0)
            {
                foreach (var rent in result)
                {
                    if (rent.ReturnDate == null)
                    {
                        return new ErrorResult(Messages.DontAvailable);
                    }
                }
            }
            return new SuccessResult();
        }
      
    }
}