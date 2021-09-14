using Dapper;
using HomeChat.Core.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeChat.Core.DataAccess
{
    public static class DataAccess
    {
        public static class Users
        {
            public static async Task<List<UserModel>> ReadUsers()
            {
                using IDbConnection cnt = new SqlConnection(DBHelper.GetConnectionString());
                return (await cnt.QueryAsync<UserModel>("select * from [dbo].[User]")).ToList();
            }

            public static async Task<bool> AddUser(UserModel user)
            {
                using IDbConnection cnt = new SqlConnection(DBHelper.GetConnectionString());
                try
                {
                    _ = await cnt.ExecuteAsync($"insert into [dbo].[User] (Name, IP) values('{user.Name}','{user.IP}');");
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public static async Task<List<UserModel>> FilterUsers(string columnName, string filter)
            {
                using IDbConnection cnt = new SqlConnection(DBHelper.GetConnectionString());
                return (await cnt.QueryAsync<UserModel>($"select * from [dbo].[User] where {columnName} = '{filter}'")).ToList();
            }


            public static async Task<bool> UpdateUser(List<(string columnName,Type dataType, string value)> columns, string id)
            {
                using IDbConnection cnt = new SqlConnection(DBHelper.GetConnectionString());
                try
                {
                    string sql = "update [dbo].[User] set ";
                    foreach (var (columnName,dataType, value) in columns)
                    {
                        if (dataType == typeof(string))
                            sql += $"{columnName} = '{value}',";
                        else
                            sql += $"{columnName} = {value},";
                    }
                    sql = sql.Remove(sql.Length - 1);
                    sql += $" where Id = {id}";
                    _ = await cnt.ExecuteAsync(sql);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public static async Task<bool> RemoveUser(string id)
            {
                using IDbConnection cnt = new SqlConnection(DBHelper.GetConnectionString());
                try
                {
                    _ = await cnt.ExecuteAsync($"delete from [dbo].[User] where Id = {id}");
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static class Messages
        {
            public static async Task<List<MessageModel>> ReadMessages()
            {
                using IDbConnection cnt = new SqlConnection(DBHelper.GetConnectionString());
                return (await cnt.QueryAsync<MessageModel>("select * from [dbo].[Message]")).ToList();
            }

            public static async Task<List<MessageModel>> ReadUserSentMessages(string id)
            {
                using IDbConnection cnt = new SqlConnection(DBHelper.GetConnectionString());
                return (await cnt.QueryAsync<MessageModel>($"select * from [dbo].[Message] where SenderId = {id}")).ToList();
            }

            public static async Task<List<MessageModel>> ReadUserReceivedMessages(string id)
            {
                using IDbConnection cnt = new SqlConnection(DBHelper.GetConnectionString());
                return (await cnt.QueryAsync<MessageModel>($"select * from [dbo].[Message] where ReceiverId = {id}")).ToList();
            }

            public static async Task<bool> AddMessage(MessageModel message)
            {
                using IDbConnection cnt = new SqlConnection(DBHelper.GetConnectionString());
                try
                {
                    _ = await cnt.ExecuteAsync($"insert into [dbo].[Message] (SenderId,ReceiverId,Content,TimeStamp,IsRead) values('{message.SenderId}','{message.ReceiverId}','{message.Content}','{message.TimeStamp:yyyy-MM-dd HH:mm:ss.fff}', 0);");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }
            }

            public static async Task<List<MessageModel>> FilterMessages(string columnName, string filter)
            {
                using IDbConnection cnt = new SqlConnection(DBHelper.GetConnectionString());
                return (await cnt.QueryAsync<MessageModel>($"select * from [dbo].[Message] where {columnName} = {filter}")).ToList();
            }


            public static async Task<bool> UpdateMessage((string columnName,Type dataType, string value)[] columns, string id)
            {
                using IDbConnection cnt = new SqlConnection(DBHelper.GetConnectionString());
                try
                {
                    string sql = "update [dbo].[Message] set ";
                    foreach (var (columnName,dataType, value) in columns)
                    {
                        if (dataType==typeof(string))
                            sql += $"{columnName} = '{value}',";
                        else
                            sql += $"{columnName} = {value},";
                    }
                    sql = sql.Remove(sql.Length - 1);
                    sql += $" where Id = {id}";
                    _ = await cnt.ExecuteAsync(sql);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public static async Task<bool> RemoveMessage(string id)
            {
                using IDbConnection cnt = new SqlConnection(DBHelper.GetConnectionString());
                try
                {
                    _ = await cnt.ExecuteAsync($"delete from [dbo].[Message] where Id = {id}");
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
