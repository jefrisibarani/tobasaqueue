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
using System.Collections.Generic;
using System.Windows.Forms;

namespace Tobasa
{
    public class PostPropertyItem<TCtrl,TValType>
        where TCtrl : Control
    {
        private readonly TCtrl _control;

        public PostPropertyItem(string name, TCtrl control)
        {
            Name = name;
            _control = control;
        }

        public string Name { get; set; }

        public TValType Value() 
        {
            dynamic retVal;

            if (_control is ComboBox)
            {
                var ctl = _control as TextBox;
                retVal = ctl.Text;
            }
            else if (_control is TextBox)
            {
                var ctl = _control as TextBox;
                retVal = ctl.Text;
            }
            else if (_control is NumericUpDown)
            {
                var ctl = _control as NumericUpDown;
                retVal = ctl.Value;
            }
            else if (_control is CheckBox)
            {
                var ctl = _control as CheckBox;
                retVal = ctl.Checked;
            }
            else
            {
                retVal = null;
            }

            return retVal;
        }
        
        public void SetValue(int val)
        {
            if (_control is NumericUpDown)
            {
                var ctl = _control as NumericUpDown;
                ctl.Value = val;
            }
            else
            {
                throw new AppException("Invalid value set for PostPropertyItem");
            }
        }

        public void SetValue(bool val)
        {
            if (_control is CheckBox)
            {
                var ctl = _control as CheckBox;
                ctl.Checked = val;
            }
            else
            {
                throw new AppException("Invalid value set for PostPropertyItem");
            }
        }

        public void SetValue(string val)
        {
            if (_control is ComboBox)
            {
                var ctl = _control as TextBox;
                ctl.Text = val;
            }
            else if (_control is TextBox)
            {
                var ctl = _control as TextBox;
                ctl.Text = val;
            }
            else
            {
                throw new AppException("Invalid value set for PostPropertyItem");
            }
        }

        public static implicit operator PostPropertyItem<TCtrl, TValType>(PostPropertyItem<NumericUpDown, string> v)
        {
            throw new NotImplementedException();
        }
    }

    public class PostPropertyItemCollection<TCtrl, TValType> :
        Dictionary<string, PostPropertyItem<TCtrl, TValType> >
             where TCtrl : Control
    {
        public PostPropertyItem<TCtrl, TValType> FindById(string id)
        {
            if (TryGetValue(id, out PostPropertyItem<TCtrl, TValType> property))
            {
                return property;
            }
            return null;
        }
    }

    public class PostProperty
    {
        public PostProperty(string id)
        {
            Id = id;
            LoadFromConfiguration();
        }

        public int Index { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Caption { get; set; }
        public bool Visible { get; set; }
        public bool Enabled { get; set; }
        public string BtnImageOn { get; set; }
        public string BtnImageOff { get; set; }
        public string PrintHeader { get; set; }
        public string PrintFooter { get; set; }
        public int PrintCopies { get; set; }

        private PostPropertyItem<TextBox, string> _ctlName;
        private PostPropertyItem<TextBox, string> _ctlCaption;
        private PostPropertyItem<CheckBox, bool> _ctlVisible;
        private PostPropertyItem<CheckBox, bool> _ctlEnabled;
        private PostPropertyItem<TextBox, string> _ctlImageOn;
        private PostPropertyItem<TextBox, string> _ctlImageOff;
        private PostPropertyItem<TextBox, string> _ctlPrintHeader;
        private PostPropertyItem<TextBox, string> _ctlPrintFooter;
        private PostPropertyItem<NumericUpDown, int> _ctlPrintCopies;

        public PostProperty SetCtlName(string name, TextBox ctl)
        {
            _ctlName = new PostPropertyItem<TextBox, string>(name,ctl);
            return this;
        }
        public PostProperty SetCtlCaption(string name, TextBox ctl)
        {
            _ctlCaption = new PostPropertyItem<TextBox, string>(name, ctl);
            return this;
        }
        public PostProperty SetCtlVisible(string name, CheckBox ctl)
        {
            _ctlVisible = new PostPropertyItem<CheckBox, bool>(name, ctl);
            return this;
        }
        public PostProperty SetCtlEnabled(string name, CheckBox ctl)
        {
            _ctlEnabled = new PostPropertyItem<CheckBox, bool>(name, ctl);
            return this;
        }
        public PostProperty SetCtlImageOn(string name, TextBox ctl)
        {
            _ctlImageOn = new PostPropertyItem<TextBox, string>(name, ctl);
            return this;
        }
        public PostProperty SetCtlImageOff(string name, TextBox ctl)
        {
            _ctlImageOff = new PostPropertyItem<TextBox, string>(name, ctl);
            return this;
        }
        public PostProperty SetCtlPrintHeader(string name, TextBox ctl)
        {
            _ctlPrintHeader = new PostPropertyItem<TextBox, string>(name, ctl);
            return this;
        }
        public PostProperty SetCtlPrintFooter(string name, TextBox ctl)
        {
            _ctlPrintFooter = new PostPropertyItem<TextBox, string>(name, ctl);
            return this;
        }
        public PostProperty SetCtlPrintCopies(string name, NumericUpDown ctl)
        {
            _ctlPrintCopies = new PostPropertyItem<NumericUpDown, int>(name, ctl);
            return this;
        }

        public PostPropertyItem<TextBox, string> GetCtlName()
        {
            return _ctlName;
        }
        public PostPropertyItem<TextBox, string> GetCtlCaption()
        {
            return _ctlCaption;
        }
        public PostPropertyItem<CheckBox, bool> GetCtlVisible()
        {
            return _ctlVisible;
        }
        public PostPropertyItem<CheckBox, bool> GetCtlEnabled()
        {
            return _ctlEnabled;
        }
        public PostPropertyItem<TextBox, string> GetCtlImageOn()
        {
            return _ctlImageOn;
        }
        public PostPropertyItem<TextBox, string> GetCtlImageOff()
        {
            return _ctlImageOff;
        }
        public PostPropertyItem<TextBox, string> GetCtlPrintHeader()
        {
            return _ctlPrintHeader;
        }
        public PostPropertyItem<TextBox, string> GetCtlPrintFooter()
        {
            return _ctlPrintFooter;
        }
        public PostPropertyItem<NumericUpDown, int> GetCtlPrintCopies()
        {
            return _ctlPrintCopies;
        }


        public void SaveToConfiguration()
        {
            Properties.Settings settings = Properties.Settings.Default;

            if (Id == "POST0")
            {
                settings.Post0Name = Name;
                //settings.Post0Post = Id;
                settings.Post0Caption = Caption;
                settings.Post0Enabled = Enabled;
                settings.Post0Visible = Visible;
                settings.Post0BtnImgOn = BtnImageOn;
                settings.Post0BtnImgOff = BtnImageOff;
                settings.Post0PrintCopies = (short)PrintCopies;
                settings.Post0PrintHeader = PrintHeader;
            }
            else if (Id == "POST1")
            {
                settings.Post1Name = Name;
                //settings.Post1Post = Id;
                settings.Post1Caption = Caption;
                settings.Post1Enabled = Enabled;
                settings.Post1Visible = Visible;
                settings.Post1BtnImgOn = BtnImageOn;
                settings.Post1BtnImgOff = BtnImageOff;
                settings.Post1PrintCopies = (short)PrintCopies;
                settings.Post1PrintHeader = PrintHeader;
            }
            else if (Id == "POST2")
            {
                settings.Post2Name = Name;
                //settings.Post2Post = Id;
                settings.Post2Caption = Caption;
                settings.Post2Enabled = Enabled;
                settings.Post2Visible = Visible;
                settings.Post2BtnImgOn = BtnImageOn;
                settings.Post2BtnImgOff = BtnImageOff;
                settings.Post2PrintCopies = (short)PrintCopies;
                settings.Post2PrintHeader = PrintHeader;
            }
            else if (Id == "POST3")
            {
                settings.Post3Name = Name;
                //settings.Post3Post = Id;
                settings.Post3Caption = Caption;
                settings.Post3Enabled = Enabled;
                settings.Post3Visible = Visible;
                settings.Post3BtnImgOn = BtnImageOn;
                settings.Post3BtnImgOff = BtnImageOff;
                settings.Post3PrintCopies = (short)PrintCopies;
                settings.Post3PrintHeader = PrintHeader;
            }
            else if (Id == "POST4")
            {
                settings.Post4Name = Name;
                //settings.Post4Post = Id;
                settings.Post4Caption = Caption;
                settings.Post4Enabled = Enabled;
                settings.Post4Visible = Visible;
                settings.Post4BtnImgOn = BtnImageOn;
                settings.Post4BtnImgOff = BtnImageOff;
                settings.Post4PrintCopies = (short)PrintCopies;
                settings.Post4PrintHeader = PrintHeader;
            }
            else if (Id == "POST5")
            {
                settings.Post5Name = Name;
                //settings.Post5Post = Id;
                settings.Post5Caption = Caption;
                settings.Post5Enabled = Enabled;
                settings.Post5Visible = Visible;
                settings.Post5BtnImgOn = BtnImageOn;
                settings.Post5BtnImgOff = BtnImageOff;
                settings.Post5PrintCopies = (short)PrintCopies;
                settings.Post5PrintHeader = PrintHeader;
            }
            else if(Id == "POST6")
            {
                settings.Post6Name = Name;
                //settings.Post6Post = Id;
                settings.Post6Caption = Caption;
                settings.Post6Enabled = Enabled;
                settings.Post6Visible = Visible;
                settings.Post6BtnImgOn = BtnImageOn;
                settings.Post6BtnImgOff = BtnImageOff;
                settings.Post6PrintCopies = (short)PrintCopies;
                settings.Post6PrintHeader = PrintHeader;
            }
            else if (Id == "POST7")
            {
                settings.Post7Name = Name;
                //settings.Post7Post = Id;
                settings.Post7Caption = Caption;
                settings.Post7Enabled = Enabled;
                settings.Post7Visible = Visible;
                settings.Post7BtnImgOn = BtnImageOn;
                settings.Post7BtnImgOff = BtnImageOff;
                settings.Post7PrintCopies = (short)PrintCopies;
                settings.Post7PrintHeader = PrintHeader;
            }
            else if (Id == "POST8")
            {
                settings.Post8Name = Name;
                //settings.Post8Post = Id;
                settings.Post8Caption = Caption;
                settings.Post8Enabled = Enabled;
                settings.Post8Visible = Visible;
                settings.Post8BtnImgOn = BtnImageOn;
                settings.Post8BtnImgOff = BtnImageOff;
                settings.Post8PrintCopies = (short)PrintCopies;
                settings.Post8PrintHeader = PrintHeader;
            }
            else if (Id == "POST9")
            {
                settings.Post9Name = Name;
                //settings.Post9Post = Id;
                settings.Post9Caption = Caption;
                settings.Post9Enabled = Enabled;
                settings.Post9Visible = Visible;
                settings.Post9BtnImgOn = BtnImageOn;
                settings.Post9BtnImgOff = BtnImageOff;
                settings.Post9PrintCopies = (short)PrintCopies;
                settings.Post9PrintHeader = PrintHeader;
            }
        }
        public void LoadFromConfiguration()
        {
            Properties.Settings settings = Properties.Settings.Default;

            if ( Id ==  "POST0" )
            {
                Index = 0;
                Name = settings.Post0Name;
                Id = settings.Post0Post;
                Caption = settings.Post0Caption;
                Enabled = settings.Post0Enabled;
                Visible = settings.Post0Visible;
                BtnImageOn = settings.Post0BtnImgOn;
                BtnImageOff = settings.Post0BtnImgOff;
                PrintCopies = (int)settings.Post0PrintCopies;
                PrintHeader = settings.Post0PrintHeader;
            }
            else if (Id == "POST1")
            {
                Index = 1;
                Name = settings.Post1Name;
                Id = settings.Post1Post;
                Caption = settings.Post1Caption;
                Enabled = settings.Post1Enabled;
                Visible = settings.Post1Visible;
                BtnImageOn = settings.Post1BtnImgOn;
                BtnImageOff = settings.Post1BtnImgOff;
                PrintCopies = (int)settings.Post1PrintCopies;
                PrintHeader = settings.Post1PrintHeader;
            }
            else if (Id == "POST2")
            {
                Index = 2;
                Name = settings.Post2Name;
                Id = settings.Post2Post;
                Caption = settings.Post2Caption;
                Enabled = settings.Post2Enabled;
                Visible = settings.Post2Visible;
                BtnImageOn = settings.Post2BtnImgOn;
                BtnImageOff = settings.Post2BtnImgOff;
                PrintCopies = (int)settings.Post2PrintCopies;
                PrintHeader = settings.Post2PrintHeader;
            }
            else if (Id == "POST3")
            {
                Index = 3;
                Name = settings.Post3Name;
                Id = settings.Post3Post;
                Caption = settings.Post3Caption;
                Enabled = settings.Post3Enabled;
                Visible = settings.Post3Visible;
                BtnImageOn = settings.Post3BtnImgOn;
                BtnImageOff = settings.Post3BtnImgOff;
                PrintCopies = (int)settings.Post3PrintCopies;
                PrintHeader = settings.Post3PrintHeader;
            }
            else if (Id == "POST4")
            {
                Index = 4;
                Name = settings.Post4Name;
                Id = settings.Post4Post;
                Caption = settings.Post4Caption;
                Enabled = settings.Post4Enabled;
                Visible = settings.Post4Visible;
                BtnImageOn = settings.Post4BtnImgOn;
                BtnImageOff = settings.Post4BtnImgOff;
                PrintCopies = (int)settings.Post4PrintCopies;
                PrintHeader = settings.Post4PrintHeader;
            }
            else if (Id == "POST5")
            {
                Index = 5;
                Name = settings.Post5Name;
                Id = settings.Post5Post;
                Caption = settings.Post5Caption;
                Enabled = settings.Post5Enabled;
                Visible = settings.Post5Visible;
                BtnImageOn = settings.Post5BtnImgOn;
                BtnImageOff = settings.Post5BtnImgOff;
                PrintCopies = (int)settings.Post5PrintCopies;
                PrintHeader = settings.Post5PrintHeader;
            }
            else if (Id == "POST6")
            {
                Index = 6;
                Name = settings.Post6Name;
                Id = settings.Post6Post;
                Caption = settings.Post6Caption;
                Enabled = settings.Post6Enabled;
                Visible = settings.Post6Visible;
                BtnImageOn = settings.Post6BtnImgOn;
                BtnImageOff = settings.Post6BtnImgOff;
                PrintCopies = (int)settings.Post6PrintCopies;
                PrintHeader = settings.Post6PrintHeader;
            }
            else if (Id == "POST7")
            {
                Index = 7;
                Name = settings.Post7Name;
                Id = settings.Post7Post;
                Caption = settings.Post7Caption;
                Enabled = settings.Post7Enabled;
                Visible = settings.Post7Visible;
                BtnImageOn = settings.Post7BtnImgOn;
                BtnImageOff = settings.Post7BtnImgOff;
                PrintCopies = (int)settings.Post7PrintCopies;
                PrintHeader = settings.Post7PrintHeader;
            }
            else if (Id == "POST8")
            {
                Index = 8;
                Name = settings.Post8Name;
                Id = settings.Post8Post;
                Caption = settings.Post8Caption;
                Enabled = settings.Post8Enabled;
                Visible = settings.Post8Visible;
                BtnImageOn = settings.Post8BtnImgOn;
                BtnImageOff = settings.Post8BtnImgOff;
                PrintCopies = (int)settings.Post8PrintCopies;
                PrintHeader = settings.Post8PrintHeader;
            }
            else if (Id == "POST9")
            {
                Index = 9;
                Name = settings.Post9Name;
                Id = settings.Post9Post;
                Caption = settings.Post9Caption;
                Enabled = settings.Post9Enabled;
                Visible = settings.Post9Visible;
                BtnImageOn = settings.Post9BtnImgOn;
                BtnImageOff = settings.Post9BtnImgOff;
                PrintCopies = (int)settings.Post9PrintCopies;
                PrintHeader = settings.Post9PrintHeader;
            }
        }
    };


    public class PostPropertyCollection : Dictionary<string, PostProperty>
    {
        public PostProperty FindById(string id)
        {
            if (TryGetValue(id, out PostProperty property))
            {
                return property;
            }
            return null;
        }

        public void LoadFromConfiguration()
        {
            foreach (KeyValuePair<string, PostProperty> kv in this)
            {
                var prop = kv.Value;
                prop.LoadFromConfiguration();
            }
        }

        public void SaveToConfiguration()
        {
            foreach (KeyValuePair<string, PostProperty> kv in this)
            {
                var prop = kv.Value;
                prop.SaveToConfiguration();
            }
        }
    }

} // namespace Tobasa