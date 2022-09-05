using ReqRes.Domain.ListUsers;
using System.Linq;
using Xunit;

namespace CreditCards.UITests.Tests
{
    public class ReqResAPITests
    {
        [Fact]
        public void Should_Return_User_Byron()
        {
            //-- Arrange
            var reqResClient = new ReqResClient();
            var listClients = reqResClient.GetListUsers();
            var expected = new ListItems()
            {
                Id = 10,
                Email = "byron.fields@reqres.in",
                FirstName = "Byron",
                LastName = "Fields",
                Avatar = "https://reqres.in/img/faces/10-image.jpg"
            };

            //-- Act
            var actual = listClients.Data.Where(x => x.FirstName == "Byron").FirstOrDefault();

            //-- Asset
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Email, actual.Email);
            Assert.Equal(expected.FirstName, actual.FirstName);
            Assert.Equal(expected.LastName, actual.LastName);
            Assert.Equal(expected.Avatar, actual.Avatar);

        }
    }
}
