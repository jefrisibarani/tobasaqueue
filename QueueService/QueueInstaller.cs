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
