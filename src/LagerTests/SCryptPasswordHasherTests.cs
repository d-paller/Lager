using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lager.Services;
using Lager.Models;
using Microsoft.AspNetCore.Identity;

namespace LagerTests
{
    [TestClass]
    public class SCryptPasswordHasherTests
    {

        private string _password;
        private SCryptPasswordHasher _hasher;
        private User _testUser;

        // Arrange
        [TestInitialize]
        public void Initialize()
        {
            _password = "Password";
            _hasher = new SCryptPasswordHasher();
            _testUser = new User()
            {
                Admin = false,
                IsActive = true,
                Name = "Peter Pan",
                Username = "peter.pan"
            };
        }

        [TestMethod]
        public void HashPasswordTest()
        {
            // Arrange
            // Done in the test initialize

            // Act
            string result = _hasher.HashPassword(_testUser, _password);

            // Assert - Checks to see if the result is a string
            Assert.IsInstanceOfType(result, typeof(string));
        }

        [TestMethod]
        public void VerifyHashedPasswordTest()
        {
            // Arrange
            string hashedPassword = _hasher.HashPassword(_testUser, _password);
            string hashedPasswordForFailed = _hasher.HashPassword(_testUser, "DifferentPassword");

            // Act
            var sucessResult = _hasher.VerifyHashedPassword(_testUser, hashedPassword, _password);
            var failedResult = _hasher.VerifyHashedPassword(_testUser, hashedPasswordForFailed, _password);

            // Assert
            Assert.AreEqual(PasswordVerificationResult.Success, sucessResult);
            Assert.AreEqual(PasswordVerificationResult.Failed, failedResult);
        }

    }
}
