using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeadLine24.FileHelper;

namespace DeadLine24
{
	struct Point
	{
		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public int x;
		public int y;
	}
	class Program4
	{
		static void Main(string[] args)
		{
			int i = 0;

			FileReader fIn = new FileReader();
				fIn.openFile("wyc10.in");
				int cap = (int)fIn.readDouble();
				List<int> tab = new List<int>(2 * cap);
				List<Point> tabOut = new List<Point>(2 * cap);
				for (i = 0; i < cap; i++)
				{
					tab.Add((int)fIn.readDouble());
				}

				Point wyn;
				int max;
				for (i = 0; i < cap; i++)
				{
					max = i;
					for (int j = i + 1; j < cap; j++)
					{
						if (tab[j] > tab[i])
						{
							if (j < i)
								wyn = new Point(j + 1, i + 1);
							else
								wyn = new Point(i + 1, j + 1);
							tabOut.Add(wyn);
							break;
						}
					}
					if (i % 1000 == 0)
						System.Console.WriteLine(i.ToString());
				}

				for (i = cap - 1; i >= 0; i--)
				{
					max = i;
					for (int j = i - 1; j >= 0; j--)
					{
						if (tab[j] > tab[i])
						{
							if (j < i)
								wyn = new Point(j + 1, i + 1);
							else
								wyn = new Point(i + 1, j + 1);
							tabOut.Add(wyn);
							break;
						}
					}
					if (i % 1000 == 0)
						System.Console.WriteLine(i.ToString());
				}

				tabOut.Sort(new SortOut());
				FileWriter fOut = new FileWriter();
				fOut.openFile(fIn.fs.Name.Substring(0, fIn.fs.Name.LastIndexOf(".in")) + ".ans");
				fOut.writeString(tabOut.Count.ToString());
				i = 0;
				foreach (Point p in tabOut)
				{
					if (i++ > 99)
						break;
					fOut.writeEnter();
					fOut.writeString(p.x.ToString() + " " + p.y.ToString());

				}
				fOut.closeFile();
				fIn.closeFile();
		}
	}
	class SortOut : IComparer<Point>
	{
		public int Compare(Point x, Point y)
		{
			if (x.x < y.x)
			{
				return -1;
			}
			else if (x.x > y.x)
				return 1;
			else
				if (x.y < y.y)
				{
					return -1;
				}
				else if (x.y > y.y)
					return 1;
				else
					return 0;
		}
	}
}
