﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement
{
    internal interface IRepository<T> where T : class
    {
        void Create(T entity);
        void Delete(int id);
        void Update(T entity);
        T GetById(int id);
        List<T> Get();
    }
}
