using DanaZhangCms.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanaZhangCms.Extensions
{
    public interface IWorkContext
    {
        Task<User> GetCurrentUser();
    }
}
