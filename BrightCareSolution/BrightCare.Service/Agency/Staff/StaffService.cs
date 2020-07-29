using System;
using System.Collections.Generic;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using BrightCare.Common.Options;
using BrightCare.Common;
using BrightCare.Repository.Interface.Agency.Users;
using BrightCare.Repository.Interface.Agency.Staff;
using HC.Common;
using static BrightCare.Common.Enums.CommonEnum;
using AutoMapper;
using System.Security.Principal;
using BrightCare.Entity.Agency;
using BrightCare.Service.Interface.Agency.Staffs;
using BrightCare.Common.Model;
using BrightCare.Dtos.Agency;
using BrightCare.Persistence;
using System.Linq;

namespace BrightCare.Service.Agency.Staff
{

    public class StaffService : IStaffService
    {
        private readonly IUserRepository iUserRepository;
        private readonly IStaffRepository iStaffRepository;
        private readonly HCOrganizationContext _context;
        private readonly IMapper _mapper;

        public StaffService(IUserRepository iUserRepository, IStaffRepository iStaffRepository, IMapper mapper, HCOrganizationContext context)
        {
            this.iUserRepository = iUserRepository;
            this.iStaffRepository = iStaffRepository;
            _mapper = mapper;
            _context = context;
        }

        public JsonModel CreateUpdateStaff(StaffsDTO staff, TokenModel token)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                token.OrganizationID = 2;
                token.UserID = 1;

                JsonModel response = new JsonModel();
                //encrypt password
                if (!string.IsNullOrEmpty(staff.Password)) { staff.Password = CommonMethods.Encrypt(staff.Password); }
                //
                if (staff.Id == 0)
                {
                    User staffDB = iUserRepository.Get(m => (m.UserName == staff.UserName) && m.OrganizationID == token.OrganizationID);
                    if (staffDB != null) //if user try to enter duplicate records
                    {
                        response = new JsonModel(new object(), StatusMessage.StaffAlreadyExist, (int)HttpStatusCodes.UnprocessedEntity);
                    }
                    else // insert new staff
                    {
                        // insert Credentials in User table
                        User requestUser = SaveUser(staff, token);
                        Staffs staffsEntity = _mapper.Map<Staffs>(staff);
                        staffsEntity.CreatedBy = token.UserID;
                        staffsEntity.UserID = requestUser.Id;
                        staffsEntity.OrganizationID = token.OrganizationID;
                        staffsEntity.CreatedDate = DateTime.UtcNow;
                        staffsEntity.IsActive = true;
                        // save data in staff table
                        iStaffRepository.Create(staffsEntity);
                        iStaffRepository.SaveChanges();
                        response = new JsonModel(staffsEntity, StatusMessage.StaffCreated, (int)HttpStatusCodes.OK);
                    }
                }
                // update Staff Info
                else
                {
                    Staffs staffDB = iStaffRepository.GetFirstOrDefault(a => a.Id == staff.Id);
                    if (staffDB != null)
                    {
                        // Update info in User Table
                        User user = iUserRepository.GetFirstOrDefault(a => a.Id == staffDB.UserID);
                        user.UserName = staff.UserName;
                        user.Password = staff.Password;
                        user.UpdatedDate = DateTime.UtcNow;
                        user.UpdatedBy = token.UserID;
                        iUserRepository.Update(user);
                        iUserRepository.SaveChanges();
                        //staffDB = _mapper.Map<Staffs>(staff);
                        Staffs updatedstaff = UpadteStaff(staffDB, staff, token);
                        response = new JsonModel(updatedstaff, StatusMessage.StaffUpdated, (int)HttpStatusCodes.OK);
                    }
                }
                transaction.Commit();
                return response;
            }
        }

        private User SaveUser(StaffsDTO entity, TokenModel token)
        {
            // ENTRY IN User table
            User requestUser = new User();
            requestUser.UserName = entity.UserName;
            requestUser.Password = entity.Password;
            requestUser.IsActive = true;
            requestUser.IsDeleted = false;
            requestUser.OrganizationID = token.OrganizationID;
            requestUser.CreatedBy = token.UserID;
            requestUser.CreatedDate = DateTime.UtcNow;
            iUserRepository.Create(requestUser);
            iUserRepository.SaveChanges();
            return requestUser;
        }

        private Staffs UpadteStaff(Staffs staffDB, StaffsDTO staff, TokenModel token)
        {
            //Update Staff Info
            staffDB.FirstName = staff.FirstName;
            staffDB.LastName = staff.LastName;
            staffDB.MiddleName = staff.MiddleName;
            staffDB.Address = staff.Address;
            staffDB.CountryID = staff.CountryID;
            staffDB.City = staff.City;
            staffDB.StateID = staff.StateID;
            staffDB.Zip = staff.Zip;
            staffDB.Latitude = staff.Latitude;
            staffDB.Longitude = staff.Longitude;
            staffDB.PhoneNumber = staff.PhoneNumber;
            staffDB.NPINumber = staff.NPINumber;
            staffDB.TaxId = staff.TaxId;
            staffDB.DOB = staff.DOB;
            staffDB.DOJ = staff.DOJ;
            staffDB.RoleID = staff.RoleID;
            staffDB.Email = staff.Email;
            staffDB.ApartmentNumber = staff.ApartmentNumber;
            staffDB.CAQHID = staff.CAQHID;
            staffDB.Language = staff.Language;
            staffDB.DegreeID = staff.DegreeID;
            staffDB.EmployeeID = staff.EmployeeID;
            staffDB.PayRate = staff.PayRate;
            staffDB.PayrollGroupID = staff.PayrollGroupID;
            staffDB.IsRenderingProvider = staff.IsRenderingProvider;
            staffDB.AboutMe = staff.AboutMe;
            staffDB.UpdatedBy = token.UserID;
            staffDB.UpdatedDate = DateTime.UtcNow;
            iStaffRepository.Update(staffDB);
            iStaffRepository.SaveChanges();
            return staffDB;
        }

        public JsonModel DeleteStaff(int id, TokenModel token)
        {
            // Begin transaction
            using (var transaction = _context.Database.BeginTransaction())
            {
                token.OrganizationID = 2;
                token.UserID = 1;
                // get staff info
                Staffs staff = iStaffRepository.Get(a => a.Id == id && a.IsDeleted == false && a.IsActive == true);
                if (staff != null)
                {
                    User user = iUserRepository.GetFirstOrDefault(a => a.Id == staff.UserID);
                    user.IsDeleted = true;
                    user.DeletedBy = token.UserID;
                    user.DeletedDate = DateTime.UtcNow;
                    user.IsActive = false;
                    iUserRepository.Update(user);
                    staff.IsDeleted = true;
                    staff.DeletedBy = token.UserID;
                    staff.UpdatedBy = token.UserID;
                    staff.DeletedDate = DateTime.UtcNow;
                    user.IsActive = false;
                    iUserRepository.Update(user);
                    iStaffRepository.Update(staff);
                    iUserRepository.SaveChanges();
                    iStaffRepository.SaveChanges();
                    transaction.Commit();
                    return new JsonModel(new object(), StatusMessage.StaffDelete, (int)HttpStatusCodes.OK);
                }
                else
                {
                    return new JsonModel(new object(), StatusMessage.NotFound, (int)HttpStatusCodes.NotFound);
                }
            }

        }

        public JsonModel GetStaff(int? id, TokenModel token)
        {
            token.OrganizationID = 2;
            // get staff info
            if (id == null)
            {
                List<StaffsDTO> staffModels = iStaffRepository.GetStaff<StaffsDTO>(token).ToList();
                return new JsonModel(staffModels, StatusMessage.FetchMessage, (int)HttpStatusCodes.OK);
            }
            else
            {
                Staffs staffs = iStaffRepository.GetFirstOrDefault(a => a.Id == id && a.IsActive == true && a.IsDeleted == false);
                if (staffs != null)
                {
                    // get user info by staffid
                    User user = iUserRepository
                                     .GetFirstOrDefault(a => a.Id == staffs.UserID && a.IsActive == true && a.IsDeleted == false);
                    StaffsDTO staffDTO = _mapper.Map<StaffsDTO>(staffs);
                    if (!string.IsNullOrEmpty(user.UserName)) { staffDTO.UserName = user.UserName; }
                    // decrypt password
                    if (!string.IsNullOrEmpty(user.Password)) { staffDTO.Password = CommonMethods.Decrypt(user.Password); }
                    return new JsonModel(staffDTO, StatusMessage.FetchMessage, (int)HttpStatusCodes.OK);
                }
                else
                {
                    return new JsonModel(new object(), StatusMessage.NotFound, (int)HttpStatusCodes.NotFound);
                }
            }
        }

    }
}
