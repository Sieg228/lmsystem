﻿using System.Collections.Generic;
using System.Linq;
using LMPlatform.Models.KnowledgeTesting;

namespace LMPlatform.UI.ViewModels.KnowledgeTestingViewModels
{
    public class QuestionViewModel
    {
        public int Id
        {
            get;
            set;
        }

        public int TestId
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public int ComplexityLevel
        {
            get;
            set;
        }

        public QuestionType QuestionType
        {
            get;
            set;
        }

        public IEnumerable<AnswerViewModel> Answers
        {
            get;
            set;
        }

        public Question ToQuestion()
        {
            return new Question
            {
                Id = Id,
                TestId = TestId,
                Title = Title,
                Description = Description,
                ComlexityLevel = ComplexityLevel,
                QuestionType = QuestionType,
                Answers = Answers.Select(answer => answer.ToAnswer()).ToList()
            };
        }

        public static QuestionViewModel FromQuestion(Question question)
        {
            return new QuestionViewModel
            {
                Id = question.Id,
                TestId = question.TestId,
                Title = question.Title,
                Description = question.Description,
                ComplexityLevel = question.ComlexityLevel,
                QuestionType = question.QuestionType,
                Answers = question.Answers != null 
                    ? question.Answers.Select(AnswerViewModel.FromAnswer) 
                    : null
            };
        }
    }
}