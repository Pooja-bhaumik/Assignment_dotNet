using Entites;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public  interface IUserLoginContract
    {
        Task<ViewUsersAndRoleDetails> Post(tblUsers _oTblUsers);
    }
}
