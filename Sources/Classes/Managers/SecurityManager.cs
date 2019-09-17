using CredentialManagement;

namespace Measurements.UI.Managers
{

    public class UserCredential
    {
        public UserCredential(string login, string password)
        {
            this.Login = login;
            this.Password = password;
        }
        public string Login { get; set; }
        public string Password { get; set; }

    }

    public static class SecurityManager
    {
        public static UserCredential GetCredential(string target)
        {
            var cm = new Credential { Target = target };
            if (!cm.Load())
            {
                return null;
            }

            return new UserCredential(cm.Username, cm.Password);
        }

        public static bool SetCredential(string target, string username, string password)
        {
            return new Credential
            {
                Target = target,
                Username = username,
                Password = password,
                PersistanceType = PersistanceType.LocalComputer
            }.Save();
        }

        public static bool RemoveCredentials(string target)
        {
            return new Credential { Target = target }.Delete();
        }
    }
}
