using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DeadLine24.FileHelper;

namespace DeadLine24
{
	
  class Program2
	{
		public static List<List<bool>> graf_bool;

    static void Main(string[] args)
    {
			for (int xyz = 1; xyz <= 10; xyz++)
			{
				FileReader fIn = new FileReader();
				Helper.max = 0;
				Helper.reg = false;
				Helper.stack = new List<Point>();
				if (xyz < 10)
					fIn.openFile("tour0" + xyz + ".in");
				else
					fIn.openFile("tour" + xyz + ".in");
				double inDouble = fIn.readDouble();
				graf_bool = new List<List<bool>>();
				for (int i = 0; i < (int)inDouble; i++)
				{
					String line = fIn.readLine();
					graf_bool.Add(new List<bool>());
					foreach (char ch in line)
					{
						if (ch == '#')
							graf_bool[i].Add(false);
						else if (ch == '.')
							(graf_bool[i]).Add(true);
					}
				}
				Helper.DFS(graf_bool, 1, 1);


				FileWriter fOut = new FileWriter();
				fOut.openFile(fIn.fs.Name.Substring(0, fIn.fs.Name.LastIndexOf(".in")) + ".ans");
				fOut.writeString((Helper.max - 1).ToString());

				foreach (Point pt in Helper.stack)
				{
					fOut.writeEnter();
					fOut.writeString((pt.x+1).ToString() + " " + (pt.y+1).ToString());
				}
				fOut.closeFile();
				fIn.closeFile();
			}

    }
  }
	struct Point
	{
		public int x, y;
		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}
	class Helper
	{
		/// <summary>
		/// Wyszukiwanie wgłąb po sąsiadach
		/// </summary>
		/// <param name="graf"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="length_to"></param>
		/// <param name="droga"></param>
		public static void DFS(List<List<bool>> graf, int x, int y, List<List<int>> length_to, int droga)
		{
			int i=0;
			length_to[x][y]=droga;
			droga++;
			if ((x-1)>0 && graf[x - 1][y] == true && length_to[x - 1][y] == 0)
				DFS(graf, x-1, y, length_to, droga);

			if ((x + 1) <graf.Count && graf[x + 1][y] == true && length_to[x + 1][y] == 0)
				DFS(graf, x + 1, y, length_to, droga);

			if ((y - 1) > 0 && graf[x][y - 1] == true && length_to[x][y - 1] == 0)
				DFS(graf, x, y-1, length_to, droga);

			if ((y + 1) < graf[x].Count && graf[x][y + 1] == true && length_to[x][y + 1] == 0)
				DFS(graf, x, y+1, length_to, droga);

		}
		/// <summary>
		/// Wyszukiwanie wgłąb po sąsiadach z rejestrowaniem wstecz
		/// </summary>
		/// <param name="graf"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="length_to"></param>
		/// <param name="droga"></param>
		public static void DFS_register(List<List<bool>> graf, int x, int y, List<List<int>> length_to, int droga)
		{
			int i = 0;
			length_to[x][y] = droga;
			droga++;
			if (droga == max)
				reg = true;
			if ((x - 1) > 0 && graf[x - 1][y] == true && length_to[x - 1][y] == 0 && reg==false)
				DFS_register(graf, x - 1, y, length_to, droga);

			if ((x + 1) < graf.Count && graf[x + 1][y] == true && length_to[x + 1][y] == 0 && reg == false)
				DFS_register(graf, x + 1, y, length_to, droga);

			if ((y - 1) > 0 && graf[x][y - 1] == true && length_to[x][y - 1] == 0 && reg == false)
				DFS_register(graf, x, y - 1, length_to, droga);

			if ((y + 1) < graf[x].Count && graf[x][y + 1] == true && length_to[x][y + 1] == 0 && reg == false)
				DFS_register(graf, x, y + 1, length_to, droga);
			if (reg == true)
				stack.Add(new Point(x, y));

		}
		/// <summary>
		/// Maksymalna ścieżka
		/// </summary>
		public static int max = 0;
		public static bool reg = false;
		/// <summary>
		/// Ścieżka
		/// </summary>
		public static List<Point> stack = new List<Point>();
		public static void  DFS(List< List<bool> > graf, int x, int y)
		{

			List<List<int>> uzyto = new List<List<int>>(graf.Count);
			for (int i = 0; i < graf.Count; i++)
			{
				uzyto.Add(new List<int>());
				for (int j = 0; j < graf[i].Count; j++)
					uzyto[i].Add(0);
			}

			int startX = 0, startY = 0;
			for (int i = 0; i < graf.Count; i++)
			{
				for (int j = 0; j < graf[i].Count; j++)
					//wywolywanie dla kazdego wiezchołka
					if (graf[i][j] == true)
					{
						//czyszczenie dlugosci
						for (int k = 0; k < graf.Count; k++)
							for (int l = 0; l < graf[k].Count; l++)
								uzyto[k][l]=0;
						//obliczanie
						DFS(graf, i, j, uzyto, 1);
						//maksymalna sciezka
						int tmp = findMax(uzyto);
						if (max < tmp)
						{
							max = findMax(uzyto);
							startX = i;
							startY = j;
						}
					}
			}
			//zerowanie
			for (int k = 0; k < graf.Count; k++)
				for (int l = 0; l < graf[k].Count; l++)
					uzyto[k][l] = 0;
			//obliczanie z rejestrowaniem trasy
			DFS_register(graf, startX, startY, uzyto, 1);
			int _tmp = findMax(uzyto);

			//wyświetlanie
			for (int i = 0; i < graf.Count; i++)
			{
				for (int j = 0; j < graf[i].Count; j++)
					System.Console.Write(uzyto[i][j] + "\t");
				System.Console.WriteLine();
			}
			
		}
		/// <summary>
		/// Znajdowanie największej liczby w tablicy 2-wym
		/// </summary>
		/// <param name="lenght_to"></param>
		/// <returns></returns>
		static int findMax(List<List<int>> lenght_to)
		{
			int max = 0;
			for (int i = 0; i < lenght_to.Count; i++)
			{
				for (int j = 0; j < lenght_to[i].Count; j++)
					if (max < lenght_to[i][j])
						max = lenght_to[i][j];
			}
			return max;
		}
	}
}
