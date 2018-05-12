using System;

namespace Movies.Core.Contracts
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}
