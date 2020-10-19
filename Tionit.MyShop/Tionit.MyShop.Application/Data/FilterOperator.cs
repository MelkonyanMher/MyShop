using System;
using System.Collections.Generic;
using System.Text;

namespace Tionit.MyShop.Application.Data
{
    public enum FilterOperator
    {
        IsLessThan,
        IsLessThanOrEqualTo,
        IsEqualTo,
        IsNotEqualTo,
        IsGreaterThanOrEqualTo,
        IsGreaterThan,
        StartsWith,
        EndsWith,
        Contains,
        IsContainedIn,
        DoesNotContain,
        IsNull,
        IsNotNull,
        IsEmpty,
        IsNotEmpty,
        IsNullOrEmpty,
        IsNotNullOrEmpty
    }
}
