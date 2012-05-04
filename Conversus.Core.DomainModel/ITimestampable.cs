using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conversus.Core.DomainModel
{
    public interface ITimestampable
    {
        long Timestamp { get; set; }
    }
}
