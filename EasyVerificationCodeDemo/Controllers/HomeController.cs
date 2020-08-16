using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyVerificationCode;

namespace EasyVerificationCodeDemo.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 服务接口
        /// </summary>
        IVerificationCodeService verificationCodeService;

        public HomeController(IVerificationCodeService verificationCodeService)
        {
            this.verificationCodeService = verificationCodeService;
        }

        public IActionResult Index()
        {
            //创建校验码
            var verificationCode = this.verificationCodeService.Create();
            this.ViewBag.Key = verificationCode.Key;
            this.ViewBag.Image = Convert.ToBase64String(verificationCode.Image);

            return View();
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<bool> VerifyCode(VerificationCode param)
        {
            return this.verificationCodeService.Verify(param.Key, param.Code);
        }
    }

    public class VerificationCode
    {
        public string Key { get; set; }
        public string Code { get; set; }
    }
}
