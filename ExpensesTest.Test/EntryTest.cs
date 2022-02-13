using DocumentFormat.OpenXml.InkML;
using Expenses.Controllers;
using Expenses.Data;
using Expenses.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ExpensesTest.Test
{ 
public class EntryTest
{
        public void GetEntryByIDTest()
        {
            //create In Memory Database
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "EntryDB")
            .Options;

            using (var context = new AppDbContext(options))
            {
                context.Entries.Add(new Entry
                {
                    Id = 3,
                    Description = "USA",
                    IsExpense = true,
                    Value = 12.2,

                });
                context.SaveChanges();
            }
            // Use a Context instance  with Data to run the test for your Business code 

            using (var dbContext = new AppDbContext(options))
            {
                EntriesController controller = new EntriesController(dbContext);
                var result = controller.GetEntry(1);//as ObjectResult;
                var actualResult = result.Value;

                Assert.Equal(3, ((Entry)actualResult).Id);
                Assert.Equal("USA", ((Entry)actualResult).Description);
                Assert.Equal(false, ((Entry)actualResult).IsExpense);
                Assert.Equal(12.2, ((Entry)actualResult).Value);

            }

            /*using (var context = new AppDbContext(options))
            {
                context.Entries.Add(new Entry
                {
                    Id = 4,
                    Description = "A",
                    IsExpense = true,
                    Value = 12.2,

                });
                context.Entries.Add(new Entry
                {
                    Id = 5,
                    Description = "U",
                    IsExpense = true,
                    Value = 12.2,

                });
                context.Entries.Add(new Entry
                {
                    Id = 6,
                    Description = "C",
                    IsExpense = true,
                    Value = 12.2,

                });


                context.SaveChanges();
            }


            using (var dbContext = new AppDbContext(options))
            {
                EntriesController controller = new EntriesController(dbContext);
                var result = controller.GetSort(1, 10, true, " ");//as ObjectResult;
                var actualResult = result;
                List<Entry> expected = new List<Entry>
                {
                    new Entry { Id = 4, Description = "A", IsExpense = true, Value = 12.2 },
                    new Entry { Id = 4, Description = "C", IsExpense = true, Value = 12.2 },
                    new Entry { Id = 4, Description = "U", IsExpense = true, Value = 12.2 }
                };                
                Assert.Equal(expected, result.data);
               *//* Assert.Equal("USA", ((Entry)actualResult).Description);
                Assert.Equal(false, ((Entry)actualResult).IsExpense);
                Assert.Equal(12.2, ((Entry)actualResult).Value);
*//*
            }*/
        }
}
}
