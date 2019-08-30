using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DeadLine24.FileHelper
{

	class FileReader
	{
		public FileStream fs { get; private set; }
		public bool EOF { get; set; }

		private String readStringToWhitespace()
		{
			char tmp;
			StringBuilder buff = new StringBuilder();
			tmp = (char)fs.ReadByte();
			if (tmp == 0)
				return null;
			do
			{
				buff.Append(tmp);

				tmp = (char)fs.ReadByte();
			} while (tmp != 32 && tmp != '\n' && fs.Length != fs.Position);
			if (tmp == 13)
				fs.ReadByte();
			if (fs.Length == fs.Position)
				this.EOF = true;
			return buff.ToString();
		}

		public Double readDouble()
		{
			if (fs == null)
				throw new NullReferenceException();
			String inS = this.readStringToWhitespace();
			if (inS == null)
			{
				EOF = true;
				return 0;
			}
			inS = inS.Replace('.', ',');
			double inD = System.Convert.ToDouble(inS);
			return inD;
		}
		public String readLine()
		{
			if (fs == null)
				throw new NullReferenceException();
			char ch;
			StringBuilder buff = new StringBuilder();
			do
			{
				ch = (char)fs.ReadByte();
				buff.Append(ch);
			} while (ch != '\n' && fs.Length != fs.Position);
			if (ch == 13)
				fs.ReadByte();
			if (fs.Length == fs.Position)
				this.EOF = true;
			return buff.ToString();
		}

		public void openFile(String path)
		{
			fs = File.OpenRead(path);
			EOF = false;
		}
		public void closeFile()
		{
			if (fs != null)
				fs.Close();
		}
	}
}
