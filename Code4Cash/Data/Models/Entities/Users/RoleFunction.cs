using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Code4Cash.Data.Models.Entities.Users
{
    [Flags]
    public enum RoleFunction
    {
        None = 0,
        ChangeMeetingRoomAssets = 1,
        BookMeetingRooms = 2,
        CanChangeRoles = 4,
        CanAddNewEntities = 8,
        CanUpdateEntities = 16,
        All = 31
    }
}