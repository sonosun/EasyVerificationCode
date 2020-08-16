# EasyVerificationCode

## 简介：

.net core平台下的图形校验码生成和校验模块。 

只需要几行代码就可以完成图形校验码生成和校验工作，让开发人员可以更专注于业务。

支持`本地内存`和`redis分布式缓存`两种模式，开发调试或单机部署时，可使用本地内存模式，项目以分布式集群形式部署时，需开启redis缓存模式。

---

## 基本使用：

### 第一步：添加引用

通过 `NuGet` 包管理器，搜索并添加依赖包 `EasyVerificationCode`

### 第二步：注册VerificationCode服务

在 `Startup.cs` 中注册图形校验码模块

``` csharp
public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //注册图形校验码模块
            services.AddVerificationCode();

            //此处忽略其他代码
        }
        //此处忽略其他代码
    }
``` 
            
### 第三步：生成校验码以及校验

`IVerificationCodeService` 接口是校验码生成及校验的接口。 

接口有两个方法：

        // 生成校验码
        VerificationCode Create(VerificationCodeCreateParam param = null);              
        // 验证校验码
        bool Verify(string key, string code);   

#### 接口使用：

由于 .net core 支持依赖注入，所以不需要关心接口的实现类，只需注入即可。
(目前默认实现类是 `VerificationCodeService`)

``` csharp
 public class HomeController : Controller
    {
        /// <summary>
        /// 校验码接口
        /// </summary>
        IVerificationCodeService verificationCodeService;

        //构造函数，注入校验码服务类
        public HomeController(IVerificationCodeService verificationCodeService)
        {
            this.verificationCodeService = verificationCodeService;
        }

        public IActionResult Index()
        {
            //创建校验码
            var verificationCode = this.verificationCodeService.Create();
            //校验码Key，提交校验时，需要提供这个Key值
            this.ViewBag.Key = verificationCode.Key;
            //Image类型为byte[]，转换成base64格式字符串之后，传递到前端展示
            this.ViewBag.Image = Convert.ToBase64String(verificationCode.Image);

            return View();
        }

        [HttpPost]
        public ActionResult<bool> VerifyCode(VerificationCode param)
        {
            //校验用户提交的校验码
            return this.verificationCodeService.Verify(param.Key, param.Code);
        }

        public class VerificationCode
        {
            public string Key { get; set; }
            public string Code { get; set; }
        }
    }
``` 

#### 前端展示销售

![](./resource/images/verification_code_1.png '描述')

---

## 进阶使用：

### 使用`redis`缓存模式
修改 `Startup.cs` 中校验码模块注册方式，增加Redis服务配置

``` csharp
public void ConfigureServices(IServiceCollection services)
        {
            //Redis缓存模式
            services.AddVerificationCode(new RedisOptions() { RedisConnection = "127.0.0.1:6379", KeyPrefix = "vcode:" });

        }
```
说明：`KeyPrefix` 是redis缓存的key前缀，起到对缓存项分组的作用。可以为空，但建议使用 vcode:

### 配置校验码生成细节
通过对`options`进行赋值，可以定制校验码
``` csharp
        public void ConfigureServices(IServiceCollection services)
        {
            //Redis缓存模式
            services.AddVerificationCode(new RedisOptions() { RedisConnection = "127.0.0.1:6379", KeyPrefix = "vcode:" },
                options =>
                {
                    //缓存超时时间设置
                    options.CodeCacheExpire = new TimeSpan(0, 5, 0);
                    //校验码字符数量
                    options.CodeCharacterCount = 6;
                    //校验码字体大小
                    options.CodeFontSize = 16;
                    //校验码类别,有三种类别可选，分别是纯数字，纯字符，数字加字符
                    options.CodeType = CodeCharacterTypeEnum.Number;
                    //校验时是否忽略字符大小写
                    options.IgnoreCase = true;
                });

```

### 通过 `IVerificationCodeService` 接口的`Create(VerificationCodeCreateParam param = null)`方法，定制校验码
``` csharp
        public IActionResult Index()
        {
            //校验码参数
            VerificationCodeCreateParam param = new VerificationCodeCreateParam()
            {
                CharacterCount = 4,
                FontSize = 18,
                Type = CodeCharacterTypeEnum.Character,
            };

            //创建校验码
            var verificationCode = this.verificationCodeService.Create(param);

            return View();
        }
```


# 结语

图形校验码起到防止恶意提交，人机校验的作用，在绝大多数项目中都会使用。

图形校验码功能的实现难度不大，绝大多数开发人员用一天甚至半天的时间就能搞定，有些开发人员可能会找到之前写过的代码，复制粘贴，一两个小时就能调试完毕，但复制粘贴并不是一个好的程序员所应该做的。

我做这个模块的目的是希望当开发人员遇到图形校验码的需求的时候，能在十分钟内完成这个功能，剩余的时间还可以去摸摸鱼。

我的口号是：`让天下没有难写的代码。`