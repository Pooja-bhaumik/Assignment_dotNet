using Entites;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public  interface IEmployeeContract
    {
        Task<string> Post(tblEmployees _oTblEmployees);
        Task<List<tblEmployees>> Get(string search);
        Task<tblEmployees> GetEmployeeById(int Id);
        Task<string> Put(tblEmployees _oTblEmployees);
        Task<string> Delete(int Id);
    }
}
