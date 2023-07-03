using EmployeeModels.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace EmployeeDataAccessLayer.DbOperations
{
	public class Test1DbOperations
	{
		private readonly string _connectionString = ConfigurationManager.ConnectionStrings["EmployeeDbEntities"].ConnectionString;
		private readonly SqlConnection conn;

		public Test1DbOperations()
		{
			conn = new SqlConnection(_connectionString);
		}

		/// <summary>
		/// It returns All list of employee from database
		/// </summary>
		/// <returns></returns>
		public List<Test1Employee> GetEmployees()
		{
			List<Test1Employee> employees = new List<Test1Employee>();
			string Query = "SELECT Id, FirstName, MiddleName, LastName, DOB, MobileNumber, Address FROM [PracThirteen].[dbo].[Employee]";
			SqlCommand cmd = new SqlCommand(Query, conn);
			conn.Open();
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				Test1Employee emp = new Test1Employee();
				emp.Id = Convert.ToInt32(reader.GetValue(0).ToString());
				emp.FirstName = reader.GetValue(1).ToString();
				emp.MiddleName = reader.GetValue(2).ToString();
				emp.LastName = reader.GetValue(3).ToString();
				emp.DOB = Convert.ToDateTime(reader.GetValue(4).ToString());
				emp.MobileNumber = reader.GetValue(5).ToString();
				emp.Address = reader.GetValue(6).ToString();
				employees.Add(emp);
			}
			conn.Close();
			return employees;
		}

		/// <summary>
		/// It Insert the data based on emplyee deatils.
		/// </summary>
		/// <param name="employee"></param>
		/// <returns>boolean</returns>
		public bool AddEmployee(Test1Employee employee)
		{
			try
			{
				conn.Open();
				string AddStudent = $"INSERT INTO [PracThirteen].[dbo].[Employee] (FirstName, MiddleName, LastName, DOB, MobileNumber, Address) VALUES ('{employee.FirstName}', '{employee.MiddleName}', '{employee.LastName}', '{employee.DOB.Year}-{employee.DOB.Month}-{employee.DOB.Day}', '{employee.MobileNumber}','{employee.Address}')";
				SqlCommand sqlCommand = new SqlCommand(AddStudent, conn);
				int RowAffected = sqlCommand.ExecuteNonQuery();
				if (RowAffected > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception)
			{
				return false;
			}
			finally
			{
				conn.Close();
			}
		}

		/// <summary>
		/// It Update the database first record firstname to SQLPerson
		/// </summary>
		public void UpdateFirstEmployeeName()
		{
			try
			{
				conn.Open();
				string updateQuery = "UPDATE [PracThirteen].[dbo].[Employee] SET FirstName = @FirstName WHERE Id = (SELECT MIN(Id) FROM [PracThirteen].[dbo].[Employee])";
				SqlCommand command = new SqlCommand(updateQuery, conn);
				command.Parameters.AddWithValue("@FirstName", "SQLPerson");

				command.ExecuteNonQuery();
			}
			finally
			{
				conn.Close();
			}
		}

		/// <summary>
		/// It Update the all the records middle name to I
		/// </summary>
		public void UpdateMiddleEmployeeName()
		{
			try
			{
				conn.Open();
				string updateQuery = "UPDATE [PracThirteen].[dbo].[Employee] SET MiddleName = @MiddleName";
				SqlCommand command = new SqlCommand(updateQuery, conn);
				command.Parameters.AddWithValue("@MiddleName", "I");

				command.ExecuteNonQuery();
			}
			finally
			{
				conn.Close();
			}
		}

		/// <summary>
		/// It delete the records from database whose id is less than 2
		/// </summary>
		public void DeleteEmployee()
		{
			try
			{
				conn.Open();
				string updateQuery = "DELETE FROM [PracThirteen].[dbo].[Employee] WHERE Id < 2";
				SqlCommand command = new SqlCommand(updateQuery, conn);

				command.ExecuteNonQuery();
			}
			finally
			{
				conn.Close();
			}
		}

		/// <summary>
		/// It delete all the Employee record from database  
		/// </summary>
		public void DeleteAllEmployee()
		{
			try
			{
				conn.Open();
				string updateQuery = "TRUNCATE TABLE [PracThirteen].[dbo].[Employee]";
				SqlCommand command = new SqlCommand(updateQuery, conn);

				command.ExecuteNonQuery();
			}
			finally
			{
				conn.Close();
			}
		}

		/// <summary>
		/// It Insert some satic data to database 
		/// </summary>
		public void InsertEmployeeData()
		{
			try
			{
				conn.Open();
				string updateQuery = "INSERT INTO Employee (FirstName, MiddleName, LastName, DOB, MobileNumber, Address) VALUES ('Vipul', 'Kumar', 'Upadhyay', '1999-07-07', '1234567890', 'Ahmedabad, Gujarat'),('Bhavin', 'Kumar', 'Kareliya', '2000-05-10', '9876543210', 'Rajkot, Gujarat'),('Jil', 'Kumar', 'Patel', '2001-09-15', '5555555555', 'Anand, Gujarat')";
				SqlCommand command = new SqlCommand(updateQuery, conn);

				command.ExecuteNonQuery();
			}
			finally
			{
				conn.Close();
			}
		}
	}
}
