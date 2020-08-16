using EasyVerificationCode.Store;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyVerificationCode
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 注册校验码模块
        /// </summary>
        /// <param name="services"></param>
        public static void AddVerificationCode(this IServiceCollection services, Action<VerificationOptions> options = null)
        {
            services.AddMemoryCache();
            services.AddSingleton(typeof(IVerificationCodeStore), typeof(VerificationCodeInMemoryStore));
            services.AddSingleton(typeof(IVerificationCodeService), typeof(VerificationCodeService));

            if (options != null)
            {
                options.Invoke(VerificationOptions.Default);
            }
        }

        /// <summary>
        /// 注册校验码模块,使用Redis存储
        /// </summary>
        /// <param name="services"></param>
        public static void AddVerificationCode(this IServiceCollection services, RedisOptions redisOptions, Action<VerificationOptions> options = null)
        {
            services.AddMemoryCache();
            services.AddSingleton(typeof(IVerificationCodeService), typeof(VerificationCodeService));
            services.AddSingleton(typeof(IVerificationCodeStore), typeof(VerificationCodeInRedisStore));
            services.AddStackExchangeRedisCache(redis =>
            {
                redis.Configuration = redisOptions.RedisConnection;
                redis.InstanceName = redisOptions.KeyPrefix;
            });

            if (options != null)
            {
                options.Invoke(VerificationOptions.Default);
            }
        }
    }
}
