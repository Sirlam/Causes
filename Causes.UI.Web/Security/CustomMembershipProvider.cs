using Causes.UI.Web.Data;
using Causes.UI.Web.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Security;
using System.Web.Caching;

namespace Causes.UI.Web.Security
{
    public class CustomMembershipProvider : MembershipProvider
    {
        private int _cacheTimeoutInMinutes = 30;
        static PasswordManager pwdManager = new PasswordManager();
        public AppDbContext db = new AppDbContext();

        private const string successStat = "0";

        public override void Initialize(string name, NameValueCollection config)
        {
            int val;
            if (!string.IsNullOrEmpty(config["cacheTimeoutInMinutes"]) && Int32.TryParse(config["cacheTimeoutInMinutes"], out val))
                _cacheTimeoutInMinutes = val;
            base.Initialize(name, config);
        }

        public override bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;
            int authSetting = int.Parse(ConfigurationManager.AppSettings["LDAPAuth"]);

            AuthenticationType authType = (AuthenticationType)(authSetting);

            switch (authType)
            {
                case AuthenticationType.ApplicationAuth:
                    var localUser = db.TB_USERS.Where(x => x.USER_ID.ToLower() == username.ToLower()).FirstOrDefault();

                    if (localUser != null)
                    {
                        if (!pwdManager.IsPasswordMatch(password, localUser.PASSWORD_SALT, localUser.PASSWORD_HASH))
                            localUser = null;
                    }
                    return localUser != null;
                    break;
                default:
                    return false;
                    break;
            }
        }

        public bool CreateUser(string Username, string Email, string password, int RoleId, string Fname, string LName)
        {
            string salt = String.Empty;

            TB_USERS up = new TB_USERS();
            string passwordHash = pwdManager.GeneratePasswordHash(password, out salt);
            int profileId = 0;

            using (var db = new AppDbContext())
            {
                var user = new TB_USERS
                {
                    USER_ID = Username,
                    EMAIL = Email,
                    FIRST_NAME = Fname,
                    LAST_NAME = LName,
                    DISPLAY_NAME = Fname + " " + LName,
                    ROLE_ID = RoleId,
                    PASSWORD_HASH = passwordHash,
                    PASSWORD_SALT = salt,
                    CREATED_DATE = DateTime.Now,
                    LAST_LOGIN_DATE = DateTime.Now
                };
                db.TB_USERS.Add(user);
                db.SaveChanges();
                profileId = user.ID;
                return true;
            }
            return false;
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var cacheKey = string.Format("UserData_{0}", username);
            if (HttpRuntime.Cache[cacheKey] != null)
                return (CustomMembershipUser)HttpRuntime.Cache[cacheKey];

            using(var context = new AppDbContext())
            {
                var user = (from u in context.TB_USERS.Include(x => x.TB_ROLES)
                            where String.Compare(u.USER_ID.ToUpper(), username.ToUpper(), StringComparison.OrdinalIgnoreCase) == 0
                            select u).FirstOrDefault();

                if (user == null)
                    return null;

                var memebershipUser = new CustomMembershipUser(user);
                if(userIsOnline)
                {
                    TB_USERS tB_USERS = db.TB_USERS.Find(memebershipUser.ProfileId);

                    tB_USERS.LAST_LOGIN_DATE = DateTime.Now;
                    db.Entry(tB_USERS).State = EntityState.Modified;
                    db.SaveChanges();
                }
                HttpRuntime.Cache.Insert(cacheKey, memebershipUser, null, DateTime.Now.AddMinutes(_cacheTimeoutInMinutes), Cache.NoSlidingExpiration);
                return memebershipUser;
            }
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval => throw new NotImplementedException();

        public override bool EnablePasswordReset => throw new NotImplementedException();

        public override bool RequiresQuestionAndAnswer => throw new NotImplementedException();

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override int MaxInvalidPasswordAttempts => throw new NotImplementedException();

        public override int PasswordAttemptWindow => throw new NotImplementedException();

        public override bool RequiresUniqueEmail => throw new NotImplementedException();

        public override MembershipPasswordFormat PasswordFormat => throw new NotImplementedException();

        public override int MinRequiredPasswordLength => throw new NotImplementedException();

        public override int MinRequiredNonAlphanumericCharacters => throw new NotImplementedException();

        public override string PasswordStrengthRegularExpression => throw new NotImplementedException();

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            int status = 0;
            TB_USERS up = new TB_USERS();

            var count = from n in db.TB_USERS
                        select new { n.ID };

            status = count.Count();

            return status;
        }


    }
}