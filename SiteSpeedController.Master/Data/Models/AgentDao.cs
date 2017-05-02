﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteSpeedController.Master.Data.Models
{
    [Table("agents")]
    public class AgentDao
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("created")]
        [Required]
        public DateTime Created { get; set; }

        [Column("lastUpdated")]
        [Required]
        public DateTime LastUpdated { get; set; }

        [Column("hostIdentifier")]
        [Required]
        public Guid HostIdentifier { get; set; }

        [Column("hostname")]
        [Required]
        public string Hostname { get; set; }

        [Column("port")]
        [Required]
        public int Port { get; set; }

        [Column("jobsRun")]
        public int JobsExecuted { get; set; }

        [Column("isApproved")]
        public bool IsApproved { get; set; }

        [Column("isDisabled")]
        public bool IsDisabled { get; set; }

        public List<AgentCountryAssociation> Countries { get; set; }
    }
}