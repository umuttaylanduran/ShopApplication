using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApplication.Business.Abstract
{
    public interface IValidator<T>
    {
        string ErrorMessage { get; set; }
        //Dictionary<string, string> ErrorMessages { get; set; }
        bool Validate(T entity); // product mı category mi?
    }
}
