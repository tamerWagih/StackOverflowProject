using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace StackOverflowProject.DomainModels
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionID { get; set; }

        public string QuestioName { get; set; }

        public DateTime QuestionDateAndTime { get; set; }

        public int UserID { get; set; }

        public int CategoryID { get; set; }

        public int VotesCount { get; set; }

        public int AnswersCount { get; set; }

        public int ViewsCount { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [ForeignKey("CategorID")]
        public virtual Category Category { get; set; }

        public virtual List<Answer> Answers { get; set; }
    }
}
