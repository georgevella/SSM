using System;

namespace SiteSpeedManager.Models.Resources.V1
{
    [Flags]
    public enum AgentStatus
    {
        Unauthorized = 0x0,
        /// <summary>
        ///     Agent is authorized and enabled.
        /// </summary>
        AuthorizedAndEnabled = 0x3,

        /// <summary>
        ///     Agent is authorized and disabled.
        /// </summary>
        AuthorizedAndDisabled = 0x1,
    }
}