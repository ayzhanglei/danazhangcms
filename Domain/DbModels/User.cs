using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DanaZhangCms.Domain.DbModels;

namespace DanaZhangCms.DbModels
{
    /// <summary>
    /// 管理员信息表
    /// </summary>

    public partial class User : IdentityUser<long>, IEntityWithTypedId<long>
    {
        public User()
        {
            CreateOn = DateTime.Now;
            LastTime = DateTime.Now;
            UserGuid = System.Guid.NewGuid().ToString();
        }
        public string UserGuid { get; set; }
        public string Avatar { get; set; }
        public string Profile { get; set; }
        public string Tel { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime LastTime { get; set; }
    }
}