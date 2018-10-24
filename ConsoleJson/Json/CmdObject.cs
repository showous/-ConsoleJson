using ConsoleJson;
using System;

namespace ConsoleJson.Json
{
	public class CmdObject
	{
		private CmdHead cmdHead;
		private CmdData cmdData;

		public CmdHead CmdHead { get => cmdHead; set => cmdHead = value; }
		public CmdData CmdData { get => cmdData; set => cmdData = value; }
	}
}
