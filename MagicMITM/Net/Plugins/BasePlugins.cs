using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicMITM.Net.Plugins
{
    public class BasePlugins : Plugin
    {
        public override void Initialize()
        {
            Session.Plugins.Register<AccountRolesPlugin>();
            Session.Plugins.Register<WorldEnteredPlugin>();
            Session.Plugins.Register<ContainersPlugin>();

            base.Initialize();
        }
    }
}
