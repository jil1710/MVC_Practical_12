using EmployeeDataAccessLayer.DbOperations;
using EmployeeModels.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PracticalTwelve.Controllers
{
	public class Test3Controller : Controller
	{
		readonly Test3DbOperations dbOperations = new Test3DbOperations();
		[HttpGet]
		public ActionResult Index()
		{
			List<Test3Employee> employees = dbOperations.GetEmployees();
			return View(employees);
		}

		public ActionResult GetDesignation()
		{
			List<DesignationModel> designation = dbOperations.GetDesignationList();
			return View(designation);
		}

		public ActionResult GetEmployeeFromView()
		{
			List<EmployeeDesignation> employees = dbOperations.GetEmployeeWithView();
			return View(employees);
		}
		public ActionResult GetEmployeeWithsp()
		{
			List<EmployeeDesignation> employees = dbOperations.GetEmpListWithsp();
			return View(employees);
		}
		public ActionResult GetEmpWithDesignation()
		{
			List<EmployeeDesignation> employees = dbOperations.GetEmployeeWithDesignation();
			return View(employees);
		}
		public ActionResult GetEmpWithDesignationId()
		{
			return View(new DesignationModel());
		}
		[HttpPost]
		public ActionResult GetEmpWithDesignationId(int id)
		{
			List<Test3Employee> employees = dbOperations.GetEmployeeWithDesignationIdsp(id);
			return View("GetEmpWithDesignationIdUsingsp", employees);
		}

		public ActionResult InsertDesignationUsingsp()
		{
			return View(new DesignationModel());
		}

		[HttpPost]
		public ActionResult InsertDesignationUsingsp(string designation)
		{
			dbOperations.InsertDesignationWithsp(designation);
			return RedirectToAction("Index");
		}

		public ActionResult InsertEmployeeUsingsp()
		{
			return View(new Test3Employee());
		}

		[HttpPost]
		public ActionResult InsertEmployeeUsingsp(Test3Employee employee)
		{
			var test = Convert.ToDateTime(employee.DOB) - DateTime.Now;
			var test1 = new DateTime(1900, 01, 01) - Convert.ToDateTime(employee.DOB);
			if (test.Days > 1 || test1.Days > 1)
			{
				ViewBag.Message = "Date Should Be between 1900-01-01 to Today's Date";
				return View(employee);
			}
			dbOperations.InsertEmployeeWithsp(employee);
			return RedirectToAction("Index");
		}

		public ActionResult GetDesignationCount()
		{
			TempData["Count"] = "Total Designation is " + dbOperations.DesignationCount();
			return RedirectToAction("GetDesignation");
		}

		public ActionResult GetDesignationNameMoreThenOneTime()
		{
			TempData["DesignationList"] = dbOperations.GetDesignationWhichMoreThenOne();
			return RedirectToAction("Index");
		}

		public ActionResult EmpMaxSalary()
		{
			TempData["EmpMaxSal"] = dbOperations.GetMaxSalaryEmployee();
			return RedirectToAction("Index");
		}
	}
}