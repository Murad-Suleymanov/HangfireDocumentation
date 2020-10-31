using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hangfire
{
    [RunInstaller(true)]
    public class Installers : Installer
    {
        public Installers()
        {

        }
    }
}
