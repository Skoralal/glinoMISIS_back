using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class JwtOptions
    {
        public string SecretKey { get; set; } = "bobrkurwabobrkurwabobrkurwabobrkurwabobrkurwabobrkurwabobrkurwabobrkurwa";
        public int ExpiresHours { get; set; } = 120;
    }
}
