using CardsManagement.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsManagement.Application.Service.Interface
{
    public interface IUserManagementService
    {
        Task<GeneralResponseModel<UserLoginResponse>> Login(LoginRequest request);
    }
}
