using PersonelVardiyaOtomasyonu.Class.Helper;
using PersonelVardiyaOtomasyonu.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PersonelVardiyaOtomasyonu.Class.DAL
{
    public class UserDal
    {
        public List<User> GetAll()
        {
            ConnectionStrings.ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Select * from Users", ConnectionStrings._sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            List<User> users = new List<User>();

            while (sqlDataReader.Read())
            {
                User user = new User
                {
                    Id = Convert.ToInt32(sqlDataReader["Id"]),
                    RoleId = Convert.ToInt32(sqlDataReader["RoleId"]),
                    IdentificationNumber = sqlDataReader["IdentificationNumber"].ToString(),
                    Name = sqlDataReader["Name"].ToString(),
                    Surname = sqlDataReader["Surname"].ToString(),
                    NameSurname = sqlDataReader["Name"].ToString() + " " + sqlDataReader["Surname"].ToString(),
                    Password = sqlDataReader["Password"].ToString(),
                    Address = sqlDataReader["Address"].ToString(),
                    EMail = sqlDataReader["E-Mail"].ToString(),
                    PhoneNumber = sqlDataReader["PhoneNumber"].ToString(),
                    Status = Convert.ToBoolean(sqlDataReader["Status"]),
                };

                users.Add(user);
            }

            sqlDataReader.Close();
            ConnectionStrings._sqlConnection.Close();

            return users;
        }

        public User GetByIdentificationNumber(string identificationNumber)
        {
            ConnectionStrings.ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Select * from Users where IdentificationNumber=@identificationNumber", ConnectionStrings._sqlConnection);

            sqlCommand.Parameters.AddWithValue("@identificationNumber", identificationNumber);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                User user = new User
                {
                    Id = Convert.ToInt32(sqlDataReader["Id"]),
                    RoleId = Convert.ToInt32(sqlDataReader["RoleId"]),
                    IdentificationNumber = sqlDataReader["IdentificationNumber"].ToString(),
                    Name = sqlDataReader["Name"].ToString(),
                    Surname = sqlDataReader["Surname"].ToString(),
                    NameSurname = sqlDataReader["Name"].ToString() + " " + sqlDataReader["Surname"].ToString(),
                    Password = sqlDataReader["Password"].ToString(),
                    Address = sqlDataReader["Address"].ToString(),
                    EMail = sqlDataReader["E-Mail"].ToString(),
                    PhoneNumber = sqlDataReader["PhoneNumber"].ToString(),
                    Status = Convert.ToBoolean(sqlDataReader["Status"]),
                };

                sqlDataReader.Close();
                ConnectionStrings._sqlConnection.Close();

                return user;
            }

            return null;
        }

        public User GetById(int id)
        {
            ConnectionStrings.ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Select * from Users where Id=@id", ConnectionStrings._sqlConnection);

            sqlCommand.Parameters.AddWithValue("@id", id);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                User user = new User
                {
                    Id = Convert.ToInt32(sqlDataReader["Id"]),
                    RoleId = Convert.ToInt32(sqlDataReader["RoleId"]),
                    IdentificationNumber = sqlDataReader["IdentificationNumber"].ToString(),
                    Name = sqlDataReader["Name"].ToString(),
                    Surname = sqlDataReader["Surname"].ToString(),
                    NameSurname = sqlDataReader["Name"].ToString() + " " + sqlDataReader["Surname"].ToString(),
                    Password = sqlDataReader["Password"].ToString(),
                    Address = sqlDataReader["Address"].ToString(),
                    EMail = sqlDataReader["E-Mail"].ToString(),
                    PhoneNumber = sqlDataReader["PhoneNumber"].ToString(),
                    Status = Convert.ToBoolean(sqlDataReader["Status"]),
                };

                sqlDataReader.Close();
                ConnectionStrings._sqlConnection.Close();

                return user;
            }

            return null;
        }

        public bool CheckByIdentificationNumber(string identificationNumber)
        {
            ConnectionStrings.ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Select * from Users where IdentificationNumber=@identificationNumber", ConnectionStrings._sqlConnection);

            sqlCommand.Parameters.AddWithValue("@identificationNumber", identificationNumber);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                sqlDataReader.Close();
                ConnectionStrings._sqlConnection.Close();

                return true;
            }

            return false;
        }

        public bool CheckByIdentificationNumberAndPassword(string identificationNumber, string password)
        {
            ConnectionStrings.ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Select * from Users where IdentificationNumber=@identificationNumber and Password=@password", ConnectionStrings._sqlConnection);

            sqlCommand.Parameters.AddWithValue("@identificationNumber", identificationNumber);
            sqlCommand.Parameters.AddWithValue("@password", password);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            if (sqlDataReader.Read())
            {
                sqlDataReader.Close();
                ConnectionStrings._sqlConnection.Close();

                return true;
            }

            return false;
        }

        public void Add(User user)
        {
            ConnectionStrings.ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Insert into Users values(@roleId,@identificationNumber,@name,@surname,@password,@address,@email,@phoneNumber,@status)", ConnectionStrings._sqlConnection);

            sqlCommand.Parameters.AddWithValue("@roleId", user.RoleId);
            sqlCommand.Parameters.AddWithValue("@identificationNumber", user.IdentificationNumber);
            sqlCommand.Parameters.AddWithValue("@name", user.Name);
            sqlCommand.Parameters.AddWithValue("@surname", user.Surname);
            sqlCommand.Parameters.AddWithValue("@password", user.Password);
            sqlCommand.Parameters.AddWithValue("@address", user.Address);
            sqlCommand.Parameters.AddWithValue("@email", user.EMail);
            sqlCommand.Parameters.AddWithValue("@phoneNumber", user.PhoneNumber);
            sqlCommand.Parameters.AddWithValue("@status", user.Status);

            sqlCommand.ExecuteNonQuery();

            ConnectionStrings._sqlConnection.Close();
        }

        public void Update(User user)
        {
            ConnectionStrings.ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Update Users set RoleId=@roleId,IdentificationNumber=@identificationNumber,Name=@name,Surname=@surname,Password=@password,Address=@address,[E-Mail]=@email,PhoneNumber=@phoneNumber,Status=@status where Id=@id", ConnectionStrings._sqlConnection);

            sqlCommand.Parameters.AddWithValue("@id", user.Id);
            sqlCommand.Parameters.AddWithValue("@roleId", user.RoleId);
            sqlCommand.Parameters.AddWithValue("@identificationNumber", user.IdentificationNumber);
            sqlCommand.Parameters.AddWithValue("@name", user.Name);
            sqlCommand.Parameters.AddWithValue("@surname", user.Surname);
            sqlCommand.Parameters.AddWithValue("@password", user.Password);
            sqlCommand.Parameters.AddWithValue("@address", user.Address);
            sqlCommand.Parameters.AddWithValue("@email", user.EMail);
            sqlCommand.Parameters.AddWithValue("@phoneNumber", user.PhoneNumber);
            sqlCommand.Parameters.AddWithValue("@status", user.Status);

            sqlCommand.ExecuteNonQuery();

            ConnectionStrings._sqlConnection.Close();
        }

        public void Delete(int id)
        {
            ConnectionStrings.ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Delete from Users where Id=@id", ConnectionStrings._sqlConnection);

            sqlCommand.Parameters.AddWithValue("@id", id);

            sqlCommand.ExecuteNonQuery();

            ConnectionStrings._sqlConnection.Close();
        }
    }
}
