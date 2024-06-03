using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using CorpManagement.DB;
using CorpManagement.Model;
using CorpManagement.ToolBox;

namespace CorpManagement.ViewModel
{
    class AllArticleViewModel : ObservableObject
    {
        private readonly RequestDBArticle RA;
        private readonly RequestDBOrderArticle ROA;
        private readonly RequestDBUser RU;
        private readonly CheckerForFolder checker;
        private readonly CheckRightAccess checkAccess;
        private readonly DispatcherTimer dtResetMessage;
        private static string method = string.Empty;
        private static int line = 0;

        private static void ShowDebugInfo()
        {
            if (Debugger.IsAttached)
            {
                StackFrame stackFrame = new StackFrame(1, true);
                method = stackFrame.GetMethod().ToString();
                line = stackFrame.GetFileLineNumber();
            }
        }

        #region ObservableCollection
        
        #endregion

        #region SelectedItem
        private DetailViewModel<ArticleModel> _SelectedArticleItem;

        public DetailViewModel<ArticleModel> SelectedArticleItem
        {
            get
            {
                return _SelectedArticleItem;
            }
            set
            {
                if (value != _SelectedArticleItem)
                {
                    _SelectedArticleItem = value;
                    if (value != null)
                    {
                        SelectedPriceItem = value.Detail.price;
                        SelectedAmountItem = value.Detail.amount;
                        SelectedMaxQuantityItem = value.Detail.maxquantity;
                        SelectedCreditItem = value.Detail.credit;
                        SelectedDescriptionItem = value.Detail.description;
                    }
                    else
                    {
                        SelectedPriceItem = 0.00f;
                        SelectedAmountItem = 0;
                        SelectedMaxQuantityItem = 0;
                        SelectedCreditItem = 0;
                        SelectedDescriptionItem = string.Empty;
                    }
                    RaisePropertyChanged(() => SelectedArticleItem);
                }
            }
        }

        private string _SelectedArticleNameItem;

        public string SelectedArticleNameItem
        {
            get
            {
                return _SelectedArticleNameItem;
            }
            set
            {
                if (value != _SelectedArticleNameItem)
                {
                    _SelectedArticleNameItem = value;
                    RaisePropertyChanged(() => SelectedArticleNameItem);
                }
            }
        }

        private string _SelectedRefArticleItem;

        public string SelectedRefArticleItem
        {
            get
            {
                return _SelectedRefArticleItem;
            }
            set
            {
                if (value != _SelectedRefArticleItem)
                {
                    _SelectedRefArticleItem = value;
                    RaisePropertyChanged(() => SelectedRefArticleItem);
                }
            }
        }

        private DetailViewModel<ProviderModel> _SelectedIdProviderItem;

        public DetailViewModel<ProviderModel> SelectedIdProviderItem
        {
            get
            {
                return _SelectedIdProviderItem;
            }
            set
            {
                if (value != _SelectedIdProviderItem)
                {
                    _SelectedIdProviderItem = value;
                    RaisePropertyChanged(() => SelectedIdProviderItem);
                }
            }
        }

        private DetailViewModel<ArticleTypeModel> _SelectedIdTypeItem;

        public DetailViewModel<ArticleTypeModel> SelectedIdTypeItem
        {
            get
            {
                return _SelectedIdTypeItem;
            }
            set
            {
                if (value != _SelectedIdTypeItem)
                {
                    _SelectedIdTypeItem = value;
                    RaisePropertyChanged(() => SelectedIdTypeItem);
                }
            }
        }

        private DetailViewModel<SizeClothingModel> _SelectedSizeClothingItem;

        public DetailViewModel<SizeClothingModel> SelectedSizeClothingItem
        {
            get
            {
                return _SelectedSizeClothingItem;
            }
            set
            {
                if (value != _SelectedSizeClothingItem)
                {
                    _SelectedSizeClothingItem = value;
                    RaisePropertyChanged(() => SelectedSizeClothingItem);
                }
            }
        }

        private float _SelectedPriceItem;

        public float SelectedPriceItem
        {
            get
            {
                return _SelectedPriceItem;
            }
            set
            {
                if (value != _SelectedPriceItem)
                {
                    _SelectedPriceItem = value;
                    RaisePropertyChanged(() => SelectedPriceItem);
                }
            }
        }

        private int _SelectedAmountItem;

        public int SelectedAmountItem
        {
            get
            {
                return _SelectedAmountItem;
            }
            set
            {
                if (value != _SelectedAmountItem)
                {
                    _SelectedAmountItem = value;
                    RaisePropertyChanged(() => SelectedAmountItem);
                }
            }
        }

        private int _SelectedMaxQuantityItem;

        public int SelectedMaxQuantityItem
        {
            get
            {
                return _SelectedMaxQuantityItem;
            }
            set
            {
                if (value != _SelectedMaxQuantityItem)
                {
                    _SelectedMaxQuantityItem = value;
                    RaisePropertyChanged(() => SelectedMaxQuantityItem);
                }
            }
        }

        private int _SelectedCreditItem;

        public int SelectedCreditItem
        {
            get
            {
                return _SelectedCreditItem;
            }
            set
            {
                if (value != _SelectedCreditItem)
                {
                    _SelectedCreditItem = value;
                    RaisePropertyChanged(() => SelectedCreditItem);
                }
            }
        }

        private string _SelectedDescriptionItem;

        public string SelectedDescriptionItem
        {
            get
            {
                return _SelectedDescriptionItem;
            }
            set
            {
                if (value != _SelectedDescriptionItem)
                {
                    _SelectedDescriptionItem = value;
                    RaisePropertyChanged(() => SelectedDescriptionItem);
                }
            }
        }

        private DetailViewModel<OrderArticleModel> _SelectedOrderArticleItem;

        public DetailViewModel<OrderArticleModel> SelectedOrderArticleItem
        {
            get
            {
                return _SelectedOrderArticleItem;
            }
            set
            {
                if (value != _SelectedOrderArticleItem)
                {
                    _SelectedOrderArticleItem = value;
                    if (value != null)
                    {
                        SelectedOrderAmountItem = value.Detail.amount;
                        SelectedOrderDateItem = value.Detail.orderdate;
                        SelectedDateReceivedItem = value.Detail.datereceived;
                        SelectedDescriptionOrderItem = value.Detail.description;
                    }
                    else
                    {
                        SelectedOrderAmountItem = 0;
                        SelectedOrderDateItem = DateTime.Now;
                        SelectedDateReceivedItem = DateTime.Now;
                        SelectedDescriptionOrderItem = string.Empty;
                    }
                    RaisePropertyChanged(() => SelectedOrderArticleItem);
                }
            }
        }

        private DetailViewModel<ArticleModel> _SelectedArticleForOrderItem;

        public DetailViewModel<ArticleModel> SelectedArticleForOrderItem
        {
            get
            {
                return _SelectedArticleForOrderItem;
            }
            set
            {
                if (value != _SelectedArticleForOrderItem)
                {
                    _SelectedArticleForOrderItem = value;
                    RaisePropertyChanged(() => SelectedArticleForOrderItem);
                }
            }
        }

        private DetailViewModel<UserModel> _SelectedUserForOrderItem;

        public DetailViewModel<UserModel> SelectedUserForOrderItem
        {
            get
            {
                return _SelectedUserForOrderItem;
            }
            set
            {
                if (value != _SelectedUserForOrderItem)
                {
                    _SelectedUserForOrderItem = value;
                    RaisePropertyChanged(() => SelectedUserForOrderItem);
                }
            }
        }

        private int _SelectedOrderAmountItem;

        public int SelectedOrderAmountItem
        {
            get
            {
                return _SelectedOrderAmountItem;
            }
            set
            {
                if (value != _SelectedOrderAmountItem)
                {
                    _SelectedOrderAmountItem = value;
                    RaisePropertyChanged(() => SelectedOrderAmountItem);
                }
            }
        }

        private DateTime _SelectedOrderDateItem;

        public DateTime SelectedOrderDateItem
        {
            get
            {
                if (_SelectedOrderDateItem.Year == 1)
                    _SelectedOrderDateItem = DateTime.Now;
                return _SelectedOrderDateItem;
            }
            set
            {
                if (value != _SelectedOrderDateItem)
                {
                    _SelectedOrderDateItem = value;
                    RaisePropertyChanged(() => SelectedOrderDateItem);
                }
            }
        }

        private DateTime _SelectedDateReceivedItem;

        public DateTime SelectedDateReceivedItem
        {
            get
            {
                if (_SelectedDateReceivedItem.Year == 1)
                    _SelectedDateReceivedItem = DateTime.Now;
                return _SelectedDateReceivedItem;
            }
            set
            {
                if (value != _SelectedDateReceivedItem)
                {
                    _SelectedDateReceivedItem = value;
                    RaisePropertyChanged(() => SelectedDateReceivedItem);
                }
            }
        }

        private DetailViewModel<OrderStatusModel> _SelectedOrderStatusItem;

        public DetailViewModel<OrderStatusModel> SelectedOrderStatusItem
        {
            get
            {
                return _SelectedOrderStatusItem;
            }
            set
            {
                if (value != _SelectedOrderStatusItem)
                {
                    _SelectedOrderStatusItem = value;
                    RaisePropertyChanged(() => SelectedOrderStatusItem);
                }
            }
        }

        private DetailViewModel<ArticleTypeModel> _SelectedArticleTypeForOrderItem;

        public DetailViewModel<ArticleTypeModel> SelectedArticleTypeForOrderItem
        {
            get
            {
                return _SelectedArticleTypeForOrderItem;
            }
            set
            {
                if (value != _SelectedArticleTypeForOrderItem)
                {
                    _SelectedArticleTypeForOrderItem = value;
                    ReloadAllOrderArticle();
                    RaisePropertyChanged(() => SelectedArticleTypeForOrderItem);
                }
            }
        }

        private string _SelectedDescriptionOrderItem;

        public string SelectedDescriptionOrderItem
        {
            get
            {
                return _SelectedDescriptionOrderItem;
            }
            set
            {
                if (value != _SelectedDescriptionOrderItem)
                {
                    _SelectedDescriptionOrderItem = value;
                    RaisePropertyChanged(() => SelectedDescriptionOrderItem);
                }
            }
        }

        private string _WarningPointUser;

        public string WarningPointUser
        {
            get
            {
                return _WarningPointUser;
            }
            set
            {
                if (value != _WarningPointUser)
                {
                    _WarningPointUser = value;
                    RaisePropertyChanged(() => WarningPointUser);
                }
            }
        }

        private string _MessageStatusRequestToDB;

        public string MessageStatusRequestToDB
        {
            get
            {
                return _MessageStatusRequestToDB;
            }
            set
            {
                if (value != _MessageStatusRequestToDB)
                {
                    _MessageStatusRequestToDB = value;
                    RaisePropertyChanged(() => MessageStatusRequestToDB);
                }
            }
        }
        #endregion

        /// <summary>
        /// Constructor AllArticle
        /// </summary>
        public AllArticleViewModel()
        {
            // Initialize the request DB
            RA = new RequestDBArticle();
            ROA = new RequestDBOrderArticle();
            RU = new RequestDBUser();

            // Initialize other class checker
            checker = new CheckerForFolder();
            checkAccess = new CheckRightAccess();

            // Initialize timer for the messages in the window
            dtResetMessage = new DispatcherTimer();
        }

        /// <summary>
        /// Return the selected article
        /// </summary>
        /// <returns></returns>
        public Article GetSelectedArticle()
        {
            return (SelectedArticleItem != null) ? new Article(SelectedArticleItem.Detail.id, SelectedArticleItem.Detail.name, SelectedArticleItem.Detail.refArticle, SelectedArticleItem.Detail.idprovider, SelectedArticleItem.Detail.price, SelectedArticleItem.Detail.amount, SelectedArticleItem.Detail.maxquantity, SelectedArticleItem.Detail.idtype, SelectedArticleItem.Detail.description, SelectedArticleItem.Detail.credit) : null;
        }

        /// <summary>
        /// Return the selected idprovider
        /// </summary>
        /// <returns></returns>
        public Provider GetSelectedIdProvider()
        {
            return (SelectedIdProviderItem != null) ? new Provider(SelectedIdProviderItem.Detail.id, SelectedIdProviderItem.Detail.name) : null;
        }

        /// <summary>
        /// Return the selected idtype
        /// </summary>
        /// <returns></returns>
        public ArticleType GetSelectedIdType()
        {
            return (SelectedIdTypeItem != null) ? new ArticleType(SelectedIdTypeItem.Detail.id, SelectedIdTypeItem.Detail.name) : null;
        }

        /// <summary>
        /// Return the selected SizeClothing
        /// </summary>
        /// <returns></returns>
        public SizeClothing GetSelectedSizeClothing()
        {
            return (SelectedSizeClothingItem != null) ? new SizeClothing(SelectedSizeClothingItem.Detail.id, SelectedSizeClothingItem.Detail.size) : null;
        }

        /// <summary>
        /// Return the selected order article (in datagrid)
        /// </summary>
        /// <returns></returns>
        public OrderArticle GetSelectedOrderArticle()
        {
            return (SelectedOrderArticleItem != null) ? new OrderArticle(SelectedOrderArticleItem.Detail.idorder, SelectedOrderArticleItem.Detail.iduser, SelectedOrderArticleItem.Detail.idarticle, SelectedOrderArticleItem.Detail.amount, SelectedOrderArticleItem.Detail.orderdate, SelectedOrderArticleItem.Detail.datereceived, SelectedOrderArticleItem.Detail.status, SelectedOrderArticleItem.Detail.description) : null;
        }

        /// <summary>
        /// Return the selected article for order
        /// </summary>
        /// <returns></returns>
        public Article GetSelectedArticleForOrder()
        {
            return (SelectedArticleForOrderItem != null) ? new Article(SelectedArticleForOrderItem.Detail.id, SelectedArticleForOrderItem.Detail.name, SelectedArticleForOrderItem.Detail.refArticle, SelectedArticleForOrderItem.Detail.idprovider, SelectedArticleForOrderItem.Detail.price, SelectedArticleForOrderItem.Detail.amount, SelectedArticleForOrderItem.Detail.maxquantity, SelectedArticleForOrderItem.Detail.idtype, SelectedArticleForOrderItem.Detail.description, SelectedArticleForOrderItem.Detail.credit) : null;
        }

        /// <summary>
        /// Return the selected order status
        /// </summary>
        /// <returns></returns>
        public OrderStatus GetSelectedOrderStatus()
        {
            return (SelectedOrderStatusItem != null) ? new OrderStatus(SelectedOrderStatusItem.Detail.id, SelectedOrderStatusItem.Detail.name) : null;
        }

        /// <summary>
        /// Return the selected user for order
        /// </summary>
        /// <returns></returns>
        public User GetSelectedUserForOrder()
        {
            return (SelectedUserForOrderItem != null) ? new User(SelectedUserForOrderItem.Detail.id, SelectedUserForOrderItem.Detail.lastname, SelectedUserForOrderItem.Detail.firstname, SelectedUserForOrderItem.Detail.matricule, SelectedUserForOrderItem.Detail.idprofilelevel, SelectedUserForOrderItem.Detail.pointarticle, SelectedUserForOrderItem.Detail.gradepoint, SelectedUserForOrderItem.Detail.status) : null;
        }

        /// <summary>
        /// Return the selected type article for order
        /// </summary>
        /// <returns></returns>
        public ArticleType GetSelectedTypeArticleOrder()
        {
            return (SelectedArticleTypeForOrderItem != null) ? new ArticleType(SelectedArticleTypeForOrderItem.Detail.id, SelectedArticleTypeForOrderItem.Detail.name) : null;
        }

        /// <summary>
        /// Clean the different data information
        /// </summary>
        public void _CleanData()
        {
            SelectedArticleItem = null;
            SelectedArticleNameItem = string.Empty;
            SelectedRefArticleItem = string.Empty;
            SelectedIdProviderItem = null;
            SelectedIdTypeItem = null;
            SelectedPriceItem = 0.00f;
            SelectedAmountItem = 0;
            SelectedMaxQuantityItem = 0;
            SelectedCreditItem = 0;
            SelectedDescriptionItem = string.Empty;
        }

        /// <summary>
        /// Clean the different data information
        /// </summary>
        public void _CleanDataOrder()
        {
            SelectedArticleForOrderItem = null;
            SelectedUserForOrderItem = null;
            SelectedOrderAmountItem = 0;
            SelectedOrderDateItem = DateTime.Now;
            SelectedDateReceivedItem = DateTime.Now;
            SelectedOrderStatusItem = null;
            SelectedSizeClothingItem = null;
            SelectedDescriptionOrderItem = string.Empty;
        }

        /// <summary>
        /// Reload all tire in observableCollection Article
        /// </summary>
        public void ReloadAllArticle(bool OpenLastArticleEncoded = false)
        {
            MainCollectionViewModel._Article.Clear();
            MainCollectionViewModel._ArticleForOrder.Clear();
            MainCollectionViewModel._BaseArticle.Clear();

            foreach (var article in RA.SelectAllElement())
            {
                MainCollectionViewModel._Article.Add(new DetailViewModel<ArticleModel>(new ArticleModel(article)));
                if (SelectedArticleTypeForOrderItem != null)
                {
                    if (SelectedArticleTypeForOrderItem.Detail.id == article.idtype.id)
                        MainCollectionViewModel._ArticleForOrder.Add(new DetailViewModel<ArticleModel>(new ArticleModel(article)));
                }
                else
                {
                    MainCollectionViewModel._ArticleForOrder.Add(new DetailViewModel<ArticleModel>(new ArticleModel(article)));
                }
                if (MainCollectionViewModel.typeArticleAttribution != 0)
                {
                    if (MainCollectionViewModel.typeArticleAttribution == article.idtype.id)
                        MainCollectionViewModel._BaseArticle.Add(new DetailViewModel<ArticleModel>(new ArticleModel(article)));
                }
                else
                {
                    MainCollectionViewModel._BaseArticle.Add(new DetailViewModel<ArticleModel>(new ArticleModel(article)));
                }
            }

            if (OpenLastArticleEncoded)
            {
                SelectedArticleItem = MainCollectionViewModel._Article.Last();
            }
        }

        /// <summary>
        /// Reload all tire in observableCollection OrderArticle
        /// </summary>
        public void ReloadAllOrderArticle(bool OpenLastOrderArticleEncoded = false)
        {
            MainCollectionViewModel._OrderArticle.Clear();
            MainCollectionViewModel._ArticleForOrder.Clear();
            MainCollectionViewModel._User.Clear();

            foreach (var user in RU.SelectAllElement())
            {
                MainCollectionViewModel._User.Add(new DetailViewModel<UserModel>(new UserModel(user)));
            }

            if (SelectedArticleTypeForOrderItem != null)
            {
                int idtype = SelectedArticleTypeForOrderItem.Detail.id;
                foreach (var article in RA.SelectAllElementByType(idtype))
                {
                    MainCollectionViewModel._ArticleForOrder.Add(new DetailViewModel<ArticleModel>(new ArticleModel(article)));
                }
                foreach (var orderarticle in ROA.SelectAllElement())
                {
                    if (orderarticle.idarticle.idtype.id == idtype)
                        MainCollectionViewModel._OrderArticle.Add(new DetailViewModel<OrderArticleModel>(new OrderArticleModel(orderarticle)));
                }
            }
            else
            {
                foreach (var article in RA.SelectAllElement())
                {
                    MainCollectionViewModel._ArticleForOrder.Add(new DetailViewModel<ArticleModel>(new ArticleModel(article)));
                }
                foreach (var orderarticle in ROA.SelectAllElement())
                {
                    MainCollectionViewModel._OrderArticle.Add(new DetailViewModel<OrderArticleModel>(new OrderArticleModel(orderarticle)));
                }
            }

            if (OpenLastOrderArticleEncoded)
            {
                SelectedOrderArticleItem = MainCollectionViewModel._OrderArticle.Last();
            }
        }

        /// <summary>
        /// Set the timer for reset the status message
        /// </summary>
        private void setTimerStatus()
        {
            // Initialize the clocktime
            dtResetMessage.Interval = checkAccess.timerDisplayMessage;
            // Set the function and start the clock time
            dtResetMessage.Tick += setStatusMessage;
            dtResetMessage.Start();
        }

        /// <summary>
        /// Reset status message
        /// </summary>
        private void setStatusMessage(object sender, EventArgs e)
        {
            // Message status
            MessageStatusRequestToDB = string.Empty;
            dtResetMessage.Stop();
        }

        public bool _CanExecuteAddArticle()
        {
            return !string.IsNullOrEmpty(SelectedArticleNameItem) && GetSelectedIdProvider() != null && GetSelectedIdType() != null && checkAccess.AuthorizedAccess(checkAccess.tabName["Article"], checkAccess.getLetterAdd);
        }

        public ICommand AddArticle
        {
            get
            {
                return new RelayCommand(_AddArticle, _CanExecuteAddArticle);
            }
        }

        /// <summary>
        /// Add article
        /// </summary>
        public void _AddArticle()
        {
            // Initialize the variables
            // Get value by selected item
            string name = SelectedArticleNameItem;
            string RefArticle = !string.IsNullOrEmpty(SelectedRefArticleItem) ? SelectedRefArticleItem : string.Empty;
            Provider provider = GetSelectedIdProvider();
            ArticleType type = GetSelectedIdType();
            float price = SelectedPriceItem;
            int amount = SelectedAmountItem;
            int maxQuantity = SelectedMaxQuantityItem;
            int credit = SelectedCreditItem;
            string description = !string.IsNullOrEmpty(SelectedDescriptionItem) ? SelectedDescriptionItem : string.Empty;

            Article article = new Article(name, RefArticle, provider, price, amount, maxQuantity, type, description, credit);

            if (RA.InsertNewElement(article))
            {
                MessageStatusRequestToDB = SpecialInformationMessage.addstatusmessage;
                setTimerStatus();
                _CleanData();
                ReloadAllArticle(true);
            }
        }

        public bool _CanExecuteUpdateArticle()
        {
            return SelectedArticleItem != null && !string.IsNullOrEmpty(SelectedArticleNameItem) && GetSelectedIdProvider() != null && GetSelectedIdType() != null && checkAccess.AuthorizedAccess(checkAccess.tabName["Article"], checkAccess.getLetterUpd);
        }

        public ICommand UpdateArticle
        {
            get
            {
                return new RelayCommand(_UpdateArticle, _CanExecuteUpdateArticle);
            }
        }

        /// <summary>
        /// Update article
        /// </summary>
        public void _UpdateArticle()
        {
            // Initialize the variables
            // Get value by selected item
            int id = GetSelectedArticle().id;
            string name = !string.IsNullOrEmpty(SelectedArticleNameItem) ? SelectedArticleNameItem : GetSelectedArticle().name;
            string RefArticle = !string.IsNullOrEmpty(SelectedRefArticleItem) ? SelectedRefArticleItem : GetSelectedArticle().refArticle;
            Provider provider = GetSelectedIdProvider() ?? GetSelectedArticle().idprovider;
            ArticleType type = GetSelectedIdType() ?? GetSelectedArticle().idtype;
            float price = SelectedPriceItem > 0.00f ? SelectedPriceItem : GetSelectedArticle().price;
            int amount = SelectedAmountItem > 0 ? SelectedAmountItem : GetSelectedArticle().amount;
            int maxQuantity = SelectedMaxQuantityItem > 0 ? SelectedMaxQuantityItem : GetSelectedArticle().maxquantity;
            int credit = SelectedCreditItem > 0 ? SelectedCreditItem : GetSelectedArticle().credit;
            string description = !string.IsNullOrEmpty(SelectedDescriptionItem) ? SelectedDescriptionItem : GetSelectedArticle().description;

            Article article = new Article(id, name, RefArticle, provider, price, amount, maxQuantity, type, description, credit);

            if (RA.UpdateElement(article))
            {
                MessageStatusRequestToDB = SpecialInformationMessage.updatestatusmessage;
                setTimerStatus();
                _CleanData();
                ReloadAllArticle();
            }
        }

        public bool _CanExecuteDeleteArticle()
        {
            return SelectedArticleItem != null && checkAccess.AuthorizedAccess(checkAccess.tabName["Article"], checkAccess.getLetterDel);
        }

        public ICommand DeleteArticle
        {
            get
            {
                return new RelayCommand(_DeleteArticle, _CanExecuteDeleteArticle);
            }
        }

        /// <summary>
        /// Delete article
        /// </summary>
        public void _DeleteArticle()
        {
            // Initialize the variables
            Article article = GetSelectedArticle();

            // Configure the message box to be displayed
            string messageBoxText = string.Format("Voulez-vous vraiment supprimer l'article {0}, référence {1} ?", article.name, article.refArticle);
            string caption = "Suppression de l'article";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;

            // Display message box
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        if (RA.DeleteElement(article))
                        {
                            MessageStatusRequestToDB = SpecialInformationMessage.deletestatusmessage;
                            setTimerStatus();
                            _CleanData();
                            ReloadAllArticle();
                        }
                        return;
                    }
                case MessageBoxResult.No:
                    return;
                case MessageBoxResult.Cancel:
                    return;
            }
        }

        public bool _CanExecuteAddOrderArticle()
        {
            return SelectedArticleForOrderItem != null && SelectedUserForOrderItem != null && SelectedOrderAmountItem > 0 && SelectedOrderStatusItem != null && CheckPointUser() && checkAccess.AuthorizedAccess(checkAccess.tabName["Article"], checkAccess.getLetterAdd);
        }

        public ICommand AddOrderArticle
        {
            get
            {
                return new RelayCommand(_AddOrderArticle, _CanExecuteAddOrderArticle);
            }
        }

        /// <summary>
        /// Add order article
        /// </summary>
        public void _AddOrderArticle()
        {
            // Initialize the variables
            bool updatepoint = false;

            // Get value by selected item
            Article article = GetSelectedArticleForOrder();
            User iduser = GetSelectedUserForOrder();
            int amount = SelectedOrderAmountItem;
            DateTime OrderDate = SelectedOrderDateItem;
            DateTime DateReceived = SelectedDateReceivedItem;
            OrderStatus orderstatus = GetSelectedOrderStatus();
            SizeClothing sizeClothing = GetSelectedSizeClothing();
            string orderdescription = !string.IsNullOrEmpty(SelectedDescriptionOrderItem) ? SelectedDescriptionOrderItem : string.Empty;
            // Substract credit to pointarticle user
            if (article.credit > 0)
            {
                iduser.pointarticle = iduser.pointarticle - (article.credit * amount);
                iduser.lastupdatepoint = DateTime.Now;
                updatepoint = true;
            }

            OrderArticle orderarticle = new OrderArticle(iduser, article, amount, OrderDate, DateReceived, orderstatus, orderdescription, sizeClothing);

            if (ROA.InsertNewElement(orderarticle))
            {
                MessageStatusRequestToDB = SpecialInformationMessage.addstatusmessage;
                setTimerStatus();

                if (updatepoint)
                    _ = RU.UpdateElement(iduser);

                _CleanDataOrder();
                ReloadAllOrderArticle(true);
            }
        }

        public bool CheckPointUser()
        {
            if (GetSelectedUserForOrder() == null || GetSelectedArticleForOrder() == null)
            {
                WarningPointUser = string.Empty;
                return false;
            }
            if (((GetSelectedArticleForOrder().credit * SelectedOrderAmountItem) < GetSelectedUserForOrder().pointarticle) || GetSelectedArticleForOrder().credit == 0)
            {
                WarningPointUser = string.Empty;
                return true;
            }
            if (GetSelectedOrderArticle() != null)
                if (GetSelectedArticleForOrder().id != GetSelectedOrderArticle().idarticle.id || GetSelectedOrderArticle().amount != SelectedOrderAmountItem)
                {
                    WarningPointUser = string.Empty;
                    int totalcreditold = GetSelectedOrderArticle().idarticle.credit * GetSelectedOrderArticle().amount;
                    int totalcreditnew = GetSelectedArticleForOrder().credit * SelectedOrderAmountItem;
                    int diffcredit = totalcreditold - totalcreditnew;
                    int pointuser = GetSelectedUserForOrder().pointarticle + diffcredit;
                    if (pointuser >= 0)
                        return true;
                }
            WarningPointUser = string.Format(" L'utilisateur choisi n'a pas suffisamment de crédit restant pour passer la commande. ");
            return false;
        }

        public bool _CanExecuteUpdateOrderArticle()
        {
            return SelectedOrderArticleItem != null && CheckPointUser() && SelectedArticleForOrderItem != null && SelectedUserForOrderItem != null && SelectedOrderAmountItem > 0 && SelectedOrderStatusItem != null && checkAccess.AuthorizedAccess(checkAccess.tabName["Article"], checkAccess.getLetterUpd);
        }

        public ICommand UpdateOrderArticle
        {
            get
            {
                return new RelayCommand(_UpdateOrderArticle, _CanExecuteUpdateOrderArticle);
            }
        }

        /// <summary>
        /// Update order article
        /// </summary>
        public void _UpdateOrderArticle()
        {
            // Initialize the variables
            bool updatepoint = false;
            User oldUser = null;
            bool updatecreditactualuser = false;

            // Get value by selected item
            int id = GetSelectedOrderArticle().idorder;
            Article article = GetSelectedArticleForOrder() ?? GetSelectedOrderArticle().idarticle;
            User iduser = GetSelectedUserForOrder() ?? GetSelectedOrderArticle().iduser;
            int amount = SelectedOrderAmountItem > 0 ? SelectedOrderAmountItem : GetSelectedOrderArticle().amount;
            DateTime OrderDate = (SelectedOrderDateItem != GetSelectedOrderArticle().orderdate) ? SelectedOrderDateItem : GetSelectedOrderArticle().orderdate;
            DateTime DateReceived = (SelectedDateReceivedItem != GetSelectedOrderArticle().datereceived) ? SelectedDateReceivedItem : GetSelectedOrderArticle().datereceived;
            OrderStatus orderstatus = GetSelectedOrderStatus() ?? GetSelectedOrderArticle().status;
            string orderdescription = !string.IsNullOrEmpty(SelectedDescriptionOrderItem) ? SelectedDescriptionOrderItem : GetSelectedOrderArticle().description;
            SizeClothing sizeClothing = GetSelectedSizeClothing() ?? GetSelectedOrderArticle().sizeclothing;
            //Check if iduser is different, add credit article to pointarticle for old iduser and remove credit to new iduser
            if (GetSelectedOrderArticle().iduser.id != GetSelectedUserForOrder().id && article.credit > 0)
            {
                oldUser = GetSelectedOrderArticle().iduser;
                oldUser.pointarticle = oldUser.pointarticle + (GetSelectedOrderArticle().idarticle.credit * GetSelectedOrderArticle().amount);
                iduser.pointarticle = iduser.pointarticle - (article.credit * amount);
                oldUser.lastupdatepoint = DateTime.Now;
                iduser.lastupdatepoint = DateTime.Now;
                updatepoint = true;
            }
            else if (GetSelectedOrderArticle().amount != amount || GetSelectedOrderArticle().idarticle.id != GetSelectedArticleForOrder().id)
            {
                int totalcreditold = GetSelectedOrderArticle().idarticle.credit * GetSelectedOrderArticle().amount;
                int totalcreditnew = article.credit * amount;
                int diffcredit = totalcreditold - totalcreditnew;
                iduser.pointarticle = iduser.pointarticle + diffcredit;
                iduser.lastupdatepoint = DateTime.Now;
                updatecreditactualuser = true;
            }

            OrderArticle orderarticle = new OrderArticle(id, iduser, article, amount, OrderDate, DateReceived, orderstatus, orderdescription, sizeClothing);

            if (ROA.UpdateElement(orderarticle))
            {
                MessageStatusRequestToDB = SpecialInformationMessage.updatestatusmessage;
                setTimerStatus();

                if (updatepoint && oldUser != null && iduser != null)
                {
                    _ = RU.UpdateElement(oldUser);
                    _ = RU.UpdateElement(iduser);
                }
                else if (updatecreditactualuser && iduser != null)
                {
                    _ = RU.UpdateElement(iduser);
                }
                _CleanDataOrder();
                ReloadAllOrderArticle();
            }
        }

        public bool _CanExecuteDeleteOrderArticle()
        {
            return SelectedOrderArticleItem != null && checkAccess.AuthorizedAccess(checkAccess.tabName["Article"], checkAccess.getLetterDel);
        }

        public ICommand DeleteOrderArticle
        {
            get
            {
                return new RelayCommand(_DeleteOrderArticle, _CanExecuteDeleteOrderArticle);
            }
        }

        /// <summary>
        /// Delete order article
        /// </summary>
        public void _DeleteOrderArticle()
        {
            // Initialize the variables
            OrderArticle orderarticle = GetSelectedOrderArticle();
            User iduser = GetSelectedOrderArticle().iduser;
            bool updatepoint = false;

            // Configure the message box to be displayed
            string messageBoxText = string.Format("Voulez-vous vraiment supprimer la commande d'article {1} n° {0}, commandé pour : {2} {3} le {4:dd/MM/yyyy} ?", orderarticle.idorder, orderarticle.idarticle.name, orderarticle.iduser.firstname, orderarticle.iduser.lastname, orderarticle.orderdate.Date);
            string caption = "Suppression de la commande";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;

            // Display message box
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (orderarticle.idarticle.credit > 0)
                updatepoint = true;

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        if (updatepoint)
                        {
                            iduser.pointarticle += orderarticle.idarticle.credit * orderarticle.amount;
                            iduser.lastupdatepoint = DateTime.Now;
                            // restore old credit for user
                            _ = RU.UpdateElement(iduser);
                        }
                        if (ROA.DeleteElement(orderarticle))
                        {
                            MessageStatusRequestToDB = SpecialInformationMessage.deletestatusmessage;
                            setTimerStatus();
                            _CleanDataOrder();
                            ReloadAllOrderArticle();
                        }
                        return;
                    }
                case MessageBoxResult.No:
                    return;
                case MessageBoxResult.Cancel:
                    return;
            }
        }

        public ICommand ArticleFilter
        {
            get
            {
                return new RelayCommand(_ArticleFilter, null);
            }
        }

        /// <summary>
        /// Filter for the article
        /// </summary>
        public void _ArticleFilter()
        {
            // Initialize the variables
            List<DetailViewModel<ArticleModel>> articleRemove = new List<DetailViewModel<ArticleModel>>();

            // Get value for the different variables
            string name = SelectedArticleNameItem;
            string RefArticle = SelectedRefArticleItem;
            Provider provider = GetSelectedIdProvider();
            ArticleType type = GetSelectedIdType();
            float price = SelectedPriceItem;
            int amount = SelectedAmountItem;
            int maxQuantity = SelectedMaxQuantityItem;
            int credit = SelectedCreditItem;
            string description = SelectedDescriptionItem;

            foreach (var article in MainCollectionViewModel._Article)
            {
                if (!string.IsNullOrEmpty(name))
                    if (!article.Detail.name.ToLower().Contains(name.ToLower()))
                        articleRemove.Add(article);

                if (!string.IsNullOrEmpty(RefArticle))
                    if (!article.Detail.refArticle.ToLower().Contains(RefArticle.ToLower()))
                        articleRemove.Add(article);

                if (provider != null)
                    if (article.Detail.idprovider.id != provider.id)
                        articleRemove.Add(article);

                if (type != null)
                    if (article.Detail.idtype.id != type.id)
                        articleRemove.Add(article);

                if (price > 0.00f)
                    if (article.Detail.price != price)
                        articleRemove.Add(article);

                if (amount > 0)
                    if (article.Detail.amount != amount)
                        articleRemove.Add(article);

                if (maxQuantity > 0)
                    if (article.Detail.maxquantity != maxQuantity)
                        articleRemove.Add(article);

                if (credit > 0)
                    if (article.Detail.credit != credit)
                        articleRemove.Add(article);

                if (!string.IsNullOrEmpty(description))
                    if (!article.Detail.description.ToLower().Contains(description.ToLower()))
                        articleRemove.Add(article);
            }

            foreach (var article in articleRemove)
            {
                MainCollectionViewModel._Article.Remove(article);
            }
        }

        public ICommand ResetArticleFilter
        {
            get
            {
                return new RelayCommand(_ResetArticleFilter, null);
            }
        }

        /// <summary>
        /// Reset the filter for the article
        /// </summary>
        public void _ResetArticleFilter()
        {
            _CleanData();
            ReloadAllArticle(false);
        }

        public ICommand OrderArticleFilter
        {
            get
            {
                return new RelayCommand(_OrderArticleFilter, null);
            }
        }

        /// <summary>
        /// Filter for the order article
        /// </summary>
        public void _OrderArticleFilter()
        {
            // Initialize the variables
            List<DetailViewModel<OrderArticleModel>> orderarticleRemove = new List<DetailViewModel<OrderArticleModel>>();

            // Get value for the different variables
            Article article = GetSelectedArticleForOrder();
            User iduser = GetSelectedUserForOrder();
            int amount = SelectedOrderAmountItem;
            DateTime OrderDate = SelectedOrderDateItem;
            DateTime DateReceived = SelectedDateReceivedItem;
            OrderStatus orderstatus = GetSelectedOrderStatus();
            ArticleType type = GetSelectedTypeArticleOrder();
            SizeClothing sizeClothing = GetSelectedSizeClothing();
            string orderdescription = SelectedDescriptionItem;

            foreach (var orderarticle in MainCollectionViewModel._OrderArticle)
            {
                if (article != null)
                    if (orderarticle.Detail.idarticle.id != article.id)
                        orderarticleRemove.Add(orderarticle);

                if (iduser != null)
                    if (orderarticle.Detail.iduser.id != iduser.id)
                        orderarticleRemove.Add(orderarticle);

                if (amount > 0)
                    if (orderarticle.Detail.amount != amount)
                        orderarticleRemove.Add(orderarticle);

                if (OrderDate.Date != DateTime.Now.Date)
                    if (orderarticle.Detail.orderdate.Date != OrderDate.Date)
                        orderarticleRemove.Add(orderarticle);

                if (DateReceived.Date != DateTime.Now.Date)
                    if (orderarticle.Detail.datereceived.Date != DateReceived.Date)
                        orderarticleRemove.Add(orderarticle);

                if (orderstatus != null)
                    if (orderarticle.Detail.status.id != orderstatus.id)
                        orderarticleRemove.Add(orderarticle);

                if (type != null)
                    if (orderarticle.Detail.idarticle.idtype.id != type.id)
                        orderarticleRemove.Add(orderarticle);

                if (sizeClothing != null)
                {
                    if (orderarticle.Detail.sizeclothing == null)
                        orderarticleRemove.Add(orderarticle);
                    else if (orderarticle.Detail.sizeclothing.id != sizeClothing.id)
                        orderarticleRemove.Add(orderarticle);
                }

                if (!string.IsNullOrEmpty(orderdescription))
                    if (!orderarticle.Detail.description.ToLower().Contains(orderdescription.ToLower()))
                        orderarticleRemove.Add(orderarticle);
            }

            foreach (var orderarticle in orderarticleRemove)
            {
                MainCollectionViewModel._OrderArticle.Remove(orderarticle);
            }
        }

        public ICommand ResetOrderArticleFilter
        {
            get
            {
                return new RelayCommand(_ResetOrderArticleFilter, null);
            }
        }

        /// <summary>
        /// Reset the filter for the order artcle
        /// </summary>
        public void _ResetOrderArticleFilter()
        {
            _CleanDataOrder();
            ReloadAllOrderArticle();
        }

        public bool _CanExecuteJoinFilesToArticle()
        {
            return SelectedArticleItem != null;
        }

        public ICommand JoinFilesToArticle
        {
            get
            {
                return new RelayCommand(_JoinFilesToArticle, _CanExecuteJoinFilesToArticle);
            }
        }

        /// <summary>
        /// Open dialog for join files to article selected
        /// </summary>
        public void _JoinFilesToArticle()
        {
            string directory = ConfigurationManager.AppSettings[SpecialInformationMessage.FileDirectoryArticle].ToString();

            if (!string.IsNullOrEmpty(directory))
            {
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                if (Directory.Exists(directory))
                {
                    string directoryElement = directory + @"\" + SelectedArticleItem.Detail.id;

                    if (!string.IsNullOrEmpty(directoryElement))
                        if (!Directory.Exists(directoryElement))
                            Directory.CreateDirectory(directoryElement);

                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.Multiselect = true;

                    if (fileDialog.ShowDialog() == true)
                    {
                        string[] files = fileDialog.FileNames;

                        foreach (string file in files)
                        {
                            string fileName = Path.GetFileName(file);
                            File.Copy(file, directoryElement + @"\" + fileName, true);
                        }
                    }
                }
            } else
            {
                ShowDebugInfo();
                new MyErrorException(SpecialInformationMessage.ErrorMessageFilesFolder, SpecialInformationMessage.ErrorTitleFilesFolder, GetType().Name, method, line);
            }
        }

        public bool _CanExecuteDisplayFilesArticle()
        {
            return SelectedArticleItem != null && checker.CheckIfFolderExist(SpecialInformationMessage.FileDirectoryArticle, SelectedArticleItem.Detail.id);
        }

        public ICommand DisplayFilesArticle
        {
            get
            {
                return new RelayCommand(_DisplayFilesArticle, _CanExecuteDisplayFilesArticle);
            }
        }

        /// <summary>
        /// Display the files for the article selected
        /// </summary>
        public void _DisplayFilesArticle()
        {
            string directory = ConfigurationManager.AppSettings[SpecialInformationMessage.FileDirectoryArticle].ToString();

            if (!string.IsNullOrEmpty(directory))
            {
                if (Directory.Exists(directory))
                {
                    string directoryElement = directory + @"\" + SelectedArticleItem.Detail.id;

                    if (!string.IsNullOrEmpty(directoryElement))
                        if (Directory.Exists(directoryElement))
                        {
                            DirectoryInfo directoryElem = new DirectoryInfo(directoryElement);
                            FileInfo[] files = directoryElem.GetFiles();

                            // Open AllFilesElement window
                            if (files.Any())
                            {
                                OpenFileDialog fileDialog = new OpenFileDialog();
                                fileDialog.InitialDirectory = directoryElement;
                                fileDialog.Multiselect = false;

                                if (fileDialog.ShowDialog() == true)
                                {
                                    try
                                    {
                                        Process.Start(@"" + fileDialog.FileName);
                                    }
                                    catch (Exception ex)
                                    {
                                        ShowDebugInfo();
                                        new MyErrorException(ex.Message, SpecialInformationMessage.ErrorTitleOpenFiles, GetType().Name, method, line);
                                    }
                                }
                            }
                        }
                }
            }
            else
            {
                ShowDebugInfo();
                new MyErrorException(SpecialInformationMessage.ErrorMessageFilesFolder, SpecialInformationMessage.ErrorTitleFilesFolder, GetType().Name, method, line);
            }
        }
    }
}
