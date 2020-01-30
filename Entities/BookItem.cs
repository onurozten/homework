using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Entities
{
    public class BookItem
    {

        public int Id { get; set; }

        public virtual  Country Country { get; set; }
        public int CountryId { get; set; }


        [MaxLength(60)]
        public string BookName { get; set; }

        public DateTime CheckOutDate { get; set; }

        public DateTime ReturnDate { get; set; }



    }
}