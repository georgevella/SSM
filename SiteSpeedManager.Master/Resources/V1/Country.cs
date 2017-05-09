﻿using Glyde.Web.Api.Resources;

namespace SiteSpeedManager.Master.Resources.V1
{
    [Resource("countries")]
    public class Country : Resource<string>
    {
        public string DisplayName { get; set; }

        public bool IsEnabled { get; set; }
    }
}