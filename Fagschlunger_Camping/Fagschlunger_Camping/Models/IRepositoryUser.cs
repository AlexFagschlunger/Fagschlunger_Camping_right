﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fagschlunger_Camping.Models.db
{
    interface IRepositoryUser
    {
        void Open();
        void Close();

        bool Insert(User user);
        bool Delete(int id);
        bool UpdateUserData(int id, User newUserData);
        List<User> GetAllUser();
        User GetUser(int id);


        User Login(UserLogin user);


    }
}