using DataMigrationApp.Data;
using DataMigrationApp.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DataMigrationApp
{
    public class Program
    {

        static async Task Main(string[] args)
        {
            Migration data = new Migration();

            char pressNumber2;
            do
            {
                Console.WriteLine("Press m for Migration");
                Console.WriteLine("Press s for show previous status");
                var pressNumber = char.ToUpper(Console.ReadLine()[0]);
                switch (pressNumber)
                {
                    case 'M':
                        
                        Console.WriteLine("Enter start row:");
                        var startRow = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter End row:");
                        var EndRow = int.Parse(Console.ReadLine());
                        if(startRow < EndRow)
                        {
                            CancellationTokenSource token = new CancellationTokenSource();
                            MigrationStatus status = new MigrationStatus();
                            Task T = Migration.DivideInBatch(startRow, EndRow, token, status);
                            Task cancelTask = Task.Run(() =>
                            {
                                while (true)
                                {
                                    var choice = char.ToUpper(Console.ReadLine()[0]);
                                    if (T.IsCompleted)
                                    {
                                        break;
                                    }
                                    if (choice == 'C')
                                    {
                                        status.Status = EnumValuesForStatus.Canceled;
                                        token.Cancel();
                                    }
                                    if (!T.IsCompleted)
                                    {
                                        Console.WriteLine("Press C to cancel current migration");
                                    }
                                    if (choice == 'S')
                                    {
                                        Console.WriteLine("************************************************************************");
                                        Console.WriteLine($"{status.Status}");
                                        Console.WriteLine("************************************************************************");
                                    }

                                }
                                if (T.IsCompleted)
                                {
                                    if (token.IsCancellationRequested)
                                    {
                                        status.Status = EnumValuesForStatus.Canceled;
                                    }
                                    else
                                    {
                                        status.Status = EnumValuesForStatus.Completed;
                                    }
                                }

                            });
                            try
                            {
                                await T;
                            }
                            catch (Exception)
                            {

                                Console.WriteLine("");
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Startrow must less than Endrow ");
                            break;
                        }
                        
                    case 'S':
                        //Console.WriteLine("print S");
                        using (var context = new CommonContext())
                        {
                            var iteration = context.MigrationStatuses.ToList();

                            foreach (var temp in iteration)
                            {
                                Console.WriteLine($"{temp.From} TO {temp.To} Migration Status is {temp.Status}");
                                Console.WriteLine("***********************************************************");
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Wrong input");
                        break;
                }
                Console.WriteLine("Do you want to further processed");
                Console.WriteLine("Press Y for yes and press N for No");
                pressNumber2 = char.ToUpper(Console.ReadLine()[0]);
            } while (pressNumber2 == 'Y');

        }   
    }
}
