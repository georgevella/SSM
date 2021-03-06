﻿using System;
using Glyde.Web.Api.Resources;

namespace SiteSpeedManager.Models.Resources.V1
{
    [Resource("jobs")]
    public class SiteSpeedJobResource : Resource<string>
    {
        public string Site { get; set; }

        public string Path { get; set; }

        public string Crontab { get; set; }

        public DateTime NextExecutionTime { get; set; }
    }
}