﻿/// Copyright(C) 2015 Unforbidable Works
///
/// This program is free software; you can redistribute it and/or
/// modify it under the terms of the GNU General Public License
/// as published by the Free Software Foundation; either version 2
/// of the License, or(at your option) any later version.
///
/// This program is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
/// GNU General Public License for more details.
///
/// You should have received a copy of the GNU General Public License
/// along with this program; if not, write to the Free Software
/// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Patcher.IO
{
    /// <summary>
    /// Creates a memory stream that can be only read from and the original buffer of which can be shared.
    /// </summary>
    public class ReadOnlyMemoryStream : MemoryStream
    {
        readonly byte[] buffer;
        public byte[] OrigialBuffer { get { return buffer; } }

        public ReadOnlyMemoryStream(byte[] buffer)
            : base(buffer, false)
        {
            this.buffer = buffer; 
        }
    }
}
