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

namespace Tobasa.Dto
{
    public class Post
    {
        public string NameOld { get; set; }
        public string Name { get; set; }
        public string NumberPrefix { get; set; }
        public string Keterangan { get; set; }
        public int Quota0 { get; set; } = 1000;
        public int Quota1 { get; set; } = 1000;
    }
}
