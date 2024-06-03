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
    class AllServicingViewModel : ObservableObject
    {
        private readonly RequestDBServicing RS;
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

        #region SelectedItem
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
                        SelectedDescriptionItem = value.Detail.description;
                        SelectedDateServicingItem = value.Detail.dateservicing;
                        SelectedKmItem = value.Detail.km;

                    }
                    else
                    {
                        SelectedPriceItem = 0.00f;
                        SelectedDescriptionItem = string.Empty;
                        SelectedDateServicingItem = DateTime.Now;
                        SelectedKmItem = 0;
                    }
                    RaisePropertyChanged(() => SelectedServicingItem);
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
                    RaisePropertyChanged(() => SelectedVehiculeItem);
                }
            }
        }

        private DateTime _SelectedDateServicingItem;

        public DateTime SelectedDateServicingItem
        {
            get
            {
                if (_SelectedDateServicingItem.Year == 1)
                    _SelectedDateServicingItem = DateTime.Now;
                return _SelectedDateServicingItem;
            }
            set
            {
                if (value != _SelectedDateServicingItem)
                {
                    _SelectedDateServicingItem = value;
                    RaisePropertyChanged(() => SelectedDateServicingItem);
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

        private DetailViewModel<TypeServicingModel> _SelectedTypeServicingItem;

        public DetailViewModel<TypeServicingModel> SelectedTypeServicingItem
        {
            get
            {
                return _SelectedTypeServicingItem;
            }
            set
            {
                if (value != _SelectedTypeServicingItem)
                {
                    _SelectedTypeServicingItem = value;
                    RaisePropertyChanged(() => SelectedTypeServicingItem);
                }
            }
        }

        private int _SelectedKmItem;

        public int SelectedKmItem
        {
            get
            {
                return _SelectedKmItem;
            }
            set
            {
                if (value != _SelectedKmItem)
                {
                    _SelectedKmItem = value;
                    RaisePropertyChanged(() => SelectedKmItem);
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
        /// Constructor AllServicing
        /// </summary>
        public AllServicingViewModel()
        {
            // Initialize the request DB
            RS = new RequestDBServicing();

            // Initialize other class checker
            checker = new CheckerForFolder();
            checkAccess = new CheckRightAccess();

            // Initialize timer for the messages in the window
            dtResetMessage = new DispatcherTimer();
        }

        /// <summary>
        /// Return the selected servicing
        /// </summary>
        /// <returns></returns>
        public Servicing GetSelectedServicing()
        {
            return (SelectedServicingItem != null) ? new Servicing(SelectedServicingItem.Detail.id, SelectedServicingItem.Detail.idvehicle, SelectedServicingItem.Detail.dateservicing, SelectedServicingItem.Detail.price, SelectedServicingItem.Detail.idprovider, SelectedServicingItem.Detail.description, SelectedServicingItem.Detail.idtypeservicing, SelectedServicingItem.Detail.km) : null;
        }

        /// <summary>
        /// Return the selected vehicle
        /// </summary>
        /// <returns></returns>
        public Vehicle GetSelectedVehicle()
        {
            return (SelectedVehiculeItem != null) ? new Vehicle(SelectedVehiculeItem.Detail.id, SelectedVehiculeItem.Detail.name, SelectedVehiculeItem.Detail.numberplate, SelectedVehiculeItem.Detail.idpolicelocality, SelectedVehiculeItem.Detail.saledate, SelectedVehiculeItem.Detail.lastcontrol, SelectedVehiculeItem.Detail.kmlastcontrol, SelectedVehiculeItem.Detail.nextcontrol, SelectedVehiculeItem.Detail.idtires, SelectedVehiculeItem.Detail.fueltype, SelectedVehiculeItem.Detail.vehicletype, SelectedVehiculeItem.Detail.status, SelectedVehiculeItem.Detail.description, SelectedVehiculeItem.Detail.idinsurance) : null;
        }

        /// <summary>
        /// Return the selected provider
        /// </summary>
        /// <returns></returns>
        public Provider GetSelectedProvider()
        {
            return (SelectedProviderItem != null) ? new Provider(SelectedProviderItem.Detail.id) : null;
        }

        /// <summary>
        /// REturn the selected type servicing
        /// </summary>
        /// <returns></returns>
        public TypeServicing GetSelectedTypeServicing()
        {
            return (SelectedTypeServicingItem != null) ? new TypeServicing(SelectedTypeServicingItem.Detail.id) : null;
        }

        /// <summary>
        /// Clean the different data information
        /// </summary>
        public void _CleanData()
        {
            SelectedVehiculeItem = null;
            SelectedServicingItem = null;
            SelectedProviderItem = null;
            SelectedPriceItem = 0.00f;
            SelectedDateServicingItem = DateTime.Now;
            SelectedTypeServicingItem = null;
            SelectedKmItem = 0;
        }

        /// <summary>
        /// Reload all servicing in observableCollection Servicing
        /// </summary>
        public void ReloadAllServicing(bool OpenLastServicingEncoded = false)
        {
            MainCollectionViewModel._Servicing.Clear();

            foreach (var servicing in RS.SelectAllElement())
            {
                MainCollectionViewModel._Servicing.Add(new DetailViewModel<ServicingModel>(new ServicingModel(servicing)));
            }

            if (OpenLastServicingEncoded)
            {
                SelectedServicingItem = MainCollectionViewModel._Servicing.Last();
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

        public bool _CanExecuteAddServicing()
        {
            return SelectedVehiculeItem != null && SelectedDateServicingItem != null && SelectedProviderItem != null && SelectedTypeServicingItem != null && checkAccess.AuthorizedAccess(checkAccess.tabName["Servicing"], checkAccess.getLetterAdd);
        }

        public ICommand AddServicing
        {
            get
            {
                return new RelayCommand(_AddServicing, _CanExecuteAddServicing);
            }
        }

        /// <summary>
        /// Add servicing
        /// </summary>
        public void _AddServicing()
        {
            // Initialize the variables
            // Get value by selected item
            Vehicle vehicle = GetSelectedVehicle();
            DateTime dateservicing = SelectedDateServicingItem;
            Provider provider = GetSelectedProvider();
            float price = SelectedPriceItem;
            string description = !string.IsNullOrEmpty(SelectedDescriptionItem) ? SelectedDescriptionItem : string.Empty;
            TypeServicing type = GetSelectedTypeServicing();
            int km = SelectedKmItem;

            Servicing servicing = new Servicing(vehicle, dateservicing, price, provider, description, type, km);

            if (RS.InsertNewElement(servicing))
            {
                MessageStatusRequestToDB = SpecialInformationMessage.addstatusmessage;
                setTimerStatus();
                _CleanData();
                ReloadAllServicing(true);
            }
        }

        public bool _CanExecuteUpdateServicing()
        {
            return SelectedServicingItem != null && SelectedVehiculeItem != null && SelectedDateServicingItem != null && SelectedProviderItem != null && SelectedTypeServicingItem != null && checkAccess.AuthorizedAccess(checkAccess.tabName["Servicing"], checkAccess.getLetterUpd);
        }

        public ICommand UpdateServicing
        {
            get
            {
                return new RelayCommand(_UpdateServicing, _CanExecuteUpdateServicing);
            }
        }

        /// <summary>
        /// Update servicing
        /// </summary>
        public void _UpdateServicing()
        {
            // Initialize the variables
            // Get value by selected item
            int id = GetSelectedServicing().id;
            Vehicle vehicle = GetSelectedVehicle() ?? GetSelectedServicing().idvehicle;
            DateTime dateservicing = (SelectedDateServicingItem.Date != GetSelectedServicing().dateservicing.Date) ? SelectedDateServicingItem : GetSelectedServicing().dateservicing;
            Provider provider = GetSelectedProvider() ?? GetSelectedServicing().idprovider;
            float price = SelectedPriceItem > 0.00f ? SelectedPriceItem : GetSelectedServicing().price;
            string description = !string.IsNullOrEmpty(SelectedDescriptionItem) ? SelectedDescriptionItem : GetSelectedServicing().description;
            TypeServicing type = GetSelectedTypeServicing() ?? GetSelectedServicing().idtypeservicing;
            int km = SelectedKmItem > 0 ? SelectedKmItem : GetSelectedServicing().km;

            Servicing servicing = new Servicing(id, vehicle, dateservicing, price, provider, description, type, km);

            if (RS.UpdateElement(servicing))
            {
                MessageStatusRequestToDB = SpecialInformationMessage.updatestatusmessage;
                setTimerStatus();
                _CleanData();
                ReloadAllServicing();
            }
        }

        public bool _CanExecuteDeleteServicing()
        {
            return SelectedServicingItem != null && checkAccess.AuthorizedAccess(checkAccess.tabName["Servicing"], checkAccess.getLetterDel);
        }

        public ICommand DeleteServicing
        {
            get
            {
                return new RelayCommand(_DeleteServicing, _CanExecuteDeleteServicing);
            }
        }

        /// <summary>
        /// Delete servicing
        /// </summary>
        public void _DeleteServicing()
        {
            // Initialize the variables
            string message = string.Empty;
            Servicing servicing = GetSelectedServicing();

            // Configure the message box to be displayed
            if (servicing != null)
                message = string.Format("Voulez-vous vraiment supprimer tous le service sur le véhicule : {0} ?", servicing.idvehicle.name);
            string messageBoxText = message;
            string caption = "Suppression du service";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;

            // Display message box
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        if (RS.DeleteElement(servicing))
                        {
                            MessageStatusRequestToDB = SpecialInformationMessage.deletestatusmessage;
                            setTimerStatus();
                            _CleanData();
                            ReloadAllServicing();
                        }
                        return;
                    }
                case MessageBoxResult.No:
                    return;
                case MessageBoxResult.Cancel:
                    return;
            }
        }

        public bool _CanExecuteJoinFilesToServicing()
        {
            return SelectedServicingItem != null;
        }

        public ICommand JoinFilesToServicing
        {
            get
            {
                return new RelayCommand(_JoinFilesToServicing, _CanExecuteJoinFilesToServicing);
            }
        }

        /// <summary>
        /// Open dialog for join files to servicing selected
        /// </summary>
        public void _JoinFilesToServicing()
        {
            string directory = ConfigurationManager.AppSettings[SpecialInformationMessage.FileDirectoryServicing].ToString();

            if (!string.IsNullOrEmpty(directory))
            {
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                if (Directory.Exists(directory))
                {
                    string directoryElement = directory + @"\" + SelectedServicingItem.Detail.id;

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

        public bool _CanExecuteDisplayFilesServicing()
        {
            return SelectedServicingItem != null && checker.CheckIfFolderExist(SpecialInformationMessage.FileDirectoryServicing, SelectedServicingItem.Detail.id);
        }

        public ICommand DisplayFilesServicing
        {
            get
            {
                return new RelayCommand(_DisplayFilesServicing, _CanExecuteDisplayFilesServicing);
            }
        }

        /// <summary>
        /// Display the files for the servicing selected
        /// </summary>
        public void _DisplayFilesServicing()
        {
            string directory = ConfigurationManager.AppSettings[SpecialInformationMessage.FileDirectoryServicing].ToString();

            if (!string.IsNullOrEmpty(directory))
            {
                if (Directory.Exists(directory))
                {
                    string directoryElement = directory + @"\" + SelectedServicingItem.Detail.id;

                    if (!string.IsNullOrEmpty(directoryElement))
                        if (Directory.Exists(directoryElement))
                        {
                            DirectoryInfo directoryElem = new DirectoryInfo(directoryElement);
                            FileInfo[] files = directoryElem.GetFiles();

                            // Open AllFilesServicing window
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

        public ICommand ServicingFilter
        {
            get
            {
                return new RelayCommand(_ServicingFilter, null);
            }
        }

        /// <summary>
        /// Filter for the servicing
        /// </summary>
        public void _ServicingFilter()
        {
            // Initialize the variables
            List<DetailViewModel<ServicingModel>> servicingRemove = new List<DetailViewModel<ServicingModel>>();

            // Get value for the different variables
            Vehicle vehicle = GetSelectedVehicle();
            DateTime dateservicing = SelectedDateServicingItem;
            Provider provider = GetSelectedProvider();
            float price = SelectedPriceItem;
            string description = SelectedDescriptionItem;
            TypeServicing type = GetSelectedTypeServicing();
            int km = SelectedKmItem;

            foreach (var servicing in MainCollectionViewModel._Servicing)
            {
                if (vehicle != null)
                    if (servicing.Detail.idvehicle.id != vehicle.id)
                        servicingRemove.Add(servicing);

                if (dateservicing.Date != DateTime.Now.Date)
                    if (servicing.Detail.dateservicing.Date != dateservicing.Date)
                        servicingRemove.Add(servicing);

                if (provider != null)
                    if (servicing.Detail.idprovider.id != provider.id)
                        servicingRemove.Add(servicing);

                if (price > 0.00f)
                    if (servicing.Detail.price != price)
                        servicingRemove.Add(servicing);

                if (!string.IsNullOrEmpty(description))
                    if (!servicing.Detail.description.ToLower().Contains(description.ToLower()))
                        servicingRemove.Add(servicing);

                if (type != null)
                    if (servicing.Detail.idtypeservicing.id != type.id)
                        servicingRemove.Add(servicing);

                if (km > 0)
                    if (servicing.Detail.km != km)
                        servicingRemove.Add(servicing);
            }

            foreach (var servicing in servicingRemove)
            {
                MainCollectionViewModel._Servicing.Remove(servicing);
            }
        }

        public ICommand ResetServicingFilter
        {
            get
            {
                return new RelayCommand(_ResetServicingFilter, null);
            }
        }

        /// <summary>
        /// Reset the filter for the servicing
        /// </summary>
        public void _ResetServicingFilter()
        {
            _CleanData();
            ReloadAllServicing();
        }
    }
}
