using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Code4Cash.Data.Databse;
using Code4Cash.Data.Models.Entities;
using Code4Cash.Data.Models.Entities.Base;
using Code4Cash.Data.Models.ModelMappings.Base;
using Code4Cash.Data.Models.ViewModels;
using Code4Cash.Data.Models.ViewModels.Base;

namespace Code4Cash.Controllers
{
    public class DevController:ApiController
    {

        [HttpGet]
        public IHttpActionResult Test()
        {
            var booking = new BookingEntity {Start = DateTime.Now, End = DateTime.Now.AddYears(-20)};
            var bookingVM = Mapper.Map<BookingViewModel>(booking);
            var res = new {booking, bookingVM};
            return Ok(res);

//           
        }

        [HttpGet]
        public IHttpActionResult Test2()
        {
            var list = EntityMap<Entity, ViewModel>.GetAllEntityMaps().ToList();
            var list2 = new List<object>();

            list.ToList().ForEach(mappingType =>
            {
                var obj = (IEntityMap) Activator.CreateInstance(mappingType);
                list2.Add(obj);
            });

            return Ok(list2);
        }

        [HttpGet]
        public IHttpActionResult Test3()
        {
            var list = new List<object>();

            var room = new MeetingRoomEntity();
            
            return Ok(list);
        }

        [HttpGet]
        public IHttpActionResult Test4()
        {
            var meetingRoom = new MeetingRoomEntity();

            meetingRoom.Assets.Add(new AssetEntity {Name = "Projector"});
            meetingRoom.Assets.Add(new AssetEntity {Name = "Tv"});
            meetingRoom.Assets.Add(new AssetEntity {Name = "Whiteboard"});

            var mrVm = Mapper.Map<MeetingRoomViewModel>(meetingRoom);

            return Ok(new object[] {meetingRoom, mrVm });
        }


        [HttpGet]
        public async Task<IHttpActionResult> Test5()
        {
            var meetingRoom = new MeetingRoomEntity();
            meetingRoom.Name = "una nouă";
            meetingRoom.Capacity = 12;
            var repo = new Database().Repo<MeetingRoomEntity>();
            var mr2 = await repo.Add(meetingRoom);

            return Ok(mr2);

        }
    }
}