using PersonelVardiyaOtomasyonu.Class.Helper;
using PersonelVardiyaOtomasyonu.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PersonelVardiyaOtomasyonu.Class.DAL
{
    public class ShiftDal
    {
        public List<Shift> GetAllWithDate(DateTime startDate, DateTime endDate)
        {
            DateTime startDateTime = startDate;
            DateTime endDateTime = endDate;
            endDateTime = endDateTime.AddDays(1);

            ConnectionStrings.ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Select * from Shifts where Date>=@startDate and Date<=@endDate", ConnectionStrings._sqlConnection);

            sqlCommand.Parameters.AddWithValue("@startDate", startDateTime.Date);
            sqlCommand.Parameters.AddWithValue("@endDate", endDateTime.Date);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            List<Shift> shifts = new List<Shift>();

            while (sqlDataReader.Read())
            {
                Shift shift = new Shift
                {
                    Id = Convert.ToInt32(sqlDataReader["Id"]),
                    RegistrantId = Convert.ToInt32(sqlDataReader["RegistrantId"]),
                    EmployeeId = Convert.ToInt32(sqlDataReader["EmployeeId"]),
                    DateOfRegistration = Convert.ToDateTime(sqlDataReader["DateOfRegistration"]),
                    Date = Convert.ToDateTime(sqlDataReader["Date"]),
                    Location = sqlDataReader["Location"].ToString(),
                    Hours = sqlDataReader["Hours"].ToString(),
                    IsNew = Convert.ToBoolean(sqlDataReader["IsNew"]),
                };

                shifts.Add(shift);
            }

            sqlDataReader.Close();
            ConnectionStrings._sqlConnection.Close();

            return shifts;
        }

        public List<Shift> GetAllWithDateAndEmployee(DateTime startDate, DateTime endDate, int employeeId)
        {
            DateTime startDateTime = startDate;
            DateTime endDateTime = endDate;
            endDateTime = endDateTime.AddDays(1);

            ConnectionStrings.ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Select * from Shifts where Date>=@startDate and Date<=@endDate and EmployeeId=@employeeId", ConnectionStrings._sqlConnection);

            sqlCommand.Parameters.AddWithValue("@startDate", startDateTime.Date);
            sqlCommand.Parameters.AddWithValue("@endDate", endDateTime.Date);
            sqlCommand.Parameters.AddWithValue("@employeeId", employeeId);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            List<Shift> shifts = new List<Shift>();

            while (sqlDataReader.Read())
            {
                Shift shift = new Shift
                {
                    Id = Convert.ToInt32(sqlDataReader["Id"]),
                    RegistrantId = Convert.ToInt32(sqlDataReader["RegistrantId"]),
                    EmployeeId = Convert.ToInt32(sqlDataReader["EmployeeId"]),
                    DateOfRegistration = Convert.ToDateTime(sqlDataReader["DateOfRegistration"]),
                    Date = Convert.ToDateTime(sqlDataReader["Date"]),
                    Location = sqlDataReader["Location"].ToString(),
                    Hours = sqlDataReader["Hours"].ToString(),
                    IsNew = Convert.ToBoolean(sqlDataReader["IsNew"]),
                };

                shifts.Add(shift);
            }

            sqlDataReader.Close();
            ConnectionStrings._sqlConnection.Close();

            return shifts;
        }

        public bool CheckByNewShiftWithEmployeeId(int employeeId)
        {
            ConnectionStrings.ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Select * from Shifts where EmployeeId=@employeeId and IsNew=1", ConnectionStrings._sqlConnection);

            sqlCommand.Parameters.AddWithValue("@employeeId", employeeId);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                sqlDataReader.Close();
                ConnectionStrings._sqlConnection.Close();

                return true;
            }

            return false;
        }

        public Shift GetById(int id)
        {
            ConnectionStrings.ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Select * from Shifts where Id=@id", ConnectionStrings._sqlConnection);

            sqlCommand.Parameters.AddWithValue("@id", id);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                Shift shift = new Shift
                {
                    Id = Convert.ToInt32(sqlDataReader["Id"]),
                    RegistrantId = Convert.ToInt32(sqlDataReader["RegistrantId"]),
                    EmployeeId = Convert.ToInt32(sqlDataReader["EmployeeId"]),
                    DateOfRegistration = Convert.ToDateTime(sqlDataReader["DateOfRegistration"]),
                    Date = Convert.ToDateTime(sqlDataReader["Date"]),
                    Location = sqlDataReader["Location"].ToString(),
                    Hours = sqlDataReader["Hours"].ToString(),
                    IsNew = Convert.ToBoolean(sqlDataReader["IsNew"]),
                };

                sqlDataReader.Close();
                ConnectionStrings._sqlConnection.Close();

                return shift;
            }

            return null;
        }

        public void DeactivateAllNewShiftsTheEmployee(int employeeId)
        {
            ConnectionStrings.ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Update Shifts set IsNew=0 where employeeId=@employeeId", ConnectionStrings._sqlConnection);

            sqlCommand.Parameters.AddWithValue("@employeeId", employeeId);

            sqlCommand.ExecuteNonQuery();

            ConnectionStrings._sqlConnection.Close();
        }

        public void Add(Shift shift)
        {
            ConnectionStrings.ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Insert into Shifts values(@registrantId,@employeeId,@dateOfRegistration,@date,@location,@hours,@isNew)", ConnectionStrings._sqlConnection);

            sqlCommand.Parameters.AddWithValue("@registrantId", shift.RegistrantId);
            sqlCommand.Parameters.AddWithValue("@employeeId", shift.EmployeeId);
            sqlCommand.Parameters.AddWithValue("@dateOfRegistration", shift.DateOfRegistration);
            sqlCommand.Parameters.AddWithValue("@date", shift.Date);
            sqlCommand.Parameters.AddWithValue("@location", shift.Location);
            sqlCommand.Parameters.AddWithValue("@hours", shift.Hours);
            sqlCommand.Parameters.AddWithValue("@isNew", shift.IsNew);

            sqlCommand.ExecuteNonQuery();

            ConnectionStrings._sqlConnection.Close();
        }

        public void Update(Shift shift)
        {
            ConnectionStrings.ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Update Shifts set RegistrantId=@registrantId,EmployeeId=@employeeId,DateOfRegistration=@dateOfRegistration,Date=@date,Location=@location,Hours=@hours,IsNew=@isNew where Id=@id", ConnectionStrings._sqlConnection);

            sqlCommand.Parameters.AddWithValue("@id", shift.Id);
            sqlCommand.Parameters.AddWithValue("@registrantId", shift.RegistrantId);
            sqlCommand.Parameters.AddWithValue("@employeeId", shift.EmployeeId);
            sqlCommand.Parameters.AddWithValue("@dateOfRegistration", shift.DateOfRegistration);
            sqlCommand.Parameters.AddWithValue("@date", shift.Date);
            sqlCommand.Parameters.AddWithValue("@location", shift.Location);
            sqlCommand.Parameters.AddWithValue("@hours", shift.Hours);
            sqlCommand.Parameters.AddWithValue("@isNew", shift.IsNew);

            sqlCommand.ExecuteNonQuery();

            ConnectionStrings._sqlConnection.Close();
        }

        public void Delete(int id)
        {
            ConnectionStrings.ConnectionControl();

            SqlCommand sqlCommand = new SqlCommand("Delete from Shifts where Id=@id", ConnectionStrings._sqlConnection);

            sqlCommand.Parameters.AddWithValue("@id", id);

            sqlCommand.ExecuteNonQuery();

            ConnectionStrings._sqlConnection.Close();
        }
    }
}
