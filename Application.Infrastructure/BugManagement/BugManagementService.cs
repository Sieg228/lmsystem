﻿using System.Collections.Generic;
using System.Linq;
using Application.Core.Data;
using LMPlatform.Data.Repositories;
using LMPlatform.Models;

namespace Application.Infrastructure.BugManagement
{
    public class BugManagementService : IBugManagementService
    {
        public Bug GetBug(int bugId)
        {
            using (var repositoriesContainer = new LmPlatformRepositoriesContainer())
            {
                return repositoriesContainer.BugsRepository.GetBy(new Query<Bug>(e => e.Id == bugId));
            }
        }

        public List<Bug> GetBugs()
        {
            using (var repositoriesContainer = new LmPlatformRepositoriesContainer())
            {
                return repositoriesContainer.BugsRepository.GetAll().ToList();
            }
        }

        public IPageableList<Bug> GetAllBugs(string searchString = null, IPageInfo pageInfo = null, IEnumerable<ISortCriteria> sortCriterias = null)
        {
            var query = new PageableQuery<Bug>(pageInfo);

            query.Include(e => e.Status);
            query.Include(e => e.Severity);
            query.Include(e => e.Symptom);
            query.Include(e => e.Project);
            if (!string.IsNullOrEmpty(searchString))
            {
                query.AddFilterClause(
                    e => e.Summary.ToLower().StartsWith(searchString) || e.Summary.ToLower().Contains(searchString));
            }

            query.OrderBy(sortCriterias);
            using (var repositoriesContainer = new LmPlatformRepositoriesContainer())
            {
                return repositoriesContainer.BugsRepository.GetPageableBy(query);
            }
        }

        public List<BugLog> GetBugLogs(int bugId)
        {
            using (var repositoriesContainer = new LmPlatformRepositoriesContainer())
            {
                return repositoriesContainer.BugLogsRepository.GetAll(new Query<BugLog>(e => e.BugId == bugId).Include(e => e.User)).ToList();
            }
        }

        public Bug SaveBug(Bug bug)
        {
            using (var repositoriesContainer = new LmPlatformRepositoriesContainer())
            {
                repositoriesContainer.BugsRepository.Save(bug);
                repositoriesContainer.ApplyChanges();
            }

            return bug;
        }

        public BugLog SaveBugLog(BugLog bugLog)
        {
            using (var repositoriesContainer = new LmPlatformRepositoriesContainer())
            {
                repositoriesContainer.BugLogsRepository.Save(bugLog);
                repositoriesContainer.ApplyChanges();
            }

            return bugLog;
        }

        public void DeleteBug(int bugId)
        {
            using (var repositoriesContainer = new LmPlatformRepositoriesContainer())
            {
                var bug =
                    repositoriesContainer.BugsRepository.GetBy(
                        new Query<Bug>().AddFilterClause(u => u.Id == bugId));
                repositoriesContainer.BugsRepository.DeleteBug(bug);
                repositoriesContainer.ApplyChanges();
            }

            ClearBugLogs(bugId);
        }

        public void ClearBugLogs(int bugId)
        {
            using (var repositoriesContainer = new LmPlatformRepositoriesContainer())
            {
                var bugLogs =
                    repositoriesContainer.BugLogsRepository.GetAll(
                        new Query<BugLog>().AddFilterClause(u => u.BugId == bugId));
                foreach (var bugLog in bugLogs)
                {
                    repositoriesContainer.BugLogsRepository.DeleteBugLog(bugLog);
                }

                repositoriesContainer.ApplyChanges();
            }
        }
    }
}
