﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Helpers;

namespace ReelioUnitTests
{
    [TestClass]
    public class EncryptionHelperTests
    {

            [TestMethod]
            public void Encrypt_ReturnsValidEncryptedPassword()
            {
                // Arrange
                string password = "SecretPassword";

                // Act
                var encryptedPassword = EncryptionHelper.Encrypt(password);

                // Assert
                Assert.IsNotNull(encryptedPassword);
                StringAssert.Contains(encryptedPassword, ":"); // Ensure separator exists
            }

            [TestMethod]
            public void Verify_WithCorrectPassword_ReturnsTrue()
            {
                // Arrange
                string password = "SecretPassword";
                string encryptedPassword = EncryptionHelper.Encrypt(password);

                // Act
                bool result = EncryptionHelper.Verify(password, encryptedPassword);

                // Assert
                Assert.IsTrue(result);
            }

            [TestMethod]
            public void Verify_WithIncorrectPassword_ReturnsFalse()
            {
                // Arrange
                string correctPassword = "SecretPassword";
                string incorrectPassword = "WrongPassword";
                string encryptedPassword = EncryptionHelper.Encrypt(correctPassword);

                // Act
                bool result = EncryptionHelper.Verify(incorrectPassword, encryptedPassword);

                // Assert
                Assert.IsFalse(result);
            }
            [TestMethod]
            public void Verify_WithInvalidEncryptedPassword_ReturnsFalse()
            {
                // Arrange
                string password = "SecretPassword";
                string invalidEncryptedPassword = "invalidPassword";

                // Act
                bool result = EncryptionHelper.Verify(password, invalidEncryptedPassword);

                // Assert
                Assert.IsFalse(result);
            }
     }
}