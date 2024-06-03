using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using CorpManagement.DB;
using CorpManagement.Model;
using CorpManagement.ToolBox;

namespace CorpManagement.ViewModel
{
    class AllPoliceLocalityViewModel : ObservableObject
    {
        private readonly RequestDBPoliceLocality RPL;
        private readonly CheckRightAccess checkAccess;
        private readonly DispatcherTimer dtResetMessage;

        #region SelectedItem
        private DetailViewModel<PoliceLocalityModel> _SelectedPoliceLocalityItem;

        public DetailViewModel<PoliceLocalityModel> SelectedPoliceLocalityItem
        {
            get
            {
                return _SelectedPoliceLocalityItem;
            }
            set
            {
                if (value != _SelectedPoliceLocalityItem)
                {
                    _SelectedPoliceLocalityItem = value;
                    RaisePropertyChanged(() => SelectedPoliceLocalityItem);
                }
            }
        }

        private string _SelectedNameItem;

        public string SelectedNameItem
        {
            get
            {
                return _SelectedNameItem;
            }
            set
            {
                if (value != _SelectedNameItem)
                {
                    _SelectedNameItem = value;
                    RaisePropertyChanged(() => SelectedNameItem);
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
        /// Constructor AllPoliceLocality
        /// </summary>
        public AllPoliceLocalityViewModel()
        {
            // Initialize the request DB
            RPL = new RequestDBPoliceLocality();

            // Initialize other class checker
            checkAccess = new CheckRightAccess();

            // Initialize timer for the messages in the window
            dtResetMessage = new DispatcherTimer();
        }

        /// <summary>
        /// Return the selected police locality
        /// </summary>
        /// <returns></returns>
        public PoliceLocality GetSelectedPoliceLocality()
        {
            return (SelectedPoliceLocalityItem != null) ? new PoliceLocality(SelectedPoliceLocalityItem.Detail.id, SelectedPoliceLocalityItem.Detail.name) : null;
        }

        /// <summary>
        /// Clean the different data information
        /// </summary>
        public void _CleanData()
        {
            SelectedPoliceLocalityItem = null;
            SelectedNameItem = string.Empty;
        }

        /// <summary>
        /// Reload all tire in observableCollection Police locality
        /// </summary>
        public void ReloadAllPoliceLocality(bool OpenLastPoliceLocalityEncoded = false)
        {
            MainCollectionViewModel._PoliceLocality.Clear();

            foreach (var policelocality in RPL.SelectAllElement())
            {
                MainCollectionViewModel._PoliceLocality.Add(new DetailViewModel<PoliceLocalityModel>(new PoliceLocalityModel(policelocality)));
            }

            if (OpenLastPoliceLocalityEncoded)
            {
                SelectedPoliceLocalityItem = MainCollectionViewModel._PoliceLocality.Last();
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

        public bool _CanExecuteAddPoliceLocality()
        {
            return (!string.IsNullOrEmpty(SelectedNameItem)) && checkAccess.AuthorizedAccess(checkAccess.tabName["PoliceLocality"], checkAccess.getLetterAdd);
        }

        public ICommand AddPoliceLocality
        {
            get
            {
                return new RelayCommand(_AddPoliceLocality, _CanExecuteAddPoliceLocality);
            }
        }

        /// <summary>
        /// Add police locality
        /// </summary>
        public void _AddPoliceLocality()
        {
            // Initialize the variables
            // Get value by selected item
            string name = SelectedNameItem;

            PoliceLocality policelocality = new PoliceLocality(name);

            if (RPL.InsertNewElement(policelocality))
            {
                MessageStatusRequestToDB = SpecialInformationMessage.addstatusmessage;
                setTimerStatus();
                _CleanData();
                ReloadAllPoliceLocality(true);
            }
        }

        public bool _CanExecuteUpdatePoliceLocality()
        {
            return SelectedPoliceLocalityItem != null && checkAccess.AuthorizedAccess(checkAccess.tabName["PoliceLocality"], checkAccess.getLetterUpd);
        }

        public ICommand UpdatePoliceLocality
        {
            get
            {
                return new RelayCommand(_UpdatePoliceLocality, _CanExecuteUpdatePoliceLocality);
            }
        }

        /// <summary>
        /// Update police locality
        /// </summary>
        public void _UpdatePoliceLocality()
        {
            // Initialize the variables
            // Get value by selected item
            int id = GetSelectedPoliceLocality().id;
            string name = !string.IsNullOrEmpty(SelectedNameItem) ? SelectedNameItem : GetSelectedPoliceLocality().name;

            PoliceLocality policelocality = new PoliceLocality(id, name);

            if (RPL.UpdateElement(policelocality))
            {
                MessageStatusRequestToDB = SpecialInformationMessage.updatestatusmessage;
                setTimerStatus();
                _CleanData();
                ReloadAllPoliceLocality();
            }
        }

        public bool _CanExecuteDeletePoliceLocality()
        {
            return SelectedPoliceLocalityItem != null && checkAccess.AuthorizedAccess(checkAccess.tabName["PoliceLocality"], checkAccess.getLetterDel);
        }

        public ICommand DeletePoliceLocality
        {
            get
            {
                return new RelayCommand(_DeletePoliceLocality, _CanExecuteDeletePoliceLocality);
            }
        }

        /// <summary>
        /// Delete provider
        /// </summary>
        public void _DeletePoliceLocality()
        {
            // Initialize the variables
            PoliceLocality policelocality = GetSelectedPoliceLocality();

            // Configure the message box to be displayed
            string messageBoxText = string.Format("Voulez-vous vraiment supprimer la proximité {0} ?", policelocality.name);
            string caption = "Suppression de la proximité";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;

            // Display message box
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        if (RPL.DeleteElement(policelocality))
                        {
                            MessageStatusRequestToDB = SpecialInformationMessage.deletestatusmessage;
                            setTimerStatus();
                            _CleanData();
                            ReloadAllPoliceLocality();
                        }
                        return;
                    }
                case MessageBoxResult.No:
                    return;
                case MessageBoxResult.Cancel:
                    return;
            }
        }

        public ICommand CloseWindowPoliceLocality
        {
            get
            {
                return new RelayCommand(_CloseWindowPoliceLocality, null);
            }
        }

        /// <summary>
        /// Close the window with the police locality
        /// </summary>
        /// <param name="parameter"></param>
        public void _CloseWindowPoliceLocality(object parameter)
        {
            if (parameter != null)
            {
                _CleanData();
                ReloadAllPoliceLocality();
                Window win = (Window)parameter;
                win.Close();
            }
        }
    }
}
