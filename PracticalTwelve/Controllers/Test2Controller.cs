using EmployeeDataAccessLayer.DbOperations;
using EmployeeModels.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PracticalTwelve.Controllers
{
	public class Test2Controller : Controller
	{
		readonly Test2DbOperations dbOperations = new Test2DbOperations();
		[HttpGet]
		public ActionResult Index()
		{
			List<Test2Employee> test2Employees = dbOperations.GetEmployees();
			return View(test2Employees);
		}
		[HttpGet]
		public ActionResult Create()
		{
			return View(new Test2Employee());
		}

		[HttpPost]
		public ActionResult Create(Test2Employee employee)
		{
			var test = Convert.ToDateTime(employee.DOB) - DateTime.Now;
			var test1 = new DateTime(1900, 01, 01) - Convert.ToDateTime(employee.DOB);
			if (test.Days > 1 || test1.Days > 1)
			{
				ViewBag.Message = "Date Should Be between 1900-01-01 to Today's Date";
				return View(employee);
			}
			bool IsAdded = dbOperations.AddEmployee(employee);
			if (IsAdded)
			{
				return RedirectToAction("Index");
			}
			else
			{
				return View(employee);
			}
		}

		[HttpGet]
		public ActionResult GetEmployee()
		{
			List<Test2Employee> test2Employees = dbOperations.GetEmployeesBasedOnAge();
			return View(test2Employees);
		}

		[HttpGet]
		public ActionResult GetTotalEmployeeSalary()
		{
			decimal TotalSalary = dbOperations.GetTotalSalary();
			TempData["TotalSalary"] = $"Total Salary : {TotalSalary}";
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult MiddleNameNullCount()
		{
			int Count = dbOperations.GetMiddleNameNullCount();
			TempData["Count"] = $"Total {Count} Employee without middle name";
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult InsertEmployee()
		{
			dbOperations.InsertEmployeeData();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult DeleteAllEmployee()
		{
			dbOperations.DeleteAllEmployees();
			return RedirectToAction("Index");
		}
	}
}