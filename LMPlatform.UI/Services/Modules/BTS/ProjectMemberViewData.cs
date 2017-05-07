﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using LMPlatform.Models;

namespace LMPlatform.UI.Services.Modules.BTS
{
    [DataContract]
    public class ProjectmemberViewData
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Role { get; set; }

        public ProjectmemberViewData(ProjectUser projectUser)
        {
            Id = projectUser.Id;
            UserId = projectUser.User.Id;
            Name = projectUser.User.FullName;
            Role = projectUser.ProjectRole.Name;
        }
    }
}