﻿using ECommerce.Core.Utilities.Results;

namespace ECommerce.Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics)
        {
            foreach (var result in logics)
            {
                if (!result.Success)
                    return result;
            }

            return null;
        }
    }
}