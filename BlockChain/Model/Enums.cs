using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.Model
{
    public enum PairTrade
    {
        USDT = 0, 
        BTC = 1, 
        ETH = 2,
        RMB = 3
    }

    public enum DataOpera
    {
        ADD = 0,
        Modefiy = 1,
        Delete = 2,
        Init = 3
    }
}
