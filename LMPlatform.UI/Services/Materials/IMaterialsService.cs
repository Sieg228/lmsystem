﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using LMPlatform.UI.Services.Modules.Materials;

namespace LMPlatform.UI.Services.Materials
{
    using System.Collections;
    using System.Collections.Generic;
    using LMPlatform.UI.Services.Modules;

    [ServiceContract]
    public interface IMaterialsService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "/getFoldersMaterials", RequestFormat = WebMessageFormat.Json, Method = "POST")]
        FoldersResult GetFolders(string Pid);

        [OperationContract]
        [WebInvoke(UriTemplate = "/backspaceFolderMaterials", RequestFormat = WebMessageFormat.Json, Method = "POST")]
        FoldersResult BackspaceFolder(string Pid);

        [OperationContract]
        [WebInvoke(UriTemplate = "/createFolderMaterials", RequestFormat = WebMessageFormat.Json, Method = "POST")]
        FoldersResult CreateFolder(string Pid);

        [OperationContract]
        [WebInvoke(UriTemplate = "/deleteFolderMaterials", RequestFormat = WebMessageFormat.Json, Method = "POST")]
        FoldersResult DeleteFolder(string IdFolder);

        [OperationContract]
        [WebInvoke(UriTemplate = "/renameFolderMaterials", RequestFormat = WebMessageFormat.Json, Method = "POST")]
        FoldersResult RenameFolder(string id, string pid, string newName);

        [OperationContract]
        [WebInvoke(UriTemplate = "/saveTextMaterials", RequestFormat = WebMessageFormat.Json, Method = "POST")]
        FoldersResult SaveTextMaterials(string idf, string name, string text);
    }
}
