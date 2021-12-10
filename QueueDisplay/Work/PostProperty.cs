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

using System.Collections.Generic;

namespace Tobasa
{
    public class PostProperty
    {
        public PostProperty(string id)
        {
            Id = id;
            LoadFromConfiguration();
        }

        public string Name { get; set; }
        public string Id { get; set; }
        public string Caption { get; set; }
        public string RunText { get; set; }
        public bool Visible { get; set; }
        public bool PlayAudio { get; set; }
        public int Index { get; set; }

        public void SaveToConfiguration()
        {
            Properties.Settings settings = Properties.Settings.Default;

            if (Id == "POST0")
            {
                settings.Post0Name = Name;
                //settings.Post0Post = Id;
                settings.Post0Caption = Caption;
                settings.Post0RunText = RunText;
                settings.Post0Visible = Visible;
                settings.Post0PlayAudio = PlayAudio;
            }
            else if (Id == "POST1")
            {
                settings.Post1Name = Name;
                //settings.Post1Post = Id;
                settings.Post1Caption = Caption;
                settings.Post1RunText = RunText;
                settings.Post1Visible = Visible;
                settings.Post1PlayAudio = PlayAudio;
            }
            else if (Id == "POST2")
            {
                settings.Post2Name = Name;
                //settings.Post2Post = Id;
                settings.Post2Caption = Caption;
                settings.Post2RunText = RunText;
                settings.Post2Visible = Visible;
                settings.Post2PlayAudio = PlayAudio;
            }
            else if (Id == "POST3")
            {
                settings.Post3Name = Name;
                //settings.Post3Post = Id;
                settings.Post3Caption = Caption;
                settings.Post3RunText = RunText;
                settings.Post3Visible = Visible;
                settings.Post3PlayAudio = PlayAudio;
            }
            else if (Id == "POST4")
            {
                settings.Post4Name = Name;
                //settings.Post4Post = Id;
                settings.Post4Caption = Caption;
                settings.Post4RunText = RunText;
                settings.Post4Visible = Visible;
                settings.Post4PlayAudio = PlayAudio;
            }
            else if (Id == "POST5")
            {
                settings.Post5Name = Name;
                //settings.Post5Post = Id;
                settings.Post5Caption = Caption;
                settings.Post5RunText = RunText;
                settings.Post5Visible = Visible;
                settings.Post5PlayAudio = PlayAudio;
            }
            else if(Id == "POST6")
            {
                settings.Post6Name = Name;
                //settings.Post6Post = Id;
                settings.Post6Caption = Caption;
                settings.Post6RunText = RunText;
                settings.Post6Visible = Visible;
                settings.Post6PlayAudio = PlayAudio;
            }
            else if (Id == "POST7")
            {
                settings.Post7Name = Name;
                //settings.Post7Post = Id;
                settings.Post7Caption = Caption;
                settings.Post7RunText = RunText;
                settings.Post7Visible = Visible;
                settings.Post7PlayAudio = PlayAudio;
            }
            else if (Id == "POST8")
            {
                settings.Post8Name = Name;
                //settings.Post8Post = Id;
                settings.Post8Caption = Caption;
                settings.Post8RunText = RunText;
                settings.Post8Visible = Visible;
                settings.Post8PlayAudio = PlayAudio;
            }
            else if (Id == "POST9")
            {
                settings.Post9Name = Name;
                //settings.Post9Post = Id;
                settings.Post9Caption = Caption;
                settings.Post9RunText = RunText;
                settings.Post9Visible = Visible;
                settings.Post9PlayAudio = PlayAudio;
            }
            else
            {
                // do nothing
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
                RunText = settings.Post0RunText;
                Visible = settings.Post0Visible;
                PlayAudio = settings.Post0PlayAudio;
            }
            else if (Id == "POST1")
            {
                Index = 1;
                Name = settings.Post1Name;
                Id = settings.Post1Post;
                Caption = settings.Post1Caption;
                RunText = settings.Post1RunText;
                Visible = settings.Post1Visible;
                PlayAudio = settings.Post1PlayAudio;
            }
            else if (Id == "POST2")
            {
                Index = 2;
                Name = settings.Post2Name;
                Id = settings.Post2Post;
                Caption = settings.Post2Caption;
                RunText = settings.Post2RunText;
                Visible = settings.Post2Visible;
                PlayAudio = settings.Post2PlayAudio;
            }
            else if (Id == "POST3")
            {
                Index = 3;
                Name = settings.Post3Name;
                Id = settings.Post3Post;
                Caption = settings.Post3Caption;
                RunText = settings.Post3RunText;
                Visible = settings.Post3Visible;
                PlayAudio = settings.Post3PlayAudio;
            }
            else if (Id == "POST4")
            {
                Index = 4;
                Name = settings.Post4Name;
                Id = settings.Post4Post;
                Caption = settings.Post4Caption;
                RunText = settings.Post4RunText;
                Visible = settings.Post4Visible;
                PlayAudio = settings.Post4PlayAudio;
            }
            else if (Id == "POST5")
            {
                Index = 5;
                Name = settings.Post5Name;
                Id = settings.Post5Post;
                Caption = settings.Post5Caption;
                RunText = settings.Post5RunText;
                Visible = settings.Post5Visible;
                PlayAudio = settings.Post5PlayAudio;
            }
            else if (Id == "POST6")
            {
                Index = 6;
                Name = settings.Post6Name;
                Id = settings.Post6Post;
                Caption = settings.Post6Caption;
                RunText = settings.Post6RunText;
                Visible = settings.Post6Visible;
                PlayAudio = settings.Post6PlayAudio;
            }
            else if (Id == "POST7")
            {
                Index = 7;
                Name = settings.Post7Name;
                Id = settings.Post7Post;
                Caption = settings.Post7Caption;
                RunText = settings.Post7RunText;
                Visible = settings.Post7Visible;
                PlayAudio = settings.Post7PlayAudio;
            }
            else if (Id == "POST8")
            {
                Index = 8;
                Name = settings.Post8Name;
                Id = settings.Post8Post;
                Caption = settings.Post8Caption;
                RunText = settings.Post8RunText;
                Visible = settings.Post8Visible;
                PlayAudio = settings.Post8PlayAudio;
            }
            else if (Id == "POST9")
            {
                Index = 9;
                Name = settings.Post9Name;
                Id = settings.Post9Post;
                Caption = settings.Post9Caption;
                RunText = settings.Post9RunText;
                Visible = settings.Post9Visible;
                PlayAudio = settings.Post9PlayAudio;
            }
            else
            {
                Index = -1;
                Name = "INVALID";
                Id = "";
                Caption = "";
                RunText = "";
                Visible = false;
                PlayAudio = false;
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