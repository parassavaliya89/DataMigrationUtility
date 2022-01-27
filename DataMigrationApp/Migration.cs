using DataMigrationApp.Data;
using DataMigrationApp.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DataMigrationApp
{
    public class Migration
    {
        public static async Task DivideInBatch(int start, int End, CancellationTokenSource token,MigrationStatus status)
        {
            Console.WriteLine($"Migrating starts From {start} To {End} Rows to Destination Table");
            int batchStart, batchEnd;
            List<Task> tasks = new List<Task>();
            int totalRows = End - start;
            status.From = start;
            status.To = End;
            batchStart = start;
            while (totalRows >= 100)
            {
                batchEnd = batchStart + 100;
                tasks.Add(BatchWiseMigration(batchStart,batchEnd, token.Token));
                batchStart = batchEnd;
                totalRows -= 100;
            }
            if (totalRows > 0 && totalRows < 100)
            {
                batchEnd = batchStart + totalRows;
                tasks.Add(BatchWiseMigration(batchStart,batchEnd,token.Token));
            }
            Task T =  Task.WhenAll(tasks);
            status.Status = EnumValuesForStatus.Ongoing;
           
            try
            {
                await T;
            }
            catch (Exception)
            {
                
                Console.WriteLine("Duplicate Entry is not allowed");
            }

            if(T.IsCompleted)
            {
                if(token.IsCancellationRequested)
                {
                    Console.WriteLine($"Migrating starts From {start} To {End} Rows is Canceled");
                    status.Status= EnumValuesForStatus.Canceled;
                }
                else if(T.IsFaulted)
                {
                    Console.WriteLine("You can not include Duplicate range");
                }
                else
                {
                    Console.WriteLine($"Migrating starts From {start} To {End} Rows is Completed");
                    status.Status = EnumValuesForStatus.Completed;
                }
            }
            using (var StatusContext = new CommonContext())
            {
                StatusContext.MigrationStatuses.Add(status);
                StatusContext.SaveChanges();
            }
            
               
        }




        public static async Task BatchWiseMigration(int start,int end, CancellationToken token)
        {
            if (!token.IsCancellationRequested)
            {
                
                List<SourceModel> sourceData = new List<SourceModel>();
                    using (var context = new CommonContext())
                    {
                        sourceData = context.Source.Where(b => (b.Id >= start && b.Id < end)).ToList();
                    }
                List<DestinationModel> destination = new List<DestinationModel>();
                foreach (var temp in sourceData)
                {
                    if (token.IsCancellationRequested)
                    {
                        
                        return;
                    }
                    int tempSum = await Sum(temp.FirstNumber,temp.SecondNumber);
                    destination.Add(new DestinationModel() { SourceModelId = temp.Id,Sum = tempSum});
                    
                }
                
                using(var context = new CommonContext()) 
                {
                    await context.Destination.AddRangeAsync(destination, token);
                    context.SaveChanges();
                }
                

            }
            else
            {
                Console.WriteLine($"Migration From {start} To {end} is Cancelled");
            }
        }

        public static async Task<int> Sum(int FirstNumber,int LastNumber)
        {
            await Task.Delay(50);
            return FirstNumber + LastNumber;
        }
        
    }
}
