﻿/*
ImageGlass Project - Image viewer for Windows
Copyright (C) 2020 DUONG DIEU PHAP
Project homepage: http://imageglass.org

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ImageGlass.UI
{
    public class InputBox
    {
        /// <summary>
        /// Get, set the message user inputs
        /// </summary>
        public static string Message { get; private set; } = "";

        /// <summary>
        /// Character filter for numbers: disallow non-numeric characters
        /// </summary>
        /// <param name="keyval"></param>
        /// <returns></returns>
        private static bool NumberFilter(char keyval)
        {
            return (char.IsDigit(keyval) || keyval == (char) Keys.Back);
        }


        /// <summary>
        /// Character filter for filenames: disallow invalid filename characters
        /// </summary>
        /// <param name="keyval"></param>
        /// <returns></returns>
        private static bool FilenameFilter(char keyval)
        {
            var badChars = Path.GetInvalidFileNameChars();
            bool invalid = badChars.Contains(keyval);
            return !invalid || keyval == (char) Keys.Back;
        }

        /// <summary>
        /// Show input dialog box
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="message">Message</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="isNumberOnly">Number input</param>
        /// <param name="topMost">Set the form to top most</param>
        /// <param name="isFilename">Filename input</param>
        /// <returns></returns>
        public static DialogResult ShowDialog(string title, string message, string defaultValue, bool @isNumberOnly = false, bool @topMost = false, bool isFilename = false)
        {
            frmDialogBox f = new frmDialogBox(title, message)
            {
                Content = defaultValue,
                TopMost = topMost
            };

            if (isNumberOnly)
            {
                f.Filter = NumberFilter;
                f.MaxLimit = 10;
            }

            if (isFilename)
            {
                f.Filter = FilenameFilter;
                f.MaxLimit = 256;
            }

            if (f.ShowDialog() == DialogResult.OK)
            {
                //Save input data
                InputBox.Message = f.Content;
            }

            return f.DialogResult;
        }



    }
}
