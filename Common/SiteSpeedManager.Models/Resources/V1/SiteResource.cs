﻿using System.Collections.Generic;
using Glyde.Web.Api.Resources;

namespace SiteSpeedManager.Models.Resources.V1
{
    [Resource("sites")]
    public class SiteResource : Resource<int>
    {
        public string Domain { get; set; }

        public bool IsEnabled { get; set; }

        public List<PageResource> Pages { get; set; } = new List<PageResource>();

        public List<string> Countries { get; set; } = new List<string>();
    }
}