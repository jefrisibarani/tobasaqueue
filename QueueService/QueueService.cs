using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading;

namespace Tobasa
{
    public class QueueService : ServiceBase
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main thread";
            QueueService ServiceToRun = new QueueService();

            if (!Environment.UserInteractive)
            {
                if (Debugger.IsAttached)
                {
                    ServiceToRun.Run();
                }
                else
                {
                    ServiceToRun.CanPauseAndContinue = false;
                    ServiceBase.Run(ServiceToRun);
                }
            }
            else
            {
                SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);
                ServiceToRun.Run();
            }
        }


		#region Required Service Related Methods

        private System.ComponentModel.Container components = null;

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.ServiceName = "QueueService";
        }

        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

		#endregion

        #region Member Variables

        /// <summary>
        /// The thread will run the job. The job is the Method Run() below
        /// </summary>
        protected Thread thread = null;
        private static QueueServer mServer = null;
      
        #endregion

        public QueueService()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set things in motion so your service can do its work.
        /// </summary>
        protected override void OnStart(string[] args)
        {
            ThreadStart starter = new ThreadStart(Run);
            thread = new Thread(starter);
            thread.Start();
        }

        /// <summary>
        /// Stop this service.
        /// The Run() Method tests for this thread state each second
        /// </summary>
        protected override void OnStop()
        {
            mServer.Stop();
            mServer.Dispose();
        }

        public void Run()
        {
            if (Environment.UserInteractive)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("\nStarting QueueServer...");
                Console.WriteLine("From " + Util.ProcessPath);
            }

            Logger.LogFile = "c:\\tmp\\QueueServer.log";
            Logger.Log("--------------------------------------------------------------------------------------");
            Logger.Log("Starting QueueServer...");
            Logger.Log("From " + Util.ProcessPath);

            mServer = new QueueServer();
            mServer.Start();
       }


        private static bool ConsoleCtrlCheck(CtrlTypes ctrlType)
        {
            switch (ctrlType)
            {
                case CtrlTypes.CTRL_C_EVENT:
                    Logger.Log("CTRL+C received!");
                    mServer.Stop();
                    mServer.Dispose();
                    break;

                case CtrlTypes.CTRL_BREAK_EVENT:
                    Logger.Log("CTRL+BREAK received!");
                    mServer.Stop();
                    mServer.Dispose();
                    break;

                case CtrlTypes.CTRL_CLOSE_EVENT:
                    Logger.Log("Program being closed!");
                    mServer.Stop();
                    mServer.Dispose();
                    break;

                case CtrlTypes.CTRL_LOGOFF_EVENT:
                case CtrlTypes.CTRL_SHUTDOWN_EVENT:
                    Logger.Log("User is logging off!");
                    mServer.Stop();
                    mServer.Dispose();
                    break;
            }
            return true;
        }

        #region unmanaged
        // Declare the SetConsoleCtrlHandler function
        // as external and receiving a delegate.

        [DllImport("Kernel32")]
        public static extern bool SetConsoleCtrlHandler(HandlerRoutine Handler, bool Add);

        // A delegate type to be used as the handler routine
        // for SetConsoleCtrlHandler.
        public delegate bool HandlerRoutine(CtrlTypes CtrlType);

        // An enumerated type for the control messages
        // sent to the handler routine.
        public enum CtrlTypes
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }

        #endregion
    }
}
