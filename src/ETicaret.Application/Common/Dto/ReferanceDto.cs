using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Common.Dto
{
    public class ReferanceDto<T>
    {
        public T Id { get; set; }
        public string Name { get; set; }
    }
}
