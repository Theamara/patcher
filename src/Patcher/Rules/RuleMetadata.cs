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

namespace Patcher.Rules
{
    /// <summary>
    /// Provides contextual information about a specific rule.
    /// </summary>
    public class RuleMetadata
    {
        /// <summary>
        /// Gets or sets the file name of the plugin associated with this rule.
        /// </summary>
        public string PluginFileName { get; set; }

        /// <summary>
        /// Gets or sets the file name of the rule file from which this rule has been loaded.
        /// </summary>
        public string RuleFileName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
