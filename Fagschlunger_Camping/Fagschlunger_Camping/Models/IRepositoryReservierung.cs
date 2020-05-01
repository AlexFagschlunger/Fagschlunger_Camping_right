using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fagschlunger_Camping.Models.db
{
    interface IRepositoryReservierung : IDBBase
    {

        bool Insert(Reservierung user);
        bool Delete(int id);

        List<Reservierung> GetReservierungs();

        bool Update(int id, bool newStatus);
        bool UpdateUserData(int id, Reservierung newUserData);
    }
}