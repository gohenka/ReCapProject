﻿using Core.Entities;


namespace Core.Entities.Concrete
{
    public class UserOperationClaim : IEntity
    {
        public int UserOperationClaimId { get; set; }
        public int OperationClaimId { get; set; }
        public int UserId { get; set; }
    }
}
