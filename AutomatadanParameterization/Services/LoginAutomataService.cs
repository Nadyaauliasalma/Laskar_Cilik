using AutomatadanParameterization.Models;
namespace AutomatadanParameterization.Services
{
    public class LoginAutomataService
    {
        private LoginState currentState = LoginState.Start;
        private readonly Dictionary<string, User> userDatabase = new();
        private User? loggedInUser;

        public LoginAutomataService()
        {
            // Seed data pengguna
            userDatabase["admin01"] = new User("admin01", "adminpass", UserRole.Admin);
            userDatabase["guru01"] = new User("guru01", "gurupass", UserRole.Guru);
        }

        public string Login(string username, string password)
        {
            // ❗ Cek apakah sudah login
            if (IsLoggedIn)
            {
                return $"Sudah login sebagai {loggedInUser?.Username} ({loggedInUser?.Role}), silakan logout dulu.";
            }


            // Precondition
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Username dan password tidak boleh kosong.");

            // Validasi login
            if (userDatabase.ContainsKey(username) && userDatabase[username].Password == password)
            {
                loggedInUser = userDatabase[username];
                currentState = LoginState.Authenticated;
                System.Diagnostics.Debug.Assert(loggedInUser != null);
                return Transition(loggedInUser.Role);
            }
            else
            {
                currentState = LoginState.Start;
                loggedInUser = null;
                return "Login gagal. Username atau password salah.";
            }
        }


        private string Transition(UserRole role)
        {
            return role switch
            {
                UserRole.Admin => (currentState = LoginState.AdminPanel, "Selamat datang Admin.").Item2,
                UserRole.Guru => (currentState = LoginState.GuruPanel, "Selamat datang Guru.").Item2,
                _ => "Role tidak dikenali."
            };
        }

        public string Logout()
        {
            currentState = LoginState.End;
            loggedInUser = null;
            return "Anda telah logout.";
        }
        // Properti ini digunakan untuk memeriksa apakah pengguna saat ini sudah login.
        // Mengembalikan true jika currentState menunjukkan pengguna sudah login dan berada di panel Admin atau Guru.
        public bool IsLoggedIn =>
           currentState == LoginState.Authenticated ||
           currentState == LoginState.AdminPanel ||
           currentState == LoginState.GuruPanel;
        public LoginState GetCurrentState()
        {
            return currentState;
        }

    }

}
