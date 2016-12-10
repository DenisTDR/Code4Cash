using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Code4Cash.Data.Database;
using Code4Cash.Data.Models.Entities.Users;
using Code4Cash.Data.Models.RequestModels;
using Code4Cash.Misc;

namespace Code4Cash.Data.Users
{
    public class AuthLayer:IDisposable
    {
        private DatabaseUnit _databaseUnit;

        public AuthLayer()
        {
            this._databaseUnit = new DatabaseUnit();
        }

        public async Task<bool> CheckCredentials(LoginRequestModel model)
        {
            return await this.GetAccount(model) != null;
        }

        public async Task<SessionEntity> CreateSession(LoginRequestModel model)
        {
            var account = await this.GetAccount(model);
            if (account == null)
            {
                throw new Exception("Invalid credentials ...");
            }
            var session = new SessionEntity
            {
                Account = account,
                Token = Utilis.GenerateRandomString(40),
                Expiration = DateTime.Now.AddDays(1)
            };
            await this._databaseUnit.Repo<SessionEntity>().Add(session);
            return session;
        }

        public async Task<bool> CheckSession(string sessionToken)
        {
            return await this.GetSession(sessionToken) != null;
        }

        public async Task<AccountEntity> GetAccount(string sessionToken)
        {
            var session = await this.GetSession(sessionToken);
            if (session == null)
            {
                throw new Exception("Invalid session ...");
            }
            return session.Account;
        }

        private async Task<AccountEntity> GetAccount(LoginRequestModel model)
        {
            var hashedPassword = Utilis.CalculateMd5(model.Password);
            var accountsRepo = _databaseUnit.Repo<AccountEntity>();
            var account =
                await accountsRepo.GetOne(acc => acc.Username.Equals(model.Username) && acc.Password.Equals(hashedPassword));
            return account;
        }

        private async Task<SessionEntity> GetSession(string sessionToken)
        {
            return await _databaseUnit.Repo<SessionEntity>().GetOne(session => session.Token == sessionToken);
        }

        public void Dispose()
        {
            this._databaseUnit?.Dispose();
            this._databaseUnit = null;
        }
    }
}