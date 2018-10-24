using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleJson
{
	class Program
	{
		static void Main(string[] args)
		{
			//string jsonText = "{\"CmdHead\": {\"wSubID\": \"3\",\"dwWorkID\": \"123456\" },\"CmdData\": {\"wAlertType\": \"3\",\"cbAlterRank\": \"1\",\"szAlertDescribe\": \"双11了\"}}";
			//JObject jo = (JObject)JsonConvert.DeserializeObject(jsonText);
			//string wSubID = jo["CmdHead"]["wSubID"].ToString();
			//string wAlertType = jo["CmdData"]["wAlertType"].ToString();

		

			//Console.WriteLine("wSubID:"+ wSubID);
			//Console.WriteLine("wAlertType:" + wAlertType);

			//Console.WriteLine("--------------");
			//var Andon_C_CreateAlert = new CmdObject
			//{
			//	CmdHead = new CmdHead{ WSubID="3", DwWorkID= "123456" },
			//	 CmdData=new CmdData { CbCode="0"}
			//};

			//string json = JsonHelper.SerializeObject(Andon_C_CreateAlert);
			//Console.WriteLine(json);
			//Console.Read();
  		}
	}
}
	

