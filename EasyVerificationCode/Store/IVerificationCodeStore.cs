using System;
using System.Collections.Generic;
using System.Text;

namespace EasyVerificationCode.Store
{
    /// <summary>
    /// 校验码数据存储
    /// </summary>
    public interface IVerificationCodeStore
    {
        /// <summary>
        /// 保存校验码
        /// </summary>
        /// <param name="key"></param>
        /// <param name="code"></param>
        /// <param name="expire"></param>
        void Add(string key, string code, TimeSpan expire);

        /// <summary>
        /// 获取校验码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetCode(string key);

        /// <summary>
        /// 移除校验码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        void Remove(string key);
    }
}
