#region License
/*
    Sotware Antrian Tobasa
    Copyright (C) 2015-2025  Jefri Sibarani

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
            {   if(_hndlrRoutine == null)
                    _hndlrRoutine = new HandlerRoutine(ConsoleCtrlCheck);

                SetConsoleCtrlHandler(_hndlrRoutine, true);
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

        private static bool ConsoleCtrlCheck(CtrlTypes ctrlType)
        {
            switch (ctrlType)
            {
                case CtrlTypes.CTRL_C_EVENT:
                    Logger.Log("CTRL+C received!");
                    _server.Stop();
                    _server.Dispose();
                    //SetConsoleCtrlHandler(_hndlrRoutine, false);
                    return true;

                case CtrlTypes.CTRL_BREAK_EVENT:
                    Logger.Log("CTRL+BREAK received!");
                    _server.Stop();
                    _server.Dispose();
                    return true;

                case CtrlTypes.CTRL_CLOSE_EVENT:
                    Logger.Log("Program being closed!");
                    _server.Stop();
                    _server.Dispose();
                    return true;

                case CtrlTypes.CTRL_LOGOFF_EVENT:
                case CtrlTypes.CTRL_SHUTDOWN_EVENT:
                    Logger.Log("User is logging off!");
                    _server.Stop();
                    _server.Dispose();
                    return true;

                default:
                    return false;
            }
        }

        #endregion


        #region Member Variables

        //! The thread will run the job. The job is the Method Run() below
        protected Thread _thread = null;
        private static HandlerRoutine _hndlrRoutine = null;
        private static QueueServer _server = null;

        #endregion


        #region Starting and stopping

        public QueueService()
        {
            InitializeComponent();
        }

        //! Set things in motion so your service can do its work.
        protected override void OnStart(string[] args)
        {
            ThreadStart starter = new ThreadStart(Run);
            _thread = new Thread(starter);
            _thread.Start();
        }


        //! Stop this service.
        // The Run() Method tests for this thread state each second
        protected override void OnStop()
        {
            _server.Stop();
            _server.Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        public void Run()
        {
            QueueServer.Log("-------------------------------------------------------------------------------");
            QueueServer.Log("Starting Tobasa QueueServer...");
            QueueServer.Log("From " + Util.ProcessPath);

            _server = new QueueServer();
            _server.Start();
       }

        #endregion


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
