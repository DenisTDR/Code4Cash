using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Code4Cash.Controllers.Base;
using Code4Cash.Data.Models.Entities;
using Code4Cash.Data.Models.ModelMappings;
using Code4Cash.Data.Models.ViewModels;

namespace Code4Cash.Controllers
{
    public class MeetingRoomController:GenericController<MeetingRoomEntity, MeetingRoomViewModel>
    {
    }
}