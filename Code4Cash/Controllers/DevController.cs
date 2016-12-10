using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Code4Cash.Data.Database;
using Code4Cash.Data.Models.Entities;
using Code4Cash.Data.Models.Entities.Base;
using Code4Cash.Data.Models.ModelMappings.Base;
using Code4Cash.Data.Models.ViewModels;
using Code4Cash.Data.Models.ViewModels.Base;
using Code4Cash.Misc;

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
            var list = EntityViewModelMap<Entity, ViewModel>.GetAllEntityMaps().ToList();
            var list2 = new List<object>();

            list.ToList().ForEach(mappingType =>
            {
                var obj = (IEntityViewModelMap) Activator.CreateInstance(mappingType);
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
            var repo = new DatabaseLayer().Repo<MeetingRoomEntity>();
            var mr2 = await repo.Add(meetingRoom);

            return Ok(mr2);

        }

        [HttpGet]
        public async Task<IHttpActionResult> Test6()
        {
            var db = new DatabaseLayer();
            var repo = db.Repo<BookingEntity>();
            var repo2 = db.Repo<MeetingRoomEntity>();

            var obj = await repo.GetOneByType("85e899d4-3284-427f-94d2-876d95923af4", typeof(MeetingRoomEntity));

            var obj2 = await repo.GetOneByType("923161cf-df93-4c71-ac00-17ea28a13ca2", typeof(BookingEntity));


            return Ok(new object[] {obj, obj2});
        }

        [HttpGet]
        public async Task<IHttpActionResult> Test7()
        {
            var props1 = ReflectionExtensions.GetPropeties<BookingEntity>(CustomPropertyTypes.Enumerable).Select(prop => prop.Name);
            var props2 = ReflectionExtensions.GetPropeties<BookingEntity>(CustomPropertyTypes.Updatable | CustomPropertyTypes.NonEntity).Select(prop => prop.Name);
            var props3 = ReflectionExtensions.GetPropeties<BookingEntity>(CustomPropertyTypes.Updatable | CustomPropertyTypes.Entity).Select(prop => prop.Name);
            var props4 = ReflectionExtensions.GetPropeties<MeetingRoomEntity>(CustomPropertyTypes.Enumerable).Select(prop => prop.Name);

            return Ok(new [] {props1, props2, props3, props4});
        }

        [HttpGet]
        public async Task<IHttpActionResult> Test8()
        {

            var mappingTypes =
                EntityViewModelMap<Entity, ViewModel>.GetAllEntityMaps().ToList();

            var types2 = mappingTypes
                    .Where(type => typeof(IEntityViewModelMap).IsAssignableFrom(type));

            return Ok(new {test1 = mappingTypes.Select(type => type.Name), test2 = types2.Select(type => type.Name)});
        }
    }
}