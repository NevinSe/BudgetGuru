using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BudgetGuru.Models
{
    public class Goals
    {
        [Key]
        public int GoalId { get; set; }
        public string UserName { get; set; }
        [Display(Name = "Goal Description")]
        public string GoalDescription { get; set; }
        [Display(Name = "Goal Achievement Date")]
        public DateTime? AchievementDate { get; set; }
        [Display(Name = "Goal Total Cost")]
        public double TotalCost { get; set; }
    }
}