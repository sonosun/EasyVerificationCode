using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EasyVerificationCodeDemo3._1.Models;
using EasyVerificationCode;

namespace EasyVerificationCodeDemo3._1.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 服务接口
        /// </summary>
        IVerificationCodeService verificationCodeService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
            IVerificationCodeService verificationCodeService)
        {
            _logger = logger;
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class VerificationCode
    {
        public string Key { get; set; }
        public string Code { get; set; }
    }
}
