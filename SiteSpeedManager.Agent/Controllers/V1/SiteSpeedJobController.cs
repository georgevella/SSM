﻿using System;
using System.Threading.Tasks;
using Glyde.Web.Api.Controllers;
using Glyde.Web.Api.Controllers.Results;
using SiteSpeedController.Agent.Core;
using SiteSpeedController.Agent.Resources.V1;

namespace SiteSpeedController.Agent.Controllers.V1
{
    public class SiteSpeedJobController : ApiController<SiteSpeedJob, Guid>
    {
        private readonly ISiteSpeedProcess _siteSpeedProcess;

        public SiteSpeedJobController(ISiteSpeedProcess siteSpeedProcess)
        {
            _siteSpeedProcess = siteSpeedProcess;
        }

        public override async Task<CreateResourceResult<Guid>> Create(SiteSpeedJob resource)
        {
            if (_siteSpeedProcess.IsRunning)
                return NotCreated();

            await _siteSpeedProcess.Run();

            return Created(Guid.NewGuid());
        }
    }
}