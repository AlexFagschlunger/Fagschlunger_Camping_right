using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fagschlunger_Camping.Models.db
{
    interface IRepositoryReservierung
    {
        void Open();
        void Close();

        bool Insert(Reservierung user);
        bool Delete(int id);
        bool UpdateUserData(int id, Reservierung newUserData);
        List<Reservierung> GetAllUser();
        Reservierung GetUser(int id);
    }
}