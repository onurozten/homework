using System;
using HomeWork.Entities;

namespace HomeWork.Models
{
    public class BookModelWithPenalty
    {
        public int Id { get; set; }
        public Country Country { get; set; }
        public string BookName { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public int TotalDays { get; set; }
        public int TotalWorkDays { get; set; }
        public int Penalty { get; set; }
        public decimal PenaltyAmount { get; set; }
    }
}
