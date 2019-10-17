using BlockChain.Helper;
using BlockChain.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.DAL
{
    public static class DataAccess
    {
        #region DataAccess
        static DataAccess()
        {
            string dbfile = Path.Combine(GenerialHelper.ExePath, "DBCoins.db");
            if (!File.Exists(dbfile))
                SQLiteConnection.CreateFile(dbfile);
            _sqlite = new SQLiteConnection("Data Source="+dbfile);            
        }
        #endregion //DataAccess 

        #region Fields
        private static SQLiteConnection _sqlite;
        #endregion //Fields

        #region Properties

        #endregion //Properties 

        #region Methond
        public static IEnumerable<TradeDetail> GetTradeDetails()
        {
            IEnumerable<TradeDetail> tradeDetails = new List<TradeDetail>();
            try
            {               
                if ((long)_sqlite.ExecuteScalar("select count(*) from CoinsDetails") <= 0) return tradeDetails;
                string sql = "SELECT CoinID,CoinName,BidPrice,BidVolume,InTotalAmo,AcutalVolume,DealType, 3 as Operation, TradeTime FROM CoinsDetails";
                tradeDetails = _sqlite.Query<TradeDetail>(sql);
            }
            catch (Exception ex)
            {
                Log.Error("获取交易明细出错", ex);                
            }
            return tradeDetails;
        }

        public static bool InsertTradeDetails(IEnumerable<TradeDetail> tradeDetails)
        {
            _sqlite.Open();
            SQLiteTransaction transaction = _sqlite.BeginTransaction();
            try
            {
                foreach (var trade in tradeDetails)
                {
                    switch (trade.Operation)
                    {
                        case DataOpera.ADD:
                            string sql = "INSERT INTO CoinsDetails(CoinName,BidPrice,BidVolume,InTotalAmo,acutalVolume,DealType,TradeTime) " +
                                "VALUES(@CoinName,@BidPrice,@BidVolume,@InTotalAmo,@acutalVolume,@DealType,@TradeTime)";
                            CommandDefinition command = new CommandDefinition(sql, new
                            {
                                trade.CoinName,
                                trade.BidPrice,
                                trade.BidVolume,
                                trade.InTotalAmo,
                                trade.AcutalVolume,
                                trade.DealType,
                                trade.TradeTime
                            }, transaction);
                            _sqlite.Execute(command);
                            break;
                        case DataOpera.Modefiy:
                            sql = "UPDATE CoinsDetails SET CoinName = @CoinName,BidPrice = @BidPrice,BidVolume = @BidVolume," +
                                "InTotalAmo = @InTotalAmo ,acutalVolume = @acutalVolume ,DealType = @DealType, TradeTime = @TradeTime WHERE CoinID = @CoinID";
                            command = new CommandDefinition(sql, new
                            {
                                trade.CoinName,
                                trade.BidPrice,
                                trade.BidVolume,
                                trade.InTotalAmo,
                                trade.AcutalVolume,
                                trade.DealType,
                                trade.TradeTime,
                                trade.CoinID
                            }, transaction);
                            _sqlite.Execute(command);
                            break;
                        case DataOpera.Delete:
                            sql  = "DELETE FROM CoinsDetails WHERE CoinID = @CoinID";
                            command = new CommandDefinition(sql, new
                            {
                                trade.CoinID
                            }, transaction);
                            _sqlite.Execute(command);
                            break;
                    }
                    trade.Operation = DataOpera.Init;
                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                Log.Error("更新表失败", ex);
                transaction.Rollback();
            }
            finally
            {
                _sqlite.Close();
            }
            return false;
        }
        #endregion //Methond 
    }
}
