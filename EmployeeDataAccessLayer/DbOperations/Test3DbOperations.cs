using EmployeeModels.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeDataAccessLayer.DbOperations
{
	public class Test3DbOperations
	{
		readonly string _connectionString = ConfigurationManager.ConnectionStrings["EmployeeDbEntities"].ConnectionString;
		readonly SqlConnection conn;
		public Test3DbOperations()
		{
			conn = new SqlConnection(_connectionString);
		}

		/// <summary>
		/// It returns All list of employee from database
		/// </summary>
		/// <returns></returns>
		public List<Test3Employee> GetEmployees()
		{
			List<Test3Employee> employees = new List<Test3Employee>();

			string Query = "SELECT Id, FirstName, MiddleName, LastName, DOB, MobileNumber, Address, Salary,DesignationId FROM [PracThirteen].[dbo].[TestThreeEmployee]";
			SqlCommand cmd = new SqlCommand(Query, conn);
			conn.Open();
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				Test3Employee emp = new Test3Employee();
				emp.Id = Convert.ToInt32(reader.GetValue(0).ToString());
				emp.FirstName = reader.GetValue(1).ToString();
				emp.MiddleName = reader.GetValue(2).ToString();
				emp.LastName = reader.GetValue(3).ToString();
				emp.DOB = Convert.ToDateTime(reader.GetValue(4).ToString());
				emp.MobileNumber = reader.GetValue(5).ToString();
				emp.Address = reader.GetValue(6).ToString();
				emp.Salary = Convert.ToDecimal(reader.GetValue(7).ToString());
				emp.DesignationId = Convert.ToInt32(reader.GetValue(8).ToString());
				employees.Add(emp);
			}
			conn.Close();
			return employees;
		}

		/// <summary>
		/// It return All the list of designation
		/// </summary>
		/// <returns></returns>
		public List<DesignationModel> GetDesignationList()
		{
			List<DesignationModel> designations = new List<DesignationModel>();
			string Query = "SELECT Id, Designation FROM [PracThirteen].[dbo].[Designation]";
			SqlCommand cmd = new SqlCommand(Query, conn);
			conn.Open();
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				DesignationModel designation = new DesignationModel();
				designation.Id = Convert.ToInt32(reader.GetValue(0).ToString());
				designation.Designation = reader.GetValue(1).ToString();
				designations.Add(designation);
			}
			conn.Close();
			return designations;
		}

		/// <summary>
		/// It return total no of designation 
		/// </summary>
		/// <returns></returns>
		public int DesignationCount()
		{
			int count = 0;
			conn.Open();
			string query = "SELECT COUNT(*) FROM [PracThirteen].[dbo].[Designation]";
			SqlCommand command = new SqlCommand(query, conn);
			count = (int)command.ExecuteScalar();
			return count;
		}

		/// <summary>
		/// It returns list of Employee with designation 
		/// </summary>
		/// <returns></returns>
		public List<EmployeeDesignation> GetEmployeeWithDesignation()
		{
			List<EmployeeDesignation> employees = new List<EmployeeDesignation>();
			string Query = "SELECT FirstName, MiddleName, LastName, Designation FROM TestThreeEmployee JOIN Designation ON TestThreeEmployee.DesignationId = Designation.Id";
			SqlCommand cmd = new SqlCommand(Query, conn);
			conn.Open();
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				EmployeeDesignation empDes = new EmployeeDesignation();
				empDes.FirstName = reader["FirstName"].ToString();
				empDes.MiddleName = reader["MiddleName"].ToString();
				empDes.LastName = reader["LastName"].ToString();
				empDes.Designation = reader["Designation"].ToString();

				employees.Add(empDes);
			}
			conn.Close();
			return employees;
		}

		/// <summary>
		/// It returns all the Employee from database using view
		/// </summary>
		/// <returns></returns>
		public List<EmployeeDesignation> GetEmployeeWithView()
		{
			List<EmployeeDesignation> employees = new List<EmployeeDesignation>();
			string Query = "SELECT * FROM [PracThirteen].[dbo].[EmployeeView]";
			SqlCommand cmd = new SqlCommand(Query, conn);
			conn.Open();
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				EmployeeDesignation empDes = new EmployeeDesignation();
				empDes.Id = Convert.ToInt32(reader.GetValue(0).ToString());
				empDes.FirstName = reader["FirstName"].ToString();
				empDes.MiddleName = reader["MiddleName"].ToString();
				empDes.LastName = reader["LastName"].ToString();
				empDes.Designation = reader["Designation"].ToString();
				empDes.DOB = Convert.ToDateTime(reader["DOB"].ToString());
				empDes.MobileNumber = reader["MobileNumber"].ToString();
				empDes.Address = reader["Address"].ToString();
				empDes.Salary = Convert.ToDecimal(reader["Salary"].ToString());

				employees.Add(empDes);
			}
			conn.Close();
			return employees;
		}

		/// <summary>
		/// It Insert the designation in database based on parameter using store procedure
		/// </summary>
		/// <param name="degigantion"></param>
		public void InsertDesignationWithsp(string degigantion)
		{
			conn.Open();
			using (SqlCommand command = new SqlCommand("[PracThirteen].[dbo].[InsertDesignation]", conn))
			{
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue("@DesignationName", degigantion);
				command.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// Insert Employee in database using store procedure with parameters 
		/// </summary>
		/// <param name="test3Employee"></param>
		public void InsertEmployeeWithsp(Test3Employee test3Employee)
		{
			conn.Open();
			using (SqlCommand command = new SqlCommand("[PracThirteen].[dbo].[InsertEmployee]", conn))
			{
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue("@FirstName", test3Employee.FirstName);
				command.Parameters.AddWithValue("@MiddleName", test3Employee.MiddleName);
				command.Parameters.AddWithValue("@LastName", test3Employee.LastName);
				command.Parameters.AddWithValue("@DOB", test3Employee.DOB);
				command.Parameters.AddWithValue("@MobileNumber", test3Employee.MobileNumber);
				command.Parameters.AddWithValue("@Address", test3Employee.Address);
				command.Parameters.AddWithValue("@Salary", test3Employee.Salary);
				command.Parameters.AddWithValue("@DesignationId", test3Employee.DesignationId);
				command.ExecuteNonQuery();
			}
			conn.Close();
		}

		/// <summary>
		/// It return the designation name which is assign to more than 1
		/// </summary>
		/// <returns></returns>
		public List<string> GetDesignationWhichMoreThenOne()
		{
			List<string> result = new List<string>();
			conn.Open();
			string query = "SELECT d.Designation " +
						   "FROM Designation d " +
						   "INNER JOIN TestThreeEmployee e ON d.Id = e.DesignationId " +
						   "GROUP BY d.Designation " +
						   "HAVING COUNT(e.Id) > 1";
			SqlCommand command = new SqlCommand(query, conn);
			SqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				string designationName = reader["Designation"].ToString();
				result.Add(designationName);
			}
			reader.Close();
			conn.Close();
			return result;
		}

		/// <summary>
		/// It returns Employee list with Designation using store procedure
		/// </summary>
		/// <returns></returns>
		public List<EmployeeDesignation> GetEmpListWithsp()
		{
			List<EmployeeDesignation> result = new List<EmployeeDesignation>();
			conn.Open();
			using (SqlCommand command = new SqlCommand("[PracThirteen].[dbo].[GetEmployeeList]", conn))
			{
				command.CommandType = CommandType.StoredProcedure;
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					EmployeeDesignation empDes = new EmployeeDesignation();
					empDes.Id = Convert.ToInt32(reader.GetValue(0).ToString());
					empDes.FirstName = reader["FirstName"].ToString();
					empDes.MiddleName = reader["MiddleName"].ToString();
					empDes.LastName = reader["LastName"].ToString();
					empDes.Designation = reader["Designation"].ToString();
					empDes.DOB = Convert.ToDateTime(reader["DOB"].ToString());
					empDes.MobileNumber = reader["MobileNumber"].ToString();
					empDes.Address = reader["Address"].ToString();
					empDes.Salary = Convert.ToDecimal(reader["Salary"].ToString());

					result.Add(empDes);
				}
				conn.Close();
				return result;
			}
		}

		/// <summary>
		/// It return Employee Based on Designation id using store procedure
		/// </summary>
		/// <param name="degigantionId"></param>
		/// <returns></returns>
		public List<Test3Employee> GetEmployeeWithDesignationIdsp(int degigantionId)
		{
			List<Test3Employee> employees = new List<Test3Employee>();
			conn.Open();
			using (SqlCommand command = new SqlCommand("[PracThirteen].[dbo].[GetEmployeesByDesignationId]", conn))
			{
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue("@DesignationId", degigantionId);
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					Test3Employee emp = new Test3Employee();
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
		}

		/// <summary>
		/// It return Emplayee name whose salasry is max
		/// </summary>
		/// <returns></returns>
		public string GetMaxSalaryEmployee()
		{
			string employeeName = String.Empty;
			conn.Open();
			string sqlQuery = "SELECT TOP 1 CONCAT(FirstName, ' ', MiddleName,' ', LastName) AS EmployeeName FROM [PracThirteen].[dbo].[TestThreeEmployee] ORDER BY Salary DESC";
			SqlCommand command = new SqlCommand(sqlQuery, conn);
			SqlDataReader reader = command.ExecuteReader();
			if (reader.Read())
			{
				employeeName = reader["EmployeeName"].ToString();
			}
			reader.Close();
			conn.Close();
			return employeeName;
		}
	}
}
