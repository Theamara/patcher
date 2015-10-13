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

using Patcher.Data.Plugins.Content.Records.Skyrim;
using Patcher.Rules.Compiled.Forms;
using Patcher.Rules.Compiled.Forms.Skyrim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Patcher.Data.Plugins.Content.Fields.Skyrim;
using Patcher.Rules.Compiled.Constants.Skyrim;
using Patcher.Rules.Proxies.Fields.Skyrim;
using Patcher.Rules.Compiled.Fields.Skyrim;
using Patcher.Data.Plugins.Content.Records;

namespace Patcher.Rules.Proxies.Forms.Skyrim
{
    [Proxy(typeof(IArmo))]
    public sealed class ArmoProxy : SkyrimFormProxy<Armo>, IArmo
    {
        public IScriptCollection Scripts { get { return GetVirtualMachineAdapterProxy(record); } set { SetVirtualMachineAdapterProxy(record, value); } }
        public IObjectBounds ObjectBounds { get { return GetObjectBoundsProxy(record); } set { SetObjectBoundsProxy(record, value); } }
        public string FullName { get { EnsureReadable(); return record.FullName; } set { EnsureWritable(); record.FullName = value; } }
        public IForm Enchantment { get { return Provider.CreateReferenceProxy<IForm>(record.Enchantment); } set { record.Enchantment = value.ToFormId(); } }
        public string MaleWorldModel { get { EnsureReadable(); return record.MaleWorldModel; } set { EnsureWritable(); record.MaleWorldModel = value; } }
        public string FemaleWorldModel { get { EnsureReadable(); return record.FemaleWorldModel; } set { EnsureWritable(); record.FemaleWorldModel = value; } }
        public Skill Skill { get { return record.SkillUsage.ToSkill(); } set { record.SkillUsage = value.ToArmorSkillUsage(); } }
        public BodyNodes BodyNodes { get { return record.BodyParts.ToBodyNodes(); } set { EnsureWritable(); record.BodyParts = value.ToBodyParts(); } }
        public bool IsPlayable { get { EnsureReadable(); return record.IsPlayable; } set { EnsureWritable(); record.IsPlayable = value; } }
        public bool IsShield { get { EnsureReadable(); return record.IsShield; } set { EnsureWritable(); record.IsShield = value; } }
        public IForm PickUpSound { get { return Provider.CreateReferenceProxy<IForm>(record.PickUpSound); } set { EnsureWritable(); record.PickUpSound = value.ToFormId(); } }
        public IForm PutDownSound { get { return Provider.CreateReferenceProxy<IForm>(record.PutDownSound); } set { record.PutDownSound = value.ToFormId(); } }
        public IForm EquipType { get { return Provider.CreateReferenceProxy<IForm>(record.EquipType); } set { record.EquipType = value.ToFormId(); } }
        public IForm BlockImpactDataSet { get { return Provider.CreateReferenceProxy<IForm>(record.BlockImpactDataSet); } set { record.BlockImpactDataSet = value.ToFormId(); } }
        public IForm AlternateBlockMaterial { get { return Provider.CreateReferenceProxy<IForm>(record.AlternateBlockMaterial); } set { record.AlternateBlockMaterial = value.ToFormId(); } }
        public IForm Race { get { return Provider.CreateReferenceProxy<IForm>(record.EquipType); } set { record.EquipType = value.ToFormId(); } }
        public IFormCollection<IKywd> Keywords { get { return Provider.CreateFormCollectionProxy<IKywd>(Mode, record.Keywords.Items); } set { record.Keywords.Items = value.ToFormIdList(); } }
        public string Description { get { EnsureReadable(); return record.Description; } set { EnsureWritable(); record.Description = value; } }
        public IFormCollection<IForm> Models { get { return Provider.CreateFormCollectionProxy<IForm>(Mode, record.Models); } set { record.Models = value.ToFormIdList(); } }
        public int Value { get { EnsureReadable(); return record.Value; } set { EnsureWritable(); record.Value = value; } }
        public float Weight { get { EnsureReadable(); return record.Weight; } set { EnsureWritable(); record.Weight = value; } }
        public float ArmorRating { get { EnsureReadable(); return record.ArmorRating; } set { EnsureWritable(); record.ArmorRating = value; } }
        public IArmo TemplateArmor { get { return Provider.CreateReferenceProxy<IArmo>(record.TemplateArmor); } set { record.TemplateArmor = value.ToFormId(); } }
    }
}
