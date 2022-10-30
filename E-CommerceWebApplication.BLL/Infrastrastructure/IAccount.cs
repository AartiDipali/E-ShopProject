using E_CommerceWebApplication.BOL.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebApplication.BLL.Infrastrastructure
{
    public interface IAccount
    {
        Task<bool> LoginAsync(LoginViewModel loginViewModel);
    }
}
