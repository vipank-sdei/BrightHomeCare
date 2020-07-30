using AutoMapper;
using BrightCare.Common;
using BrightCare.Common.Model;
using BrightCare.Dtos.Agency.DiagnosisCodes;
using BrightCare.Entity.Agency;
using BrightCare.Repository.Interface.Agency.DiagnosisCodes;
using BrightCare.Service.Interface.Agency.DiagnosisCodes;
using HC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BrightCare.Common.Enums.CommonEnum;

namespace BrightCare.Service.Agency.DiagnosisCodes
{
    public class DiagnosisCodesService: IDiagnosisCodesService
    {
        private readonly IDiagnosisCodesRepository _diagnosisCodesRepository;
        private readonly IMapper _mapper;

        public DiagnosisCodesService(IDiagnosisCodesRepository _diagnosisCodesRepository, IMapper mapper)
        {
            this._diagnosisCodesRepository = _diagnosisCodesRepository;
            _mapper = mapper;

        }

        public JsonModel GetDiagnosisCodes(TokenModel token)
        {
            List<DiagnosisCodesDTO> diagnosisCodesDTO = new List<DiagnosisCodesDTO>();
            List<DiagnosisCode> diagnosisCodes = _diagnosisCodesRepository.GetAll(l => l.IsDeleted == false && l.OrganizationId == 2).ToList();// token.OrganizationID);
            diagnosisCodesDTO = _mapper.Map<List<DiagnosisCodesDTO>>(diagnosisCodes); // Mapping

            return new JsonModel(diagnosisCodesDTO, StatusMessage.Success, (int)HttpStatusCodes.OK);
        }

        public JsonModel AddUpdateDiagnosisCodes(DiagnosisCodesDTO diagnosisCodesDTO, TokenModel token)
        {
            JsonModel Result = new JsonModel()
            {
                data = false,
                Message = StatusMessage.Success,
                StatusCode = (int)HttpStatusCodes.OK
            };
            DiagnosisCode diagnosisCodeEntity = null;
            DateTime CurrentDate = DateTime.UtcNow;

            if (diagnosisCodesDTO.Id == 0 || diagnosisCodesDTO.Id == null)
            {
                diagnosisCodeEntity = _mapper.Map<DiagnosisCode>(diagnosisCodesDTO);
                diagnosisCodeEntity.OrganizationId = 2; // token.OrganizationID;
                diagnosisCodeEntity.CreatedBy = 2;// token.UserID;
                diagnosisCodeEntity.CreatedDate = CurrentDate;
                diagnosisCodeEntity.IsActive = true;
                diagnosisCodeEntity.IsDeleted = false;
                _diagnosisCodesRepository.Create(diagnosisCodeEntity);
                _diagnosisCodesRepository.SaveChanges();
            }

            else
            {
                DiagnosisCode diagnosisCode = _diagnosisCodesRepository.Get(l => l.Id == diagnosisCodesDTO.Id && l.OrganizationId == 2); // token.OrganizationID);
                diagnosisCode.UpdatedBy = 2; // token.UserID;
                diagnosisCode.UpdatedDate = CurrentDate;
                diagnosisCode.ICDCode = diagnosisCodesDTO.ICDCode;
                diagnosisCode.Description = diagnosisCodesDTO.Description;

                _diagnosisCodesRepository.Update(diagnosisCode);
                _diagnosisCodesRepository.SaveChanges();
            }

            return Result;
        }

        public bool DeleteDiagnosisCodes(int Id, TokenModel token)
        {
            DiagnosisCode diagnosisCodeEntity = _diagnosisCodesRepository.Get(l => l.Id == Id && l.OrganizationId == 2);// token.OrganizationID);
            diagnosisCodeEntity.IsDeleted = true;
            diagnosisCodeEntity.IsActive = false;
            diagnosisCodeEntity.DeletedBy = 2;// token.UserID;
            diagnosisCodeEntity.DeletedDate = DateTime.UtcNow;
            _diagnosisCodesRepository.SaveChanges();

            return true;
        }
    }
}
