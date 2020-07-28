using AutoMapper;
using BrightCare.Common;
using BrightCare.Common.IService;
using BrightCare.Common.Model;
using BrightCare.Common.Service;
using BrightCare.Dtos.Agency.MasterServices;
using BrightCare.Entity.Agency;
using BrightCare.Repository.Interface.Agency.MasterService;
using BrightCare.Service.Interface.Agency.MasterServices;
using HC.Common.HC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using static BrightCare.Common.Enums.CommonEnum;

namespace BrightCare.Service.Agency.MasterService
{
    public class MasterServicesService : BaseService, IMasterServices
    {
        private readonly IMasterServicesRepository imasterServicesRepository;
        private readonly IMapper _mapper;

        public MasterServicesService(IMasterServicesRepository imasterServicesRepository, IMapper mapper)
        {
            this.imasterServicesRepository = imasterServicesRepository;
            _mapper = mapper;

        }       

        public JsonModel GetMasterService(TokenModel token)
        {
            List<MasterServicesDTO> masterServicesModel = new List<MasterServicesDTO>();                        
            List<MasterServices> masterService = imasterServicesRepository.GetAll(l => l.IsDeleted == false && l.OrganizationId == 2).ToList();// token.OrganizationID);
            masterServicesModel = _mapper.Map<List<MasterServicesDTO>>(masterService); // Mapping

            return new JsonModel(masterServicesModel, StatusMessage.Success, (int)HttpStatusCodes.OK);
        }



        public JsonModel AddUpdateMasterService(MasterServicesDTO masterServices, TokenModel token)
        {
            JsonModel Result = new JsonModel()
            {
                data = false,
                Message = StatusMessage.Success,
                StatusCode = (int)HttpStatusCodes.OK
            };
            MasterServices masterServiceEntity = null;
            DateTime CurrentDate = DateTime.UtcNow;

            if (masterServices.Id == 0 || masterServices.Id == null)
            {                
                masterServiceEntity = _mapper.Map<MasterServices>(masterServices);
                masterServiceEntity.OrganizationId = 2; // token.OrganizationID;
                masterServiceEntity.CreatedBy = 2;// token.UserID;
                masterServiceEntity.CreatedDate = CurrentDate;
                masterServiceEntity.IsActive = true;
                imasterServicesRepository.Create(masterServiceEntity);
                imasterServicesRepository.SaveChanges();
            }

            else
            {
                MasterServices masterService = imasterServicesRepository.Get(l => l.Id == masterServices.Id && l.OrganizationId == 2); // token.OrganizationID);
                masterService.UpdatedBy = 2; // token.UserID;
                masterService.UpdatedDate = CurrentDate;
                masterService.ServiceName = masterServices.ServiceName;
                imasterServicesRepository.Update(masterService);
                imasterServicesRepository.SaveChanges();
            }
            
            return Result;
        }


        public bool DeleteMasterService(int Id, TokenModel token)
        {
            MasterServices masterService = imasterServicesRepository.Get(l => l.Id == Id && l.OrganizationId == 2);// token.OrganizationID);
            masterService.IsDeleted = true;
            masterService.IsActive = false;
            masterService.DeletedBy = 2;// token.UserID;
            masterService.DeletedDate = DateTime.UtcNow;
            imasterServicesRepository.SaveChanges();

            return true;
        }

    }
}
