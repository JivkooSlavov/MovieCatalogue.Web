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

        [TestCase("2c4b23c8-3f5c-4e6a-b2f1-df4f5dd5f1bc", true)]
        [TestCase(null, false)]
        [TestCase("", false)] 
        [TestCase("invalid-guid", false)]
        public void IsGuidValid_ReturnsExpectedResult(string? input, bool expectedResult)
        {
            Guid parsedGuid = Guid.Empty;

            bool result = _baseService.IsGuidValid(input, ref parsedGuid);

            Assert.AreEqual(expectedResult, result);
            if (expectedResult)
            {
                Assert.AreNotEqual(Guid.Empty, parsedGuid);
            }
            else
            {
                Assert.AreEqual(Guid.Empty, parsedGuid);
            }
        }
    }
}
