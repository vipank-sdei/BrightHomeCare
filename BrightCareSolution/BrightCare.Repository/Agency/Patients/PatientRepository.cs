using BrightCare.Common;
using BrightCare.Entity.Agency;
using BrightCare.Persistence;
using BrightCare.Repository.Interface.Agency.Patients;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BrightCare.Common.Enums.CommonEnum;

namespace BrightCare.Repository.Agency.Patients
{
    public class PatientRepository : RepositoryBase<Entity.Agency.Patients>, IPatientRepository
    {
        private HCOrganizationContext context;
        public PatientRepository(HCOrganizationContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable<T> CheckExistingPatient<T>(string email, string mrn, int patientId, TokenModel token) where T : class, new()
        {
            SqlParameter[] parameters = { new SqlParameter("@Email",email),
                                          new SqlParameter("@MRN", mrn),
                                          new SqlParameter("@PatientId", patientId),
                                          new SqlParameter("@OrganizationId",token.OrganizationID )
            };
            return context.ExecStoredProcedureListWithOutput<T>(SQLObjects.PAT_CheckExistingPatient, parameters.Length, parameters).AsQueryable(); //throw new NotImplementedException();
        }

        public IQueryable<T> GetEncryptedPHIData<T>(string firstName, string middleName, string lastName, string dob, string emailAddress, string ssn, string mrn, string aptnumber, string address1, string address2, string city, string zipCode, string phonenumber, string healthPlanBeneficiaryNumber) where T : class, new()
        {
            SqlParameter[] parameters = { new SqlParameter("@FirstName", firstName),
                                          new SqlParameter("@MiddleName", middleName),
                                          new SqlParameter("@LastName", lastName),
                                          new SqlParameter("@DateOfBirth", dob),
                                          new SqlParameter("@EmailAddress", emailAddress),
                                          new SqlParameter("@SSN", ssn),
                                          new SqlParameter("@MRN", mrn),
                                          new SqlParameter("@AptNumber", aptnumber),
                                          new SqlParameter("@Address1", address1),
                                          new SqlParameter("@Address2", address2),
                                          new SqlParameter("@City", city),
                                          new SqlParameter("@ZipCode", zipCode),
                                          new SqlParameter("@Phonenumber", phonenumber),
                                          new SqlParameter("@HealthPlanBeneficiaryNumber", healthPlanBeneficiaryNumber)
            };
            return context.ExecStoredProcedureListWithOutput<T>(SQLObjects.MTR_EncryptPHIData, parameters.Length, parameters).AsQueryable();
        }

        public IQueryable<T> GetDecryptedPHIData<T>(byte[] firstName, byte[] middleName, byte[] lastName, byte[] dob, byte[] emailAddress, byte[] ssn, byte[] mrn, byte[] aptnumber, byte[] address1, byte[] address2, byte[] city, byte[] zipCode, byte[] phonenumber, byte[] healthPlanBeneficiaryNumber) where T : class, new()
        {
            SqlParameter[] parameters = { new SqlParameter("@FirstName", firstName),
                                          new SqlParameter("@MiddleName", middleName),
                                          new SqlParameter("@LastName", lastName),
                                          new SqlParameter("@DateOfBirth", dob),
                                          new SqlParameter("@EmailAddress", emailAddress),
                                          new SqlParameter("@SSN", ssn),
                                          new SqlParameter("@MRN", mrn),
                                          new SqlParameter("@AptNumber", aptnumber),
                                          new SqlParameter("@Address1", address1),
                                          new SqlParameter("@Address2", address2),
                                          new SqlParameter("@City", city),
                                          new SqlParameter("@ZipCode", zipCode),
                                          new SqlParameter("@Phonenumber", phonenumber),
                                          new SqlParameter("@HealthPlanBeneficiaryNumber", healthPlanBeneficiaryNumber)
            };
            return context.ExecStoredProcedureListWithOutput<T>(SQLObjects.MTR_DecryptPHIData, parameters.Length, parameters).AsQueryable();
        }

        public IQueryable<T> GetPatients<T>(TokenModel token) where T : class, new()
        {
            SqlParameter[] parameters = {
                                          new SqlParameter("@IsActive",true),
                                          new SqlParameter("@OrganizationID",token.OrganizationID),
                };
            return context.ExecStoredProcedureListWithOutput<T>("GetPatients", parameters.Length, parameters).AsQueryable();
        }
    } 
}
