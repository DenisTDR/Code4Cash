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
        private DatabaseLayer _databaseLayer;

        public AuthLayer()
        {
            _databaseLayer = new DatabaseLayer();
        }

        public async Task<bool> CheckCredentials(LoginRequestModel model)
        {
            return await GetAccount(model) != null;
        }

        public async Task<SessionEntity> CreateSession(LoginRequestModel model)
        {
            var account = await GetAccount(model);
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
            await _databaseLayer.Repo<SessionEntity>().Add(session);
            return session;
        }

        public async Task<bool> CheckSession(string sessionToken)
        {
            return await GetSession(sessionToken) != null;
        }

        public async Task<AccountEntity> GetAccount(string sessionToken)
        {
            var session = await GetSession(sessionToken);
            if (session == null)
            {
                throw new Exception("Invalid session ...");
            }
            return session.Account;
        }

        private async Task<AccountEntity> GetAccount(LoginRequestModel model)
        {
            var hashedPassword = Utilis.CalculateMd5(model.Password);
            var accountsRepo = _databaseLayer.Repo<AccountEntity>();
            var account =
                await
                    accountsRepo.GetOne(
                        acc => acc.Active && acc.Username.Equals(model.Username) && acc.Password.Equals(hashedPassword));
            return account;
        }

        private async Task<SessionEntity> GetSession(string sessionToken)
        {
            return await _databaseLayer.Repo<SessionEntity>().GetOne(session => session.Token == sessionToken);
        }

        public void Dispose()
        {
            _databaseLayer?.Dispose();
            _databaseLayer = null;
        }
    }
}