using AutoMapper;
using Code4Cash.Data.Models.Entities;
using Code4Cash.Data.Models.ModelMappings.Base;
using Code4Cash.Data.Models.ViewModels;

namespace Code4Cash.Data.Models.ModelMappings
{
    public class MeetingRoomMap : EntityViewModelMap<MeetingRoomEntity, MeetingRoomViewModel>
    {
        public MeetingRoomMap()
        {
            HasMany(r => r.Accounts).WithMany(a => a.MeetingRooms).Map(mc =>
            {
                mc.ToTable("MeetingRoomAccountRellation");
            });
        }

        public override void ConfigureEntityToViewModelMapper(IMapperConfigurationExpression configurationExpression)
        {
            configurationExpression.CreateMap<MeetingRoomEntity, MeetingRoomViewModel>()
                .AfterMap((entity, viewModel) =>
                {
                    viewModel.Id = entity.Selector;
                    viewModel.Floor.MeetingRooms = null;
                }).PreserveReferences();
        }
    }
}