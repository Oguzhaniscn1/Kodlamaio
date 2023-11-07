﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key);
        void Add(string key, object value, int duratin);
        bool IsAdd(string key);//cachde var mı?
        void Remove(string key);
        void RemoveByPattern(string pattern);
    }
}
