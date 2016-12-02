
using System;
using System.Linq;
using System.Threading.Tasks;
using Code4Cash.Data.Databse;
using Code4Cash.Data.Models.Entities;

namespace Code4Cash.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Code4Cash.Data.Databse.Code4CashDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Code4CashDbContext context)
        {
            SeedRooms();
            SeedBookings();
        }

        private void SeedBookings()
        {
            using (var db = new Database())
            {
                var bookingsRepo = db.Repo<BookingEntity>();
                if (bookingsRepo.Count() >= 4)
                {
                    return;
                }

                var roomsRepo = db.Repo<MeetingRoomEntity>();
                var rooms = roomsRepo.All().Result.ToList();

                var booking1 = new BookingEntity
                {
                    Start = DateTime.Now.AddDays(-2),
                    End = DateTime.Now.AddDays(-2).AddHours(2),
                    MeetingRoom = rooms[0]
                };
                var booking2 = new BookingEntity
                {
                    Start = DateTime.Now.AddDays(-3),
                    End = DateTime.Now.AddDays(-3).AddHours(2),
                    MeetingRoom = rooms[1]
                };
                var booking3 = new BookingEntity
                {
                    Start = DateTime.Now.AddDays(-4),
                    End = DateTime.Now.AddDays(-4).AddHours(2),
                    MeetingRoom = rooms[2]
                };
                var booking4 = new BookingEntity
                {
                    Start = DateTime.Now.AddDays(-5),
                    End = DateTime.Now.AddDays(-5).AddHours(2),
                    MeetingRoom = rooms[2]
                };

                bookingsRepo.Add(booking1).Wait();
                bookingsRepo.Add(booking2).Wait();
                bookingsRepo.Add(booking3).Wait();
                bookingsRepo.Add(booking4).Wait();
            }
        }
        private void SeedRooms()
        {
            using (var db = new Database())
            {
                var roomsRepo = db.Repo<MeetingRoomEntity>();
                if (roomsRepo.Count(room => true) >= 3)
                {
                    return;
                }

                var room1 = new MeetingRoomEntity { Capacity = 15, Name = "B528" };
                var room2 = new MeetingRoomEntity { Capacity = 4, Name = "B530" };
                var room3 = new MeetingRoomEntity { Capacity = 20, Name = "B520" };

                var asset1 = new AssetEntity { Name = "Small Projector", Description = "A small projector" };
                var asset2 = new AssetEntity { Name = "Big Projector", Description = "A big projector" };
                var asset3 = new AssetEntity { Name = "Medium Projector", Description = "A medium projector" };
                var asset4 = new AssetEntity { Name = "White Projector", Description = "A white projector" };

                room1.Assets.Add(asset1);
                room2.Assets.Add(asset2);
                room3.Assets.Add(asset3);
                room3.Assets.Add(asset4);

                roomsRepo.Add(room1).Wait();
                roomsRepo.Add(room2).Wait();
                roomsRepo.Add(room3).Wait();

            }
        }
    }
}
