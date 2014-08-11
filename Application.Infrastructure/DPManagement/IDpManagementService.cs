﻿using System.Collections.Generic;
using Application.Core.Data;
using Application.Infrastructure.DTO;

namespace Application.Infrastructure.DPManagement
{
    public interface IDpManagementService
    {
        PagedList<DiplomProjectData> GetProjects(GetPagedListParams parms);

        DiplomProjectData GetProject(int id);

        void SaveProject(DiplomProjectData projectData);

        void DeleteProject(int userId, int id);

        void AssignProject(int userId, int projectId, int studentId);

        void SetStudentDiplomMark(int lecturerId, int assignedProjectId, int mark);

        void DeleteAssignment(int userId, int id);

        PagedList<StudentData> GetStudentsByDiplomProjectId(GetPagedListParams parms);

        PagedList<StudentData> GetGraduateStudentsForLecturer(int lecturerId, GetPagedListParams parms);
    }
}
