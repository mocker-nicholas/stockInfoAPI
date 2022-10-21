using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockInfoApi.DAL.Interfaces
{
    public interface IModifiable
    {
        DateTime ModifiedAt { get; set; }
    }
}
