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
        public virtual Color baseTextBrandLogoColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");

        public virtual Color baseInfoTextBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#013150");
        public virtual Color basePostTextColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#444444");
        public virtual Color textInfoStrip0BackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#0B5485");
        public virtual Color textInfoStrip1BackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#0c5d93");
        public virtual Color textInfoStrip0Color { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public virtual Color textInfoStrip1Color { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");

        public virtual Color labelJobFinishedColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#444444");
        public virtual Color centerMainInfoBoxColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#444444");
        public virtual Color centerMainInfoBoxBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#fff4e1");
        public virtual Color centerMainInfoBoxCaptionBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#FFDE59");
        public virtual Color labelFinishEvenRowBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");

        public virtual Color leftRigthTopQueueNoColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public virtual Color leftRigthTopCounterColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public virtual Color postPanelBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public virtual Color postCaptionColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#013150");       // fore color
        public virtual Color postCaptionBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#9ccff0");
        public virtual Color postQueueNoBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public virtual Color postCounterNoBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#CFEAFA");
        public virtual Color postTotalQueueLabelBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#fff4e1");
        public virtual Color postTotalQueueValueBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#fff4e1");
        public virtual Color bottomDivBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#CFEAFA");
        public virtual Color bottomDivForeColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#013150");     // fore color
    }

    public class ThemeClassic : DisplayTheme
    {
        public override Color baseBackgroundColor { get; set; } = System.Drawing.Color.Transparent;
        public override Color baseTextColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#014215");             // fore color
        public override Color basetTextLightColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color baseTextBrandLogoColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");

        public override Color baseInfoTextBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#0F4C22");
        public override Color basePostTextColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#444444");
        public override Color textInfoStrip0BackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#14682E");
        public override Color textInfoStrip1BackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#1A853C");
        public override Color textInfoStrip0Color { get; set; } = System.Drawing.ColorTranslator.FromHtml("#014215");
        public override Color textInfoStrip1Color { get; set; } = System.Drawing.ColorTranslator.FromHtml("#014215");

        public override Color labelJobFinishedColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#444444");
        public override Color centerMainInfoBoxColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#444444");
        public override Color centerMainInfoBoxBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#DBF1FF");
        public override Color centerMainInfoBoxCaptionBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#FFDE59");
        public override Color labelFinishEvenRowBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");

        public override Color leftRigthTopQueueNoColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color leftRigthTopCounterColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color postPanelBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color postCaptionColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#014215");          // fore color
        public override Color postCaptionBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#87D5A0");
        public override Color postQueueNoBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color postCounterNoBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#D2FADF");
        public override Color postTotalQueueLabelBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#DBF1FF");
        public override Color postTotalQueueValueBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#DBF1FF");
        public override Color bottomDivBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#D2FADF");
        public override Color bottomDivForeColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#014215");       // fore color
    }

    // Green Theme
    public class ThemeGreen : DisplayTheme
    {
        public override Color baseBackgroundColor { get; set; } = ColorTranslator.FromHtml("#006721");
        public override Color baseTextColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#014215");             // fore color
        public override Color basetTextLightColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color baseTextBrandLogoColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");

        public override Color baseInfoTextBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#0F4C22"); 
        public override Color basePostTextColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#444444");
        public override Color textInfoStrip0BackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#14682E");
        public override Color textInfoStrip1BackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#1A853C");
        public override Color textInfoStrip0Color { get; set; } = System.Drawing.ColorTranslator.FromHtml("#014215");
        public override Color textInfoStrip1Color { get; set; } = System.Drawing.ColorTranslator.FromHtml("#014215");

        public override Color labelJobFinishedColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#444444");
        public override Color centerMainInfoBoxColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#444444");
        public override Color centerMainInfoBoxBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#DBF1FF");
        public override Color centerMainInfoBoxCaptionBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#FFDE59");
        public override Color labelFinishEvenRowBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");

        public override Color leftRigthTopQueueNoColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color leftRigthTopCounterColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color postPanelBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color postCaptionColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#014215");          // fore color
        public override Color postCaptionBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#87D5A0");
        public override Color postQueueNoBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color postCounterNoBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#D2FADF");
        public override Color postTotalQueueLabelBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#DBF1FF");
        public override Color postTotalQueueValueBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#DBF1FF");
        public override Color bottomDivBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#D2FADF");
        public override Color bottomDivForeColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#014215");       // fore color
    }

    // Dark Theme
    public class ThemeDark : DisplayTheme
    {
        public override Color baseBackgroundColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#0F1112");
        public override Color baseTextColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color basetTextLightColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color baseTextBrandLogoColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");

        public override Color baseInfoTextBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#2B3338");
        public override Color basePostTextColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#D6D7D9");
        public override Color textInfoStrip0BackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#282B2D");
        public override Color textInfoStrip1BackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#1C1E1E");
        public override Color textInfoStrip0Color { get; set; } = System.Drawing.ColorTranslator.FromHtml("#F9DC85");
        public override Color textInfoStrip1Color { get; set; } = System.Drawing.ColorTranslator.FromHtml("#F9DC85");

        public override Color labelJobFinishedColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#D6D7D9");
        public override Color centerMainInfoBoxColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#D6D7D9");
        public override Color centerMainInfoBoxBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#33393D");
        public override Color centerMainInfoBoxCaptionBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#0F1112");
        public override Color labelFinishEvenRowBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#2B3338");

        public override Color leftRigthTopQueueNoColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color leftRigthTopCounterColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color postPanelBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#23282A");
        public override Color postCaptionColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#FFD965");
        public override Color postCaptionBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#1B1E20");
        public override Color postQueueNoBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#23272A");
        public override Color postCounterNoBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#33393D");
        public override Color postTotalQueueLabelBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#2C2F30"); //e4ffd3 e4f4ff
        public override Color postTotalQueueValueBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#2C2F30");
        public override Color bottomDivBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#0F1112");
        public override Color bottomDivForeColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
    }

    public class ThemeRed : DisplayTheme
    {
        public override Color baseBackgroundColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#E4080A");
        public override Color baseTextColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#9D0506");
        public override Color basetTextLightColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color baseTextBrandLogoColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");

        public override Color baseInfoTextBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#810304");
        public override Color basePostTextColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#444444");
        public override Color textInfoStrip0BackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#A10304");
        public override Color textInfoStrip1BackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#C00405");
        public override Color textInfoStrip0Color { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color textInfoStrip1Color { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");

        public override Color labelJobFinishedColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#444444");
        public override Color centerMainInfoBoxColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#444444");
        public override Color centerMainInfoBoxBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#A7DCFC");
        public override Color centerMainInfoBoxCaptionBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#67C3F9");
        public override Color labelFinishEvenRowBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");

        public override Color leftRigthTopQueueNoColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color leftRigthTopCounterColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color postPanelBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#23282A");
        public override Color postCaptionColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#E4080A");
        public override Color postCaptionBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#FFDDDF");
        public override Color postQueueNoBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color postCounterNoBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#FFECED");
        public override Color postTotalQueueLabelBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#CFEAFA"); 
        public override Color postTotalQueueValueBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#CFEAFA");
        public override Color bottomDivBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#FFECED");
        public override Color bottomDivForeColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#9D0506");
    }

    public class ThemeOrange : DisplayTheme
    {
        public override Color baseBackgroundColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#FE9900");
        public override Color baseTextColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#764802");
        public override Color basetTextLightColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color baseTextBrandLogoColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");

        public override Color baseInfoTextBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#633D02");
        public override Color basePostTextColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#444444");
        public override Color textInfoStrip0BackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#794A04");
        public override Color textInfoStrip1BackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#8E5603");
        public override Color textInfoStrip0Color { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color textInfoStrip1Color { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");

        public override Color labelJobFinishedColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#444444");
        public override Color centerMainInfoBoxColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#444444");
        public override Color centerMainInfoBoxBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#E0FDD6");
        public override Color centerMainInfoBoxCaptionBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#90E16E");
        public override Color labelFinishEvenRowBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");

        public override Color leftRigthTopQueueNoColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color leftRigthTopCounterColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color postPanelBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#23282A");
        public override Color postCaptionColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#764802");
        public override Color postCaptionBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#FFE5C1");
        public override Color postQueueNoBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        public override Color postCounterNoBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#FFF0DB");
        public override Color postTotalQueueLabelBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#E0FDD6");
        public override Color postTotalQueueValueBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#E0FDD6");
        public override Color bottomDivBackColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#FFF0DB");
        public override Color bottomDivForeColor { get; set; } = System.Drawing.ColorTranslator.FromHtml("#764802");
    }

}
