using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Interfaces;
using Models.Address;

namespace DataAccess
{
    public class Address: IAddress
    {
        public bool SaveAddress(Models.Address.Address address)
        {

            using (var conn = new SqlConnection(GetDbConnString()))
            {
                using (var cmd = new SqlCommand("dbo.spSaveAddress", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 10;
                    cmd.Parameters.Add("@addressId", SqlDbType.Int).Value = address.AddressId;
                    cmd.Parameters.Add("@stateId", SqlDbType.Int).Value = address.State.Id;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar, 150).Value = address.Name;
                    cmd.Parameters.Add("@company", SqlDbType.VarChar, 150).Value = address.Company;
                    cmd.Parameters.Add("@address1", SqlDbType.VarChar, 150).Value = address.AddressLine1;
                    cmd.Parameters.Add("@address2", SqlDbType.VarChar, 150).Value = address.AddressLine2;
                    cmd.Parameters.Add("@city", SqlDbType.VarChar, 100).Value = address.City;
                    cmd.Parameters.Add("@zip", SqlDbType.VarChar, 50).Value = address.Zip;

                    cmd.Connection.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("DataAccess.Address SaveAddress() error:" + ex.Message);
                        // Log to applicable logging repository here
                        return false;
                    }
                }
            }
        }

        public bool TryGetAddress(int id, out Models.Address.Address address)
        {
            address = null;
            using (var conn = new SqlConnection(GetDbConnString()))
            {
                using (var cmd = new SqlCommand("dbo.spGetAddress", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 10;
                    cmd.Parameters.Add("@addressId", SqlDbType.Int).Value = id;
                    cmd.Connection.Open();
                    try
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                var state = new State
                                {
                                    Id = (dr["stateId"].Equals(DBNull.Value)) ? 0 : int.Parse(dr["stateId"].ToString()),
                                    Abbreviation = dr["Abbreviation"].ToString(),
                                    Name = dr["Name"].ToString()

                                };
                                address = new Models.Address.Address
                                {
                                    AddressId = id,
                                    Name = dr["name"].ToString(),
                                    Company = dr["company"].ToString(),
                                    AddressLine1 = dr["addressline1"].ToString(),
                                    AddressLine2 = dr["addressline2"].ToString(),
                                    City = dr["city"].ToString(),
                                    Zip = dr["zip"].ToString(),
                                    State = state
                                };

                            }
                            return address != null && address.AddressId > 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("DataAccess.Address TryGetAddress() error:" + ex.Message);
                        // Log to applicable logging repository here
                        return false;
                    }
                }
            }
        }


        private static string GetDbConnString()
        {
            var server = ConfigurationManager.AppSettings["DataSource"];
            var db =   ConfigurationManager.AppSettings["DatabaseName"];
            const string sqlUser ="test1234" ;//"appcore_user";
            const string sqlPw = "test1234";//"appcore1234";

            return string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}",
                server,
                db,
                sqlUser,
                sqlPw);
        }
    }
}
