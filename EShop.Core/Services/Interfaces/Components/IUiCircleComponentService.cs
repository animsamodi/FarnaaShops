using EShop.Core.Services.Base;
using EShop.DataLayer.Entities.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Services.Interfaces.Components
{
    public interface IUiCircleComponentService : IBaseService<UiCircleComponent>
    {
        Task<List<UiCircleComponent>> GetAll();
    }
}
