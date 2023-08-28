using E_CommerceWebApplication.BOL.Models.Admin;
using E_CommerceWebApplication.BOL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebApplication.BLL.Infrastrastructure
{
    public interface IManageData
    {
        void AddBrand(Brand brand);
        void AddGender(Gender gender);
        void AddSize(Sizes size);
       IEnumerable<SizesViewModel> GetAllSizeData();
    }
}
