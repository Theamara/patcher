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
using System.Linq;
using System.Text;

namespace Patcher.Rules.Compiled.Fields.Skyrim
{
    /// <summary>
    /// Represents a set of alpha channel for each of the four parts of the day: sunrise, day, sunset and night.
    /// </summary>
    public interface IWeatherAlphaQuad
    {
        /// <summary>
        /// Gets or sets the alpha channel during the sunrise.
        /// </summary>
        float Sunrise { get; set; }
        /// <summary>
        /// Gets or sets the alpha channel during the day.
        /// </summary>
        float Day { get; set; }
        /// <summary>
        /// Gets or sets the alpha channel during the sunset.
        /// </summary>
        float Sunset { get; set; }
        /// <summary>
        /// Gets or sets the alpha channel during the night.
        /// </summary>
        float Night { get; set; }
    }
}
