using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyVerificationCode.Store
{
    /// <summary>
    /// 内存存储校验码
    /// </summary>
    public class VerificationCodeInRedisStore : IVerificationCodeStore
    {
        IDistributedCache store;

        public VerificationCodeInRedisStore(IDistributedCache store)
        {
            this.store = store;
        }

        /// <summary>
        /// 添加校验码
        /// </summary>
        /// <param name="key"></param>
        /// <param name="code"></param>
        /// <param name="expire"></param>
        public void Add(string key, string code, TimeSpan expire)
        {
            this.store.SetString(key, code, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = expire });
        }

        /// <summary>
        /// 获取校验码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetCode(string key)
        {
            return this.store.GetString(key);
        }

        /// <summary>
        /// 移除校验码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void Remove(string key)
        {
            this.store.Remove(key);
        }
    }
}
