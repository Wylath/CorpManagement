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
    class AllInsuranceViewModel : ObservableObject
    {
        private readonly RequestDBInsuranceVehicle RIV;
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

        public enum ProviderType { GARAGE = 1, INSURANCE = 2, FED = 3, PUBLIC = 4 };

        #region SelectedItem
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
                        SelectedDescriptionItem = value.Detail.description;
                        SelectedEffectiveDateItem = value.Detail.effectivedate;
                        SelectedExpireDateItem = value.Detail.expiredate;
                        SelectedActiveItem = value.Detail.active;
                    }
                    else
                    {
                        SelectedPriceItem = 0.00f;
                        SelectedDescriptionItem = string.Empty;
                        SelectedEffectiveDateItem = DateTime.Now;
                        SelectedExpireDateItem = DateTime.Now;
                        SelectedActiveItem = false;
                    }
                    RaisePropertyChanged(() => SelectedInsuranceVehicleItem);
                }
            }
        }

        private int _SelectedInsuranceNumberItem;

        public int SelectedInsuranceNumberItem
        {
            get
            {
                return _SelectedInsuranceNumberItem;
            }
            set
            {
                if (value != _SelectedInsuranceNumberItem)
                {
                    _SelectedInsuranceNumberItem = value;
                    RaisePropertyChanged(() => SelectedInsuranceNumberItem);
                }
            }
        }

        private DetailViewModel<ProviderModel> _SelectedProviderItem;

        public DetailViewModel<ProviderModel> SelectedProviderItem
        {
            get
            {
                return _SelectedProviderItem;
            }
            set
            {
                if (value != _SelectedProviderItem)
                {
                    _SelectedProviderItem = value;
                    RaisePropertyChanged(() => SelectedProviderItem);
                }
            }
        }

        private DateTime _SelectedEffectiveDateItem;

        public DateTime SelectedEffectiveDateItem
        {
            get
            {
                if (_SelectedEffectiveDateItem.Year == 1)
                    _SelectedEffectiveDateItem = DateTime.Now;
                return _SelectedEffectiveDateItem;
            }
            set
            {
                if (value != _SelectedEffectiveDateItem)
                {
                    _SelectedEffectiveDateItem = value;
                    RaisePropertyChanged(() => SelectedEffectiveDateItem);
                }
            }
        }

        private DateTime _SelectedExpireDateItem;

        public DateTime SelectedExpireDateItem
        {
            get
            {
                if (_SelectedExpireDateItem.Year == 1)
                    _SelectedExpireDateItem = DateTime.Now;
                return _SelectedExpireDateItem;
            }
            set
            {
                if (value != _SelectedExpireDateItem)
                {
                    _SelectedExpireDateItem = value;
                    RaisePropertyChanged(() => SelectedExpireDateItem);
                }
            }
        }

        private bool _SelectedActiveItem;

        public bool SelectedActiveItem
        {
            get
            {
                return _SelectedActiveItem;
            }
            set
            {
                if (value != _SelectedActiveItem)
                {
                    _SelectedActiveItem = value;
                    RaisePropertyChanged(() => SelectedActiveItem);
                }
            }
        }

        private string _SelectedCoverageItem;

        public string SelectedCoverageItem
        {
            get
            {
                return _SelectedCoverageItem;
            }
            set
            {
                if (value != _SelectedCoverageItem)
                {
                    _SelectedCoverageItem = value;
                    RaisePropertyChanged(() => SelectedCoverageItem);
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
        /// Constructor AllInsurance
        /// </summary>
        public AllInsuranceViewModel()
        {
            // Initialize the request DB
            RIV = new RequestDBInsuranceVehicle();
            RI = new RequestDBInvoice();

            // Initialize other class checker
            checker = new CheckerForFolder();
            checkAccess = new CheckRightAccess();

            // Initialize timer for the messages in the window
            dtResetMessage = new DispatcherTimer();
        }

        /// <summary>
        /// Return the selected insurance
        /// </summary>
        /// <returns></returns>
        public InsuranceVehicle GetSelectedInsurance()
        {
            return (SelectedInsuranceVehicleItem != null) ? new InsuranceVehicle(SelectedInsuranceVehicleItem.Detail.id, SelectedInsuranceVehicleItem.Detail.insurancenumber, SelectedInsuranceVehicleItem.Detail.idprovider, SelectedInsuranceVehicleItem.Detail.effectivedate, SelectedInsuranceVehicleItem.Detail.expiredate, SelectedInsuranceVehicleItem.Detail.active, SelectedInsuranceVehicleItem.Detail.coverage, SelectedInsuranceVehicleItem.Detail.price, SelectedInsuranceVehicleItem.Detail.description) : null;
        }

        /// <summary>
        /// Return the selected provider
        /// </summary>
        /// <returns></returns>
        public Provider GetSelectedProvider()
        {
            return (SelectedProviderItem != null) ? new Provider(SelectedProviderItem.Detail.id, SelectedProviderItem.Detail.name) : null;
        }

        /// <summary>
        /// Clean the different data information
        /// </summary>
        public void _CleanData()
        {
            SelectedInsuranceVehicleItem = null;
            SelectedProviderItem = null;
            SelectedInsuranceNumberItem = 0;
            SelectedDescriptionItem = string.Empty;
            SelectedCoverageItem = string.Empty;
            SelectedActiveItem = false;
            SelectedEffectiveDateItem = DateTime.Now;
            SelectedExpireDateItem = DateTime.Now;
            SelectedPriceItem = 0.00f;
        }

        /// <summary>
        /// Reload all Insurance Vehicle in observableCollection
        /// </summary>
        public void ReloadAllInsuranceVehicle(bool OpenLastInsuranceVehicleEncoded = false)
        {
            MainCollectionViewModel._Insurance.Clear();

            foreach (var insurance in RIV.SelectAllElement())
            {
                MainCollectionViewModel._Insurance.Add(new DetailViewModel<InsuranceVehicleModel>(new InsuranceVehicleModel(insurance)));
            }

            if (OpenLastInsuranceVehicleEncoded)
            {
                SelectedInsuranceVehicleItem = MainCollectionViewModel._Insurance.Last();
            }
        }

        /// <summary>
        /// Check if insurance number is already used
        /// </summary>
        /// <param name="insurancenumber"></param>
        /// <returns></returns>
        private bool checkInsuranceNumber(int insurancenumber)
        {
            bool insuranceNumberUsed = false;
            foreach (var insurance in MainCollectionViewModel._Insurance)
            {
                if (insurance.Detail.insurancenumber == insurancenumber)
                    insuranceNumberUsed = true;
            }

            if (insuranceNumberUsed)
            {
                // Configure the message box to be displayed
                string messageBoxText = string.Format("Le numéro d'assurance {0} est déjà utilisé. Voulez-vous continuer ou annuler ?", insurancenumber);
                string caption = "Vérification du numéro d'assurance";
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;

                // Display message box
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

                // Process message box results
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        return true;
                    case MessageBoxResult.No:
                    case MessageBoxResult.Cancel:
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Check if the current price is equal to the price in the invoice for the insurance
        /// </summary>
        /// <param name="currentPrice"></param>
        /// <returns></returns>
        private float checkMaxPriceInvoice(InsuranceVehicle insurance, float currentPrice)
        {
            if (insurance == null)
                return currentPrice;

            bool updatePrice = false;
            float maxvalue = RI.SelectMaxPriceInvoiceByInsurance(insurance);

            if (currentPrice < maxvalue)
                updatePrice = true;

            if (updatePrice)
            {
                // Configure the message box to be displayed
                string messageBoxText = string.Format("Un prix plus élevé a été trouvé dans les dernières factures sur l'assurance. Voulez-vous mettre à jour le prix ?");
                string caption = "Prix de l'assurance";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Warning;

                // Display message box
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

                // Process message box results
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        return maxvalue;
                    case MessageBoxResult.No:
                        return currentPrice;
                }
            }

            return currentPrice;
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

        public bool _CanExecuteInsertInsuranceVehicle()
        {
            return SelectedProviderItem != null && SelectedInsuranceNumberItem > 0 && checkAccess.AuthorizedAccess(checkAccess.tabName["Insurance"], checkAccess.getLetterAdd);
        }

        public ICommand InsertInsuranceVehicle
        {
            get
            {
                return new RelayCommand(_InsertInsuranceVehicle, _CanExecuteInsertInsuranceVehicle);
            }
        }

        /// <summary>
        /// Insert insurance vehicle
        /// </summary>
        public void _InsertInsuranceVehicle()
        {
            // Initialize the variables
            // Get value by selected item
            int insurancenumber = SelectedInsuranceNumberItem;
            Provider idprovider = GetSelectedProvider();
            DateTime effectivedate = SelectedEffectiveDateItem;
            DateTime expiredate = SelectedExpireDateItem;
            bool active = SelectedActiveItem;
            string coverage = !string.IsNullOrEmpty(SelectedCoverageItem) ? SelectedCoverageItem : string.Empty;
            float price = SelectedPriceItem;
            string description = !string.IsNullOrEmpty(SelectedDescriptionItem) ? SelectedDescriptionItem : string.Empty;

            InsuranceVehicle insurance = new InsuranceVehicle(insurancenumber, idprovider, effectivedate, expiredate, active, coverage, price, description);

            if (checkInsuranceNumber(insurancenumber))
                if (RIV.InsertNewElement(insurance))
                {
                    MessageStatusRequestToDB = SpecialInformationMessage.addstatusmessage;
                    setTimerStatus();
                    _CleanData();
                    ReloadAllInsuranceVehicle(true);
                }
        }

        public bool _CanExecuteUpdateInsuranceVehicle()
        {
            return SelectedInsuranceVehicleItem != null && SelectedProviderItem != null && SelectedInsuranceNumberItem > 0 && checkAccess.AuthorizedAccess(checkAccess.tabName["Insurance"], checkAccess.getLetterUpd);
        }

        public ICommand UpdateInsuranceVehicle
        {
            get
            {
                return new RelayCommand(_UpdateInsuranceVehicle, _CanExecuteUpdateInsuranceVehicle);
            }
        }

        /// <summary>
        /// Update insurance vehicle
        /// </summary>
        public void _UpdateInsuranceVehicle()
        {
            // Initialize the variables
            // Get value by selected item
            int id = GetSelectedInsurance().id;
            int insurancenumber = SelectedInsuranceNumberItem > 0 ? SelectedInsuranceNumberItem : GetSelectedInsurance().insurancenumber;
            Provider idprovider = GetSelectedProvider() ?? GetSelectedInsurance().idprovider;
            DateTime effectivedate = (SelectedEffectiveDateItem.Date != GetSelectedInsurance().effectivedate.Date) ? SelectedEffectiveDateItem : GetSelectedInsurance().effectivedate;
            DateTime expiredate = (SelectedExpireDateItem.Date != GetSelectedInsurance().expiredate.Date) ? SelectedExpireDateItem : GetSelectedInsurance().expiredate;
            bool active = SelectedActiveItem ? SelectedActiveItem : GetSelectedInsurance().active;
            string coverage = !string.IsNullOrEmpty(SelectedCoverageItem) ? SelectedCoverageItem : GetSelectedInsurance().coverage;
            float price = SelectedPriceItem > 0.00f ? SelectedPriceItem : GetSelectedInsurance().price;
            string description = !string.IsNullOrEmpty(SelectedDescriptionItem) ? SelectedDescriptionItem : GetSelectedInsurance().description;

            // Check price in invoice
            price = checkMaxPriceInvoice(GetSelectedInsurance(), price);

            InsuranceVehicle insurance = new InsuranceVehicle(id, insurancenumber, idprovider, effectivedate, expiredate, active, coverage, price, description);

            bool insurancevalid = true;

            if (SelectedInsuranceNumberItem > 0)
                if (SelectedInsuranceVehicleItem.Detail.insurancenumber != SelectedInsuranceNumberItem)
                    insurancevalid = checkInsuranceNumber(SelectedInsuranceNumberItem);

            if (insurancevalid)
                if (RIV.UpdateElement(insurance))
                {
                    MessageStatusRequestToDB = SpecialInformationMessage.updatestatusmessage;
                    setTimerStatus();
                    _CleanData();
                    ReloadAllInsuranceVehicle();
                }
        }

        public bool _CanExecuteDeleteInsuranceVehicle()
        {
            return SelectedInsuranceVehicleItem != null && checkAccess.AuthorizedAccess(checkAccess.tabName["Insurance"], checkAccess.getLetterDel);
        }

        public ICommand DeleteInsuranceVehicle
        {
            get
            {
                return new RelayCommand(_DeleteInsuranceVehicle, _CanExecuteDeleteInsuranceVehicle);
            }
        }

        /// <summary>
        /// Delete insurance vehicle
        /// </summary>
        public void _DeleteInsuranceVehicle()
        {
            // Initialize the variables
            InsuranceVehicle insurance = GetSelectedInsurance();

            // Configure the message box to be displayed
            string messageBoxText = string.Format("Voulez-vous vraiment supprimer le n° d'assurance {0} ?", insurance.insurancenumber);
            string caption = "Suppression du n° d'assurance";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;

            // Display message box
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        if (RIV.DeleteElement(insurance))
                        {
                            MessageStatusRequestToDB = SpecialInformationMessage.deletestatusmessage;
                            setTimerStatus();
                            _CleanData();
                            ReloadAllInsuranceVehicle();
                        }
                        return;
                    }
                case MessageBoxResult.No:
                    return;
                case MessageBoxResult.Cancel:
                    return;
            }
        }

        public ICommand ChangeStatusInsurance
        {
            get
            {
                return new RelayCommand(_ChangeStatusInsurance, null);
            }
        }

        /// <summary>
        /// Switch value status insurance
        /// </summary>
        public void _ChangeStatusInsurance()
        {
            SelectedActiveItem = SelectedActiveItem ? true : false;
            if (SelectedInsuranceVehicleItem != null)
            {
                SelectedInsuranceVehicleItem.Detail.active = SelectedActiveItem;
            }
        }

        public bool _CanExecuteChangeStatusInsuranceInDG()
        {
            return SelectedInsuranceVehicleItem != null;
        }

        public ICommand ChangeStatusInsuranceInDG
        {
            get
            {
                return new RelayCommand(_ChangeStatusInsuranceInDG, _CanExecuteChangeStatusInsuranceInDG);
            }
        }

        /// <summary>
        /// Switch value status insurance
        /// </summary>
        public void _ChangeStatusInsuranceInDG()
        {
            SelectedActiveItem = SelectedInsuranceVehicleItem.Detail.active;
        }

        public ICommand InsuranceVehicleFilter
        {
            get
            {
                return new RelayCommand(_InsuranceVehicleFilter, null);
            }
        }

        /// <summary>
        /// Filter for the insurance
        /// </summary>
        public void _InsuranceVehicleFilter()
        {
            // Initialize the variables
            List<DetailViewModel<InsuranceVehicleModel>> insuranceRemove = new List<DetailViewModel<InsuranceVehicleModel>>();

            // Get value for the different variables
            int insurancenumber = SelectedInsuranceNumberItem;
            Provider idprovider = GetSelectedProvider();
            DateTime effectivedate = SelectedEffectiveDateItem;
            DateTime expiredate = SelectedExpireDateItem;
            string coverage = SelectedCoverageItem;
            float price = SelectedPriceItem;
            string description = SelectedDescriptionItem;

            foreach (var insurance in MainCollectionViewModel._Insurance)
            {
                if (insurancenumber > 0)
                    if (insurance.Detail.insurancenumber != insurancenumber)
                        insuranceRemove.Add(insurance);

                if (idprovider != null)
                    if (insurance.Detail.idprovider.id != idprovider.id)
                        insuranceRemove.Add(insurance);

                if (effectivedate.Date != DateTime.Now.Date)
                    if (insurance.Detail.effectivedate.Date != effectivedate.Date)
                        insuranceRemove.Add(insurance);

                if (expiredate.Date != DateTime.Now.Date)
                    if (insurance.Detail.expiredate.Date != expiredate.Date)
                        insuranceRemove.Add(insurance);

                if (!string.IsNullOrEmpty(coverage))
                    if (!insurance.Detail.coverage.ToLower().Contains(coverage.ToLower()))
                        insuranceRemove.Add(insurance);

                if (price > 0.00f)
                    if (insurance.Detail.price != price)
                        insuranceRemove.Add(insurance);

                if (!string.IsNullOrEmpty(description))
                    if (!insurance.Detail.description.ToLower().Contains(description.ToLower()))
                        insuranceRemove.Add(insurance);
            }

            foreach (var insurance in insuranceRemove)
            {
                MainCollectionViewModel._Insurance.Remove(insurance);
            }
        }

        public ICommand ResetInsuranceVehicleFilter
        {
            get
            {
                return new RelayCommand(_ResetInsuranceVehicleFilter, null);
            }
        }

        /// <summary>
        /// Reset the filter for the insurance
        /// </summary>
        public void _ResetInsuranceVehicleFilter()
        {
            _CleanData();
            ReloadAllInsuranceVehicle();
        }

        public bool _CanExecuteJoinFilesToInsurance()
        {
            return SelectedInsuranceVehicleItem != null;
        }

        public ICommand JoinFilesToInsurance
        {
            get
            {
                return new RelayCommand(_JoinFilesToInsurance, _CanExecuteJoinFilesToInsurance);
            }
        }

        /// <summary>
        /// Open dialog for join files to insurance selected
        /// </summary>
        public void _JoinFilesToInsurance()
        {
            string directory = ConfigurationManager.AppSettings[SpecialInformationMessage.FileDirectoryInsurance].ToString();

            if (!string.IsNullOrEmpty(directory))
            {
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                if (Directory.Exists(directory))
                {
                    string directoryElement = directory + @"\" + SelectedInsuranceVehicleItem.Detail.id;

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

        public bool _CanExecuteDisplayFilesInsurance()
        {
            return SelectedInsuranceVehicleItem != null && checker.CheckIfFolderExist(SpecialInformationMessage.FileDirectoryInsurance, SelectedInsuranceVehicleItem.Detail.id);
        }

        public ICommand DisplayFilesInsurance
        {
            get
            {
                return new RelayCommand(_DisplayFilesInsurance, _CanExecuteDisplayFilesInsurance);
            }
        }

        /// <summary>
        /// Display the files for the insurance selected
        /// </summary>
        public void _DisplayFilesInsurance()
        {
            string directory = ConfigurationManager.AppSettings[SpecialInformationMessage.FileDirectoryInsurance].ToString();

            if (!string.IsNullOrEmpty(directory))
            {
                if (Directory.Exists(directory))
                {
                    string directoryElement = directory + @"\" + SelectedInsuranceVehicleItem.Detail.id;

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
