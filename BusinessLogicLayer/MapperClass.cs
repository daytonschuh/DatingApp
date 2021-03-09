using System;
using ModelLayer;

namespace BusinessLogicLayer
{
    public class MapperClass
    {
        internal UserDto ConvertAppUserToUserDto(AppUser user)
        {
            UserDto convertedUser = new UserDto
            {
                Username = user.UserName
            };

            return convertedUser;
        }
    }
}