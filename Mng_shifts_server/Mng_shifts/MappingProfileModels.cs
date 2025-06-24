    using AutoMapper;
    using Mng_shifts.Core.Entities;
namespace Mng_shifts.Api
{


    public class MappingProfileModels : Profile
    {
        public MappingProfileModels()
        {
            CreateMap<CreateEmployeeModel, Employee>();
            CreateMap<CreateShiftModel, Employee>();
            CreateMap<UpdateShiftStatusModel, Shift>();
            CreateMap<CreateSwapRequestModel, SwapRequest>();
            CreateMap<CreateSwapProposalModel, SwapProposal>();
            CreateMap<UpdateProposalStatusModel, SwapProposal>();

            
        }
    }

}
