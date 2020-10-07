using Moq;
using System;
using Xunit;
using Microsoft.Extensions.Configuration;

namespace AlexApi.AppServices.Tests
{
    public class UnitTest1
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }

        [Fact]
        public void Test_GeneratingJWTToken()
        {
            // Arrange
            var config = InitConfiguration();

            //var configMock = new Mock<IConfiguration>();

            var jwtService = new JwtTokenService(config);

            // Act
            var token = jwtService.GenerateJWTToken("Alex");

            // Assert
            Assert.True(token.Length > 32);
        }
    }
}
