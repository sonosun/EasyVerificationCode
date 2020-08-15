using System;
using System.Collections.Generic;
using System.Text;

namespace EasyVerificationCode.Model
{
    /// <summary>
    /// 生成校验码参数
    /// </summary>
    public class VerificationCodeCreateParam
    {
        /// <summary>
        /// 校验码字体大小
        /// </summary>
        public int FontSize { get; set; } = 18;

        /// <summary>
        /// 校验码字符数量
        /// </summary>
        public int CharacterCount { get; set; } = 4;

        /// <summary>
        /// 校验码类别,有三种类别可选，分别是纯数字，纯字符，数字加字符
        /// </summary>
        public CodeCharacterTypeEnum Type { get; set; } = CodeCharacterTypeEnum.NumberAndCharacter;
    }
}
