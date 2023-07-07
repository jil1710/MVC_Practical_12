using EmployeeModels.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace EmployeeDataAccessLayer.DbOperations
{
	public class Test2DbOperations
	{
		readonly string _connectionString = ConfigurationManager.ConnectionStrings["EmployeeDbEntities"].ConnectionString;
		readonly SqlConnection conn;
		public Test2DbOperations()
		{
			conn = new SqlConnection(_connectionString);
		}
		/// <summary>
		/// It returns All list of employee from database
		/// </summary>
		/// <returns></returns>
		public List<Test2Employee> GetEmployees()
		{
			List<Test2Employee> employees = new List<Test2Employee>();
			try
			{
				string Query = "SELECT Id, FirstName, MiddleName, LastName, DOB, MobileNumber, Address, Salary FROM [PracThirteen].[dbo].[TestTwoEmployee]";
				SqlCommand cmd = new SqlCommand(Query, conn);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					Test2Employee emp = new Test2Employee();
					emp.Id = Convert.ToInt32(reader.GetValue(0).ToString());
					emp.FirstName = reader.GetValue(1).ToString();
					emp.MiddleName = reader.GetValue(2).ToString();
					emp.LastName = reader.GetValue(3).ToString();
					emp.DOB = Convert.ToDateTime(reader.GetValue(4).ToString());
					emp.MobileNumber = reader.GetValue(5).ToString();
					emp.Address = reader.GetValue(6).ToString();
					emp.Salary = Convert.ToDecimal(reader.GetValue(7).ToString());
					employees.Add(emp);
				}
				return employees;
			}
			finally
			{
				conn.Close();
			}
		}

		/// <summary>
		/// It Insert the data based on emplyee deatils.
		/// </summary>
		/// <param name="employee"></param>
		/// <returns>boolean</returns>
		public bool AddEmployee(Test2Employee employee)
		{
			try
			{
				string AddStudent = $"INSERT INTO [PracThirteen].[dbo].[TestTwoEmployee] (FirstName, MiddleName, LastName, DOB, MobileNumber, Address, Salary) VALUES ('{employee.FirstName}', '{employee.MiddleName}', '{employee.LastName}', '{employee.DOB.Year}-{employee.DOB.Month}-{employee.DOB.Day}', '{employee.MobileNumber}','{employee.Address}',{employee.Salary})";
				if(employee.MiddleName == "" || employee.MiddleName == null)
				{
					AddStudent = $"INSERT INTO[PracThirteen].[dbo].[TestTwoEmployee](FirstName, LastName, DOB, MobileNumber, Address, Salary) VALUES('{employee.FirstName}', '{employee.LastName}', '{employee.DOB.Year}-{employee.DOB.Month}-{employee.DOB.Day}', '{employee.MobileNumber}', '{employee.Address}',{ employee.Salary})";
				}
				conn.Open();
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
			finally
			{
				conn.Close();
			}
		}

		/// <summary>
		/// It return the List of Employee Whose Dob is less than 01-01-2000
		/// </summary>
		/// <returns>It return List of Employees</returns>
		public List<Test2Employee> GetEmployeesBasedOnAge()
		{
			List<Test2Employee> employees = new List<Test2Employee>();

			string Query = "SELECT Id, FirstName, MiddleName, LastName, DOB, MobileNumber, Address, Salary FROM [PracThirteen].[dbo].[TestTwoEmployee] WHERE DOB > '01-01-2000'";
			SqlCommand cmd = new SqlCommand(Query, conn);
			conn.Open();
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				Test2Employee emp = new Test2Employee();
				emp.Id = Convert.ToInt32(reader.GetValue(0).ToString());
				emp.FirstName = reader.GetValue(1).ToString();
				emp.MiddleName = reader.GetValue(2).ToString();
				emp.LastName = reader.GetValue(3).ToString();
				emp.DOB = Convert.ToDateTime(reader.GetValue(4).ToString());
				emp.MobileNumber = reader.GetValue(5).ToString();
				emp.Address = reader.GetValue(6).ToString();
				emp.Salary = Convert.ToDecimal(reader.GetValue(7).ToString());
				employees.Add(emp);
			}
			conn.Close();
			return employees;
		}

		/// <summary>
		/// It Return Sum of Employee Salary
		/// </summary>
		/// <returns>decimal</returns>
		public decimal GetTotalSalary()
		{
			decimal total = 0;
			string query = "SELECT SUM(Salary) FROM [PracThirteen].[dbo].[TestTwoEmployee]";
			SqlCommand command = new SqlCommand(query, conn);
			conn.Open();
			object result = command.ExecuteScalar();

			if (result != DBNull.Value)
			{
				total = Convert.ToDecimal(result);
			}
			conn.Close();
			return total;
		}

		/// <summary>
		/// It will return total number of Employee whose middle name is Null
		/// </summary>
		/// <returns>Int</returns>
		public int GetMiddleNameNullCount()
		{
			int count = 0;
			conn.Open();
			string query = "SELECT COUNT(*) FROM [PracThirteen].[dbo].[TestTwoEmployee] WHERE MiddleName IS NULL";
			SqlCommand command = new SqlCommand(query, conn);
			count = (int)command.ExecuteScalar();
			conn.Close();
			return count;
		}

		/// <summary>
		/// It will Insert the data in database
		/// </summary>
		public void InsertEmployeeData()
		{
			try
			{
				conn.Open();
				string updateQuery = "INSERT INTO [PracThirteen].[dbo].[TestTwoEmployee] (FirstName, MiddleName, LastName, DOB, MobileNumber, Address, Salary) VALUES  ('Vipul', 'Kumar', 'Upadhyay', '1999-07-07', '1234567890', 'Ahmedabad, Gujarat', 85000), ('Bhavin', 'Kumar', 'Kareliya', '2000-05-10', '9876543210', 'Rajkot, Gujarat', 25000),  ('Jil', 'Kumar', 'Patel', '1999-09-07', '5555555555', 'Anand, Gujarat',75000), ('Test1', NULL, 'test', '2001-09-15', '5555555555', 'Anand, Gujarat',65000), ('Test2', NULL, 'testing', '2002-07-15', '5555555555', 'Anand, Gujarat',25000)";
				SqlCommand command = new SqlCommand(updateQuery, conn);

				command.ExecuteNonQuery();
			}
			finally
			{
				conn.Close();
			}
		}

		/// <summary>
		/// It will delete all Employees from database
		/// </summary>
		public void DeleteAllEmployees()
		{
			try
			{
				conn.Open();
				string updateQuery = "TRUNCATE TABLE [PracThirteen].[dbo].[TestTwoEmployee]";
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
