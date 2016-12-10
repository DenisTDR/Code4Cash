using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Code4Cash.Data.Database;
using Code4Cash.Data.Models.Entities;
using Code4Cash.Data.Models.Entities.Users;
using Code4Cash.Misc;

namespace Code4Cash.Migrations
{
    public class DatabaseSeeder
    {
        private static Random _rand = new Random(DateTime.Now.Millisecond);
        public void Seed()
        {
            SeedFloors();
            SeedRooms();
            SeedRoles();
            SeedAccounts();
            SeedRemainingRooms();
            SeedBookings();
        }
        private void SeedFloors()
        {
            using (var db = new DatabaseLayer())
            {
                var floorsRepo = db.Repo<FloorEntity>();
                if (floorsRepo.Count() >= 3)
                {
                    return;
                }
                for (var i = 0; i <= 4; i++)
                {
                    var floor = new FloorEntity
                    {
                        Name = "Floor " + i,
                        Level = i
                    };
                    floorsRepo.Add(floor).Wait();
                }
            }
        }

        private void SeedBookings()
        {
            using (var db = new DatabaseLayer())
            {
                var bookingsRepo = db.Repo<BookingEntity>();
                if (bookingsRepo.Count() >= 4)
                {
                    return;
                }
                var roomsRepo = db.Repo<MeetingRoomEntity>();
                
                foreach (var room in roomsRepo.All().Result)
                {
                    var bookingsCount = _rand.Next(3, 10);
                    var dt = DateTime.Now.AddDays(_rand.Next(4));
                    
                    for (var i = 0; i < bookingsCount; i++)
                    {
                        if (room.Accounts.Count == 0)
                        {
                        }
                        var booking = new BookingEntity
                        {
                            MeetingRoom = room,
                            Owner = room.Accounts[_rand.Next(0, room.Accounts.Count)],
                            Start = dt,
                            End = dt = dt.AddHours(_rand.Next(1, 4))
                        };
                        bookingsRepo.Add(booking).Wait();
                        dt = dt.AddDays(_rand.Next(0, 5));
                    }
                }

            }
        }

        private void SeedRooms()
        {
            using (var db = new DatabaseLayer())
            {
                var roomsRepo = db.Repo<MeetingRoomEntity>();
                if (roomsRepo.Count() >= 3)
                {
                    return;
                }
                var floorsRepo = db.Repo<FloorEntity>();
                foreach (var floorEntity in floorsRepo.All().Result)
                {
                    var roomsCount = _rand.Next(3, 30);
                    for (var i = 0; i < roomsCount; i++)
                    {
                        var room = new MeetingRoomEntity
                        {
                            Assets = GetRandomAssetsCollection(),
                            Capacity = _rand.Next(5, 25),
                            Floor = floorEntity,
                            Name = floorEntity.Level + (i < 10 ? "0" : "") + i
                        };
                        roomsRepo.Add(room).Wait();
                    }
                }
                
            }
        }

        private List<AssetEntity> GetRandomAssetsCollection()
        {
            var list = new List<AssetEntity>();
            //projector, whiteboard, flip board, tv, video conferencing equipment
            var possibleAssets = new[] {"Projector", "Whiteboard", "Flipboard", "TV", "Conferencing equipment"};
            var assetsCount = _rand.Next(2, 6);
            for (var i = 0; i < assetsCount; i++)
            {
                var name = possibleAssets[_rand.Next(0, possibleAssets.Length)] + " " + _rand.Next(100, 1000);
                var asset = new AssetEntity
                {
                    Name = name,
                    Description = name + " - Some random description ... "
                };
                list.Add(asset);
            }

            return list;
        }

        private void SeedRoles()
        {
            using (var db = new DatabaseLayer())
            {
                var rolesRepo = db.Repo<RoleEntity>();
                if (rolesRepo.Count() >= 3)
                {
                    return;
                }

                var role1 = new RoleEntity { Name = "CEO", Power = 1, Functions = RoleFunction.All };
                var role2 = new RoleEntity { Name = "Administrator", Power = 1, Functions = RoleFunction.All };
                var role3 = new RoleEntity
                {
                    Name = "ProjectManager",
                    Power = 2,
                    Functions = RoleFunction.BookMeetingRooms
                };
                var role4 = new RoleEntity
                {
                    Name = "Developer",
                    Power = 3,
                    Functions =
                        RoleFunction.BookMeetingRooms | RoleFunction.CanUpdateEntities | RoleFunction.CanAddNewEntities
                };
                var role5 = new RoleEntity
                {
                    Name = "Tester",
                    Power = 3,
                    Functions =
                        RoleFunction.BookMeetingRooms | RoleFunction.CanUpdateEntities | RoleFunction.CanAddNewEntities
                };
                var role6 = new RoleEntity { Name = "None", Power = 4, Functions = RoleFunction.None };

                rolesRepo.Add(role6, role1, role2, role3, role4, role5).Wait();
            }
        }

        private void SeedAccounts()
        {
            using (var db = new DatabaseLayer())
            {
                var accountsRepo = db.Repo<AccountEntity>();
                if (accountsRepo.Count() >= 2)
                {
                    return;
                }
                var rooms = db.Repo<MeetingRoomEntity>().All().Result.ToList();
                var roles = db.Repo<RoleEntity>().All().Result.ToList();

                for (var i = 0; i < roles.Count*2; i++)
                {
                    var account = new AccountEntity
                    {
                        Username = "user" + i,
                        Role = roles[i%roles.Count],
                        MeetingRooms = GetRandomRoomsFrom(rooms),
                        Password = Utilis.CalculateMd5("parola01"),
                        Active = true
                    };
                    accountsRepo.Add(account).Wait();
                }
            }
        }

        private void SeedRemainingRooms()
        {
            using (var db = new DatabaseLayer())
            {
                var roomsRepo = db.Repo<MeetingRoomEntity>();
                var rooms = roomsRepo.All().Result.ToList();
                var accounts = db.Repo<AccountEntity>().All().Result.ToList();
                foreach (var room in rooms)
                {
                    while (room.Accounts.Count < 2)
                    {
                        var randAccount = accounts[_rand.Next(accounts.Count)];
                        if(room.Accounts.Contains(randAccount))
                            continue;
                        room.Accounts.Add(randAccount);
                        roomsRepo.Update(room.Selector, room).Wait();
                        roomsRepo.SaveChangesAsync().Wait();
                    }
                    
                }
            }
        }
        private List<MeetingRoomEntity> GetRandomRoomsFrom(IReadOnlyList<MeetingRoomEntity> list)
        {
            var newList = new List<MeetingRoomEntity>();
            var howMany = _rand.Next(6, list.Count - 2);
            for (var i = 0; i < howMany; i++)
            {
                var index = _rand.Next(0, list.Count);
                if (newList.Contains(list[index]))
                {
                    howMany--;
                }
                else
                {
                    newList.Add(list[index]);
                }
            }
            return newList;
        }

    }
}