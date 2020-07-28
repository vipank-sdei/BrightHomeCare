using BrightCare.Entity.SuperAdmin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace BrightCare.Persistence
{
    public class HCMasterContext : DbContext
    {

        public HCMasterContext(DbContextOptions<HCMasterContext> options) : base(options) { }
        public DbSet<MasterOrganization> MasterOrganization { get; set; }
        public DbSet<OrganizationDatabaseDetail> OrganizationDatabaseDetail { get; set; }         
        public DbSet<SuperUser> SuperUser { get; set; }       
        public DbSet<IpAddressLog> IpAddressLog { get; set; }
        public DbSet<ExceptionLog> ExceptionLog { get; set; }

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
