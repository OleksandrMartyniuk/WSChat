using AuthServer;
using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class XO: IGame
    {
        string client1Name;
        string client2Name;
        string turn;
        string[] combinations = new string[8];
        string[] matrix = new string[] { "", "", "", "", "", "", "", "", "" };
       
        public string Result{ get; set; }

        public XO(string client1Name, string client2Name)
        {
            this.client1Name = client1Name;
            this.client2Name = client2Name;
            turn = client1Name;
          
        }

        public bool IsTurn(string name)
        {
            if (name == turn)
                return true;

            return false;
        }
        public RequestObject Move(object info)
        {
            object[] args = JsonConvert.DeserializeObject<object[]>(info.ToString());
            
            if (turn == args[3].ToString())
            {
                int x = Convert.ToInt32(args[2]);
                if (matrix[x] == "")
                {
                    string tmp = args[1].ToString();
                    matrix[x] = tmp;
                    if (args[3].ToString() == client1Name)
                        turn = client2Name;
                    else
                        turn = client1Name;

                    LogProvider.AppendRecord(string.Format("user [{0}] - {1} [{2}]", turn, tmp, x.ToString()));
                    return new RequestObject("Game", "Move", new object[] { tmp, x.ToString() });
                }
            }
            return null;
        }
        
        public bool IsOver()
        {
            combinations[0] = (matrix[0] + matrix[1] + matrix[2]);
            combinations[1] = (matrix[3] + matrix[4] + matrix[5]);
            combinations[2] = (matrix[6] + matrix[7] + matrix[8]);
            combinations[3] = (matrix[0] + matrix[3] + matrix[6]);
            combinations[4] = (matrix[1] + matrix[4] + matrix[7]);
            combinations[5] = (matrix[2] + matrix[5] + matrix[8]);
            combinations[6] = (matrix[0] + matrix[4] + matrix[8]);
            combinations[7] = (matrix[2] + matrix[4] + matrix[6]);

            bool draw = true;

            for (int i = 0; i < combinations.Length; i++)
            {
                if (combinations[i].Length < 3)
                {
                    draw = false;
                    continue;
                }
                if (combinations[i].All(ch => { return ch == 'X'; })
                   || combinations[i].All(ch => { return ch == 'O'; }))
                {
                    Result = combinations[i][0] + " win!";
                    return true;
                }
            }

            if (draw)
            {
                Result = "Draw!";
            }
            return draw;
        }
    }
}
