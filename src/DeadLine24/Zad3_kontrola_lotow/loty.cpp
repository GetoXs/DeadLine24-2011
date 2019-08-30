#include <iostream>
#include <fstream>
#include <cstdlib>
#include <string>
#include <map>
#include <climits>

using namespace std;

int main()
{
    string nazwaPliku = "loty";

    for(short file = 7; file <= 7; file++)
    {
        char* str = new char[255];
        sprintf(str,"%02d",file);
        string fileSufix = string(str) + string(".in");
        ifstream input(string(nazwaPliku+fileSufix).c_str(), ios_base::in);
        int n = 0;
        input >> n;
        cout << n << endl;
        unsigned long max = 0;
        unsigned long pos_min = 0, pos_max = 0;
        pos_min = ~pos_min;

        {
            unsigned long tab[n][2];

            for(int i = 0; i < n; i++)
            {
                input >> tab[i][0];
                input >> tab[i][1];
                if(pos_min > tab[i][0])
                    pos_min = tab[i][0];
                if(pos_max < tab[i][1])
                    pos_max = tab[i][1];
            }
            cout << "Liczenie od " << pos_min << " do " << pos_max << endl;
            unsigned long max_loc;
            int i;
			/***** d³uuugo ***/
            for(unsigned long pos = pos_min; pos <= pos_max; ++pos)
            {
                max_loc = 0;
                for(i = 0; i < n; ++i)
                {
                    if(pos >= tab[i][0] && pos <= tab[i][1])
                    {
                        ++max_loc;
                    }
                }
                if(max_loc > max)
                {
                    max = max_loc;
                    cout << max << " " << pos << endl;
                }
            }
			/***** d³uuugo_end ***/
            cout << string(nazwaPliku+fileSufix) << " " << max << endl << endl;

            fileSufix = string(str) + string(".ans");
            ofstream output(string(nazwaPliku+fileSufix).c_str(), ios_base::out);
            output << max << endl;
            output.close();
        }

        input.close();
        delete[] str;
    }

}
