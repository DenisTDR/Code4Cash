﻿using Code4Cash.Data.Models.Enums;
using Code4Cash.Data.Models.ViewModels.Base;

namespace Code4Cash.Data.Models.ViewModels
{
    public class AssetViewModel : ViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AssetValue Value { get; set; }
    }
}