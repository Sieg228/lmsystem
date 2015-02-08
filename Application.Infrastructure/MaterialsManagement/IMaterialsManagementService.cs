﻿using System.Collections.Generic;
using Application.Core.Data;
using LMPlatform.Models;

namespace Application.Infrastructure.MaterialsManagement
{
    using System.Net;

    public interface IMaterialsManagementService
    {
        List<Folders> GetFolders(int pid);

        int GetPidById(int pid);

        Folders CreateFolder(int pid);

        void DeleteFolder(int id);

        void RenameFolder(int id, string name);

        void SaveTextMaterials(int iddocument, int idfolder, string name, string text);

        List<Materials> GetDocumentsByIdFolders(int id);

        Materials GetTextById(int id);
    }
}