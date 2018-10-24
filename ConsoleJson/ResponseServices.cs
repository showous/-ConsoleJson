using Schneider.Andon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleJson
{
/// <summary>
///处理服务端响应数据
/// </summary>
	public class ResponseServices
	{
		/// <summary>
		/// 默认
		/// </summary>
		/// <returns></returns>
		public string Andon_S_Defult()
		{
			return "";
		}
	
		/// <summary>
		/// 警报总计
		/// </summary>
		/// <returns></returns>
		public string Andon_S_AllAlertInfoList()
		{
			//data数据
			AllAlertInfoListResult data = new AllAlertInfoListResult();
			data.cbCode = 0;
			data.dwCount = 1;

			data.cmdAllAlertInfoItem = new AllAlertInfoItem[data.dwCount];
			data.cmdAllAlertInfoItem[0] = new AllAlertInfoItem();
			data.cmdAllAlertInfoItem[0].wAlertType = 0;
			data.cmdAllAlertInfoItem[0].dwWaitingCount = 0;
			data.cmdAllAlertInfoItem[0].dwDealtingCount = 0;
			data.cmdAllAlertInfoItem[0].dwWaitingOutTimeCount = 0;
			data.cmdAllAlertInfoItem[0].dwDealtingOutTimeCount = 0;

			//head
			CmdHead head = new CmdHead();
			head.wSubID = CmdAndon.Andon_S_AllAlertInfoList;
			head.dwWorkID = 123456;

			string serverJsonText = data.GetSendData(head.ToArray(), data.ToArray());
			return serverJsonText;
		}


		/// <summary>
		/// 警报信息
		/// </summary>
		/// <returns></returns>
		public string Andon_S_AlertItemList()
		{
			//data数据
			AlertItemList data = new AlertItemList();
			data.cbCode = 0;
			data.dwCount = 1;

			data.cmdAlertItem = new AlertItem[data.dwCount];
			data.cmdAlertItem[0] = new AlertItem();
			data.cmdAlertItem[0].wStatu = 0;
			data.cmdAlertItem[0].wAlertType = 0;
			data.cmdAlertItem[0].cbAlterRank = 0;
			data.cmdAlertItem[0].szAlertDescribe = "1";
			data.cmdAlertItem[0].dwCreaterWorkID = 0;
			data.cmdAlertItem[0].szCreaterName = "1";
			data.cmdAlertItem[0].szCreateTime = "1";
			data.cmdAlertItem[0].dwDealtorID = 0;
			data.cmdAlertItem[0].szDealtorName = "1";
			data.cmdAlertItem[0].szDealtTime = "1";
			data.cmdAlertItem[0].dwCloserID = 0;
			data.cmdAlertItem[0].szCloserName = "1";
			data.cmdAlertItem[0].szCloserTime = "1";
			data.cmdAlertItem[0].szResolusion = "1";

			//head
			CmdHead head = new CmdHead();
			head.wSubID = CmdAndon.Andon_S_AlertItemList;
			head.dwWorkID = 123456;

			string serverJsonText = data.GetSendData(head.ToArray(), data.ToArray());
			return serverJsonText;
		}

		/// <summary>
		/// 创建警报消息
		/// </summary>
		/// <returns></returns>
		public string Andon_S_CreateAlert()
		{
			//data数据
			CmdResult data = new CmdResult();
			data.cbCode = 0;

			//head
			CmdHead head = new CmdHead();
			head.wSubID = CmdAndon.Andon_S_CreateAlert;
			head.dwWorkID = 123456;

			string serverJsonText = data.GetSendData(head.ToArray(), data.ToArray());
			return serverJsonText;
		}

		/// <summary>
		/// 升级警报
		/// </summary>
		/// <returns></returns>
		public string Andon_S_UpdateAlert()
		{
			//data数据
			CmdResult data = new CmdResult();
			data.cbCode = 0;

			//head
			CmdHead head = new CmdHead();
			head.wSubID = CmdAndon.Andon_S_UpdateAlert;
			head.dwWorkID = 123456;

			string serverJsonText = data.GetSendData(head.ToArray(), data.ToArray());
			return serverJsonText;
		}

		/// <summary>
		/// 接收警报
		/// </summary>
		/// <returns></returns>
		public string Andon_S_ReceiveAlert()
		{
			//data数据
			CmdResult data = new CmdResult();
			data.cbCode = 0;

			//head
			CmdHead head = new CmdHead();
			head.wSubID = CmdAndon.Andon_C_ReceiveAlert;
			head.dwWorkID = 123456;

			string serverJsonText = data.GetSendData(head.ToArray(), data.ToArray());
			return serverJsonText;
		}

		/// <summary>
		/// 关闭警报
		/// </summary>
		/// <returns></returns>
		public string Andon_S_CloseAlert()
		{
			//data数据
			CmdResult data = new CmdResult();
			data.cbCode = 0;

			//head
			CmdHead head = new CmdHead();
			head.wSubID = CmdAndon.Andon_S_CloseAlert;
			head.dwWorkID = 123456;

			string serverJsonText = data.GetSendData(head.ToArray(), data.ToArray());
			return serverJsonText;
		}

		/// <summary>
		/// 当班总计
		/// </summary>
		/// <returns></returns>
		public string Andon_S_AllDutyList()
		{
			//data数据
			AllDutyListResult data = new AllDutyListResult();
			data.cbCode = 0;
			data.dwCount = 1;

			data.cmdAllDutyItem = new AllDutyItem[data.dwCount];
			data.cmdAllDutyItem[0] = new AllDutyItem();
			data.cmdAllDutyItem[0].wAlertType = 0;
			data.cmdAllDutyItem[0].dwAlertCount = 0;
			data.cmdAllDutyItem[0].dwDealtingCount = 0;
			data.cmdAllDutyItem[0].dwWaitingCount = 0;
			data.cmdAllDutyItem[0].dwTimeoutWaitingCount = 0;
			data.cmdAllDutyItem[0].dwDealtingCount = 0;
			data.cmdAllDutyItem[0].dwTimeoutDealtingCount = 0;
			//head
			CmdHead head = new CmdHead();
			head.wSubID = CmdAndon.Andon_S_AllDutyList;
			head.dwWorkID = 123456;

			string serverJsonText = data.GetSendData(head.ToArray(), data.ToArray());
			return serverJsonText;
		}

		/// <summary>
		/// 产线信息
		/// </summary>
		/// <returns></returns>
		public string Andon_S_ProductionLineList()
		{
			//data数据
			ProductionLineListResult data = new ProductionLineListResult();
			data.cbCode = 0;
			data.dwCount = 1;

			data.cmdProductionLineItem = new ProductionLineItem[data.dwCount];
			data.cmdProductionLineItem[0] = new ProductionLineItem();
			data.cmdProductionLineItem[0].dwProductionLineID = 0;
			data.cmdProductionLineItem[0].szProductionLineName = "1";
			CmdHead head = new CmdHead();
			head.wSubID = CmdAndon.Andon_S_ProductionLineList;
			head.dwWorkID = 123456;

			string serverJsonText = data.GetSendData(head.ToArray(), data.ToArray());
			return serverJsonText;
		}

		/// <summary>
		/// 报表信息
		/// </summary>
		/// <returns></returns>
		public string Andon_S_SumTable()
		{
			//data数据
			SumTableResult data = new SumTableResult();
			data.cbCode = 0;
			data.dwCount = 1;

			data.cmdSumTableItem = new SumTableItem[data.dwCount];
			data.cmdSumTableItem[0] = new SumTableItem();
			data.cmdSumTableItem[0].szTableName = "平均时间统计表";
			data.cmdSumTableItem[0].szYDescribe = "平均时间";
			data.cmdSumTableItem[0].szYUnit = "min";
			data.cmdSumTableItem[0].cbCount = 1;
			data.cmdSumTableItem[0].szXNames =new string[]{ "xyz","abc" };
			data.cmdSumTableItem[0].szXCounts = new string[] { "1", "2" };
			CmdHead head = new CmdHead();
			head.wSubID = CmdAndon.Andon_S_SumTable;
			head.dwWorkID = 123456;

			string serverJsonText = data.GetSendData(head.ToArray(), data.ToArray());
			return serverJsonText;
		}

		/// <summary>
		/// 报警者信息
		/// </summary>
		/// <returns></returns>
		public string Andon_S_AlertorInfo()
		{
			//data数据
			AlertorInfoResult data = new AlertorInfoResult();
			data.cbCode = 0;
			data.cbStatu = 1;
			data.szName = "";
			data.dwWorkID = 1;
			data.wAlertType = 1;
			data.dwWorkGroup =1;
			data.szWatchID = "";

			//head
			CmdHead head = new CmdHead();
			head.wSubID = CmdAndon.Andon_S_AlertorInfo;
			head.dwWorkID = 123456;

			string serverJsonText = data.GetSendData(head.ToArray(), data.ToArray());
			return serverJsonText;
		}

		/// <summary>
		/// 选择产线
		/// </summary>
		/// <returns></returns>
		public string Andon_S_ChooseProductionLine()
		{
			//data数据
			CmdResult data = new CmdResult();
			data.cbCode = 0;

			//head
			CmdHead head = new CmdHead();
			head.wSubID = CmdAndon.Andon_S_ChooseProductionLine;
			head.dwWorkID = 123456;

			string serverJsonText = data.GetSendData(head.ToArray(), data.ToArray());
			return serverJsonText;
		}
	}
}
