using System;
using System.Collections.Generic;
using System.Text;

namespace EasyVerificationCode
{
    /// <summary>
    /// 校验码字符类别，有数字，字符，数字加字符
    /// </summary>
    public enum CodeCharacterTypeEnum
    {
        /// <summary>
        /// 数字
        /// </summary>
        Number = 1,

        /// <summary>
        /// 字符
        /// </summary>
        Character = 2,

        /// <summary>
        /// 数字加字符
        /// </summary>
        NumberAndCharacter = 3,
    }
}
