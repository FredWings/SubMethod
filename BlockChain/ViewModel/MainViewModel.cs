using BlockChain.DAL;
using BlockChain.Helper;
using BlockChain.Model;
using MahApps.Metro.Controls;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace BlockChain.ViewModel
{
    public class MainViewModel : NotificationObject
    {
        #region Construction
        public MainViewModel()
        {
            LoadCoins();
            AllTradeDetails = new List<TradeDetail>(DataAccess.GetTradeDetails());
        }
        #endregion //Construction 

        #region Fields
        
        #endregion //Fields 

        #region Properties

        #region [AllCoins]
        private ObservableCollection<Coins> _allCoins = new ObservableCollection<Coins>();

        public ObservableCollection<Coins> AllCoins
        {
            get { return _allCoins; }
            set
            {
                _allCoins = value;
                RaisePropertyChanged(nameof(AllCoins));
            }
        }
        #endregion

        #region [HamContent]
        public TradeContext HamContent { get; set; } = new TradeContext();
        #endregion

        #region [AllTradeDetails]
        public List<TradeDetail> AllTradeDetails { get; set; }
        #endregion

        #endregion //Properties 

        #region Command

        #region [ItemClickCommand]
        public ICommand ItemClickCommand
        {
            get
            {
                return new DelegateCommand<ItemClickEventArgs>((e) =>
                {
                    try
                    {
                        Coins coins = e.ClickedItem as Coins;
                        if (coins == null) return;
                        HamContent.CoinName = coins.CoinName;
                        HamContent.TradeDetails.Clear();

                        //虚拟数据
                        AllTradeDetails.Where(x => x.CoinName == coins.CoinName).ToList().ForEach(x =>
                        {
                            HamContent.TradeDetails.Add(x);
                        });
                        HamContent.TradeDetails.OrderByDescending(x => x.TradeTime);
                    }
                    catch (Exception ex)
                    {
                        Log.Error("展示数据出错!", ex);
                    }
                });
            }
        }

        #endregion

        #endregion //Command 

        #region Public Method
        /// <summary>
        /// 加载数字币
        /// </summary>
        private void LoadCoins()
        {
            AllCoins.Clear();
            DirectoryInfo directory = new DirectoryInfo(Path.Combine(GenerialHelper.ExePath, "Images"));
            var pics = directory.GetFiles("*.png", SearchOption.TopDirectoryOnly);
            foreach (var pic in pics)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = File.OpenRead(pic.FullName);
                bitmap.EndInit();
                AllCoins.Add(new Coins() { Glyph = bitmap, CoinName = Path.GetFileNameWithoutExtension(pic.Name).ToUpper() });
            }
        }
        #endregion //Public Method 

        #region Private Method

        #endregion //Private Method 
    }
}
