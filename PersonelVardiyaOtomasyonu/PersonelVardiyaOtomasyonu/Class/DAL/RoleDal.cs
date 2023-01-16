using PersonelVardiyaOtomasyonu.Class.Helper;
using PersonelVardiyaOtomasyonu.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PersonelVardiyaOtomasyonu.Class.DAL
{
    public class RoleDal
    {
        public List<Role> GetAll()
        {
            ConnectionStrings.ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Select * from Roles", ConnectionStrings._sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            List<Role> roles = new List<Role>();

            while (sqlDataReader.Read())
            {
                Role role = new Role
                {
                    Id = Convert.ToInt32(sqlDataReader["Id"]),
                    Name = sqlDataReader["Name"].ToString(),
                    Add = Convert.ToBoolean(sqlDataReader["Add"]),
                    Update = Convert.ToBoolean(sqlDataReader["Update"]),
                    Delete = Convert.ToBoolean(sqlDataReader["Delete"]),
                    Print = Convert.ToBoolean(sqlDataReader["Print"]),
                    UsersManagement = Convert.ToBoolean(sqlDataReader["UsersManagement"]),
                    RolesManagement = Convert.ToBoolean(sqlDataReader["RolesManagement"]),
                    SeeAllShifts = Convert.ToBoolean(sqlDataReader["SeeAllShifts"]),
                    Status = Convert.ToBoolean(sqlDataReader["Status"]),
                };

                roles.Add(role);
            }

            sqlDataReader.Close();
            ConnectionStrings._sqlConnection.Close();

            return roles;
        }

        public Role GetById(int id)
        {
            ConnectionStrings.ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Select * from Roles where Id=@id", ConnectionStrings._sqlConnection);

            sqlCommand.Parameters.AddWithValue("@id", id);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                Role role = new Role
                {
                    Id = Convert.ToInt32(sqlDataReader["Id"]),
                    Name = sqlDataReader["Name"].ToString(),
                    Add = Convert.ToBoolean(sqlDataReader["Add"]),
                    Update = Convert.ToBoolean(sqlDataReader["Update"]),
                    Delete = Convert.ToBoolean(sqlDataReader["Delete"]),
                    Print = Convert.ToBoolean(sqlDataReader["Print"]),
                    UsersManagement = Convert.ToBoolean(sqlDataReader["UsersManagement"]),
                    RolesManagement = Convert.ToBoolean(sqlDataReader["RolesManagement"]),
                    SeeAllShifts = Convert.ToBoolean(sqlDataReader["SeeAllShifts"]),
                    Status = Convert.ToBoolean(sqlDataReader["Status"]),
                };

                sqlDataReader.Close();
                ConnectionStrings._sqlConnection.Close();

                return role;
            }

            return null;
        }

        public void Add(Role role)
        {
            ConnectionStrings.ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Insert into Roles values(@name,@add,@update,@delete,@print,@usersManagement,@rolesManagement,@seeAllShifts,@status)", ConnectionStrings._sqlConnection);

            sqlCommand.Parameters.AddWithValue("@name", role.Name);
            sqlCommand.Parameters.AddWithValue("@add", role.Add);
            sqlCommand.Parameters.AddWithValue("@update", role.Update);
            sqlCommand.Parameters.AddWithValue("@delete", role.Delete);
            sqlCommand.Parameters.AddWithValue("@print", role.Print);
            sqlCommand.Parameters.AddWithValue("@usersManagement", role.UsersManagement);
            sqlCommand.Parameters.AddWithValue("@rolesManagement", role.RolesManagement);
            sqlCommand.Parameters.AddWithValue("@seeAllShifts", role.SeeAllShifts);
            sqlCommand.Parameters.AddWithValue("@status", role.Status);

            sqlCommand.ExecuteNonQuery();

            ConnectionStrings._sqlConnection.Close();
        }

        public void Update(Role role)
        {
            ConnectionStrings.ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Update Roles set Name=@name,[Add]=@add,[Update]=@update,[Delete]=@delete,[Print]=@print,UsersManagement=@usersManagement,RolesManagement=@rolesManagement,SeeAllShifts=@seeAllShifts,Status=@status where Id=@id", ConnectionStrings._sqlConnection);

            sqlCommand.Parameters.AddWithValue("@id", role.Id);
            sqlCommand.Parameters.AddWithValue("@name", role.Name);
            sqlCommand.Parameters.AddWithValue("@add", role.Add);
            sqlCommand.Parameters.AddWithValue("@update", role.Update);
            sqlCommand.Parameters.AddWithValue("@delete", role.Delete);
            sqlCommand.Parameters.AddWithValue("@print", role.Print);
            sqlCommand.Parameters.AddWithValue("@usersManagement", role.UsersManagement);
            sqlCommand.Parameters.AddWithValue("@rolesManagement", role.RolesManagement);
            sqlCommand.Parameters.AddWithValue("@seeAllShifts", role.SeeAllShifts);
            sqlCommand.Parameters.AddWithValue("@status", role.Status);

            sqlCommand.ExecuteNonQuery();

            ConnectionStrings._sqlConnection.Close();
        }

        public void Delete(int id)
        {
            ConnectionStrings.ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Delete from Roles where Id=@id", ConnectionStrings._sqlConnection);

            sqlCommand.Parameters.AddWithValue("@id", id);

            sqlCommand.ExecuteNonQuery();

            ConnectionStrings._sqlConnection.Close();
        }
    }
}
