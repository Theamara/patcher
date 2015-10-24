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

namespace Patcher.Rules.Compiled.Forms.Skyrim
{
    /// <summary>
    /// Represents a <b>Keyword</b> form.
    /// </summary>
    public interface IKywd : IForm
    {
        /// <summary>
        /// Gets or sets the color of this <b>Keyword</b>.
        /// </summary>
        uint Color { get; set; }
    }
}
