using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebApplication.BLL.Service
{
    public interface IEmail
    {
        public bool SendEmail(string userEmail, string confirmationLink);
        public bool SendForgetPasswordEmail(string userEmail, string token);

    }
}
