﻿using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace RuiChen.AbpPro.Platform
{
    public class LayoutCreateOrUpdateDto
    {
        [Required]
        [DynamicStringLength(typeof(RouteConsts), nameof(RouteConsts.MaxNameLength))]
        public string Name { get; set; }

        [Required]
        [DynamicStringLength(typeof(RouteConsts), nameof(RouteConsts.MaxDisplayNameLength))]
        public string DisplayName { get; set; }

        [DynamicStringLength(typeof(RouteConsts), nameof(RouteConsts.MaxDescriptionLength))]
        public string Description { get; set; }

        [Required]
        [DynamicStringLength(typeof(RouteConsts), nameof(RouteConsts.MaxPathLength))]
        public string Path { get; set; }

        [DynamicStringLength(typeof(RouteConsts), nameof(RouteConsts.MaxRedirectLength))]
        public string Redirect { get; set; }
    }
}
