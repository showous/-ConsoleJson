using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleJson.Json
{
	public class CmdHead
	{
		private string wSubID;
		private string dwWorkID;

		public string WSubID { get => wSubID; set => wSubID = value; }
		public string DwWorkID { get => dwWorkID; set => dwWorkID = value; }
	}
}
