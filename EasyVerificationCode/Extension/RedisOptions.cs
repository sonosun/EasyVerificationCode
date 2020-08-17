using System;
using System.Collections.Generic;
using System.Text;

namespace EasyVerificationCode
{
    /// <summary>
    /// Redis缓存配置项
    /// </summary>
    public class RedisOptions
    {
        /// <summary>
        /// redis链接，如：127.0.0.1:6379
        /// </summary>
        public string RedisConnection { get; set; }

        /// <summary>
        /// redis缓存key前缀，起到对缓存项分组的作用。可以为空，但建议使用 vcode:
        /// </summary>
        public string KeyPrefix { get; set; }
    }
}
