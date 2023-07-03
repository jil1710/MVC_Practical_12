using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeModels.Model
{
	public class Test1Employee
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "Enter First name less then 20 character")]
		[MaxLength(20)]
		public string FirstName { get; set; }
	
		[MaxLength(20, ErrorMessage = "Enter Middle name less then 20 character")]
		public string MiddleName { get; set; }
		[Required(ErrorMessage ="Enter Last name less then 20 character.")]
		[MaxLength(20)]
		public string LastName { get; set; }
		[DisplayName("Date of Birth")]
		[Required]
		
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime DOB { get; set; }
		[Required(ErrorMessage= "Enter Mobile number 10 digit only")]
		[MaxLength(10), MinLength(10)]
		public string MobileNumber { get; set; }
		[MaxLength(100)]
		public string Address { get; set; }
	}
}
