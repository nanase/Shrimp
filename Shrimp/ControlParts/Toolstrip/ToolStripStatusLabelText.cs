﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Shrimp.ControlParts.Toolstrip
{
    class ToolStripStatusLabelText
    {
        private readonly string _text;
        private readonly Bitmap _image;

        public ToolStripStatusLabelText ( string text, Bitmap image )
        {
            this._text = text;
            this._image = image;
        }

        public string text
        {
            get { return this._text; }
        }

        public Bitmap image
        {
            get { return this._image; }
        }
    }
}