using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fagschlunger_Camping.Models.db
{
    interface IRepositoryUser : IDBBase
    {

        bool Insert(User user);
        bool Delete(int id);
        bool UpdateUserData(int id, User newUserData);
        List<User> GetAllUser();
        List<User> GetRegisteredUser();


        User Login(UserLogin user);


    }
}