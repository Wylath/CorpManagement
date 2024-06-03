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
    class AllInvoiceViewModel : ObservableObject
    {
        private readonly RequestDBInvoice RI;
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

        enum InvoiceTypeEnum
        {
            Insurance               = 1,
            OrderArticle            = 2,
            Servicing               = 3,
        }

        #region SelectedItem
        private DetailViewModel<InvoiceModel> _SelectedInvoiceItem;

        public DetailViewModel<InvoiceModel> SelectedInvoiceItem
        {
            get
            {
                return _SelectedInvoiceItem;
            }
            set
            {
                if (value != _SelectedInvoiceItem)
                {
                    _SelectedInvoiceItem = value;
                    if (value != null)
                    {
                        SelectedDescriptionItem = value.Detail.description;
                        SelectedPriceItem = value.Detail.price;
                        SelectedDateInvoiceItem = value.Detail.dateinvoice;
                        SelectedDatePaidItem = value.Detail.datepaid;
                        if (value.Detail.datepaid.Year > 1)
                        {
                            SelectedStatusInvoiceItem = true;
                            GetVisibilityForDatePaid = Visibility.Visible;
                        }
                        else
                        {
                            SelectedStatusInvoiceItem = false;
                            GetVisibilityForDatePaid = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        SelectedDescriptionItem = string.Empty;
                        SelectedPriceItem = 0.00f;
                        SelectedDateInvoiceItem = DateTime.Now;
                        SelectedDatePaidItem = DateTime.Now;
                        SelectedStatusInvoiceItem = false;
                        GetVisibilityForDatePaid = Visibility.Collapsed;
                    }
                    RaisePropertyChanged(() => SelectedInvoiceItem);
                }
            }
        }

        private DetailViewModel<InsuranceVehicleModel> _SelectedInsuranceVehicleItem;

        public DetailViewModel<InsuranceVehicleModel> SelectedInsuranceVehicleItem
        {
            get
            {
                return _SelectedInsuranceVehicleItem;
            }
            set
            {
                if (value != _SelectedInsuranceVehicleItem)
                {
                    _SelectedInsuranceVehicleItem = value;
                    if (value != null)
                    {
                        SelectedPriceItem = value.Detail.price;
                    }
                    else
                    {
                        SelectedPriceItem = 0.00f;
                    }
                    RaisePropertyChanged(() => SelectedInsuranceVehicleItem);
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
                        SelectedPriceItem = value.Detail.idarticle.price * value.Detail.amount;
                    }
                    else
                    {
                        SelectedPriceItem = 0.00f;
                    }
                    RaisePropertyChanged(() => SelectedOrderArticleItem);
                }
            }
        }

        private DetailViewModel<ServicingModel> _SelectedServicingItem;

        public DetailViewModel<ServicingModel> SelectedServicingItem
        {
            get
            {
                return _SelectedServicingItem;
            }
            set
            {
                if (value != _SelectedServicingItem)
                {
                    _SelectedServicingItem = value;
                    if (value != null)
                    {
                        SelectedPriceItem = value.Detail.price;
                    }
                    else
                    {
                        SelectedPriceItem = 0.00f;
                    }
                    RaisePropertyChanged(() => SelectedServicingItem);
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

        private DateTime _SelectedDateInvoiceItem;

        public DateTime SelectedDateInvoiceItem
        {
            get
            {
                if (_SelectedDateInvoiceItem.Year == 1)
                    _SelectedDateInvoiceItem = DateTime.Now;
                return _SelectedDateInvoiceItem;
            }
            set
            {
                if (value != _SelectedDateInvoiceItem)
                {
                    _SelectedDateInvoiceItem = value;
                    RaisePropertyChanged(() => SelectedDateInvoiceItem);
                }
            }
        }

        private DateTime _SelectedDatePaidItem;

        public DateTime SelectedDatePaidItem
        {
            get
            {
                if (_SelectedDatePaidItem.Year == 1)
                    _SelectedDatePaidItem = DateTime.Now;
                return _SelectedDatePaidItem;
            }
            set
            {
                if (value != _SelectedDatePaidItem)
                {
                    _SelectedDatePaidItem = value;
                    RaisePropertyChanged(() => SelectedDatePaidItem);
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

        private DetailViewModel<VehicleModel> _SelectedVehiculeItem;

        public DetailViewModel<VehicleModel> SelectedVehiculeItem
        {
            get
            {
                return _SelectedVehiculeItem;
            }
            set
            {
                if (value != _SelectedVehiculeItem)
                {
                    _SelectedVehiculeItem = value;
                    if (value != null)
                    {
                        MainCollectionViewModel._BaseServicing.Clear();
                        foreach (var servicing in MainCollectionViewModel._Servicing)
                        {
                            if (servicing.Detail.idvehicle.id == value.Detail.id)
                                MainCollectionViewModel._BaseServicing.Add(servicing);
                        }
                    }
                    RaisePropertyChanged(() => SelectedVehiculeItem);
                }
            }
        }

        private DetailViewModel<InvoiceTypeModel> _SelectedInvoiceTypeItem;

        public DetailViewModel<InvoiceTypeModel> SelectedInvoiceTypeItem
        {
            get
            {
                return _SelectedInvoiceTypeItem;
            }
            set
            {
                if (value != _SelectedInvoiceTypeItem)
                {
                    _SelectedInvoiceTypeItem = value;
                    if (value != null)
                    {
                        switch(value.Detail.id)
                        {
                            case (int)InvoiceTypeEnum.Insurance:
                                GetVisibilityForInsurance = Visibility.Visible;
                                GetVisibilityForOrderArticle = Visibility.Collapsed;
                                GetVisibilityForServicing = Visibility.Collapsed;
                                ReloadAllInvoice();
                                break;
                            case (int)InvoiceTypeEnum.OrderArticle:
                                GetVisibilityForInsurance = Visibility.Collapsed;
                                GetVisibilityForOrderArticle = Visibility.Visible;
                                GetVisibilityForServicing = Visibility.Collapsed;
                                ReloadAllInvoice();
                                break;
                            case (int)InvoiceTypeEnum.Servicing:
                                GetVisibilityForInsurance = Visibility.Collapsed;
                                GetVisibilityForOrderArticle = Visibility.Collapsed;
                                GetVisibilityForServicing = Visibility.Visible;
                                ReloadAllInvoice();
                                break;
                            default:
                                GetVisibilityForInsurance = Visibility.Collapsed;
                                GetVisibilityForOrderArticle = Visibility.Collapsed;
                                GetVisibilityForServicing = Visibility.Collapsed;
                                MainCollectionViewModel._Invoice.Clear();
                                break;
                        }
                    }
                    else
                    {
                        GetVisibilityForInsurance = Visibility.Collapsed;
                        GetVisibilityForOrderArticle = Visibility.Collapsed;
                        GetVisibilityForServicing = Visibility.Collapsed;
                        MainCollectionViewModel._Invoice.Clear();
                    }
                    RaisePropertyChanged(() => SelectedInvoiceTypeItem);
                }
            }
        }

        private bool _SelectedStatusInvoiceItem;

        public bool SelectedStatusInvoiceItem
        {
            get
            {
                return _SelectedStatusInvoiceItem;
            }
            set
            {
                if (value != _SelectedStatusInvoiceItem)
                {
                    _SelectedStatusInvoiceItem = value;
                    RaisePropertyChanged(() => SelectedStatusInvoiceItem);
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

        #region VisibilityForInterface
        private Visibility _GetVisibilityForInsurance;

        public Visibility GetVisibilityForInsurance
        {
            get
            {
                return _GetVisibilityForInsurance;
            }
            set
            {
                if (value != _GetVisibilityForInsurance)
                {
                    _GetVisibilityForInsurance = value;
                    RaisePropertyChanged(() => GetVisibilityForInsurance);
                }
            }
        }

        private Visibility _GetVisibilityForOrderArticle;

        public Visibility GetVisibilityForOrderArticle
        {
            get
            {
                return _GetVisibilityForOrderArticle;
            }
            set
            {
                if (value != _GetVisibilityForOrderArticle)
                {
                    _GetVisibilityForOrderArticle = value;
                    RaisePropertyChanged(() => GetVisibilityForOrderArticle);
                }
            }
        }

        private Visibility _GetVisibilityForServicing;

        public Visibility GetVisibilityForServicing
        {
            get
            {
                return _GetVisibilityForServicing;
            }
            set
            {
                if (value != _GetVisibilityForServicing)
                {
                    _GetVisibilityForServicing = value;
                    RaisePropertyChanged(() => GetVisibilityForServicing);
                }
            }
        }

        private Visibility _GetVisibilityForDatePaid;

        public Visibility GetVisibilityForDatePaid
        {
            get
            {
                return _GetVisibilityForDatePaid;
            }
            set
            {
                if (value != _GetVisibilityForDatePaid)
                {
                    _GetVisibilityForDatePaid = value;
                    RaisePropertyChanged(() => GetVisibilityForDatePaid);
                }
            }
        }
        #endregion

        /// <summary>
        /// Constructor AllInvoice
        /// </summary>
        public AllInvoiceViewModel()
        {
            // Set visibility column in DG & combobox
            GetVisibilityForInsurance = Visibility.Collapsed;
            GetVisibilityForOrderArticle = Visibility.Collapsed;
            GetVisibilityForServicing = Visibility.Collapsed;
            GetVisibilityForDatePaid = Visibility.Collapsed;

            // Initialize the request DB
            RI = new RequestDBInvoice();

            // Initialize other class checker
            checker = new CheckerForFolder();
            checkAccess = new CheckRightAccess();

            // Initialize timer for the messages in the window
            dtResetMessage = new DispatcherTimer();
        }

        /// <summary>
        /// Return the selected invoice
        /// </summary>
        /// <returns></returns>
        public Invoice GetSelectedInvoice()
        {
            return (SelectedInvoiceItem != null) ? new Invoice(SelectedInvoiceItem.Detail.id, SelectedInvoiceItem.Detail.idinsurance, SelectedInvoiceItem.Detail.idorderarticle, SelectedInvoiceItem.Detail.idservicing, SelectedInvoiceItem.Detail.price, SelectedInvoiceItem.Detail.dateinvoice, SelectedInvoiceItem.Detail.description, SelectedInvoiceItem.Detail.datepaid, SelectedInvoiceItem.Detail.idtype) : null;
        }

        /// <summary>
        /// Return the selected insurance vehicle
        /// </summary>
        /// <returns></returns>
        public InsuranceVehicle GetSelectedInsurance()
        {
            return (SelectedInsuranceVehicleItem != null) ? new InsuranceVehicle(SelectedInsuranceVehicleItem.Detail.id, SelectedInsuranceVehicleItem.Detail.insurancenumber, SelectedInsuranceVehicleItem.Detail.idprovider, SelectedInsuranceVehicleItem.Detail.effectivedate, SelectedInsuranceVehicleItem.Detail.expiredate, SelectedInsuranceVehicleItem.Detail.active, SelectedInsuranceVehicleItem.Detail.coverage, SelectedInsuranceVehicleItem.Detail.price, SelectedInsuranceVehicleItem.Detail.description) : null;
        }

        /// <summary>
        /// Return the selected order article
        /// </summary>
        /// <returns></returns>
        public OrderArticle GetSelectedOrderArticle()
        {
            return (SelectedOrderArticleItem != null) ? new OrderArticle(SelectedOrderArticleItem.Detail.idorder, SelectedOrderArticleItem.Detail.iduser, SelectedOrderArticleItem.Detail.idarticle, SelectedOrderArticleItem.Detail.amount, SelectedOrderArticleItem.Detail.orderdate, SelectedOrderArticleItem.Detail.datereceived, SelectedOrderArticleItem.Detail.status, SelectedOrderArticleItem.Detail.description) : null;
        }

        /// <summary>
        /// Return the selected servicing
        /// </summary>
        /// <returns></returns>
        public Servicing GetSelectedServicing()
        {
            return (SelectedServicingItem != null) ? new Servicing(SelectedServicingItem.Detail.id, SelectedServicingItem.Detail.idvehicle, SelectedServicingItem.Detail.dateservicing, SelectedServicingItem.Detail.price, SelectedServicingItem.Detail.idprovider, SelectedServicingItem.Detail.description) : null;
        }

        /// <summary>
        /// Return the selected invoice type
        /// </summary>
        /// <returns></returns>
        public InvoiceType GetSelectedInvoiceType()
        {
            return (SelectedInvoiceTypeItem != null) ? new InvoiceType(SelectedInvoiceTypeItem.Detail.id, SelectedInvoiceTypeItem.Detail.name) : null;
        }

        /// <summary>
        /// Return the selected vehicle
        /// </summary>
        /// <returns></returns>
        public Vehicle GetSelectedVehicle()
        {
            return (SelectedVehiculeItem != null) ? new Vehicle(SelectedVehiculeItem.Detail.id) : null;
        }

        /// <summary>
        /// Clean the different data information
        /// </summary>
        public void _CleanData()
        {
            SelectedInvoiceItem = null;
            SelectedInsuranceVehicleItem = null;
            SelectedOrderArticleItem = null;
            SelectedServicingItem = null;
            SelectedDescriptionItem = string.Empty;
            SelectedVehiculeItem = null;
            //SelectedInvoiceTypeItem = null;
        }

        /// <summary>
        /// Reload all vehicle in observableCollection invoice
        /// </summary>
        public void ReloadAllInvoice(bool OpenLastInvoiceEncoded = false)
        {
            MainCollectionViewModel._Invoice.Clear();

            foreach (var invoice in RI.SelectAllElement())
            {
                if (SelectedInvoiceTypeItem != null)
                {
                    if (invoice.idtype.id == SelectedInvoiceTypeItem.Detail.id)
                        MainCollectionViewModel._Invoice.Add(new DetailViewModel<InvoiceModel>(new InvoiceModel(invoice)));
                }
                else
                {
                    MainCollectionViewModel._Invoice.Add(new DetailViewModel<InvoiceModel>(new InvoiceModel(invoice)));
                }
            }

            if (OpenLastInvoiceEncoded)
            {
                SelectedInvoiceItem = MainCollectionViewModel._Invoice.Last();
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

        public bool _CanExecuteInsertInvoice()
        {
            return (SelectedInsuranceVehicleItem != null || SelectedOrderArticleItem != null || SelectedServicingItem != null) && SelectedInvoiceTypeItem != null && checkAccess.AuthorizedAccess(checkAccess.tabName["Invoice"], checkAccess.getLetterAdd);
        }

        public ICommand InsertInvoice
        {
            get
            {
                return new RelayCommand(_InsertInvoice, _CanExecuteInsertInvoice);
            }
        }

        /// <summary>
        /// Insert new invoice
        /// </summary>
        public void _InsertInvoice()
        {
            // Initialize the variables
            Invoice invoice = null;
            DateTime datepaid = DateTime.Now;
            // Get value by selected item
            InsuranceVehicle insurance = GetSelectedInsurance();
            OrderArticle idorderarticle = GetSelectedOrderArticle();
            Servicing idservicing = GetSelectedServicing();
            string description = !string.IsNullOrEmpty(SelectedDescriptionItem) ? SelectedDescriptionItem : string.Empty;
            float price = SelectedPriceItem;
            DateTime dateinvoice = SelectedDateInvoiceItem;
            if (SelectedStatusInvoiceItem)
                datepaid = SelectedDatePaidItem;
            InvoiceType idtype = GetSelectedInvoiceType();

            if (SelectedStatusInvoiceItem)
                invoice = new Invoice(insurance, idorderarticle, idservicing, price, dateinvoice, description, datepaid, idtype);
            else invoice = new Invoice(insurance, idorderarticle, idservicing, price, dateinvoice, description, new DateTime(), idtype);

            if (RI.InsertNewElement(invoice))
            {
                MessageStatusRequestToDB = SpecialInformationMessage.addstatusmessage;
                setTimerStatus();
                _CleanData();
                ReloadAllInvoice(true);
            }
        }

        public bool _CanExecuteUpdateInvoice()
        {
            return SelectedInvoiceItem != null && (SelectedInsuranceVehicleItem != null || SelectedOrderArticleItem != null || SelectedServicingItem != null) && SelectedInvoiceTypeItem != null && checkAccess.AuthorizedAccess(checkAccess.tabName["Invoice"], checkAccess.getLetterUpd);
        }

        public ICommand UpdateInvoice
        {
            get
            {
                return new RelayCommand(_UpdateInvoice, _CanExecuteUpdateInvoice);
            }
        }

        /// <summary>
        /// Update invoice
        /// </summary>
        public void _UpdateInvoice()
        {
            // Initialize the variables
            Invoice invoice = null;
            DateTime datepaid = DateTime.Now;

            // Get value by selected item
            int id = GetSelectedInvoice().id;
            InsuranceVehicle insurance = GetSelectedInsurance() ?? GetSelectedInvoice().idinsurance;
            OrderArticle idorderarticle = GetSelectedOrderArticle() ?? GetSelectedInvoice().idorderarticle;
            Servicing idservicing = GetSelectedServicing() ?? GetSelectedInvoice().idservicing;
            string description = !string.IsNullOrEmpty(SelectedDescriptionItem) ? SelectedDescriptionItem : GetSelectedInvoice().description;
            float price = SelectedPriceItem > 0.00f ? SelectedPriceItem : GetSelectedInvoice().price;
            DateTime dateinvoice = (SelectedDateInvoiceItem != GetSelectedInvoice().dateinvoice) ? SelectedDateInvoiceItem : GetSelectedInvoice().dateinvoice;
            if (SelectedStatusInvoiceItem)
                datepaid = (SelectedDatePaidItem != GetSelectedInvoice().datepaid) ? SelectedDatePaidItem : GetSelectedInvoice().datepaid;
            InvoiceType idtype = GetSelectedInvoiceType() ?? GetSelectedInvoice().idtype;

            if (SelectedStatusInvoiceItem)
                invoice = new Invoice(id, insurance, idorderarticle, idservicing, price, dateinvoice, description, datepaid, idtype);
            else invoice = new Invoice(id, insurance, idorderarticle, idservicing, price, dateinvoice, description, new DateTime(), idtype);

            if (RI.UpdateElement(invoice))
            {
                MessageStatusRequestToDB = SpecialInformationMessage.updatestatusmessage;
                setTimerStatus();
                _CleanData();
                ReloadAllInvoice();
            }
        }

        public bool _CanExecuteDeleteInvoice()
        {
            return SelectedInvoiceItem != null && checkAccess.AuthorizedAccess(checkAccess.tabName["Invoice"], checkAccess.getLetterDel);
        }

        public ICommand DeleteInvoice
        {
            get
            {
                return new RelayCommand(_DeleteInvoice, _CanExecuteDeleteInvoice);
            }
        }

        /// <summary>
        /// Delete invoice
        /// </summary>
        public void _DeleteInvoice()
        {
            // Initialize the variables
            Invoice invoice = GetSelectedInvoice();
            string messageBoxText = string.Empty;

            // Configure the message box to be displayed
            switch (invoice.idtype.id)
            {
                case (int)InvoiceTypeEnum.Insurance:
                    messageBoxText = string.Format("Voulez-vous vraiment supprimer la facture avec le numéro d'assurance {0} du {1}/{2}/{3} ?", invoice.idinsurance.insurancenumber, invoice.dateinvoice.Day, invoice.dateinvoice.Month, invoice.dateinvoice.Year);
                    break;
                case (int)InvoiceTypeEnum.OrderArticle:
                    messageBoxText = string.Format("Voulez-vous vraiment supprimer la facture sur le numéro de commande d'article {0} du {1}/{2}/{3} ?", invoice.idorderarticle.idorder, invoice.dateinvoice.Day, invoice.dateinvoice.Month, invoice.dateinvoice.Year);
                    break;
                case (int)InvoiceTypeEnum.Servicing:
                    messageBoxText = string.Format("Voulez-vous vraiment supprimer la facture avec le numéro de service {0} du {1}/{2}/{3} ?", invoice.idservicing.id, invoice.dateinvoice.Day, invoice.dateinvoice.Month, invoice.dateinvoice.Year);
                    break;
                default:
                    messageBoxText = string.Format("Voulez-vous vraiment supprimer la facture du type {0} du {1}/{2}/{3} ?", invoice.idtype.name, invoice.dateinvoice.Day, invoice.dateinvoice.Month, invoice.dateinvoice.Year);
                    break;
            }
            string caption = "Suppression de la facture";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;

            // Display message box
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        if (RI.DeleteElement(invoice))
                        {
                            MessageStatusRequestToDB = SpecialInformationMessage.deletestatusmessage;
                            setTimerStatus();
                            _CleanData();
                            ReloadAllInvoice();
                        }
                        return;
                    }
                case MessageBoxResult.No:
                    return;
                case MessageBoxResult.Cancel:
                    return;
            }
        }

        public ICommand DisplayDatePaid
        {
            get
            {
                return new RelayCommand(_DisplayDatePaid, null);
            }
        }

        /// <summary>
        /// Switch value status invoice
        /// </summary>
        public void _DisplayDatePaid()
        {
            GetVisibilityForDatePaid = !SelectedStatusInvoiceItem ? Visibility.Collapsed : Visibility.Visible;
        }

        public bool _CanExecuteInvoiceFilter()
        {
            return SelectedInvoiceTypeItem != null;
        }

        public ICommand InvoiceFilter
        {
            get
            {
                return new RelayCommand(_InvoiceFilter, _CanExecuteInvoiceFilter);
            }
        }

        /// <summary>
        /// Filter for the invoice
        /// </summary>
        public void _InvoiceFilter()
        {
            // Initialize the variables
            List<DetailViewModel<InvoiceModel>> invoiceRemove = new List<DetailViewModel<InvoiceModel>>();

            // Get value for the different variables
            InsuranceVehicle insurance = GetSelectedInsurance();
            OrderArticle idorderarticle = GetSelectedOrderArticle();
            Servicing idservicing = GetSelectedServicing();
            Vehicle idvehicle = GetSelectedVehicle();
            string description = SelectedDescriptionItem;
            float price = SelectedPriceItem;
            DateTime dateinvoice = SelectedDateInvoiceItem;
            DateTime datepaid = SelectedDatePaidItem;

            foreach (var invoice in MainCollectionViewModel._Invoice)
            {
                if (insurance != null)
                    if (invoice.Detail.idinsurance.id != insurance.id)
                        invoiceRemove.Add(invoice);

                if (idorderarticle != null)
                    if (invoice.Detail.idorderarticle.idorder != idorderarticle.idorder)
                        invoiceRemove.Add(invoice);

                if (idvehicle != null)
                    if (invoice.Detail.idservicing.idvehicle.id != idvehicle.id)
                        invoiceRemove.Add(invoice);

                if (idservicing != null)
                    if (invoice.Detail.idservicing.id != idservicing.id)
                        invoiceRemove.Add(invoice);

                if (dateinvoice.Date != DateTime.Now.Date)
                    if (invoice.Detail.dateinvoice.Date != dateinvoice.Date)
                        invoiceRemove.Add(invoice);

                if (datepaid.Date != DateTime.Now.Date)
                    if (invoice.Detail.datepaid.Date != datepaid.Date)
                        invoiceRemove.Add(invoice);

                if (price > 0.00f)
                    if (invoice.Detail.price != price)
                        invoiceRemove.Add(invoice);

                if (!string.IsNullOrEmpty(description))
                    if (!invoice.Detail.description.ToLower().Contains(description.ToLower()))
                        invoiceRemove.Add(invoice);
            }

            foreach (var invoice in invoiceRemove)
            {
                MainCollectionViewModel._Invoice.Remove(invoice);
            }
        }

        public bool _CanExecuteResetInvoiceFilter()
        {
            return SelectedInvoiceTypeItem != null;
        }

        public ICommand ResetInvoiceFilter
        {
            get
            {
                return new RelayCommand(_ResetInvoiceFilter, _CanExecuteResetInvoiceFilter);
            }
        }

        /// <summary>
        /// Reset the filter for the insurance
        /// </summary>
        public void _ResetInvoiceFilter()
        {
            _CleanData();
            ReloadAllInvoice();
        }

        public bool _CanExecuteJoinFilesToInvoice()
        {
            return SelectedInvoiceItem != null;
        }

        public ICommand JoinFilesToInvoice
        {
            get
            {
                return new RelayCommand(_JoinFilesToInvoice, _CanExecuteJoinFilesToInvoice);
            }
        }

        /// <summary>
        /// Open dialog for join files to invoice selected
        /// </summary>
        public void _JoinFilesToInvoice()
        {
            string directory = ConfigurationManager.AppSettings[SpecialInformationMessage.FileDirectoryInvoice].ToString();

            if (!string.IsNullOrEmpty(directory))
            {
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                if (Directory.Exists(directory))
                {
                    string directoryElement = directory + @"\" + SelectedInvoiceItem.Detail.id;

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
            }
            else
            {
                ShowDebugInfo();
                new MyErrorException(SpecialInformationMessage.ErrorMessageFilesFolder, SpecialInformationMessage.ErrorTitleFilesFolder, GetType().Name, method, line);
            }
        }

        public bool _CanExecuteDisplayFilesInvoice()
        {
            return SelectedInvoiceItem != null && checker.CheckIfFolderExist(SpecialInformationMessage.FileDirectoryInvoice, SelectedInvoiceItem.Detail.id);
        }

        public ICommand DisplayFilesInvoice
        {
            get
            {
                return new RelayCommand(_DisplayFilesInvoice, _CanExecuteDisplayFilesInvoice);
            }
        }

        /// <summary>
        /// Display the files for the invoice selected
        /// </summary>
        public void _DisplayFilesInvoice()
        {
            string directory = ConfigurationManager.AppSettings[SpecialInformationMessage.FileDirectoryInvoice].ToString();

            if (!string.IsNullOrEmpty(directory))
            {
                if (Directory.Exists(directory))
                {
                    string directoryElement = directory + @"\" + SelectedInvoiceItem.Detail.id;

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
