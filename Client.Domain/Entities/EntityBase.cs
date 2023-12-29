﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdate { get; set; }
        public bool IsDeleted { get; set; }

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}
