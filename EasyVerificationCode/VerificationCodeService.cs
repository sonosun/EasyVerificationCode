using EasyVerificationCode.Common;
using EasyVerificationCode.Model;
using EasyVerificationCode.Store;
using System;

namespace EasyVerificationCode
{
    /// <summary>
    /// 
    /// </summary>
    public class VerificationCodeService : IVerificationCodeService
    {
        IVerificationCodeStore store;

        public VerificationCodeService(IVerificationCodeStore store)
        {
            this.store = store;
        }

        /// <summary>
        /// 创建校验码
        /// </summary>
        /// <returns></returns>
        public VerificationCode Create(VerificationCodeCreateParam param = null)
        {
            if (param == null)
            {
                param = new VerificationCodeCreateParam()
                {
                    CharacterCount = VerificationOptions.Default.CodeCharacterCount,
                    FontSize = VerificationOptions.Default.CodeFontSize,
                    Type = VerificationOptions.Default.CodeType
                };
            }

            VerificationCode result = InnerCreate(param);
            this.store.Add(result.Key, result.Code, VerificationOptions.Default.CodeCacheExpire);

            return result;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private VerificationCode InnerCreate(VerificationCodeCreateParam param)
        {
            if (param == null) return null;

            VerificationCode result = new VerificationCode();
            result.Key = Guid.NewGuid().ToString();

            switch (param.Type)
            {
                case CodeCharacterTypeEnum.Character:
                    result.Code = RandomCodeCreator.CreatRandomChar(param.CharacterCount);
                    break;
                case CodeCharacterTypeEnum.Number:
                    result.Code = RandomCodeCreator.CreatRandomNum(param.CharacterCount);
                    break;
                case CodeCharacterTypeEnum.NumberAndCharacter:
                    result.Code = RandomCodeCreator.CreatRandomNumAndChar(param.CharacterCount);
                    break;
            }
            result.Image = RandomCodeImageCreator.Create(result.Code, param.FontSize);

            return result;
        }

        /// <summary>
        /// 校验校验码
        /// </summary>
        /// <param name="key"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool Verify(string key, string code)
        {
            return string.Compare(this.store.GetCode(key), code, VerificationOptions.Default.IgnoreCase) == 0;
        }
    }
}
