using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using HomeWork.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HomeWork.Models;

namespace HomeWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var model = (from b in _context.BookItems
                select new BookModelWithPenalty
                {
                    Id = b.Id,
                    Country = b.Country,
                    BookName = b.BookName,
                    CheckOutDate = b.CheckOutDate,
                    ReturnDate = b.ReturnDate
                }).ToList();

            model.ForEach(x =>
            {
                x.TotalDays = DateHelper.TotalDays(x.ReturnDate, x.CheckOutDate);

                var countryHolidays = _context.CountryOffDays.Where(c => c.CountryId == x.Country.Id).ToList();
                x.TotalWorkDays = DateHelper.TotalWorkDays(x.CheckOutDate, x.ReturnDate, x.Country, countryHolidays);

                if (x.TotalWorkDays > 10)
                    x.Penalty = x.TotalWorkDays-10;
                else
                    x.Penalty = 0;

                x.PenaltyAmount = x.Penalty * x.Country.PenaltyAmount;
            });

            return View(model);
        }

        public static class DateHelper
        {
            public static int TotalDays(DateTime startDate, DateTime? endDate)
            {
                if (!endDate.HasValue)
                    return 0;

                return (int)(startDate - (DateTime)endDate).TotalDays;
            }

            public static int TotalWorkDays(DateTime startDate, DateTime? endDate, Country country, List<CountryOffDay> holidays)
            {
                if (!endDate.HasValue)
                    return 0;

                var weekends = country.WeekEndDays.Split(','); // todo: error handling 
                var weekend1 = Convert.ToInt32(weekends[0]);
                var weekend2 = Convert.ToInt32(weekends[1]);

                var workDays = 0;
                
                do
                {
                    var currentDayOfWeek = (int) startDate.DayOfWeek;
                    if(currentDayOfWeek == weekend1 || currentDayOfWeek == weekend2)
                    {
                        startDate = startDate.AddDays(1);
                        continue;
                    }

                    var isHoliday = holidays.Any(x => x.Month == startDate.Month && x.Day == startDate.Day);
                    if(isHoliday)
                    {
                        startDate = startDate.AddDays(1);
                        continue;
                    }

                    workDays++;
                    startDate = startDate.AddDays(1);

                } while (startDate.Date!=endDate.Value.Date);

                return workDays;
            }
        }

        #region MyRegion

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion
        
    }
}
