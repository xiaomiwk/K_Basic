using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace Utility.Windows
{
    public static class H检测权限
    {
        public static bool Is管理员()
        {
            WindowsIdentity __id = WindowsIdentity.GetCurrent();
            WindowsPrincipal __principal = new WindowsPrincipal(__id);
            return __principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
