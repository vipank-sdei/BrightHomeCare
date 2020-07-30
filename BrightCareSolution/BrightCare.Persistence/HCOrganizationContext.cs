using BrightCare.Entity;
using BrightCare.Entity.Agency;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace BrightCare.Persistence
{
    public class HCOrganizationContext : DbContext
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private ConfigurationBuilderContext _configurationBuilderContext = new ConfigurationBuilderContext();
        /// <summary>
        /// Db context for Patient module
        /// </summary>
        /// <param name="options"></param>
        public HCOrganizationContext(DbContextOptions<HCOrganizationContext> options, IHttpContextAccessor contextAccessor) : base(options) { _contextAccessor = contextAccessor; }


        /// <summary>
        /// override the context from http request
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var NewDbConnection = new ConfigurationBuilderContext();

            //check http request is not null
            if (_contextAccessor.HttpContext != null)
            {
                //create new connection string from the http context
                string con = NewDbConnection.GetNewConnection(_contextAccessor.HttpContext);
                if (!string.IsNullOrEmpty(con))
                {
                    //create new option builder with new connection string
                    optionsBuilder.UseSqlServer(con);
                }
            }
            else
            {
                //first time when application load the "HttpContext" is null so we bind database wirth static one
                //if http context null then bind the context with default connection string
                optionsBuilder.UseSqlServer(_configurationBuilderContext.CreateOrganizationConnectionString());
            }
        }
        public DbSet<ExceptionLog> ExceptionLog { get; set; }
        public DbSet<Organization> MasterOrganization { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Staffs> Staffs { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<MasterServices> MasterServices { get; set; }
        public DbSet<MasterServiceType> MasterServiceType { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<MasterDocumentTypes> MasterDocumentTypes { get; set; }
        public DbSet<StaffLeaves> StaffLeave { get; set; }
        public DbSet<LeaveType> LeaveType { get; set; }
        public DbSet<LeaveReason> LeaveReason { get; set; }
        public DbSet<UserDocument> UserDocuments { get; set; }
        public DbSet<StaffExperience> StaffExperiences { get; set; }
        public DbSet<MasterAllergies> MasterAllergies { get; set; }
        public DbSet<MasterAllergiesReaction> MasterAllergiesReaction { get; set; }
        public DbSet<DiagnosisCode> DiagnosisCode { get; set; }

        public int ExecuteStoredProcedureNonQuery(string commandText, params object[] parameters)
        {
            var connection = this.Database.GetDbConnection();
            try
            {
                //open the connection for use
                if (connection.State == ConnectionState.Closed)
                connection.Open();

            //create a command object
            using (var cmd = connection.CreateCommand())
            {
                //command to execute
                cmd.CommandText = commandText;
                cmd.CommandType = CommandType.StoredProcedure;

                // move parameters to command object
                if (parameters != null)
                    foreach (var p in parameters)
                    {
                        if (p != null)
                            cmd.Parameters.Add(p);
                    }

                var rowseffected = cmd.ExecuteNonQuery();
                //close connection finally if open.(DS)
                if (connection.State == ConnectionState.Open) { connection.Close(); }
                return rowseffected;
            }
            }
            finally
            {
                connection.Close();

            }
        }
        public TEntity ExecuteStoredProcedure<TEntity>(string commandText, params object[] parameters) where TEntity : new()
        {
            TEntity entity;
            var connection = Database.GetDbConnection();
            try
            {
                //open the connection for use
                if (connection.State == ConnectionState.Closed) { connection.Open(); }

                //create a command object
                using (var cmd = connection.CreateCommand())
                {
                    AddParametersToDbCommand(commandText, parameters, cmd);
                    using (var reader = cmd.ExecuteReader())
                    {
                        entity = DataReaderMap<TEntity>(reader);
                    }
                    //close connection finally if open.(DS)
                    if (connection.State == ConnectionState.Open) { connection.Close(); }

                }
                return entity;
            }
            finally
            {
                connection.Close();

            }

        }
        public IList<TEntity> ExecuteStoredProcedureSingleList<TEntity>(string commandText, params object[] parameters) where TEntity : new()
        {
            IList<TEntity> entity;
            var connection = Database.GetDbConnection();
            try
            {
                //open the connection for use
                if (connection.State == ConnectionState.Closed) { connection.Open(); }

                //create a command object
                using (var cmd = connection.CreateCommand())
                {
                    AddParametersToDbCommand(commandText, parameters, cmd);
                    using (var reader = cmd.ExecuteReader())
                    {
                        entity = DataReaderMapToList<TEntity>(reader).ToList();
                    }
                    //close connection finally if open.(DS)
                    if (connection.State == ConnectionState.Open) { connection.Close(); }

                }
                return entity;
            }
            finally 
            {
                connection.Close();

            }
           
        }

        public IList<TEntity> ExecStoredProcedureListWithOutput<TEntity>(string commandText, int totalOutputParams, params object[] parameters) where TEntity : class, new()
        {
            var connection = this.Database.GetDbConnection();
            IList<TEntity> result = new List<TEntity>();
            try
            {
                totalOutputParams = totalOutputParams == 0 ? 1 : totalOutputParams;
                if (connection.State == ConnectionState.Closed) { connection.Open(); }
                using (var cmd = connection.CreateCommand())
                {
                    AddParametersToDbCommand(commandText, parameters, cmd);
                    using (var reader = cmd.ExecuteReader())
                    {
                        result = DataReaderMapToList<TEntity>(reader);
                        reader.NextResult();
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }


        #region Common Section
        private void AddParametersToDbCommand(string commandText, object[] parameters, System.Data.Common.DbCommand cmd)
        {
            cmd.CommandText = commandText;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1000;

            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    if (p != null)
                    {
                        cmd.Parameters.Add(p);
                    }
                }
            }
        }
        public static IList<T> DataReaderMapToList<T>(IDataReader dr)
        {
            IList<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())               //Solution - Check if property is there in the reader and then try to remove try catch code
                {
                    try
                    {
                        if (!object.Equals(dr[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, dr[prop.Name], null);
                        }
                    }
                    catch (Exception ex)
                    { continue; }
                }
                list.Add(obj);
            }
            return list;
        }
        public static T DataReaderMap<T>(IDataReader dr)
        {
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())               //Solution - Check if property is there in the reader and then try to remove try catch code
                {
                    try
                    {
                        if (!object.Equals(dr[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, dr[prop.Name], null);
                        }
                    }
                    catch (Exception ex)
                    { continue; }
                }
            }
            return obj;
        }
        #endregion

    }
}
