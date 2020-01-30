using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Entities
{
    public class Country
    {

        public int Id { get; set; }

        [MaxLength(500)]
        public string Name { get; set; }

        [MaxLength(10)]
        public string WeekEndDays { get; set; }

        [MaxLength(10)]
        public string CurrencyCode { get; set; }

        public decimal PenaltyAmount { get; set; }

        public ICollection<CountryOffDay> CountryOffDays { get; set; }

        public ICollection<BookItem> BookItems { get; set; }

    }
}