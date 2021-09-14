using System;
using System.Threading.Tasks;
using HomeChat.Core.DataAccess;
using System.Collections.Generic;
using System.Text;

namespace HomeChat.UI.CLI
{
    class Program
    {
        static string _userId = "";
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Default;
            if (_userId != "")
                goto start;
            Console.Write("Enter User Id: ");
            _userId = Console.ReadLine();
            //Console.WriteLine( await DataAccess.Users.RemoveUser("1"));
        start:
            Console.Write("→ ");
            string[] input = Console.ReadLine().Split(' ');
            if (input.Length < 1)
                goto start;
            switch (input[0] + " " + input[1])
            {
                case "user add":
                    if (await DataAccess.Users.AddUser(new Core.DataModels.UserModel() { Name = input[2], IP = input[3] }))
                        WriteSuccess("Users.AddUser - SUCCESS");
                    else
                        WriteError("Users.AddUser - ERROR");
                    break;
                case "user update":
                    var dataType = input[3] == "int" ? typeof(int) : typeof(string);
                    var columns = new List<(string columnName, Type dataType, string value)>() { 
                        (input[2], dataType, input[4]),
                    };
                    if (await DataAccess.Users.UpdateUser(columns, input[5]))
                        WriteSuccess("Users.AddUser - SUCCESS");
                    else
                        WriteError("Users.AddUser - ERROR");
                    break;
                case "user send":
                    if (await DataAccess.Messages.AddMessage(new Core.DataModels.MessageModel() { SenderId = int.Parse(_userId), ReceiverId = int.Parse(input[2]), Content=input[3].Replace("_"," "),TimeStamp=DateTime.Now }))
                        WriteSuccess("Users.AddUser - SUCCESS");
                    else
                        WriteError("Users.AddUser - ERROR");
                    break;
                case "user remove":
                    Console.Write("Are you sure? [Y/n]");
                    if (Console.ReadLine() != "Y")
                        break;
                    if (await DataAccess.Users.RemoveUser(input[2]))
                        WriteSuccess("Users.RemoveUser - SUCCESS");
                    else
                        WriteError("Users.RemoveUser - ERROR");
                    break;
                case "user list":
                    try
                    {
                        foreach (var user in await DataAccess.Users.ReadUsers())
                        {
                            Console.WriteLine(user);
                        }
                    }
                    catch (Exception) 
                    { 
                        WriteError("Users.RemoveUser - ERROR");
                    }
                    break;
                case "user sent":
                    try
                    {
                        foreach (var message in await DataAccess.Messages.ReadUserSentMessages(_userId))
                        {
                            Console.WriteLine(message);
                        }
                    }
                    catch (Exception)
                    {
                        WriteError("Users.RemoveUser - ERROR");
                    }
                    break;
                case "user received":
                    try
                    {
                        foreach (var message in await DataAccess.Messages.ReadUserReceivedMessages(_userId))
                        {
                            Console.WriteLine(message);
                        }
                    }
                    catch (Exception)
                    {
                        WriteError("Users.RemoveUser - ERROR");
                    }
                    break;
                case "user resignin":
                    _userId = input[2];
                    break;
                case "user exit":
                    goto end;
                case "clr scr":
                    Console.Clear();
                    break;
                default:
                    break;
            }
            goto start;
        end:
            Console.WriteLine("Bye...");
        }

        static void WriteSuccess(string arg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(arg);
            Console.ResetColor();
        }

        static void WriteError(string arg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(arg);
            Console.ResetColor();
        }
    }
}
