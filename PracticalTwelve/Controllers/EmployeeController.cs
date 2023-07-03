using EmployeeDataAccessLayer.DbOperations;
using EmployeeModels.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PracticalTwelve.Controllers
{
	public class EmployeeController : Controller
	{
		readonly Test1DbOperations dbOperations = new Test1DbOperations();
		[HttpGet]
		public ActionResult Index()
		{
			List<Test1Employee> employees = dbOperations.GetEmployees();

			return View(employees);
		}

		[HttpGet]
		public ActionResult Create()
		{
			return View(new Test1Employee());
		}

		[HttpPost]
		public ActionResult Create(Test1Employee employee)
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
		public ActionResult UpdateFName()
		{
			dbOperations.UpdateFirstEmployeeName();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult UpdateMName()
		{
			dbOperations.UpdateMiddleEmployeeName();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Delete()
		{
			dbOperations.DeleteEmployee();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult DeleteAllEmployee()
		{
			dbOperations.DeleteAllEmployee();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult InsertEmployee()
		{
			dbOperations.InsertEmployeeData();
			return RedirectToAction("Index");
		}

	}
}
