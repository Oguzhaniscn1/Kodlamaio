using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        //adapter pattern -> kendi sistemimize uyarlamak ve uyarladığım şeyler değişir ise geçişi basite indirgemek.
        //şu an microsoftun nimetlerinden yararlanıyorum ama ek yazılım da kullanabilirim. bu geçiş basit ve maliyetsiz olmalı.
        IMemoryCache _memoryCache;

        //public MemoryCacheManager(IMemoryCache memoryCache)
        //{
        //    _memoryCache = memoryCache; bağımlılık zincirinin içinde olmadığı için bu şekilde enjekte edemeyiz.dependencyresolvers.coremodule de belirtmemiz gerekir
        //}
        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();//using Microsoft.Extensions.DependencyInjection;
        }

        public void Add(string key, object value, int duratin)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duratin));
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _);//var mı yok mu bak, data döndürme.
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            //git belleğe bak, bellekte entriescollection içersinde tutuyor git onu bul.
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection)//her bir cache elemanını gez 
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);//pattern belirttik
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
