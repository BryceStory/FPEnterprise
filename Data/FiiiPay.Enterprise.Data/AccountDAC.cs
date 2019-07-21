﻿using FiiiPay.Enterprise.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Data
{
    public class AccountDAC : BaseDataAccess
    {
        public async Task<Account> SelectByUsernameAsync(string username)
        {
            const string sql = @"SELECT * FROM [dbo].[Accounts] WHERE Username=@Username";

            using (var con = await ReadConnectionAsync())
            {
                return await con.QueryFirstOrDefaultAsync<Account>(sql, new { Username = username });
            }
        }

        public Account SelectById(Guid accountId)
        {
            const string sql = @"SELECT * FROM [dbo].[Accounts] WHERE [Id]=@AccountId";

            using (var con = ReadConnection())
            {
                return con.QueryFirstOrDefault<Account>(sql, new { AccountId = accountId });
            }
        }

        public async Task<Account> SelectByEmailAsync(string email)
        {
            const string sql = @"SELECT * FROM [dbo].[Accounts] WHERE Email=@EmailAddress";

            using (var con = await ReadConnectionAsync())
            {
                return await con.QueryFirstOrDefaultAsync<Account>(sql, new { EmailAddress = email });
            }
        }

        public async Task<bool> CheckEmailExistAsync(Guid accountId, string emailAddress)
        {
            const string sql = @"SELECT COUNT(1) FROM [dbo].[Accounts] WHERE [Email]=@EmailAddress AND [Id]<>@AccountId";

            using (var con = await ReadConnectionAsync())
            {
                var recordCount = await con.QueryFirstAsync<int>(sql, new { AccountId = accountId, EmailAddress = emailAddress });
                return recordCount > 0;
            }
        }

        public async Task<Account> SelectByIdAsync(Guid accountId)
        {
            const string sql = @"SELECT * FROM [dbo].[Accounts] WHERE [Id]=@AccountId";

            using (var con = await ReadConnectionAsync())
            {
                return await con.QueryFirstOrDefaultAsync<Account>(sql, new { AccountId = accountId });
            }
        }

        public async Task SetPasswordAndEmailAsync(Guid accountId, string password,string email,byte status)
        {
            const string sql = @"UPDATE [dbo].[Accounts] SET [Password]=@Password,[Email]=@Email,[Status]=@Status WHERE [Id]=@AccountId";

            using (var con = await WriteConnectionAsync())
            {
                await con.ExecuteAsync(sql, new { Password = password, Email= email, AccountId=accountId, Status = status });
            }
        }

        public async Task SetPasswordAsync(Guid accountId, string password)
        {
            const string sql = @"UPDATE [dbo].[Accounts] SET [Password]=@Password WHERE [Id]=@AccountId";

            using (var con = await WriteConnectionAsync())
            {
                await con.ExecuteAsync(sql, new { AccountId = accountId, Password = password });
            }
        }

        public async Task SetEmailAsync(Guid accountId, string email)
        {
            const string sql = @"UPDATE [dbo].[Accounts] SET [Email]=@Email WHERE [Id]=@AccountId";

            using (var con = await WriteConnectionAsync())
            {
                await con.ExecuteAsync(sql, new { AccountId = accountId, Email = email });
            }
        }
    }
}
