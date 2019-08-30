using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DeadLine24.FileHelper
{
	class FileWriter
	{
		public FileStream fs { get; private set; }
		public void writeString(String str)
		{
			if (fs == null)
				return;
			List<byte> lst = new List<byte>();
			foreach (char tmp in str.ToCharArray())
				lst.Add((byte)tmp);
			fs.Write(lst.ToArray(), 0, lst.Count);
		}
		public void writeEnter()
		{
			if (fs == null)
				return;
			fs.WriteByte((byte)'\n');
		}
		public void openFile(String fileName)
		{
			fs = File.OpenWrite(fileName);
		}
		public void closeFile()
		{
			if (fs != null)
				fs.Close();
		}
	}
}
