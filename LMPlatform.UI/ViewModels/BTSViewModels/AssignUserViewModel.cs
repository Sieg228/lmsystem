﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.Core;
using Application.Infrastructure.GroupManagement;
using Application.Infrastructure.LecturerManagement;
using Application.Infrastructure.ProjectManagement;
using Application.Infrastructure.StudentManagement;
using Application.Infrastructure.UserManagement;
using LMPlatform.Data.Infrastructure;
using LMPlatform.Data.Repositories;
using LMPlatform.Data.Repositories.RepositoryContracts;
using LMPlatform.Models;
using WebMatrix.WebData;

namespace LMPlatform.UI.ViewModels.BTSViewModels
{
    public class AssignUserViewModel : Controller
    {
        private readonly LazyDependency<IGroupManagementService> _groupManagementService = new LazyDependency<IGroupManagementService>();
        private readonly LazyDependency<IProjectManagementService> _projectManagementService = new LazyDependency<IProjectManagementService>();
        private readonly LazyDependency<IStudentsRepository> _studentsRepository = new LazyDependency<IStudentsRepository>();
        private readonly LazyDependency<IStudentManagementService> _studentManagementService = new LazyDependency<IStudentManagementService>();

        public IGroupManagementService GroupManagementService
        {
            get
            {
                return _groupManagementService.Value;
            }
        }

        public IProjectManagementService ProjectManagementService
        {
            get
            {
                return _projectManagementService.Value;
            }
        }

        public IStudentsRepository StudentsRepository
        {
            get
            {
                return _studentsRepository.Value;
            }
        }

        public IStudentManagementService StudentManagementService
        {
            get
            {
                return _studentManagementService.Value;
            }
        }

        [Required(ErrorMessage = "Поле Группа обязательно для заполнения")]
        [DisplayName("Группа")]
        public int GroupId { get; set; }

        [Required(ErrorMessage = "Поле ФИО обязательно для заполнения")]
        [DisplayName("ФИО")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Поле Роль обязательно для заполнения")]
        [DisplayName("Роль")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Поле ФИО обязательно для заполнения")]
        [DisplayName("ФИО")]
        public int LecturerId { get; set; }

        [Required(ErrorMessage = "Поле Роль обязательно для заполнения")]
        [DisplayName("Роль")]
        public int LecturerRoleId { get; set; }

        public int ProjectId { get; set; }

        public int Id { get; set; }

        public AssignUserViewModel()
        {
        }

        public AssignUserViewModel(int id, int projectId)
        {
            ProjectId = projectId;

            if (id != 0)
            {
                var projectUser = ProjectManagementService.GetProjectUser(id);
                ProjectId = projectUser.ProjectId;
                RoleId = projectUser.ProjectRoleId;
                UserId = projectUser.UserId;
                Id = projectUser.Id;
            }
        }

        public List<Group> GetAssignedGroups(int userId)
        {
            var groups = new LmPlatformRepositoriesContainer().ProjectsRepository.GetGroups(userId);

            return groups;
        }

        public IList<SelectListItem> GetGroups()
        {
            var groups = new List<Group>(); 

            var user = new UsersManagementService().GetUser(WebSecurity.CurrentUserId);
            if (user != null)
            {
                groups = GetAssignedGroups(WebSecurity.CurrentUserId);
            }
            else
            {
                groups = new GroupManagementService().GetGroups();
            }

            return groups.Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString(CultureInfo.InvariantCulture)
            }).ToList();
        }

        public List<SelectListItem> GetStudents(int groupId)
        {
            var students = StudentManagementService.GetGroupStudents(groupId).OrderBy(e => e.FirstName);

            return students.Select(v => new SelectListItem
            {
                Text = v.FullName,
                Value = v.Id.ToString(CultureInfo.InvariantCulture)
            }).ToList();
        }

        public IList<SelectListItem> GetLecturers()
        {
            var lecturers = new LecturerManagementService().GetLecturers();

            var lecturerList = new List<Lecturer>();

            foreach (var lecturer in lecturers)
            {
                if (ProjectManagementService.IsUserAssignedOnProject(lecturer.Id, ProjectId) == false)
                {
                    lecturerList.Add(lecturer);
                }
            }

            return lecturerList.Select(v => new SelectListItem
            {
                Text = v.LastName + " " + v.FirstName + " " + v.MiddleName,
                Value = v.Id.ToString(CultureInfo.InvariantCulture)
            }).ToList();
        }

        public IList<SelectListItem> GetRoles()
        {
            var roles = new LmPlatformModelsContext().ProjectRoles.ToList();
            return roles.Select(e => new SelectListItem
            {
                Text = e.Name,
                Value = e.Id.ToString(CultureInfo.InvariantCulture)
            }).ToList();
        }

        public void SaveAssignment()
        {
            var projectUser = new ProjectUser
            {
                UserId = UserId,
                ProjectId = Id,
                ProjectRoleId = RoleId,
            };

            ProjectManagementService.AssingRole(projectUser);
        }
    }
}