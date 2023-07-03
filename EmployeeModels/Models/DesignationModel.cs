using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeModels.Model
{
	public class DesignationModel
	{
		public int Id { get; set; }
		[Required]
		[MaxLength(20, ErrorMessage = "Designation should be 20 character only")]
		public string Designation { get; set; }
	}
}
