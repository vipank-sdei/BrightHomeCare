using BrightCare.Entity.Agency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BrightCare.Common;

namespace BrightCare.Repository.Interface.Agency.Patients
{
    public interface IPatientRepository : IRepositoryBase<Entity.Agency.Patients>
    {
        IQueryable<T> CheckExistingPatient<T>(string email, string mrn, int patientId, TokenModel token) where T : class, new();

        IQueryable<T> GetEncryptedPHIData<T>(string firstName, string middleName, string lastName, string dob, string emailAddress, string ssn, string mrn, string aptnumber, string address1, string address2, string city, string zipCode, string phonenumber, string healthPlanBeneficiaryNumber) where T : class, new();

        IQueryable<T> GetDecryptedPHIData<T>(byte[] firstName, byte[] middleName, byte[] lastName, byte[] dob, byte[] emailAddress, byte[] ssn, byte[] mrn, byte[] aptnumber, byte[] address1, byte[] address2, byte[] city, byte[] zipCode, byte[] phonenumber, byte[] healthPlanBeneficiaryNumber) where T : class, new();

       // IQueryable<T> GetPatients<T>(ListingFiltterModel patientFiltterModel, TokenModel token) where T : class, new();
    }
}
