using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schneider.Andon;
using Schneider.Frame;

	namespace ConsoleJson
{
	public class Common : StructBase
	{
	
		/// <summary>
		/// 通过客户端请求执行不同业务类型的方法
		/// </summary>
		/// <param name="jsonText">客户端发送的JSON数据</param>
		/// <returns></returns>
		public string GetReturnBusinessData( string  clientJsonText)
		{
			RequestServices req = new RequestServices();
			ResponseServices res = new ResponseServices();
	
			CmdHead head = new CmdHead();
			head.Init(GetCmdHead(clientJsonText));
			switch (head.wSubID)
			{
				case CmdAndon.Andon_C_Defult:
					req.Andon_C_Defult(clientJsonText);
					return res.Andon_S_Defult();

				case CmdAndon.Andon_C_AllAlertInfoList:
					req.Andon_C_AllAlertInfoList(clientJsonText);
					return res.Andon_S_AllAlertInfoList();

				case CmdAndon.Andon_C_AlertItemList:
					req.Andon_C_AlertItemList(clientJsonText);
					return res.Andon_S_AlertItemList();

				case CmdAndon.Andon_C_CreateAlert:
					req.Andon_C_CreateAlert(clientJsonText);
					return res.Andon_S_CreateAlert();

				case CmdAndon.Andon_C_UpdateAlert:
					req.Andon_C_UpdateAlert(clientJsonText);
					return res.Andon_S_UpdateAlert();

				case CmdAndon.Andon_C_ReceiveAlert:
					req.Andon_C_ReceiveAlert(clientJsonText);
					return res.Andon_S_ReceiveAlert();

				case CmdAndon.Andon_C_CloseAlert:
					req.Andon_C_CloseAlert(clientJsonText);
					return res.Andon_S_CloseAlert();

				case CmdAndon.Andon_C_AllDutyList:
					req.Andon_C_AllDutyList(clientJsonText);
					return res.Andon_S_AllDutyList();

				case CmdAndon.Andon_C_ProductionLineList:
					req.Andon_C_ProductionLineList(clientJsonText);
					return res.Andon_S_ProductionLineList();

				case CmdAndon.Andon_C_SumTable:
					req.Andon_C_SumTable(clientJsonText);
					return res.Andon_S_SumTable();

				case CmdAndon.Andon_C_AlertorInfo:
					req.Andon_C_AlertorInfo(clientJsonText);
					return res.Andon_S_AlertorInfo();

				case CmdAndon.Andon_C_ChooseProductionLine:
					req.Andon_C_ChooseProductionLine(clientJsonText);
					return res.Andon_S_ChooseProductionLine();
			}
			return "";
		}
		
	}

}
