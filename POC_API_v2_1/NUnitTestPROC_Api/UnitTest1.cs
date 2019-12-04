using Xunit;

namespace NUnitTestPROC_Api
{
    public class APITest
    {
        [Fact]
        public async void InicioProcesoTest()
        {
            // Arrange  
            double expectedValue = 1;

            // Act  
            POC_API_v2_1.Controllers.APIController prueba = new POC_API_v2_1.Controllers.APIController();
            double actionResult = await prueba.IniciaLlamada();

            //Assert  
            Assert.Equal(expectedValue, actionResult, 1);
        }
    }
}