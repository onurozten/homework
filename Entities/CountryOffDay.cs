using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Entities
{
    public class CountryOffDay
    {
        public int Id { get; set; }

        public virtual Country Country{ get; set; }

        public int CountryId { get; set; }

        [MaxLength(500)]
        public string Name { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }
    }
}