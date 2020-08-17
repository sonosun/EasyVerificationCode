using System;
using System.Collections.Generic;
using System.Text;

namespace EasyVerificationCode
{
    /// <summary>
    /// 
    /// </summary>
    public class VerificationOptions
    {
        public static readonly VerificationOptions Default = new VerificationOptions()
        {
            CodeCacheExpire = new TimeSpan(0, 10, 0),
            CodeCharacterCount = 4,
            CodeFontSize = 18,
            CodeType = CodeCharacterTypeEnum.NumberAndCharacter,
            IgnoreCase = true,
            UseOnce = true,
        };

        /// <summary>
        /// 校验码缓存时间(默认10分钟)
        /// </summary>
        public TimeSpan CodeCacheExpire { get; set; }

        /// <summary>
        /// 校验码字体大小（默认18）
        /// </summary>
        public int CodeFontSize { get; set; }

        /// <summary>
        /// 校验码字符数量（默认4个字符）
        /// </summary>
        public int CodeCharacterCount { get; set; }

        /// <summary>
        /// 校验码类别,有三种类别可选，分别是纯数字，纯字符，数字加字符
        /// </summary>
        public CodeCharacterTypeEnum CodeType { get; set; }

        /// <summary>
        /// 是否忽略字母大小写(默认为true)
        /// </summary>
        public bool IgnoreCase { get; set; }

        /// <summary>
        /// 只验证一次（验证通过后，删除校验码，防止用一个校验码反复提交）
        /// </summary>
        public bool UseOnce { get; set; }
    }
}
