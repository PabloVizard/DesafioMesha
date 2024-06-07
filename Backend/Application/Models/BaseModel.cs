using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public abstract class BaseModel
    {
        public int id { get; set; }
    }
    public class BaseResponseModel<T>
    {
        public T data { get; set; }
        public string message { get; set; }
    }
}
