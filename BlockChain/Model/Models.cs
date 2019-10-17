using BlockChain.DAL;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BlockChain.Model
{
    /// <summary>
    /// 数字货币集合
    /// </summary>
    public class Coins : NotificationObject
    {

        #region [Glyph]
        private ImageSource _Glyph;

        public ImageSource Glyph
        {
            get { return _Glyph; }
            set
            {
                _Glyph = value;
                RaisePropertyChanged(nameof(Glyph));
            }

        }
        #endregion

        #region [CoinName]
        private string _coinName;

        public string CoinName
        {
            get { return _coinName; }
            set
            {
                _coinName = value;
                RaisePropertyChanged(nameof(CoinName));
            }

        }
        #endregion
    }

    public class TradeDetail : NotificationObject
    {

        #region [CoinID]
        public int CoinID { get; set; }
        #endregion

        #region [CoinName]
        private string _coinname;

        public string CoinName
        {
            get { return _coinname; }
            set
            {
                _coinname = value;
                RaisePropertyChanged(nameof(CoinName));
            }
        }
        #endregion

        #region [BidPrice]
        private decimal _bidPrice;

        public decimal BidPrice
        {
            get { return _bidPrice; }
            set
            {
                _bidPrice = value;
                RaisePropertyChanged(nameof(BidPrice));
            }

        }
        #endregion

        #region [BidVolume]
        private decimal _bidVolume;

        public decimal BidVolume
        {
            get { return _bidVolume; }
            set
            {
                _bidVolume = value;
                RaisePropertyChanged(nameof(BidVolume));
            }

        }
        #endregion

        #region [InTotalAmo]
        private decimal _inTotalAmo;

        public decimal InTotalAmo
        {
            get { return _inTotalAmo; }
            set
            {
                _inTotalAmo = value;
                RaisePropertyChanged(nameof(InTotalAmo));
            }

        }
        #endregion


        #region [AcutalVolume]
        private double _acutalVoulme;

        public double AcutalVolume
        {
            get { return _acutalVoulme; }
            set
            {
                _acutalVoulme = value;
                RaisePropertyChanged(nameof(AcutalVolume));
            }

        }
        #endregion

        #region [DealType]
        private PairTrade _dealType;

        public PairTrade DealType
        {
            get { return _dealType; }
            set
            {
                _dealType = value;
                RaisePropertyChanged(nameof(DealType));
            }

        }
        #endregion

        #region [Operation]
        private DataOpera _operation;

        public DataOpera Operation
        {
            get { return _operation; }
            set
            {
                _operation = value;
                RaisePropertyChanged(nameof(Operation));
            }

        }
        #endregion

        #region [TradeTime]
        private int _tradeTime;

        public int TradeTime
        {
            get { return _tradeTime; }
            set
            {
                _tradeTime = value;
                RaisePropertyChanged(nameof(TradeTime));
            }

        }
        #endregion

        #region Command

        #region [KeyDownCommand]
        public ICommand KeyDownCommand
        {
            get
            {
                return new DelegateCommand<KeyEventArgs>((e) =>
                {
                    if (e.KeyStates == Keyboard.GetKeyStates(Key.D) & Keyboard.Modifiers == ModifierKeys.Alt)
                    {
                        Operation = DataOpera.Delete;                        
                    }
                    else if (e.KeyStates == Keyboard.GetKeyStates(Key.M) & Keyboard.Modifiers == ModifierKeys.Alt)
                    {
                        Operation = DataOpera.Modefiy;
                    }
                    
                });
            }
        }
        #endregion
        #endregion //Command 

    }

    public class TradeContext : NotificationObject
    {
        #region Construnction
        public TradeContext()
        {
            
        }
        #endregion //Construnction 

        #region Properties
        #region [CoinName]
        private string _coinName;

        public string CoinName
        {
            get { return _coinName; }
            set
            {
                _coinName = value;
                RaisePropertyChanged(nameof(CoinName));
            }

        }
        #endregion

        #region [TradeDetails]
        private ObservableCollection<TradeDetail> _tradeDetails = new ObservableCollection<TradeDetail>();

        public ObservableCollection<TradeDetail> TradeDetails
        {
            get { return _tradeDetails; }
            set
            {
                _tradeDetails = value;
                RaisePropertyChanged(nameof(TradeDetails));
            }

        }
        #endregion

        #region [TradeDetail]
        private TradeDetail _tradeDetail = new TradeDetail();

        public TradeDetail TradeDetail
        {
            get { return _tradeDetail; }
            set
            {
                _tradeDetail = value;
                RaisePropertyChanged(nameof(TradeDetail));
            }

        }
        #endregion

        #region [FlyIsOpen]
        private bool _flyIsOpen = true;

        public bool FlyIsOpen
        {
            get { return _flyIsOpen; }
            set
            {
                _flyIsOpen = value;
                RaisePropertyChanged(nameof(FlyIsOpen));
            }

        }
        #endregion

        #region [MsgTip]
        private string _msgTip;

        public string MsgTip
        {
            get { return _msgTip; }
            set
            {
                _msgTip = value;
                RaisePropertyChanged(nameof(MsgTip));
            }

        }
        #endregion


        #endregion //Properties 

        #region Command

        #region [GotFocusCommand]
        public ICommand GotFocusCommand
        {
            get
            {
                return new DelegateCommand<TextBox>((tb) =>
                {
                    TradeDetail = tb.DataContext as TradeDetail;
                });
            }
        }

        #region [KeyDownCommand]
        public ICommand KeyDownCommand
        {
            get
            {
                return new DelegateCommand<KeyEventArgs>((e) =>
                {
                    if (e.KeyStates == Keyboard.GetKeyStates(Key.X) & Keyboard.Modifiers == ModifierKeys.Alt)
                    {
                        TradeDetail detail = new TradeDetail();
                        detail.CoinName = CoinName;
                        detail.Operation = DataOpera.ADD;
                        TradeDetails.Add(detail);
                        MsgTip = string.Empty;
                    }
                    else if (e.KeyStates == Keyboard.GetKeyStates(Key.O) & Keyboard.Modifiers == ModifierKeys.Alt)
                    {
                        FlyIsOpen = !FlyIsOpen;
                    }
                    else if (e.KeyStates == Keyboard.GetKeyStates(Key.S) & Keyboard.Modifiers == ModifierKeys.Alt)
                    {
                        DataAccess.InsertTradeDetails(TradeDetails);
                        MsgTip = "保存成功！";
                    }                    
                });
            }
        }
        #endregion

        #region [MouseLeftButtonDownCommand]
        public ICommand MouseLeftButtonDownCommand
        {
            get
            {
                return new DelegateCommand<WrapPanel>((wp) =>
                {
                    wp.Focus();
                });
            }
        }
        #endregion


        #endregion

        #endregion //Command 

        #region Methond

        #endregion //Methond 
    }
}
