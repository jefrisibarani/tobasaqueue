#region License
/*
    Tobasa Library - Provide Async TCP server, DirectShow wrapper and simple Logger class
    Copyright (C) 2021  Jefri Sibarani
 
    This library is free software; you can redistribute it and/or
    modify it under the terms of the GNU Lesser General Public
    License as published by the Free Software Foundation; either
    version 2.1 of the License, or (at your option) any later version.

    This library is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
    Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public
    License along with this library; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
*/
#endregion

using System;

namespace Tobasa
{
    public enum NotifyType
    {
        NOTIFY_LOG,
        NOTIFY_MSG,
        NOTIFY_ERR
    }

    public class NotifyEventArgs : EventArgs
    {
        public NotifyEventArgs()
        {
            Type = NotifyType.NOTIFY_MSG;
        }

        public NotifyType Type { get; set; }
        public string Summary { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }

    /** Notifier class.
        Raise notify event by calling Notify()
    */
    public class Notifier
    {
        public event Action<NotifyEventArgs> Notified;
        protected virtual void OnNotifyError(NotifyEventArgs e)
        {
            e.Type = NotifyType.NOTIFY_ERR;
            OnNotify(e);
        }

        protected virtual void OnNotifyError(Exception ex)
        {
            NotifyEventArgs args = new NotifyEventArgs();
            args.Summary = ex.GetType().Name;
            args.Source = ex.Source;
            args.Message = ex.Message;
            args.Exception = ex;
            args.Type = NotifyType.NOTIFY_ERR;

            OnNotify(args);
        }

        protected virtual void OnNotifyError(string source, Exception ex)
        {
            NotifyEventArgs args = new NotifyEventArgs();
            args.Summary = ex.GetType().Name;
            args.Source = source;
            args.Message = ex.Message;
            args.Exception = ex;
            args.Type = NotifyType.NOTIFY_ERR;

            OnNotify(args);
        }

        protected virtual void OnNotifyMessage(NotifyEventArgs e)
        {
            e.Type = NotifyType.NOTIFY_MSG;
            OnNotify(e);
        }

        protected virtual void OnNotifyMessage(string source, string message, string summary="Info")
        {
            NotifyEventArgs args = new NotifyEventArgs();
            args.Summary = summary;
            args.Source = source;
            args.Message = message;
            args.Exception = null;
            args.Type = NotifyType.NOTIFY_MSG;

            OnNotify(args);
        }

        protected virtual void OnNotifyLog(string source, string message, string summary = "Info")
        {
            NotifyEventArgs args = new NotifyEventArgs();
            args.Summary = summary;
            args.Source = source;
            args.Message = message;
            args.Exception = null;
            args.Type = NotifyType.NOTIFY_LOG;

            OnNotify(args);
        }

        protected virtual void OnNotifyLog(string source, Exception ex)
        {
            NotifyEventArgs args = new NotifyEventArgs();
            args.Summary = ex.GetType().Name;
            args.Source = source;
            args.Message = ex.Message;
            args.Exception = ex;
            args.Type = NotifyType.NOTIFY_LOG;

            OnNotify(args);
        }

        protected virtual void OnNotify(NotifyEventArgs e)
        {
            Notified?.Invoke(e);
        }
    }
}
