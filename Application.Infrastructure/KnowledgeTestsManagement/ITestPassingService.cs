﻿using System.Collections.Generic;
using LMPlatform.Models;
using LMPlatform.Models.KnowledgeTesting;

namespace Application.Infrastructure.KnowledgeTestsManagement
{
    public interface ITestPassingService
    {
        NextQuestionResult GetNextQuestion(int testId, int userId, int currentQuestionNumber);

        void MakeUserAnswer(IEnumerable<Answer> answers, int userId, int testId, int questionNumber);

        IEnumerable<Student> GetPassTestResults(int groupId, string searchString = null);
    }
}