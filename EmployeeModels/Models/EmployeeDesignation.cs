using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeModels.Model
{
	public class EmployeeDesignation
	{
		public int Id { get; set; }
		[Required]
		[StringLength(50)]
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		[DisplayName("Date of Birth")]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime DOB { get; set; }
		public string MobileNumber { get; set; }
		public string Address { get; set; }
		public decimal Salary { get; set; }
		public int DesignationId { get; set; }
		public string Designation { get; set; }
	}
}
