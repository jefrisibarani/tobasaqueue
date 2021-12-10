#region License
/*
    Sotware Antrian Tobasa
    Copyright (C) 2021  Jefri Sibarani

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

namespace Tobasa
{
    public delegate void MessageReceived(Message message);
    public class MessageReceivedEventArgs : EventArgs
    {
        public MessageReceivedEventArgs(Message message)
        {
            Message = message;
        }

        public Message Message
        {
            get; set;
        }
    }

    public class Tbl
    {
        public const string runningtexts = "runningtexts";
        public const string ipaccesslists = "ipaccesslists";
        public const string stations = "stations";
        public const string posts = "posts";
        public const string logins = "logins";
    }

    public class TableProp
    {
        private string _tableName;
        
        // initial total page
        private int _pageCount = 1;
        
        // maximum row in a page
        private int _pageSize = 10;
        
        // total row in this table
        private int _totalRows = 0;
        
        // initial page position
        private int _currentPage = 1;
        
        private int _currentOffset = 0;

        public TableProp(string tableName, int totalRows=0)
        {
            _tableName = tableName;
            _totalRows = totalRows;

            CalcculatePageCount();
        }

        private void CalcculatePageCount()
        {
            if (_totalRows > 0 && _pageSize > 0 && _totalRows >= _pageSize)
            {
                int remainder = _totalRows % _pageSize;
                _pageCount = _totalRows / _pageSize;
                
                if (remainder > 0)
                    _pageCount += 1;
            }
        }

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }
        public int TotalRows
        {
            get 
            { 
                return _totalRows; 
            }
            set 
            { 
                _totalRows = value;
                CalcculatePageCount();
            }
        }
        public int CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; }
        }
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        public int LocatePageOffset(int pagePosition)
        {
            if (pagePosition>0 && pagePosition <= _pageCount)
            {
                _currentPage = pagePosition;
                _currentOffset = (pagePosition - 1) * _pageSize;
                return _currentOffset;
            }

            return _currentOffset;
        }

        public string NavigationStatus
        {
            get
            {
                int min = _currentOffset + 1;
                int ma  = _currentOffset + _pageSize;
                
                if (_totalRows < ma)
                    ma = _totalRows;
                if (_totalRows == 0)
                    min = 0;

                string naTet = string.Format("{0} - {1} of {2}", min, ma, _totalRows);
                return naTet;
            }
        }

        public string MoveNextPage(MainForm form)
        {
            int lastOffset = _currentOffset;
            int newOffset = LocatePageOffset(_currentPage + 1);

            if(lastOffset!= newOffset)
                RequestTableFromServer(form);

            return NavigationStatus;
        }

        public string MovePreviousPage(MainForm form)
        {
            int lastOffset = _currentOffset;
            int newOffset = LocatePageOffset(_currentPage - 1);

            if (lastOffset != newOffset)
                RequestTableFromServer(form);

            return NavigationStatus;
        }

        public string MoveFirstPage(MainForm form)
        {
            int lastOffset = _currentOffset;
            int newOffset = LocatePageOffset(1);

            if (lastOffset != newOffset)
                RequestTableFromServer(form);
            
            return NavigationStatus;
        }

        public string MoveLastPage(MainForm form)
        {
            int lastOffset = _currentOffset;
            int newOffset = LocatePageOffset(_pageCount);

            if (lastOffset != newOffset)
                RequestTableFromServer(form);

            return NavigationStatus;
        }

        public void RequestTableFromServer(MainForm form)
        {
            if (form.TcpClient != null && form.TcpClient.Connected)
            {
                int Offset = _currentOffset;
                int limit  = _pageSize;

                string message = Msg.SysGetTable.Text +
                                 Msg.Separator + "REQ" +
                                 Msg.Separator + "Identifier" +
                                 Msg.Separator + _tableName +
                                 Msg.CompDelimiter + Offset.ToString() +
                                 Msg.CompDelimiter + limit.ToString();

                form.TcpClient.Send(message);
            }
            else
                Util.ShowConnectionError(form);
        }

    }
}