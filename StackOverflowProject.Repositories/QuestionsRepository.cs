﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using StackOverflowProject.DomainModels;


namespace StackOverflowProject.Repositories
{
    public interface IQuestionsRepository
    {
        void InsertQuestion(Question q);
        void UpdateQuestionDetails(Question q);
        void UpdateQuestionVotesCount(int qid, int value);
        void UpdateQuestionAnswersCount(int qid, int value);
        void UpdateQuestionViewsCount(int qid, int value);
        void DeleteQuestion(int qid);
        List<Question> GetQuestions();
        List<Question> GetQuestionsByQuestionID(int QuestionID);

    }

    public class QuestionsRepository : IQuestionsRepository
    {
        StackOverflowDatabaseDbContext db;

        public QuestionsRepository()
        {
            db = new StackOverflowDatabaseDbContext();
        }

        public void DeleteQuestion(int qid)
        {
            Question qt = db.Questions.Where(temp => temp.QuestionID == qid).FirstOrDefault();
            if (qt != null)
            {
                db.Questions.Remove(qt);
                db.SaveChanges();
            }
        }

        public List<Question> GetQuestions()
        {
            List<Question> qt = db.Questions.OrderByDescending(temp => temp.QuestionDateAndTime).ToList();

            return qt;
        }

        public List<Question> GetQuestionsByQuestionID(int QuestionID)
        {
            List<Question> qt = db.Questions.Where(temp => temp.QuestionID == QuestionID).ToList();
            return qt;
        }

        public void InsertQuestion(Question q)
        {
            db.Questions.Add(q);
            db.SaveChanges();
        }

        public void UpdateQuestionAnswersCount(int qid, int value)
        {
            Question qt = db.Questions.Where(temp => temp.QuestionID == qid).FirstOrDefault();
            if (qt != null)
            {
                qt.AnswersCount += value;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionDetails(Question q)
        {
            Question qt = db.Questions.Where(temp => temp.QuestionID == q.QuestionID).FirstOrDefault();
            if(qt != null)
            {
                qt.QuestioName = q.QuestioName;
                qt.QuestionDateAndTime = q.QuestionDateAndTime;
                qt.CategoryID = q.QuestionID;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionViewsCount(int qid, int value)
        {
            Question qt = db.Questions.Where(temp => temp.QuestionID == qid).FirstOrDefault();
            if (qt != null)
            {
                qt.ViewsCount += value;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionVotesCount(int qid, int value)
        {
            Question qt = db.Questions.Where(temp => temp.QuestionID == qid).FirstOrDefault();
            if(qt != null)
            {
                qt.VotesCount += value;
                db.SaveChanges();
            }
        }
    }
}
