using System.Collections;
using System.Collections.Generic;

using LitJson;

namespace Schneider.Frame
{
    public class StructBase
    {
        JsonData ReceiveData;

        string cmdData = "";

        //序列化接口
        public virtual string ToArray()
        {
            cmdData = "{" + cmdData + "}";

            return cmdData;
        }
		 
        protected void WriteData(string key, string value)
        {
            //"key":"","",...
            if (!string.IsNullOrEmpty(cmdData)) cmdData += ",";
            cmdData += "\"" + key + "\":" + "\"" + value + "\"";
        }

        protected void WriteItems(string key, StructBase[] items)
        {
            //"key":[{...},{...},...]
            string value = "";

            for (int i = 0; i < items.Length; i++)
            {
                if (!string.IsNullOrEmpty(value)) value += ",";
                value += items[i].ToArray();
            }

            if (!string.IsNullOrEmpty(cmdData)) cmdData += ",";
            cmdData += "\"" + key + "\":" + "[" + value + "]";
        }

        protected void WriteStrings(string key,string[]values)
        {
            string value = "";

            for (int i = 0; i < values.Length; i++)
            {
                if (!string.IsNullOrEmpty(value)) value += ",";
                value += values[i];
            }

            if (!string.IsNullOrEmpty(cmdData)) cmdData += ",";
            cmdData += "\"" + key + "\":" + "\"" + value + "\"";
        }

        //解析接口
        public virtual void Init(string content)
        {
            ReceiveData = JsonMapper.ToObject(content);
        }

        public virtual void Init(JsonData jsd)
        {
            ReceiveData = jsd;
        }

        #region 读取子项
        protected byte ReadByte(string key)
        {
            return byte.Parse((string)ReceiveData[key]);
        }

        protected byte[] ReadBytes(string key)
        {
            string content = (string)ReceiveData[key];
            string[] strings = content.Split(',');
            byte[] bytes = new byte[strings.Length];

            for (int i = 0; i < content.Length; i++)
            {
                bytes[i] = byte.Parse(strings[i]);
            }
            return bytes;
        }

        protected int ReadInt(string key)
        {
            return int.Parse((string)ReceiveData[key]);
        }

        protected uint ReadUInt(string key)
        {
            return uint.Parse((string)ReceiveData[key]);
        }

        protected ushort ReadUShort(string key)
        {
            return ushort.Parse((string)ReceiveData[key]);
        }

        protected string ReadString(string key)
        {
            return (string)ReceiveData[key];
        }

        protected string [] ReadStrings(string key,int count)
        {
            string content = (string)ReceiveData[key];
            string[] strings = content.Split(',');

            return strings;
        }

        protected string ReadStringItem(string key, int index)
        {
            return (string)ReceiveData[key][index];
        }

        protected JsonData ReadJsonItem(string key, int index)
        {
            return (JsonData)ReceiveData[key][index];
        }

        //protected void ReadItems(string key, ref StructBase[] items)
        //{
        //    for (int i = 0; i < items.Length; i++)
        //    {
        //        items[i].Init((string)ReceiveData[key][i]);
        //    }
        //}
        #endregion

        //获取包头
        public JsonData GetCmdHead(string msg)
        {
            JsonData json = JsonMapper.ToObject(msg);
            return (JsonData)json["CmdHead"];
        }

        //获取数据
        public JsonData GetCmdData(string msg)
        {
            JsonData json = JsonMapper.ToObject(msg);
            return (JsonData)json["CmdData"];
        }

        public string GetSendData(string head,string data) {
            return "{\"CmdHead\":" + head + ",\"CmdData\":" + data + "}";
        }

        //public virtual string ToString()
        //{
        //    return "";
        //}
    }
}

