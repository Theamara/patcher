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

using Patcher.Data.Plugins.Content.Records;
using Patcher.Rules.Compiled.Fields;
using Patcher.Rules.Compiled.Forms.Skyrim;
using Patcher.Rules.Proxies.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patcher.Rules.Proxies.Forms.Skyrim
{
    [Proxy(typeof(IKywd))]
    public sealed class KywdProxy : FormProxy<Kywd>, IKywd
    {
        public IColor Color
        {
            get
            {
                return Provider.CreateProxy<ColorProxy>(Mode).With(record.Color);
            }
            set
            {
                EnsureWritable();
                record.Color.Red = value.Red;
                record.Color.Green = value.Green;
                record.Color.Blue = value.Blue;
            }
        }
    }
}
