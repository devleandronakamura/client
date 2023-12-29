using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Util.ExtensionMethods
{
    public static class GuidExtension
    {
        public static bool IsNullOrEmpty(this Guid? value)
        {
            return value == null || value == Guid.Empty;
        }

        public static bool IsEmpty(this Guid value)
        {
            return value == Guid.Empty;
        }
    }
}
