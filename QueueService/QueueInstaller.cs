#region License
/*
    Sotware Antrian Tobasa
    Copyright (C) 2015-2024  Jefri Sibarani

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
#endregion

using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Tobasa
{
    /// <summary>
    /// The service must be installed before it can execute. 
    /// Services are installed with "installutil.exe" and uninstalled with "installutil.exe /u" with the service executable as the last parameter.
    /// For example, "c:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil QueueService.exe" will install the service to the Services Manager.
    /// </summary>
    [RunInstaller(true)]
	public class QueueInstaller: Installer
	{
		private ServiceInstaller serviceInstaller;
		private ServiceProcessInstaller processInstaller;

      public QueueInstaller()
		{
			// Instantiate installers for process and services.
			processInstaller = new ServiceProcessInstaller();
			serviceInstaller = new ServiceInstaller();

			// The services run under the system account.
			//processInstaller.Account = ServiceAccount.LocalSystem;
            processInstaller.Account = System.ServiceProcess.ServiceAccount.User;
            processInstaller.Password = null;
            processInstaller.Username = null;


			// The services are started automatically.
			serviceInstaller.StartType = ServiceStartMode.Automatic;
            serviceInstaller.DelayedAutoStart = true;

			// ServiceName must equal those on ServiceBase derived classes.            
            serviceInstaller.ServiceName = "QueueService";

			// displayed in list
            serviceInstaller.DisplayName = "QueueService";

			// Add installers to collection. Order is not important.
			Installers.Add(serviceInstaller);
			Installers.Add(processInstaller);
		}
	}
}
