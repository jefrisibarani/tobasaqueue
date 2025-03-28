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
using System.ComponentModel;
using System.Drawing;

namespace Tobasa
{

    // Light Blue Theme
    public class DisplayTheme
    { 
        public virtual Color baseBackgroundColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#0c5d93");    
        public virtual Color baseTextColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#013150");          // fore color
        public virtual Color basetTextLightColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public virtual Color basePostCaptionBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public virtual Color bottomDivBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#CFEAFA");
        public virtual Color bottomDivForeColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#013150");     // fore color
    }

    // Green Theme
    public class ThemeGreen : DisplayTheme
    {
        public override Color baseBackgroundColor { get; set; } = ColorTranslator.FromHtml("#006721");
        public override Color baseTextColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#014215");             // fore color
        public override Color basetTextLightColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color basePostCaptionBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color bottomDivBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#D2FADF");
        public override Color bottomDivForeColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#014215");       // fore color
    }

    // Dark Theme
    public class ThemeDark : DisplayTheme
    {
        public override Color baseBackgroundColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#0F1112");
        public override Color baseTextColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color basetTextLightColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color basePostCaptionBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#302F2E");
        public override Color bottomDivBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#272727");
        public override Color bottomDivForeColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
    }

    public class ThemeRed : DisplayTheme
    {
        public override Color baseBackgroundColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#E4080A");
        public override Color baseTextColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#9D0506");
        public override Color basetTextLightColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color basePostCaptionBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color bottomDivBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#FFECED");
        public override Color bottomDivForeColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#9D0506");
    }

    public class ThemeOrange : DisplayTheme
    {
        public override Color baseBackgroundColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#FE9900");
        public override Color baseTextColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#764802");
        public override Color basetTextLightColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color basePostCaptionBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color bottomDivBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#FFF0DB");
        public override Color bottomDivForeColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#764802");
    }

}
