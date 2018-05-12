using System;

namespace Movies.Core.Contracts
{
    public interface IAuditable
    {
        DateTime? CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
