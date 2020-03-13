using System;
using System.Collections.Generic;
using System.Text;

namespace Tudou.Abp.OrganizationUnit
{
   public class OrganizationUnitConsts
    {
        public const int MaxNameLength = 128;
        public const int MaxDepth = 16;
        public const int CodeUnitLength = 5;
        public const int MaxCodeLength = MaxDepth * (CodeUnitLength + 1) - 1;
    }
}
