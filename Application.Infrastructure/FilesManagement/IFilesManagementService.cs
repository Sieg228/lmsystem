﻿using System;
using System.Collections.Generic;
using LMPlatform.Models;

namespace Application.Infrastructure.FilesManagement
{
    public interface IFilesManagementService
    {
        void DeleteFileAttachment(Attachment attachment);

        string GetFileDisplayName(string guid);

        void SaveFiles(IEnumerable<Attachment> attachment, string path = "");

        IList<Attachment> GetAttachments(string path);

		string GetPathName(string guid);
    }
}