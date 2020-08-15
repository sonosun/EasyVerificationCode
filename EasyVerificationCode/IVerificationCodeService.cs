using EasyVerificationCode.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyVerificationCode
{
    /// <summary>
    /// 
    /// </summary>
    public interface IVerificationCodeService
    {
        /// <summary>
        /// 生成校验码
        /// </summary>
        VerificationCode Create(VerificationCodeCreateParam param = null);
        
        /// <summary>
        /// 验证校验码
        /// </summary>
        /// <returns></returns>
        bool Verify(string key, string code);
    }
}
