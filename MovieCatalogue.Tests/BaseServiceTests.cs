using MovieCatalogue.Services.Data;
using NUnit.Framework;
using System;

namespace MovieCatalogue.Tests
{
    [TestFixture]
    public class BaseServiceTests
    {
        private BaseService _baseService;

        [SetUp]
        public void SetUp()
        {
            _baseService = new BaseService();
        }

        [Test]
        public void IsGuidValid_ReturnsTrue_ForValidGuid()
        {
            string validGuidString = Guid.NewGuid().ToString();
            Guid parsedGuid = Guid.Empty;

            bool result = _baseService.IsGuidValid(validGuidString, ref parsedGuid);

            Assert.IsTrue(result);
            Assert.AreNotEqual(Guid.Empty, parsedGuid);
        }

        [Test]
        public void IsGuidValid_ReturnsFalse_ForNullString()
        {
            string? nullString = null;
            Guid parsedGuid = Guid.Empty;

            bool result = _baseService.IsGuidValid(nullString, ref parsedGuid);

            Assert.IsFalse(result);
            Assert.AreEqual(Guid.Empty, parsedGuid);
        }

        [Test]
        public void IsGuidValid_ReturnsFalse_ForEmptyString()
        {
            string emptyString = "";
            Guid parsedGuid = Guid.Empty;

            bool result = _baseService.IsGuidValid(emptyString, ref parsedGuid);

            Assert.IsFalse(result);
            Assert.AreEqual(Guid.Empty, parsedGuid);
        }

        [Test]
        public void IsGuidValid_ReturnsFalse_ForInvalidGuidString()
        {

            string invalidGuidString = "invalid-guid";
            Guid parsedGuid = Guid.Empty;

            bool result = _baseService.IsGuidValid(invalidGuidString, ref parsedGuid);


            Assert.IsFalse(result);
            Assert.AreEqual(Guid.Empty, parsedGuid);
        }
    }
}
