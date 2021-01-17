﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface IAnswersRepository
    {
        void insertAnswer(Answer a);
        void UpdateAnswer(Answer a);
        void UpdateAnswerVoteCount(int aid, int uid, int value);
        void DeleteAnswer(int aid);
        List<Answer> GetAnswersByQuestionID(int qid);
        List<Answer> GetAnswersByAnswerID(int AnswerID);
    }

    public class AnswersRepository : IAnswersRepository
    {
        StackOverflowDatabaseDbContext db;
        IQuestionsRepository qr;
        IVotesRepository vr;

        public AnswersRepository()
        {
            db = new StackOverflowDatabaseDbContext();
            qr = new QuestionsRepository();
            vr = new VotesRepository();
        }

        public void DeleteAnswer(int aid)
        {
            Answer ans = db.Answers.Where(temp => temp.AnswerID == aid).FirstOrDefault();
            if(ans != null)
            {
                db.Answers.Remove(ans);
                db.SaveChanges();
                qr.UpdateQuestionAnswersCount(ans.QuestionID, -1);
            }
        }

        public List<Answer> GetAnswersByAnswerID(int AnswerID)
        {
            List<Answer> ans = db.Answers.Where(temp => temp.AnswerID == AnswerID).ToList();
            return ans;
        }

        public List<Answer> GetAnswersByQuestionID(int qid)
        {
            List<Answer> ans = db.Answers.Where(temp => temp.QuestionID == qid).OrderByDescending(temp => temp.AnswerDateAndTime).ToList();
            return ans;
        }

        public void insertAnswer(Answer a)
        {
            db.Answers.Add(a);
            db.SaveChanges();
            qr.UpdateQuestionAnswersCount(a.AnswerID, 1);

        }

        public void UpdateAnswer(Answer a)
        {
            Answer ans = db.Answers.Where(temp => temp.AnswerID == a.AnswerID).FirstOrDefault();

            if(ans != null)
            {
                ans.AnswerText = a.AnswerText;
                db.SaveChanges();
            }
        }

        public void UpdateAnswerVoteCount(int aid, int uid, int value)
        {
            Answer ans = db.Answers.Where(temp => temp.AnswerID == aid).FirstOrDefault();

            if(ans != null)
            {
                ans.VotesCount += value;
                db.SaveChanges();

                qr.UpdateQuestionVotesCount(ans.QuestionID, value);
                vr.UpdateVote(aid, uid, value);
            }
            
        }
    }
}
