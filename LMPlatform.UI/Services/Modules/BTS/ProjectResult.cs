﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace LMPlatform.UI.Services.Modules.BTS
{
    [DataContract]
    public class ProjectResult : ResultViewData
    {
        [DataMember]
        public ProjectViewData Project { get; set; }
    }
}