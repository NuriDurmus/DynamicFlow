using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DynamicFlow.Business
{
    public class RolePermissionService
    {
        public async Task<bool> ChangePersonRole(int userId,int roleId,bool throwException)
        {
            if (throwException)
            {
                throw new Exception($"ChangePersonRole hatası. Gelen parametreler UserId: {userId} roleID: {roleId}");
            }
            return true;
        }
    }
}
