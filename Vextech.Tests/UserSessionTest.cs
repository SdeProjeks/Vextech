using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vextech_API.Models;
using Vextech_API.Controllers;
using Xunit;

namespace Vextech.Tests
{
    public class UserSessionTest
    {
        [Theory]
        [InlineData("jidaojdaeiodjaeiopdjaeiopdjaeiojdiaeodjaeiodjaeiodjaeiodjaeiodjaeiodjaeiopdjaeiodjiaeojdioaejdeaiojt", "Session")]
        [InlineData("asdfghjkjhgfdsasdrtyuioiuytrewqwertyuilkmnbvcdrtyujbvcdrtyhgvcdrtyhbvcdrtyuhgvcxsertgcdrtyhgfrtyhgfd", "Session")]
        public void SessionDoesNotExsist_sholdwork(string session, string Session)
        {
            Assert.Throws<ArgumentException>(Session, () => UserSessionController.SessionExist(session));
        }

        [Theory]
        [InlineData("comments_create", "jidaojdaeiodjaeiopdjaeiopdjaeiojdiaeodjaeiodjaeiodjaeiodjaeiodjaeiodjaeiopdjaeiodjiaeojdioaejdeaiojd")]
        public void SessionPermissionGrant_Granted(string permission, string oldsession)
        {
            // Arrange
            string expected = "Granted";

            //Act
            string actual = UserSessionController.SessionPermissionGrant(permission, oldsession);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("comments_create", "asdfghjkjhgfdsasdrtyuioiuytrewqwertyuilkmnbvcdrtyujbvcdrtyhgvcdrtyhbvcdrtyuhgvcxsertgcdrtyhgfrtyhgft")]
        [InlineData("comments_update_all", "asdfghjkjhgfdsasdrtyuioiuytrewqwertyuilkmnbvcdrtyujbvcdrtyhgvcdrtyhbvcdrtyuhgvcxsertgcdrtyhgfrtyhgft")]
        public void SessionPermissionGrant_Denied(string permission, string oldsession)
        {
            // Arrange
            string expected = "Denied";

            //Act
            string actual = UserSessionController.SessionPermissionGrant(permission, oldsession);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("comments_update_own", "jidaojdaeiodjaeiopdjaeiopdjaeiojdiaeodjaeiodjaeiodjaeiodjaeiodjaeiodjaeiopdjaeiodjiaeojdioaejdeaiojd", "Param")]
        public void SessionPermissionGrant_exception(string permission, string oldsession, string param)
        {
            Assert.Throws<ArgumentException>(param, () => UserSessionController.SessionPermissionGrant(permission,oldsession));
        }

        //[Theory]
        //[InlineData("asdfghjkjhgfdsasdrtyuioiuytrewqwertyuilkmnbvcdrtyujbvcdrtyhgvcdrtyhbvcdrtyuhgvcxsertgcdrtyhgfrtyhgft")]
        //public void SessionLoginHandler_Update(string oldsession)
        //{
        //    //Arrange
        //    string expected = "Session has been updated.";

        //    //Act
        //    string actual = UserSessionController.UserLoginSessionHandler(1,oldsession);

        //    //Assert
        //    Assert.Equal(expected, actual);
        //}

        //[Theory]
        //[InlineData(3, "jidaojdaeiodjaeiopdjaeiopdjaeiojdiaeodjaeiodjaeiodjaeiodjaeiodjaeiodjaeiopdjaeiodjiaeojdioaejdeaiojd")]
        //public void SessionLoginHandler_Delete(ulong userID,string oldsession)
        //{
        //    //Arrange
        //    string expected = "Session has expired.";

        //    //Act
        //    string actual = UserSessionController.UserLoginSessionHandler(userID,oldsession);

        //    //Assert
        //    Assert.Equal(expected, actual);
        //}
    }
}
