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
        public VerificationCode Create()
        {
            VerificationCode result = new VerificationCode();
            result.Key = Guid.NewGuid().ToString();

            switch (VerificationOptions.Default.CodeType)
            {
                case CodeCharacterTypeEnum.Character:
                    result.Code = RandomCodeCreator.CreatRandomChar(VerificationOptions.Default.CodeCharacterCount);
                    break;
                case CodeCharacterTypeEnum.Number:
                    result.Code = RandomCodeCreator.CreatRandomNum(VerificationOptions.Default.CodeCharacterCount);
                    break;
                case CodeCharacterTypeEnum.NumberAndCharacter:
                    result.Code = RandomCodeCreator.CreatRandomNumAndChar(VerificationOptions.Default.CodeCharacterCount);
                    break;
            }
            result.Image = RandomCodeImageCreator.Create(result.Code, VerificationOptions.Default.CodeFontSize);

            this.store.Add(result.Key, result.Code, VerificationOptions.Default.CodeCacheExpire);

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
