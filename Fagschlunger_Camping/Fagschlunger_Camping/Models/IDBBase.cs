using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fagschlunger_Camping.Models
{
    interface IDBBase
    {
        void Open();
        void Close();
    }
}