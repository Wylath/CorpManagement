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
    class AllVehicleViewModel : ObservableObject
    {
        private readonly RequestDBVehicle RV;
        private readonly RequestDBServicing RS;
        private readonly CheckerForFolder checker;
        private readonly CheckRightAccess checkAccess;
        private readonly DispatcherTimer dtClockTime;
        private readonly DispatcherTimer dtResetMessage;
        private int incrementalMessage = 0;
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
        private DetailViewModel<VehicleModel> _SelectedVehicleItem;

        public DetailViewModel<VehicleModel> SelectedVehicleItem
        {
            get
            {
                return _SelectedVehicleItem;
            }
            set
            {
                if (value != _SelectedVehicleItem)
                {
                    _SelectedVehicleItem = value;

                    if (value != null)
                    {
                        SelectedDescriptionItem = value.Detail.description;
                        SelectedKmItem = value.Detail.kmlastcontrol;
                        SelectedVehicleTypeItem = value.Detail.vehicletype;
                        SelectedSaleDateItem = value.Detail.saledate;
                        SelectedLastControlItem = value.Detail.lastcontrol;
                        SelectedNextControlItem = value.Detail.nextcontrol;
                        if (value.Detail.idtires != null)
                            SelectedTiresItem = new DetailViewModel<TiresModel>(new TiresModel(value.Detail.idtires));
                    }
                    else
                    {
                        SelectedDescriptionItem = string.Empty;
                        SelectedKmItem = 0;
                        SelectedVehicleTypeItem = string.Empty;
                        SelectedSaleDateItem = DateTime.Now;
                        SelectedLastControlItem = DateTime.Now;
                        SelectedNextControlItem = DateTime.Now;
                    }
                    RaisePropertyChanged(() => SelectedVehicleItem);
                }
            }
        }

        private string _SelectedVehicleNameItem;

        public string SelectedVehicleNameItem
        {
            get
            {
                return _SelectedVehicleNameItem;
            }
            set
            {
                if (value != _SelectedVehicleNameItem)
                {
                    _SelectedVehicleNameItem = value;
                    RaisePropertyChanged(() => SelectedVehicleNameItem);
                }
            }
        }

        private string _SelectedNumberPlateItem;

        public string SelectedNumberPlateItem
        {
            get
            {
                return _SelectedNumberPlateItem;
            }
            set
            {
                if (value != _SelectedNumberPlateItem)
                {
                    _SelectedNumberPlateItem = value;
                    RaisePropertyChanged(() => SelectedNumberPlateItem);
                }
            }
        }

        private DetailViewModel<PoliceLocalityModel> _SelectedLocalityItem;

        public DetailViewModel<PoliceLocalityModel> SelectedLocalityItem
        {
            get
            {
                return _SelectedLocalityItem;
            }
            set
            {
                if (value != _SelectedLocalityItem)
                {
                    _SelectedLocalityItem = value;
                    RaisePropertyChanged(() => SelectedLocalityItem);
                }
            }
        }

        private DetailViewModel<TiresModel> _SelectedTiresItem;

        public DetailViewModel<TiresModel> SelectedTiresItem
        {
            get
            {
                return _SelectedTiresItem;
            }
            set
            {
                if (value != _SelectedTiresItem)
                {
                    _SelectedTiresItem = value;
                    RaisePropertyChanged(() => SelectedTiresItem);
                }
            }
        }

        private DetailViewModel<FuelModel> _SelectedFuelItem;

        public DetailViewModel<FuelModel> SelectedFuelItem
        {
            get
            {
                return _SelectedFuelItem;
            }
            set
            {
                if (value != _SelectedFuelItem)
                {
                    _SelectedFuelItem = value;
                    RaisePropertyChanged(() => SelectedFuelItem);
                }
            }
        }

        private DetailViewModel<StatusVehicleModel> _SelectedStatusItem;

        public DetailViewModel<StatusVehicleModel> SelectedStatusItem
        {
            get
            {
                return _SelectedStatusItem;
            }
            set
            {
                if (value != _SelectedStatusItem)
                {
                    _SelectedStatusItem = value;
                    RaisePropertyChanged(() => SelectedStatusItem);
                }
            }
        }

        private DateTime _SelectedSaleDateItem;

        public DateTime SelectedSaleDateItem
        {
            get
            {
                if (_SelectedSaleDateItem.Year == 1)
                    _SelectedSaleDateItem = DateTime.Now;
                return _SelectedSaleDateItem;
            }
            set
            {
                if (value != _SelectedSaleDateItem)
                {
                    _SelectedSaleDateItem = value;
                    RaisePropertyChanged(() => SelectedSaleDateItem);
                }
            }
        }

        private DateTime _SelectedLastControlItem;

        public DateTime SelectedLastControlItem
        {
            get
            {
                if (_SelectedLastControlItem.Year == 1)
                    _SelectedLastControlItem = DateTime.Now;
                return _SelectedLastControlItem;
            }
            set
            {
                if (value != _SelectedLastControlItem)
                {
                    _SelectedLastControlItem = value;
                    RaisePropertyChanged(() => SelectedLastControlItem);
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

        private DateTime _SelectedNextControlItem;

        public DateTime SelectedNextControlItem
        {
            get
            {
                if (_SelectedNextControlItem.Year == 1)
                    _SelectedNextControlItem = DateTime.Now;
                return _SelectedNextControlItem;
            }
            set
            {
                if (value != _SelectedNextControlItem)
                {
                    _SelectedNextControlItem = value;
                    RaisePropertyChanged(() => SelectedNextControlItem);
                }
            }
        }

        private string _SelectedVehicleTypeItem;

        public string SelectedVehicleTypeItem
        {
            get
            {
                return _SelectedVehicleTypeItem;
            }
            set
            {
                if (value != _SelectedVehicleTypeItem)
                {
                    _SelectedVehicleTypeItem = value;
                    RaisePropertyChanged(() => SelectedVehicleTypeItem);
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

        private DetailViewModel<InsuranceVehicleModel> _SelectedInsuranceItem;

        public DetailViewModel<InsuranceVehicleModel> SelectedInsuranceItem
        {
            get
            {
                return _SelectedInsuranceItem;
            }
            set
            {
                if (value != _SelectedInsuranceItem)
                {
                    _SelectedInsuranceItem = value;
                    RaisePropertyChanged(() => SelectedInsuranceItem);
                }
            }
        }

        private string _WarningMessageForDataGrid;

        public string WarningMessageForDataGrid
        {
            get
            {
                return _WarningMessageForDataGrid;
            }
            set
            {
                if (value != _WarningMessageForDataGrid)
                {
                    _WarningMessageForDataGrid = value;
                    RaisePropertyChanged(() => WarningMessageForDataGrid);
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
        /// Constructor AllVehicle
        /// </summary>
        public AllVehicleViewModel()
        {
            // Initialize the request DB
            RV = new RequestDBVehicle();
            RS = new RequestDBServicing();

            // Initialize other class checker
            checker = new CheckerForFolder();
            checkAccess = new CheckRightAccess();

            // Initialize timer for the messages in the window
            dtClockTime = new DispatcherTimer();
            dtResetMessage = new DispatcherTimer();
            setTimerInterval();
            setWarningMessage(null, new EventArgs());
        }

        /// <summary>
        /// Return the selected vehicle
        /// </summary>
        /// <returns></returns>
        public Vehicle GetSelectedVehicle()
        {
            return (SelectedVehicleItem != null) ? new Vehicle(SelectedVehicleItem.Detail.id, SelectedVehicleItem.Detail.name, SelectedVehicleItem.Detail.numberplate, SelectedVehicleItem.Detail.idpolicelocality, SelectedVehicleItem.Detail.saledate, SelectedVehicleItem.Detail.lastcontrol, SelectedVehicleItem.Detail.kmlastcontrol, SelectedVehicleItem.Detail.nextcontrol, SelectedVehicleItem.Detail.idtires, SelectedVehicleItem.Detail.fueltype, SelectedVehicleItem.Detail.vehicletype, SelectedVehicleItem.Detail.status, SelectedVehicleItem.Detail.description, SelectedVehicleItem.Detail.idinsurance) : null;
        }

        /// <summary>
        /// Return the selected locality
        /// </summary>
        /// <returns></returns>
        public PoliceLocality GetSelectedPoliceLocality()
        {
            return (SelectedLocalityItem != null) ? new PoliceLocality(SelectedLocalityItem.Detail.id) : null;
        }

        /// <summary>
        /// Return the selected tires
        /// </summary>
        /// <returns></returns>
        public Tires GetSelectedTires()
        {
            return (SelectedTiresItem != null) ? new Tires(SelectedTiresItem.Detail.id) : null;
        }

        /// <summary>
        /// Return the selected fuel
        /// </summary>
        /// <returns></returns>
        public Fuel GetSelectedFuel()
        {
            return (SelectedFuelItem != null) ? new Fuel(SelectedFuelItem.Detail.id) : null;
        }

        /// <summary>
        /// Return the selected status
        /// </summary>
        /// <returns></returns>
        public StatusVehicle GetSelectedStatus()
        {
            return (SelectedStatusItem != null) ? new StatusVehicle(SelectedStatusItem.Detail.id) : null;
        }

        /// <summary>
        /// Return the selected insurance
        /// </summary>
        /// <returns></returns>
        public InsuranceVehicle GetSelectedInsurance()
        {
            return (SelectedInsuranceItem != null) ? new InsuranceVehicle(SelectedInsuranceItem.Detail.id, SelectedInsuranceItem.Detail.insurancenumber, SelectedInsuranceItem.Detail.idprovider, SelectedInsuranceItem.Detail.effectivedate, SelectedInsuranceItem.Detail.expiredate, SelectedInsuranceItem.Detail.active, SelectedInsuranceItem.Detail.coverage, SelectedInsuranceItem.Detail.price, SelectedInsuranceItem.Detail.description) : null;
        }

        /// <summary>
        /// Clean the different data information
        /// </summary>
        public void _CleanData()
        {
            SelectedVehicleItem = null;
            SelectedVehicleNameItem = string.Empty;
            SelectedNumberPlateItem = string.Empty;
            SelectedLocalityItem = null;
            SelectedSaleDateItem = DateTime.Now;
            SelectedLastControlItem = DateTime.Now;
            SelectedKmItem = 0;
            SelectedNextControlItem = DateTime.Now;
            SelectedTiresItem = null;
            SelectedFuelItem = null;
            SelectedVehicleTypeItem = string.Empty;
            SelectedStatusItem = null;
            SelectedDescriptionItem = string.Empty;
            SelectedInsuranceItem = null;
            // Warning message
            WarningMessageForDataGrid = string.Empty;
            incrementalMessage = 0;
        }

        /// <summary>
        /// Reload all vehicle in observableCollection Vehicle
        /// </summary>
        public void ReloadAllVehicle(bool OpenLastVehicleEncoded = false)
        {
            MainCollectionViewModel._Vehicle.Clear();

            foreach (var vehicle in RV.SelectAllElement())
            {
                MainCollectionViewModel._Vehicle.Add(new DetailViewModel<VehicleModel>(new VehicleModel(vehicle)));
            }

            setTimerInterval();
            setWarningMessage(null, new EventArgs());

            if (OpenLastVehicleEncoded)
            {
                SelectedVehicleItem = MainCollectionViewModel._Vehicle.Last();
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

        /// <summary>
        /// Set the timer for display the warning message
        /// </summary>
        private void setTimerInterval()
        {
            // Initialize the clocktime
            dtClockTime.Interval = checkAccess.timerDisplaySpecialMessage;
            // Set the function and start the clock time
            dtClockTime.Tick += setWarningMessage;
            dtClockTime.Start();
        }

        /// <summary>
        /// Display warning message or remove message for the next services
        /// </summary>
        private void setWarningMessage(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            int totalDayForWarning = 60; // 60 days for the warning / replace old DateTime.DaysInMonth(currentDate.Year, currentDate.Month)
            int totalfordisplay = 0;

            foreach (var vehicle in MainCollectionViewModel._Vehicle)
            {
                if ((vehicle.Detail.nextcontrol.DayOfYear - currentDate.DayOfYear) <= totalDayForWarning && currentDate.Year == vehicle.Detail.nextcontrol.Year)
                {
                    totalfordisplay += 1;
                }
            }

            int i = 0;

            if (totalfordisplay > 0 && incrementalMessage < totalfordisplay)
                foreach (var vehicle in MainCollectionViewModel._Vehicle)
                {
                    if ((vehicle.Detail.nextcontrol.DayOfYear - currentDate.DayOfYear) <= totalDayForWarning && currentDate.Year == vehicle.Detail.nextcontrol.Year)
                    {
                        if (i == incrementalMessage)
                        {
                            WarningMessageForDataGrid = string.Format(" Le véhicule : '{0}' devra bientôt passer au contrôle technique. ", vehicle.Detail.numberplate);
                            incrementalMessage += 1;
                            break;
                        }
                        i += 1;
                    }
                }

            if (incrementalMessage == totalfordisplay)
                incrementalMessage = 0;

            if (totalfordisplay == 0)
                dtClockTime.Stop();
        }

        /// <summary>
        /// Check if number plate is already used
        /// </summary>
        /// <param name="numberplate"></param>
        /// <returns></returns>
        private bool checkNumberPlate(string numberplate)
        {
            bool numberplateUsed = false;
            foreach(var vehicle in MainCollectionViewModel._Vehicle)
            {
                if (vehicle.Detail.numberplate.ToLower() == numberplate.ToLower())
                    numberplateUsed = true;
            }

            if (numberplateUsed)
            {
                // Configure the message box to be displayed
                string messageBoxText = string.Format("Le numéro d'immatriculation {0} est déjà utilisé. Voulez-vous continuer ou annuler ?", numberplate);
                string caption = "Vérification du numéro d'immatriculation";
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
        /// Check if the insurance is active
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        private bool checkIfInsuranceIsActive(bool active)
        {
            if (!active)
            {
                // Configure the message box to be displayed
                string messageBoxText = string.Format("La couverture d'assurance choisie est indiquée comme inactive, voulez-vous vraiment la lier au véhicule ?");
                string caption = "Assurance inactive";
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
        /// Check if the current km is equal to the last km in the servicing for the vehicle
        /// </summary>
        /// <param name="currentkm"></param>
        /// <returns></returns>
        private int checkKmLastControl(Vehicle vehicle, int lastvalueinput)
        {
            if (vehicle == null)
                return lastvalueinput;

            bool updatekm = false;
            int maxvalue = RS.SelectMaxKmServicingByVehicle(vehicle);

            if (lastvalueinput < maxvalue)
                updatekm = true;

            if (updatekm)
            {
                // Configure the message box to be displayed
                string messageBoxText = string.Format("Un kilométrage plus élevé a été trouvé dans les derniers services effectués sur le véhicule. Voulez-vous mettre à jour le kilométrage ?");
                string caption = "Kilométrage du véhicule";
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
                        return lastvalueinput;
                }
            }
            
            return lastvalueinput;
        }

        public bool _CanExecuteInsertVehicle()
        {
            return (SelectedVehicleItem != null || !string.IsNullOrEmpty(SelectedVehicleNameItem)) && SelectedNumberPlateItem != null && SelectedLocalityItem != null
                && SelectedFuelItem != null && SelectedStatusItem != null && checkAccess.AuthorizedAccess(checkAccess.tabName["Vehicle"], checkAccess.getLetterAdd);
        }

        public ICommand InsertVehicle
        {
            get
            {
                return new RelayCommand(_InsertVehicle, _CanExecuteInsertVehicle);
            }
        }

        /// <summary>
        /// Insert new vehicle
        /// </summary>
        public void _InsertVehicle()
        {
            // Initialize the variables
            // Get value by selected item
            string name = SelectedVehicleNameItem;
            PoliceLocality locality = GetSelectedPoliceLocality();
            Tires tires = GetSelectedTires();
            Fuel fuel = GetSelectedFuel();
            StatusVehicle status = GetSelectedStatus();
            InsuranceVehicle insurance = GetSelectedInsurance();
            string numberplate = SelectedNumberPlateItem;
            DateTime saledate = SelectedSaleDateItem;
            DateTime lastcontrol = SelectedLastControlItem;
            int km = SelectedKmItem;
            DateTime nextcontrol = SelectedNextControlItem;
            string vehicletype = !string.IsNullOrEmpty(SelectedVehicleTypeItem) ? SelectedVehicleTypeItem : string.Empty;
            string description = !string.IsNullOrEmpty(SelectedDescriptionItem) ? SelectedDescriptionItem : string.Empty;

            Vehicle vehicle = new Vehicle(name, numberplate, locality, saledate, lastcontrol, km, nextcontrol, tires, fuel, vehicletype, status, description, insurance);

            bool numberplateinsert = true;
            bool insuranceinsert = true;
            if (!string.IsNullOrEmpty(numberplate))
                numberplateinsert = checkNumberPlate(numberplate);

            if (insurance != null)
                insuranceinsert = checkIfInsuranceIsActive(insurance.active);

            if (numberplateinsert && insuranceinsert)
                if (RV.InsertNewElement(vehicle))
                {
                    MessageStatusRequestToDB = SpecialInformationMessage.addstatusmessage;
                    setTimerStatus();
                    _CleanData();
                    ReloadAllVehicle(true);
                }
        }

        public bool _CanExecuteUpdateVehicle()
        {
            return SelectedVehicleItem != null && !string.IsNullOrEmpty(SelectedVehicleNameItem) && SelectedNumberPlateItem != null && SelectedLocalityItem != null
                && SelectedFuelItem != null && SelectedStatusItem != null && checkAccess.AuthorizedAccess(checkAccess.tabName["Vehicle"], checkAccess.getLetterUpd);
        }

        public ICommand UpdateVehicle
        {
            get
            {
                return new RelayCommand(_UpdateVehicle, _CanExecuteUpdateVehicle);
            }
        }

        /// <summary>
        /// Update vehicle
        /// </summary>
        public void _UpdateVehicle()
        {
            // Initialize the variables
            // Get value by selected item
            int id = GetSelectedVehicle().id;
            string name = !string.IsNullOrEmpty(SelectedVehicleNameItem) ? SelectedVehicleNameItem : GetSelectedVehicle().name;
            string numberplate = !string.IsNullOrEmpty(SelectedNumberPlateItem) ? SelectedNumberPlateItem : SelectedVehicleItem.Detail.numberplate;
            PoliceLocality locality = GetSelectedPoliceLocality() ?? SelectedVehicleItem.Detail.idpolicelocality;
            DateTime saledate = (SelectedSaleDateItem != null) ? SelectedSaleDateItem : SelectedVehicleItem.Detail.saledate;
            DateTime lastcontrol = (SelectedLastControlItem != null) ? SelectedLastControlItem : SelectedVehicleItem.Detail.lastcontrol;
            int kmconstrol = SelectedKmItem > 0 ? SelectedKmItem : SelectedVehicleItem.Detail.kmlastcontrol;
            DateTime nextcontrol = (SelectedNextControlItem != null) ? SelectedNextControlItem : SelectedVehicleItem.Detail.nextcontrol;
            Tires tires = GetSelectedTires() ?? SelectedVehicleItem.Detail.idtires;
            Fuel fuel = GetSelectedFuel() ?? SelectedVehicleItem.Detail.fueltype;
            string vehicletype = !string.IsNullOrEmpty(SelectedVehicleTypeItem) ? SelectedVehicleTypeItem : SelectedVehicleItem.Detail.vehicletype;
            StatusVehicle status = GetSelectedStatus() ?? SelectedVehicleItem.Detail.status;
            string description = !string.IsNullOrEmpty(SelectedDescriptionItem) ? SelectedDescriptionItem : SelectedVehicleItem.Detail.description;
            InsuranceVehicle insurance = GetSelectedInsurance() ?? SelectedVehicleItem.Detail.idinsurance;

            // Check km in servicing
            kmconstrol = checkKmLastControl(GetSelectedVehicle(), kmconstrol);

            Vehicle vehicle = new Vehicle(id, name, numberplate, locality, saledate, lastcontrol, kmconstrol, nextcontrol, tires, fuel, vehicletype, status, description, insurance);

            bool numberplatevalid = true;
            bool insuranceactive = true;

            if (!string.IsNullOrEmpty(SelectedNumberPlateItem))
                if (SelectedVehicleItem.Detail.numberplate.ToLower() != SelectedNumberPlateItem.ToLower())
                    numberplatevalid = checkNumberPlate(SelectedNumberPlateItem);

            if (GetSelectedInsurance() != null && SelectedVehicleItem.Detail.idinsurance != null)
            {
                if (SelectedVehicleItem.Detail.idinsurance.active != GetSelectedInsurance().active)
                    insuranceactive = checkIfInsuranceIsActive(GetSelectedInsurance().active);
            } else if (GetSelectedInsurance() != null && SelectedVehicleItem.Detail.idinsurance == null)
            {
                insuranceactive = checkIfInsuranceIsActive(GetSelectedInsurance().active);
            }

            if (numberplatevalid && insuranceactive)
                if (RV.UpdateElement(vehicle))
                {
                    MessageStatusRequestToDB = SpecialInformationMessage.updatestatusmessage;
                    setTimerStatus();
                    _CleanData();
                    ReloadAllVehicle();
                }
        }

        public bool _CanExecuteDeleteVehicle()
        {
            return SelectedVehicleItem != null && checkAccess.AuthorizedAccess(checkAccess.tabName["Vehicle"], checkAccess.getLetterDel);
        }

        public ICommand DeleteVehicle
        {
            get
            {
                return new RelayCommand(_DeleteVehicle, _CanExecuteDeleteVehicle);
            }
        }

        /// <summary>
        /// Delete vehicle
        /// </summary>
        public void _DeleteVehicle()
        {
            // Initialize the variables
            Vehicle vehicle = GetSelectedVehicle();

            // Configure the message box to be displayed
            string messageBoxText = string.Format("Voulez-vous vraiment supprimer le véhicule {0} immatriculé {1} ?", vehicle.name, vehicle.numberplate);
            string caption = "Suppression du véhicule";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;

            // Display message box
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        if (RV.DeleteElement(vehicle))
                        {
                            MessageStatusRequestToDB = SpecialInformationMessage.deletestatusmessage;
                            setTimerStatus();
                            _CleanData();
                            ReloadAllVehicle();
                        }
                        return;
                    }
                case MessageBoxResult.No:
                    return;
                case MessageBoxResult.Cancel:
                    return;
            }
        }

        public bool _CanExecuteJoinFilesToVehicle()
        {
            return SelectedVehicleItem != null;
        }

        public ICommand JoinFilesToVehicle
        {
            get
            {
                return new RelayCommand(_JoinFilesToVehicle, _CanExecuteJoinFilesToVehicle);
            }
        }

        /// <summary>
        /// Open dialog for join files to vehicle selected
        /// </summary>
        public void _JoinFilesToVehicle()
        {
            string directory = ConfigurationManager.AppSettings[SpecialInformationMessage.FileDirectoryVehicle].ToString();

            if (!string.IsNullOrEmpty(directory))
            {
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                if (Directory.Exists(directory))
                {
                    string directoryElement = directory + @"\" + SelectedVehicleItem.Detail.id;

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

        public bool _CanExecuteDisplayFilesVehicle()
        {
            return SelectedVehicleItem != null && checker.CheckIfFolderExist(SpecialInformationMessage.FileDirectoryVehicle, SelectedVehicleItem.Detail.id);
        }

        public ICommand DisplayFilesVehicle
        {
            get
            {
                return new RelayCommand(_DisplayFilesVehicle, _CanExecuteDisplayFilesVehicle);
            }
        }

        /// <summary>
        /// Display the files for the vehicle selected
        /// </summary>
        public void _DisplayFilesVehicle()
        {
            string directory = ConfigurationManager.AppSettings[SpecialInformationMessage.FileDirectoryVehicle].ToString();

            if (!string.IsNullOrEmpty(directory))
            {
                if (Directory.Exists(directory))
                {
                    string directoryElement = directory + @"\" + SelectedVehicleItem.Detail.id;

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

        public ICommand VehicleFilter
        {
            get
            {
                return new RelayCommand(_VehicleFilter, null);
            }
        }

        /// <summary>
        /// Filter for the vehicle
        /// </summary>
        public void _VehicleFilter()
        {
            // Initialize the variables
            List<DetailViewModel<VehicleModel>> vehicleRemove = new List<DetailViewModel<VehicleModel>>();

            // Get value for the different variables
            string name = SelectedVehicleNameItem;
            PoliceLocality locality = GetSelectedPoliceLocality();
            Tires tires = GetSelectedTires();
            Fuel fuel = GetSelectedFuel();
            StatusVehicle status = GetSelectedStatus();
            InsuranceVehicle insurance = GetSelectedInsurance();
            string numberplate = SelectedNumberPlateItem;
            DateTime saledate = SelectedSaleDateItem;
            DateTime lastcontrol = SelectedLastControlItem;
            int km = SelectedKmItem;
            DateTime nextcontrol = SelectedNextControlItem;
            string vehicletype = SelectedVehicleTypeItem;
            string description = SelectedDescriptionItem;

            foreach (var vehicle in MainCollectionViewModel._Vehicle)
            {
                if (!string.IsNullOrEmpty(name))
                    if (!vehicle.Detail.name.ToLower().Contains(name.ToLower()))
                        vehicleRemove.Add(vehicle);

                if (locality != null)
                    if (vehicle.Detail.idpolicelocality.id != locality.id)
                        vehicleRemove.Add(vehicle);

                if (tires != null)
                {
                    if (vehicle.Detail.idtires == null)
                        vehicleRemove.Add(vehicle);
                    else if (vehicle.Detail.idtires.id != tires.id)
                        vehicleRemove.Add(vehicle);
                }

                if (fuel != null)
                    if (vehicle.Detail.fueltype.id != fuel.id)
                        vehicleRemove.Add(vehicle);

                if (status != null)
                    if (vehicle.Detail.status.id != status.id)
                        vehicleRemove.Add(vehicle);

                if (insurance != null)
                {
                    if (vehicle.Detail.idinsurance == null)
                        vehicleRemove.Add(vehicle);
                    else if (vehicle.Detail.idinsurance.id != insurance.id)
                        vehicleRemove.Add(vehicle);
                }

                if (!string.IsNullOrEmpty(numberplate))
                    if (!vehicle.Detail.numberplate.ToLower().Contains(numberplate.ToLower()))
                        vehicleRemove.Add(vehicle);

                if (saledate.Date != DateTime.Now.Date)
                    if (vehicle.Detail.saledate.Date != saledate.Date)
                        vehicleRemove.Add(vehicle);

                if (lastcontrol.Date != DateTime.Now.Date)
                    if (vehicle.Detail.lastcontrol.Date != lastcontrol.Date)
                        vehicleRemove.Add(vehicle);

                if (km > 0)
                    if (vehicle.Detail.kmlastcontrol != km)
                        vehicleRemove.Add(vehicle);

                if (nextcontrol.Date != DateTime.Now.Date)
                    if (vehicle.Detail.nextcontrol.Date != nextcontrol.Date)
                        vehicleRemove.Add(vehicle);

                if (!string.IsNullOrEmpty(vehicletype))
                    if (!vehicle.Detail.vehicletype.ToLower().Contains(vehicletype.ToLower()))
                        vehicleRemove.Add(vehicle);

                if (!string.IsNullOrEmpty(description))
                    if (!vehicle.Detail.description.ToLower().Contains(description.ToLower()))
                        vehicleRemove.Add(vehicle);
            }

            foreach (var vehicle in vehicleRemove)
            {
                MainCollectionViewModel._Vehicle.Remove(vehicle);
            }
        }

        public ICommand ResetVehicleFilter
        {
            get
            {
                return new RelayCommand(_ResetVehicleFilter, null);
            }
        }

        /// <summary>
        /// Reset the filter for the vehicle
        /// </summary>
        public void _ResetVehicleFilter()
        {
            _CleanData();
            ReloadAllVehicle();
        }
    }
}
