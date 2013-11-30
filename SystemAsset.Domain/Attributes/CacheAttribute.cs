using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemAsset.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheAttribute : Attribute
    {
        public bool IsRefreshNeeded { get; set; }
        public string CachedObjectName { get; set; }

        public CacheAttribute()
        {
        }
    }
}
