/// <summary>
/// Creater : SiQian
/// Time : 2018.09.28
/// </summary>

using System.Collections;
using System.Collections.Generic;
using Schneider.Frame;
using LitJson;

namespace Schneider.Andon
{
	public class CmdAndon
	{
		//警报状态
		public const uint AlertStatu_Defult = 0x0000;                 //默认
		public const uint AlertStatu_Waiting = 0x0001;                //等待处理
		public const uint AlertStatu_WaitingOuttime = 0x0002;         //等待超时
		public const uint AlertStatu_Dealting = 0x0004;               //处理中
		public const uint AlertStatu_DealtingOuttime = 0x0008;        //处理超时
		public const uint AlertStatu_Dealted = 0x0010;                //处理完成

		//客户端Client:
		/// <summary>
		/// 默认（暂停使用）
		/// </summary>
		public const ushort Andon_C_Defult = 0;                 
		public const ushort Andon_C_AllAlertInfoList = 1;      //警报总计
		public const ushort Andon_C_AlertItemList = 2;         //警报信息
		public const ushort Andon_C_CreateAlert = 3;           //创建警报
		public const ushort Andon_C_UpdateAlert = 4;           //更新警报
		public const ushort Andon_C_ReceiveAlert = 5;          //接收警报
		public const ushort Andon_C_CloseAlert = 6;            //关闭警报
		public const ushort Andon_C_AllDutyList = 7;           //当班总计
		public const ushort Andon_C_ProductionLineList = 8;    //产线信息
		public const ushort Andon_C_SumTable = 9;              //报表信息
		public const ushort Andon_C_AlertorInfo = 10;          //查询报警者信息
		public const ushort Andon_C_ChooseProductionLine = 11; //选择产线

		//服务端Server:
		public const ushort Andon_S_Defult = 0;                //默认（暂停使用）
		public const ushort Andon_S_AllAlertInfoList = 1;      //警报总计
		public const ushort Andon_S_AlertItemList = 2;         //警报信息
		public const ushort Andon_S_CreateAlert = 3;           //创建警报
		public const ushort Andon_S_UpdateAlert = 4;           //更新警报
		public const ushort Andon_S_ReceiveAlert = 5;          //接收警报
		public const ushort Andon_S_CloseAlert = 6;            //关闭警报
		public const ushort Andon_S_AllDutyList = 7;           //当班总计
		public const ushort Andon_S_ProductionLineList = 8;    //产线信息
		public const ushort Andon_S_SumTable = 9;              //报表信息
		public const ushort Andon_S_AlertorInfo = 10;          //查询报警者信息
		public const ushort Andon_S_ChooseProductionLine = 11; //选择产线
	}

	/// <summary>
	/// 请求单元(包头)
	/// StructBase处理数据封装和解析
	/// 
	/// exp1: 请求报表信息：SendSingleMsg("Andon","{"wSubID":"9"}");
	///       返回:{"code":"0","Tables":"{{...},{...},{...}}"}
	///       
	/// exp2: 请求警报单元表(制程、等待处理状态)：SendSingleMsg("Andon","{"wSubID":"9","wAlertType":"5","wStatu":"1"}");
	/// </summary>
	public class CmdHead : StructBase
	{
		public ushort wSubID;
		public uint dwWorkID;

		public override void Init(JsonData content)
		{
			base.Init(content);
			wSubID = ReadUShort("wSubID");
			dwWorkID = ReadUInt("dwWorkID");
		}

		public override string ToArray()
		{
			WriteData("wSubID", wSubID.ToString());
			WriteData("dwWorkID", dwWorkID.ToString());
			return base.ToArray();
		}
		public override string ToString()
		{
			string result = "CmdHead:wSubID=" + wSubID + ",dwWaitingCount=" + dwWorkID;
			return result;
		}
	}

	public class CmdResult : StructBase
	{
		public ushort cbCode;

		public override void Init(JsonData content)
		{
			base.Init(content);
			cbCode = ReadByte("cbCode");
		}

		public override string ToArray()
		{
			WriteData("cbCode", cbCode.ToString());
			return base.ToArray();
		}
		public override string ToString()
		{
			string result = "CmdResult:cbCode=" + cbCode;
			return result;
		}
	}

	public enum AlertorInfoStatu
	{
		None = 0,
		Value = 1,
	}

	public enum AlertType
	{
		Defult = 0,            //默认
		Safety = 1,            //安全
		BaseContent = 2,       //库存
		Quality = 3,           //质量
		Repair = 4,            //维修
		MakeProcess = 5,       //制程
		Logistics = 6,         //物流
	}

	#region 报警系统
	//ReqUint
	//警报总计列表返回结果
	public class AllAlertInfoListResult : StructBase
	{
		public byte cbCode;// 结果码 0成功  其他值映射失败原因
		public uint dwCount;//数量
		public AllAlertInfoItem[] cmdAllAlertInfoItem; //警报信息表

		public override void Init(JsonData content)
		{
			base.Init(content);
			cbCode = ReadByte("cbCode");
			dwCount = ReadUInt("dwCount");

			cmdAllAlertInfoItem = new AllAlertInfoItem[dwCount];
			for (int i = 0; i < dwCount; i++)
			{
				cmdAllAlertInfoItem[i] = new AllAlertInfoItem();
				cmdAllAlertInfoItem[i].Init(ReadJsonItem("cmdAllAlertInfoItem", i));
			}

		}

		public override string ToArray()
		{
			WriteData("cbCode", cbCode.ToString());
			WriteData("dwCount", dwCount.ToString());
			WriteItems("cmdAllAlertInfoItem", cmdAllAlertInfoItem);
			return base.ToArray();
		}

		public override string ToString()
		{
			string result = "AllAlertInfoListResult:cbCode=" + cbCode + ",dwCount=" + dwCount + ",cmdAllAlertInfoItem={" + AndonUtility.GetItemsSeting(cmdAllAlertInfoItem) + "}";
			return result;
		}
	}

	//警报总计
	public class AllAlertInfoItem : StructBase
	{
		public ushort wAlertType; //警报类型
		public uint dwWaitingCount;//等待处理数量
		public uint dwDealtingCount;//正在处理数量

		public uint dwWaitingOutTimeCount;//等待超时数量
		public uint dwDealtingOutTimeCount;//处理超时数量

		public override void Init(JsonData content)
		{
			base.Init(content);
			wAlertType = ReadUShort("wAlertType");
			dwWaitingCount = ReadUInt("dwWaitingCount");
			dwDealtingCount = ReadUInt("dwDealtingCount");

			dwWaitingOutTimeCount = ReadUInt("dwWaitingOutTimeCount");
			dwDealtingOutTimeCount = ReadUInt("dwDealtingOutTimeCount");
		}

		public override string ToArray()
		{
			WriteData("wAlertType", wAlertType.ToString());
			WriteData("dwWaitingCount", dwWaitingCount.ToString());
			WriteData("dwDealtingCount", dwDealtingCount.ToString());

			WriteData("dwWaitingOutTimeCount", dwWaitingOutTimeCount.ToString());
			WriteData("dwDealtingOutTimeCount", dwDealtingOutTimeCount.ToString());
			return base.ToArray();
		}

		public override string ToString()
		{
			string result = "[AllAlertInfoItem:wAlertType=" + wAlertType + ",dwWaitingCount=" + dwWaitingCount + ",dwDealtingCount=" + dwDealtingCount +
				",dwWaitingOutTimeCount=" + dwWaitingOutTimeCount + ",dwDealtingOutTimeCount=" + dwDealtingOutTimeCount + "]";
			return result;
		}
	}

	//////////////////////////////////////////////////////////////////////////////////////////

	//请求警报单元表 -- 返回 AlertUnitList
	public class AlertItemListReq : StructBase
	{
		public ushort wAlertType; //警报类型(与或) -- 对应AlertType
		public uint wStatu;//处理状态(与或) -- 对应 CmdAndon.AlertStatu_XX

		public override void Init(JsonData content)
		{
			base.Init(content);
			wAlertType = ReadUShort("wAlertType");
			wStatu = ReadUInt("wStatu");
		}

		public override string ToArray()
		{
			WriteData("wAlertType", wAlertType.ToString());
			WriteData("wStatu", wStatu.ToString());
			return base.ToArray();
		}
		public override string ToString()
		{
			string result = "AlertItemListReq:wAlertType=" + wAlertType + ",wStatu=" + wStatu;
			return result;
		}

	}

	//警报单元表
	public class AlertItemList : StructBase
	{
		public byte cbCode;// 结果码 0成功  其他值映射失败原因
		public uint dwCount;//数量
		public AlertItem[] cmdAlertItem;
		public override void Init(JsonData content)
		{
			base.Init(content);
			cbCode = ReadByte("cbCode");
			dwCount = ReadUInt("dwCount");

			cmdAlertItem = new AlertItem[dwCount];
			for (int i = 0; i < dwCount; i++)
			{
				cmdAlertItem[i] = new AlertItem();
				cmdAlertItem[i].Init(ReadJsonItem("cmdAlertItem", i));
			}
		}

		public override string ToArray()
		{
			WriteData("cbCode", cbCode.ToString());
			WriteData("dwCount", dwCount.ToString());
			WriteItems("cmdAlertItem", cmdAlertItem);
			return base.ToArray();
		}

		public override string ToString()
		{
			string result = "AlertItemList:cbCode=" + cbCode + ",dwCount=" + dwCount + ",AlertItem={" + AndonUtility.GetItemsSeting(cmdAlertItem) + "}";
			return result;
		}

	}

	//警报单元
	public class AlertItem : StructBase
	{
		public uint wStatu;//处理状态 对应 CmdAndon.AlertStatu_XX

		public ushort wAlertType; //警报类型
		public byte cbAlterRank; //警报级别
		public string szAlertDescribe; //警报描述

		public uint dwCreaterWorkID;  //上传者工号
		public string szCreaterName;  //上传者名字
		public string szCreateTime; //上传时间

		public uint dwDealtorID;  //处理者工号
		public string szDealtorName;  //处理者名字
		public string szDealtTime;   //处理时间

		public uint dwCloserID;  //关警者工号
		public string szCloserName;  //关警者名字
		public string szCloserTime;   //关警时间

		public string szResolusion;   //解决方案
		public override void Init(JsonData content)
		{
			base.Init(content);
			wStatu = ReadUInt("wStatu");

			wAlertType = ReadUShort("wAlertType");
			cbAlterRank = ReadByte("cbAlterRank");
			szAlertDescribe = ReadString("szAlertDescribe");

			dwCreaterWorkID = ReadUInt("dwCreaterWorkID");
			szCreaterName = ReadString("szCreaterName");
			szCreateTime = ReadString("szCreateTime");

			dwDealtorID = ReadUInt("dwDealtorID");
			szDealtorName = ReadString("szDealtorName");
			szDealtTime = ReadString("szDealtTime");

			dwCloserID = ReadUInt("dwCloserID");
			szCloserName = ReadString("szCloserName");
			szCloserTime = ReadString("szCloserTime");

			szResolusion = ReadString("szResolusion");
		}

		public override string ToArray()
		{
			WriteData("wStatu", wStatu.ToString());

			WriteData("wAlertType", wAlertType.ToString());
			WriteData("cbAlterRank", cbAlterRank.ToString());
			WriteData("szAlertDescribe", szAlertDescribe.ToString());

			WriteData("dwCreaterWorkID", dwCreaterWorkID.ToString());
			WriteData("szCreaterName", szCreaterName.ToString());
			WriteData("szCreateTime", szCreateTime.ToString());

			WriteData("dwDealtorID", dwDealtorID.ToString());
			WriteData("szDealtorName", szDealtorName.ToString());
			WriteData("szDealtTime", szDealtTime.ToString());

			WriteData("dwCloserID", dwCloserID.ToString());
			WriteData("szCloserName", szCloserName.ToString());
			WriteData("szCloserTime", szCloserTime.ToString());

			WriteData("szResolusion", szResolusion.ToString());

			return base.ToArray();
		}

		public override string ToString()
		{
			string result = "[AlertItem:wStatu=" + wStatu + ",wAlertType=" + wAlertType + ",cbAlterRank=" + cbAlterRank + ",szAlertDescribe=" + szAlertDescribe + ",dwCreaterWorkID=" + dwCreaterWorkID + ",szCreaterName=" + szCreaterName + ",szCreateTime=" + szCreateTime + ",dwDealtorID=" + dwDealtorID + ",szDealtorName=" + szDealtorName + ",szDealtTime=" + szDealtTime + ",dwCloserID=" + dwCloserID + ",szCloserName=" + szCloserName + ",szCloserTime=" + szCloserTime + ",szResolusion=" + szResolusion + "]";
			return result;
		}

	}

	//////////////////////////////////////////////////////////////////////////////////////////

	//创建警报  -- 返回 Code 结果码 0成功  其他值映射失败原因
	public class CreateAlertReq : StructBase
	{
		public ushort wAlertType; //警报类型
		public byte cbAlterRank; //警报级别
		public string szAlertDescribe; //警报描述

		public override void Init(JsonData content)
		{
			base.Init(content);
			wAlertType = ReadUShort("wAlertType");
			cbAlterRank = ReadByte("cbAlterRank");
			szAlertDescribe = ReadString("szAlertDescribe");
		}

		public override string ToArray()
		{
			WriteData("wAlertType", wAlertType.ToString());
			WriteData("cbAlterRank", cbAlterRank.ToString());
			WriteData("szAlertDescribe", szAlertDescribe.ToString());
			return base.ToArray();
		}

		public override string ToString()
		{
			string result = "CreateAlertReq:wAlertType=" + wAlertType + ",cbAlterRank=" + cbAlterRank + ",szAlertDescribe=" + szAlertDescribe;
			return result;
		}


	}

	

	//升级警报  -- 返回 Code 结果码 0成功  其他值映射失败原因
	public class UpdateAlertReq : StructBase
	{
		public ushort wAlertType; //警报类型
		public uint dwAlertID;//警报编号

		public ushort wUpdateType; //升级类型
		public byte cbUpdateRank; //升级级别
		public string szUpdateDescribe; //升级描述

		public override void Init(JsonData content)
		{
			base.Init(content);
			wAlertType = ReadUShort("wAlertType");
			dwAlertID = ReadUInt("dwAlertID");

			wUpdateType = ReadUShort("wUpdateType");
			cbUpdateRank = ReadByte("cbUpdateRank");
			szUpdateDescribe = ReadString("szUpdateDescribe");
		}

		public override string ToArray()
		{
			WriteData("wAlertType", wAlertType.ToString());
			WriteData("dwAlertID", dwAlertID.ToString());
			WriteData("wUpdateType", wUpdateType.ToString());
			WriteData("cbUpdateRank", cbUpdateRank.ToString());
			WriteData("szUpdateDescribe", szUpdateDescribe.ToString());
			return base.ToArray();
		}

		public override string ToString()
		{
			string result = "UpdateAlertReq:wAlertType=" + wAlertType + ",dwAlertID=" + dwAlertID + ",wUpdateType=" + wUpdateType + ",cbUpdateRank=" + cbUpdateRank + ",szUpdateDescribe=" + szUpdateDescribe;
			return result;
		}

	}

	//接受警报  -- 返回 Code 结果码 0成功  其他值映射失败原因
	public class ReceiveAlertReq : StructBase
	{
		public ushort wAlertType; //警报类型
		public uint dwAlertID;//警报编号

		public override void Init(JsonData content)
		{
			base.Init(content);
			wAlertType = ReadUShort("wAlertType");
			dwAlertID = ReadUInt("dwAlertID");
		}

		public override string ToArray()
		{
			WriteData("wAlertType", wAlertType.ToString());
			WriteData("dwAlertID", dwAlertID.ToString());
			return base.ToArray();
		}

		public override string ToString()
		{
			string result = "ReceiveAlertReq:wAlertType=" + wAlertType + ",dwAlertID=" + dwAlertID;
			return result;
		}

	}

	//关闭警报  -- 返回 Code 结果码 0成功  其他值映射失败原因
	public class CloseAlertReq : StructBase
	{
		public ushort wAlertType; //警报类型
		public uint dwAlertID;//警报编号
		public string szResolusion;//解决方案

		public override void Init(JsonData content)
		{
			base.Init(content);
			wAlertType = ReadUShort("wAlertType");
			dwAlertID = ReadUInt("dwAlertID");
			szResolusion = ReadString("szResolusion");
		}

		public override string ToArray()
		{
			WriteData("wAlertType", wAlertType.ToString());
			WriteData("dwAlertID", dwAlertID.ToString());
			WriteData("szResolusion", szResolusion.ToString());
			return base.ToArray();
		}

		public override string ToString()
		{
			string result = "CloseAlertReq:wAlertType=" + wAlertType + ",dwAlertID=" + dwAlertID + ",szResolusion=" + szResolusion;
			return result;
		}

	}
	#endregion

	#region 当班系统
	//ReqUint
	//当班总计返回结果
	public class AllDutyListResult : StructBase
	{
		public byte cbCode;// 结果码 0成功  其他值映射失败原因
		public uint dwCount;//数量
		public AllDutyItem[] cmdAllDutyItem;//任务表

		public override void Init(JsonData content)
		{
			base.Init(content);
			cbCode = ReadByte("cbCode");
			dwCount = ReadUInt("dwCount");

			cmdAllDutyItem = new AllDutyItem[dwCount];
			for (int i = 0; i < dwCount; i++)
			{
				cmdAllDutyItem[i] = new AllDutyItem();
				cmdAllDutyItem[i].Init(ReadJsonItem("cmdAllDutyItem", i));
			}
		}

		public override string ToArray()
		{
			WriteData("cbCode", cbCode.ToString());
			WriteData("dwCount", dwCount.ToString());
			WriteItems("cmdAllDutyItem", cmdAllDutyItem);
			return base.ToArray();
		}

		public override string ToString()
		{
			string result = "AllDutyListResult:cbCode=" + cbCode + ",dwCount=" + dwCount + ",AlertItem={" + AndonUtility.GetItemsSeting(cmdAllDutyItem) + "}";
			return result;
		}

	}

	public class AllDutyItem : StructBase
	{
		public ushort wAlertType; //警报类型

		public uint dwAlertCount;//警报数量
		public uint dwDealtedCount;//完成数量

		public uint dwWaitingCount;//待处理数量
		public uint dwTimeoutWaitingCount;//等待超时数量

		public uint dwDealtingCount;//在处理数量
		public uint dwTimeoutDealtingCount;//处理超时数量

		public override void Init(JsonData content)
		{
			base.Init(content);
			wAlertType = ReadUShort("wAlertType");

			dwAlertCount = ReadUInt("dwAlertCount");
			dwDealtedCount = ReadUInt("dwDealtedCount");

			dwWaitingCount = ReadUInt("dwWaitingCount");
			dwTimeoutWaitingCount = ReadUInt("dwTimeoutWaitingCount");

			dwDealtingCount = ReadUInt("dwDealtingCount");
			dwTimeoutDealtingCount = ReadUInt("dwTimeoutDealtingCount");
		}

		public override string ToArray()
		{
			WriteData("wAlertType", wAlertType.ToString());

			WriteData("dwAlertCount", dwAlertCount.ToString());
			WriteData("dwDealtedCount", dwDealtedCount.ToString());

			WriteData("dwWaitingCount", dwWaitingCount.ToString());
			WriteData("dwTimeoutWaitingCount", dwTimeoutWaitingCount.ToString());

			WriteData("dwDealtingCount", dwDealtingCount.ToString());
			WriteData("dwTimeoutDealtingCount", dwTimeoutDealtingCount.ToString());
			return base.ToArray();
		}


		public override string ToString()
		{
			string result = "[AllDutyItem:wAlertType=" + wAlertType + ",dwAlertCount=" + dwAlertCount + ",dwDealtedCount=" + dwDealtedCount + ",dwWaitingCount=" + dwWaitingCount + ",dwTimeoutWaitingCount=" + dwTimeoutWaitingCount + ",dwDealtingCount=" + dwDealtingCount + ",dwTimeoutDealtingCount=" + dwTimeoutDealtingCount + "]";
			return result;
		}
	}

	//具体任务-警报 -- 请求警报单元表 AlertItemListReq
	#endregion

	#region 功能该系统
	#region 选择产线
	//ReqUint
	//产线信息返回结果
	public class ProductionLineListResult : StructBase
	{
		public byte cbCode;// 结果码 0成功  其他值映射失败原因
		public uint dwCount;//数量
		public ProductionLineItem[] cmdProductionLineItem;

		public override void Init(JsonData content)
		{
			base.Init(content);
			cbCode = ReadByte("cbCode");
			dwCount = ReadUInt("dwCount");

			cmdProductionLineItem = new ProductionLineItem[dwCount];
			for (int i = 0; i < dwCount; i++)
			{
				cmdProductionLineItem[i] = new ProductionLineItem();
				cmdProductionLineItem[i].Init(ReadJsonItem("cmdProductionLineItem", i));
			}
		}

		public override string ToArray()
		{
			WriteData("cbCode", cbCode.ToString());
			WriteData("dwCount", dwCount.ToString());
			WriteItems("cmdProductionLineItem", cmdProductionLineItem);
			return base.ToArray();
		}

		public override string ToString()
		{
			string result = "ProductionLineListResult:cbCode=" + cbCode + ",dwCount=" + dwCount + ",AlertItem={" + AndonUtility.GetItemsSeting(cmdProductionLineItem) + "}";
			return result;
		}

	}

	public class ProductionLineItem : StructBase
	{
		public uint dwProductionLineID; //产线编号
		public string szProductionLineName; //产线名称

		public override void Init(JsonData content)
		{
			base.Init(content);
			dwProductionLineID = ReadUInt("dwProductionLineID");
			szProductionLineName = ReadString("szProductionLineName");
		}

		public override string ToArray()
		{
			WriteData("dwProductionLineID", dwProductionLineID.ToString());
			WriteData("szProductionLineName", szProductionLineName.ToString());
			return base.ToArray();
		}

		public override string ToString()
		{
			string result = "[ProductionLineItem:dwProductionLineID=" + dwProductionLineID + ",szProductionLineName=" + szProductionLineName + "]";
			return result;
		}
	}

	//选择产线 -- 返回 Code 结果码 0成功  其他值映射失败原因
	public class ChooseProductionLineReq : StructBase
	{
		public uint dwWorkID;  //工号
		public uint dwProductionLineID; //产线编号

		public override void Init(JsonData content)
		{
			base.Init(content);
			dwWorkID = ReadUInt("dwWorkID");
			dwProductionLineID = ReadUInt("dwProductionLineID");
		}

		public override string ToArray()
		{
			WriteData("dwWorkID", dwWorkID.ToString());
			WriteData("dwProductionLineID", dwProductionLineID.ToString());
			return base.ToArray();
		}

		public override string ToString()
		{
			string result = "ChooseProductionLineReq:dwWorkID=" + dwWorkID + ",dwProductionLineID=" + dwProductionLineID;
			return result;
		}
	}
	#endregion

	#region 报警者设置
	//ReqUint
	//报警者信息返回结果
	public class AlertorInfoResult : StructBase
	{
		public byte cbCode;// 结果码 0成功  其他值映射失败原因
		public byte cbStatu; //结果码 (设置过/没设置过)--对应-- AlertorInfoStatu

		public string szName;     //名字
		public uint dwWorkID;     //工号
		public ushort wAlertType; //警报类型
		public uint dwWorkGroup;  //报警组
		public string szWatchID;  //手表ID
		public override void Init(JsonData content)
		{
			base.Init(content);
			cbCode = ReadByte("cbCode");
			cbStatu = ReadByte("cbStatu");

			szName = ReadString("szName");
			dwWorkID = ReadUInt("dwWorkID");
			wAlertType = ReadUShort("wAlertType");
			dwWorkGroup = ReadUInt("dwWorkGroup");
			szWatchID = ReadString("szWatchID");
		}

		public override string ToArray()
		{
			WriteData("cbCode", cbCode.ToString());
			WriteData("cbStatu", cbStatu.ToString());

			WriteData("szName", szName.ToString());
			WriteData("dwWorkID", dwWorkID.ToString());
			WriteData("wAlertType", wAlertType.ToString());
			WriteData("dwWorkGroup", dwWorkGroup.ToString());
			WriteData("szWatchID", szWatchID.ToString());

			return base.ToArray();
		}

		public override string ToString()
		{
			string result = "AlertorInfoResult:cbCode=" + cbCode + ",cbStatu=" + cbStatu + ",szName=" + szName + ",dwWorkID=" + dwWorkID + ",wAlertType=" + wAlertType + ",dwWorkGroup=" + dwWorkGroup + ",szWatchID=" + szWatchID;
			return result;
		}

	}

	//设置报警者信息  -- 返回 Code 结果码 0成功  其他值映射失败原因
	public class SetAlertorInfoReq : StructBase
	{
		public string szName;     //名字
		public uint dwWorkID;     //工号
		public ushort wAlertType; //警报类型
		public uint dwWorkGroup;  //报警组
		public string szWatchID;  //手表ID

		public override void Init(JsonData content)
		{
			base.Init(content);
			szName = ReadString("szName");
			dwWorkID = ReadUInt("dwWorkID");
			wAlertType = ReadUShort("wAlertType");
			dwWorkGroup = ReadUInt("dwWorkGroup");
			szWatchID = ReadString("szWatchID");
		}

		public override string ToArray()
		{
			WriteData("szName", szName.ToString());
			WriteData("dwWorkID", dwWorkID.ToString());
			WriteData("wAlertType", wAlertType.ToString());
			WriteData("dwWorkGroup", dwWorkGroup.ToString());
			WriteData("szWatchID", szWatchID.ToString());
			return base.ToArray();
		}

		public override string ToString()
		{
			string result = "SetAlertorInfoReq:szName=" + szName + ",dwWorkID=" + dwWorkID + ",wAlertType=" + wAlertType + ",dwWorkGroup=" + dwWorkGroup + ",szWatchID=" + szWatchID;
			return result;
		}
	}
	#endregion

	#region 报表信息
	//ReqUint
	//报表信息返回结果
	public class SumTableResult : StructBase
	{
		public byte cbCode;// 结果码 0成功  其他值映射失败原因
		public uint dwCount;//数量
		public SumTableItem[] cmdSumTableItem;

		public override void Init(JsonData content)
		{
			base.Init(content);
			cbCode = ReadByte("cbCode");
			dwCount = ReadUInt("dwCount");

			cmdSumTableItem = new SumTableItem[dwCount];
			for (int i = 0; i < dwCount; i++)
			{
				cmdSumTableItem[i] = new SumTableItem();
				cmdSumTableItem[i].Init(ReadJsonItem("cmdSumTableItem", i));
			}
		}

		public override string ToArray()
		{
			WriteData("cbCode", cbCode.ToString());
			WriteData("dwCount", dwCount.ToString());
			WriteItems("cmdSumTableItem", cmdSumTableItem);
			return base.ToArray();
		}

		public override string ToString()
		{
			string result = "SumTableResult:cbCode=" + cbCode + ",dwCount=" + dwCount + ",cmdSumTableItem={" + AndonUtility.GetItemsSeting(cmdSumTableItem) + "}";
			return result;
		}
	}

	public class SumTableItem : StructBase
	{
		public string szTableName;//表名字,eg:平均响应时间统计表
		public string szYDescribe;//Y描述,eg:平均响应时间
		public string szYUnit;//Y单位,eg:min

		public byte cbCount;//数量
		public string[] szXNames;//X名字,eg:质量,制程
		public string[] szXCounts;//X数量,eg:20,10

		public override void Init(JsonData content)
		{
			base.Init(content);
			szTableName = ReadString("szTableName");
			szYDescribe = ReadString("szYDescribe");
			szYUnit = ReadString("szYUnit");

			cbCount = ReadByte("cbCount");

			szXNames = ReadStrings("szXNames", cbCount);
			szXCounts = ReadStrings("szXCounts", cbCount);
		}

		public override string ToArray()
		{
			WriteData("szTableName", szTableName.ToString());
			WriteData("szYDescribe", szYDescribe.ToString());
			WriteData("szYUnit", szYUnit.ToString());

			WriteData("cbCount", cbCount.ToString());

			WriteStrings("szXNames", szXNames);
			WriteStrings("szXCounts", szXCounts);
			return base.ToArray();
		}

		public override string ToString()
		{
			string result = "[ProductionLineItem:szTableName=" + szTableName + ",szYDescribe=" + szYDescribe + ",szYUnit=" + szYUnit + ",cbCount=" + cbCount +
				",szXNames=" + AndonUtility.GetStrings(szXNames) + ",szXCounts=" + AndonUtility.GetStrings(szXCounts) + "]";
			return result;
		}
	}
	#endregion
	#endregion

}