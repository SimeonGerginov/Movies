﻿using System;
using Movies.Core.Contracts;

namespace Movies.Core.Entities
{
    public class BaseEntity : IBaseEntity, IAuditable, IDeletable
    {
        public int Id { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
