using AutomatadanParameterization.Services;

namespace AutomatadanParameterization.Test
{
    [TestClass]
    public sealed class Test1
    {
        private LoginAutomataService _loginService;

        [TestInitialize]
        public void Setup()
        {
            _loginService = new LoginAutomataService();
        }

        [TestMethod]
        public void LoginBerhasil()
        {
            // Act
            var result = _loginService.Login("admin01", "adminpass");

            Console.WriteLine(result);
            // Assert
            Assert.AreEqual("Selamat datang Admin.", result);
        }

        [TestMethod]
        public void LoginGagal()
        {
            var result = _loginService.Login("admin01", "salahpassword");

            Console.WriteLine(result);
            Assert.AreEqual("Login gagal. Username atau password salah.", result);
        }

        [TestMethod]
        public void Logout()
        {
            _loginService.Login("admin01", "adminpass");

            var result = _loginService.Logout();

            Console.WriteLine(result);
            Assert.AreEqual("Anda telah logout.", result);
        }

        [TestMethod]
        public void Login_TanpaLogout()
        {
            // Arrange: Login pertama sebagai admin
            _loginService.Login("admin01", "adminpass");

            // Act: Coba login lagi sebagai guru tanpa logout
            var result = _loginService.Login("guru01", "gurupass");

            // Output hasil login kedua untuk membantu debug (opsional)
            Console.WriteLine(result); // debug output sementara

            // Assert: Pastikan login kedua ditolak karena belum logout dari sesi pertama
            Assert.AreEqual("Sudah login sebagai admin01 (Admin), silakan logout dulu.", result);
        }


    }
}
