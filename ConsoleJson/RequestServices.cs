using Schneider.Andon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleJson
{
	/// <summary>
	/// 初始化客户端请求数据
	/// </summary>
	public class RequestServices
	{
		/// <summary>
		/// 默认
		/// </summary>
		/// <param name="clientJsonText"></param>
		/// <returns></returns>
		public string Andon_C_Defult(string clientJsonText)
		{
			return "";
		}

		/// <summary>
		/// 警报总计
		/// </summary>
		/// <param name="clientJsonText"></param>
		/// <returns></returns>
		public void Andon_C_AllAlertInfoList(string clientJsonText)
		{
			
		}

		/// <summary>
		/// 警报信息
		/// </summary>
		/// <param name="clientJsonText"></param>
		public void Andon_C_AlertItemList(string clientJsonText)
		{
			AlertItemListReq req = new AlertItemListReq();
			req.Init(clientJsonText);

		}
		/// <summary>
		/// 创建警报
		/// </summary>
		/// <param name="clientJsonText"></param>
		public void Andon_C_CreateAlert(string clientJsonText)
		{
			CreateAlertReq req = new CreateAlertReq();
			req.Init(clientJsonText);
		}

		/// <summary>
		/// 升级警报
		/// </summary>
		/// <param name="clientJsonText"></param>
		public void Andon_C_UpdateAlert(string clientJsonText)
		{
			UpdateAlertReq req = new UpdateAlertReq();
			req.Init(clientJsonText);
		}

		/// <summary>
		/// 接收警报
		/// </summary>
		/// <param name="clientJsonText"></param>
		public void Andon_C_ReceiveAlert(string clientJsonText)
		{
			ReceiveAlertReq req = new ReceiveAlertReq();
			req.Init(clientJsonText);
		}

		/// <summary>
		/// 关闭警报
		/// </summary>
		/// <param name="clientJsonText"></param>
		public void Andon_C_CloseAlert(string clientJsonText)
		{
			CloseAlertReq req = new CloseAlertReq();
			req.Init(clientJsonText);
		}

		/// <summary>
		/// 当班统计
		/// </summary>
		/// <param name="clientJsonText"></param>
		public void Andon_C_AllDutyList(string clientJsonText)
		{
			
		}
		/// <summary>
		/// 产线信息
		/// </summary>
		/// <param name="clientJsonText"></param>
		public void Andon_C_ProductionLineList(string clientJsonText)
		{
			
		}
		/// <summary>
		/// 报表信息
		/// </summary>
		/// <param name="clientJsonText"></param>
		public void Andon_C_SumTable(string clientJsonText)
		{
		
		}

		/// <summary>
		/// 查询报警者信息
		/// </summary>
		/// <param name="clientJsonText"></param>
		public void Andon_C_AlertorInfo(string clientJsonText)
		{
			SetAlertorInfoReq req = new SetAlertorInfoReq();
			req.Init(clientJsonText);
		}

		/// <summary>
		/// 选择产线
		/// </summary>
		/// <param name="clientJsonText"></param>
		public void Andon_C_ChooseProductionLine(string clientJsonText)
		{
			ChooseProductionLineReq req = new ChooseProductionLineReq();
			req.Init(clientJsonText);
		}
	}
}
