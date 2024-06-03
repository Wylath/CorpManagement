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
    class AllTiresViewModel : ObservableObject
    {
        private readonly RequestDBTires RT;
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
                    if (value != null)
                    {
                        SelectedDescriptionItem = value.Detail.description;
                    }
                    else
                    {
                        SelectedDescriptionItem = string.Empty;
                    }
                    RaisePropertyChanged(() => SelectedTiresItem);
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

        private DetailViewModel<StatusTireModel> _SelectedStateItem;

        public DetailViewModel<StatusTireModel> SelectedStateItem
        {
            get
            {
                return _SelectedStateItem;
            }
            set
            {
                if (value != _SelectedStateItem)
                {
                    _SelectedStateItem = value;
                    RaisePropertyChanged(() => SelectedStateItem);
                }
            }
        }

        private int _SelectedSetNumberItem;

        public int SelectedSetNumberItem
        {
            get
            {
                return _SelectedSetNumberItem;
            }
            set
            {
                if (value != _SelectedSetNumberItem)
                {
                    _SelectedSetNumberItem = value;
                    RaisePropertyChanged(() => SelectedSetNumberItem);
                }
            }
        }

        private string _SelectedDim1Item;

        public string SelectedDim1Item
        {
            get
            {
                return _SelectedDim1Item;
            }
            set
            {
                if (value != _SelectedDim1Item)
                {
                    _SelectedDim1Item = value;
                    RaisePropertyChanged(() => SelectedDim1Item);
                }
            }
        }

        private string _SelectedDim2Item;

        public string SelectedDim2Item
        {
            get
            {
                return _SelectedDim2Item;
            }
            set
            {
                if (value != _SelectedDim2Item)
                {
                    _SelectedDim2Item = value;
                    RaisePropertyChanged(() => SelectedDim2Item);
                }
            }
        }

        private string _SelectedDim3Item;

        public string SelectedDim3Item
        {
            get
            {
                return _SelectedDim3Item;
            }
            set
            {
                if (value != _SelectedDim3Item)
                {
                    _SelectedDim3Item = value;
                    RaisePropertyChanged(() => SelectedDim3Item);
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
        /// Constructor AllTires
        /// </summary>
        public AllTiresViewModel()
        {
            // Initialize the request DB
            RT = new RequestDBTires();

            // Initialize other class checker
            checker = new CheckerForFolder();
            checkAccess = new CheckRightAccess();

            // Initialize timer for the messages in the window
            dtResetMessage = new DispatcherTimer();
        }

        /// <summary>
        /// Return the selected tire
        /// </summary>
        /// <returns></returns>
        public Tires GetSelectedTire()
        {
            return (SelectedTiresItem != null) ? new Tires(SelectedTiresItem.Detail.id, SelectedTiresItem.Detail.name, SelectedTiresItem.Detail.state, SelectedTiresItem.Detail.description, SelectedTiresItem.Detail.setnumber, SelectedTiresItem.Detail.Dim1, SelectedTiresItem.Detail.Dim2, SelectedTiresItem.Detail.Dim3) : null;
        }

        /// <summary>
        /// Return the selected status tires
        /// </summary>
        /// <returns></returns>
        public StatusTire GetSelectedStateItem()
        {
            return (SelectedStateItem != null) ? new StatusTire(SelectedStateItem.Detail.id, SelectedStateItem.Detail.name) : null;
        }

        /// <summary>
        /// Clean the different data information
        /// </summary>
        public void _CleanData()
        {
            SelectedTiresItem = null;
            SelectedNameItem = string.Empty;
            SelectedStateItem = null;
            SelectedSetNumberItem = 0;
            SelectedDim1Item = string.Empty;
            SelectedDim2Item = string.Empty;
            SelectedDim3Item = string.Empty;
            SelectedDescriptionItem = string.Empty;
        }

        /// <summary>
        /// Reload all tire in observableCollection Tires
        /// </summary>
        public void ReloadAllTire(bool OpenLastTiresEncoded = false)
        {
            MainCollectionViewModel._Tires.Clear();

            foreach (var tires in RT.SelectAllElement())
            {
                MainCollectionViewModel._Tires.Add(new DetailViewModel<TiresModel>(new TiresModel(tires)));
            }

            if (OpenLastTiresEncoded)
            {
                SelectedTiresItem = MainCollectionViewModel._Tires.Last();
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
        /// Check if set number is already used
        /// </summary>
        /// <param name="setnumber"></param>
        /// <returns></returns>
        private bool checkSetNumber(int setnumber)
        {
            bool setNumberUsed = false;
            foreach (var tires in MainCollectionViewModel._Tires)
            {
                if (tires.Detail.setnumber == setnumber)
                    setNumberUsed = true;
            }

            if (setNumberUsed)
            {
                // Configure the message box to be displayed
                string messageBoxText = string.Format("Le numéro de set {0} est déjà utilisé. Voulez-vous continuer ou annuler ?", setnumber);
                string caption = "Vérification numéro de set";
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

        public bool _CanExecuteAddTires()
        {
            return !string.IsNullOrEmpty(SelectedNameItem) && GetSelectedStateItem() != null && SelectedSetNumberItem > 0 && checkAccess.AuthorizedAccess(checkAccess.tabName["Tires"], checkAccess.getLetterAdd);
        }

        public ICommand AddTires
        {
            get
            {
                return new RelayCommand(_AddTires, _CanExecuteAddTires);
            }
        }

        /// <summary>
        /// Add tires
        /// </summary>
        public void _AddTires()
        {
            // Initialize the variables
            // Get value by selected item
            string name = SelectedNameItem;
            StatusTire state = GetSelectedStateItem();
            int setnumber = SelectedSetNumberItem;
            string dim1 = !string.IsNullOrEmpty(SelectedDim1Item) ? SelectedDim1Item : string.Empty;
            string dim2 = !string.IsNullOrEmpty(SelectedDim2Item) ? SelectedDim2Item : string.Empty;
            string dim3 = !string.IsNullOrEmpty(SelectedDim3Item) ? SelectedDim3Item : string.Empty;
            string description = !string.IsNullOrEmpty(SelectedDescriptionItem) ? SelectedDescriptionItem : string.Empty;

            Tires tire = new Tires(name, state, description, setnumber, dim1, dim2, dim3);

            if (checkSetNumber(setnumber))
                if (RT.InsertNewElement(tire))
                {
                    MessageStatusRequestToDB = SpecialInformationMessage.addstatusmessage;
                    setTimerStatus();
                    _CleanData();
                    ReloadAllTire(true);
                }
        }

        public bool _CanExecuteUpdateTires()
        {
            return SelectedTiresItem != null && !string.IsNullOrEmpty(SelectedNameItem) && GetSelectedStateItem() != null && SelectedSetNumberItem > 0 && checkAccess.AuthorizedAccess(checkAccess.tabName["Tires"], checkAccess.getLetterUpd);
        }

        public ICommand UpdateTires
        {
            get
            {
                return new RelayCommand(_UpdateTires, _CanExecuteUpdateTires);
            }
        }

        /// <summary>
        /// Update user
        /// </summary>
        public void _UpdateTires()
        {
            // Initialize the variables
            // Get value by selected item
            int id = GetSelectedTire().id;
            string name = !string.IsNullOrEmpty(SelectedNameItem) ? SelectedNameItem : GetSelectedTire().name;
            StatusTire state = GetSelectedStateItem() ?? GetSelectedTire().state;
            int setnumber = SelectedSetNumberItem > 0 ? SelectedSetNumberItem : GetSelectedTire().setnumber;
            string dim1 = !string.IsNullOrEmpty(SelectedDim1Item) ? SelectedDim1Item : GetSelectedTire().Dim1;
            string dim2 = !string.IsNullOrEmpty(SelectedDim2Item) ? SelectedDim2Item : GetSelectedTire().Dim2;
            string dim3 = !string.IsNullOrEmpty(SelectedDim3Item) ? SelectedDim3Item : GetSelectedTire().Dim3;
            string description = !string.IsNullOrEmpty(SelectedDescriptionItem) ? SelectedDescriptionItem : GetSelectedTire().description;

            Tires tire = new Tires(id, name, state, description, setnumber, dim1, dim2, dim3);

            bool tiresvalid = true;

            if (SelectedSetNumberItem > 0)
                if (GetSelectedTire().setnumber != SelectedSetNumberItem)
                    tiresvalid = checkSetNumber(SelectedSetNumberItem);

            if (tiresvalid)
                if (RT.UpdateElement(tire))
                {
                    MessageStatusRequestToDB = SpecialInformationMessage.updatestatusmessage;
                    setTimerStatus();
                    _CleanData();
                    ReloadAllTire();
                }
        }

        public bool _CanExecuteDeleteTires()
        {
            return SelectedTiresItem != null && checkAccess.AuthorizedAccess(checkAccess.tabName["Tires"], checkAccess.getLetterDel);
        }

        public ICommand DeleteTires
        {
            get
            {
                return new RelayCommand(_DeleteTires, _CanExecuteDeleteTires);
            }
        }

        /// <summary>
        /// Delete tires
        /// </summary>
        public void _DeleteTires()
        {
            // Initialize the variables
            Tires tire = GetSelectedTire();

            // Configure the message box to be displayed
            string messageBoxText = string.Format("Voulez-vous vraiment supprimer le pneu {0} du set : {1} ?", tire.name, tire.setnumber);
            string caption = "Suppression du pneu";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;

            // Display message box
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        if (RT.DeleteElement(tire))
                        {
                            MessageStatusRequestToDB = SpecialInformationMessage.deletestatusmessage;
                            setTimerStatus();
                            _CleanData();
                            ReloadAllTire();
                        }
                        return;
                    }
                case MessageBoxResult.No:
                    return;
                case MessageBoxResult.Cancel:
                    return;
            }
        }

        public bool _CanExecuteJoinFilesToTires()
        {
            return SelectedTiresItem != null;
        }

        public ICommand JoinFilesToTires
        {
            get
            {
                return new RelayCommand(_JoinFilesToTires, _CanExecuteJoinFilesToTires);
            }
        }

        /// <summary>
        /// Open dialog for join files to tires selected
        /// </summary>
        public void _JoinFilesToTires()
        {
            string directory = ConfigurationManager.AppSettings[SpecialInformationMessage.FileDirectoryTires].ToString();

            if (!string.IsNullOrEmpty(directory))
            {
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                if (Directory.Exists(directory))
                {
                    string directoryElement = directory + @"\" + SelectedTiresItem.Detail.id;

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

        public bool _CanExecuteDisplayFilesTires()
        {
            return SelectedTiresItem != null && checker.CheckIfFolderExist(SpecialInformationMessage.FileDirectoryTires, SelectedTiresItem.Detail.id);
        }

        public ICommand DisplayFilesTires
        {
            get
            {
                return new RelayCommand(_DisplayFilesTires, _CanExecuteDisplayFilesTires);
            }
        }

        /// <summary>
        /// Display the files for the tires selected
        /// </summary>
        public void _DisplayFilesTires()
        {
            string directory = ConfigurationManager.AppSettings[SpecialInformationMessage.FileDirectoryTires].ToString();

            if (!string.IsNullOrEmpty(directory))
            {
                if (Directory.Exists(directory))
                {
                    string directoryElement = directory + @"\" + SelectedTiresItem.Detail.id;

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

        public ICommand TiresFilter
        {
            get
            {
                return new RelayCommand(_TiresFilter, null);
            }
        }

        /// <summary>
        /// Filter for the tires
        /// </summary>
        public void _TiresFilter()
        {
            // Initialize the variables
            List<DetailViewModel<TiresModel>> tiresRemove = new List<DetailViewModel<TiresModel>>();

            // Get value for the different variables
            string name = SelectedNameItem;
            StatusTire state = GetSelectedStateItem();
            int setnumber = SelectedSetNumberItem;
            string dim1 = SelectedDim1Item;
            string dim2 = SelectedDim2Item;
            string dim3 = SelectedDim3Item;
            string description = SelectedDescriptionItem;

            foreach (var tires in MainCollectionViewModel._Tires)
            {
                if (!string.IsNullOrEmpty(name))
                    if (!tires.Detail.name.ToLower().Contains(name.ToLower()))
                        tiresRemove.Add(tires);

                if (state != null)
                    if (tires.Detail.state.id != state.id)
                        tiresRemove.Add(tires);

                if (setnumber > 0)
                    if (tires.Detail.setnumber != setnumber)
                        tiresRemove.Add(tires);

                if (!string.IsNullOrEmpty(dim1))
                    if (!tires.Detail.Dim1.ToLower().Contains(dim1.ToLower()))
                        tiresRemove.Add(tires);

                if (!string.IsNullOrEmpty(dim2))
                    if (!tires.Detail.Dim2.ToLower().Contains(dim2.ToLower()))
                        tiresRemove.Add(tires);

                if (!string.IsNullOrEmpty(dim3))
                    if (!tires.Detail.Dim3.ToLower().Contains(dim3.ToLower()))
                        tiresRemove.Add(tires);

                if (!string.IsNullOrEmpty(description))
                    if (!tires.Detail.description.ToLower().Contains(description.ToLower()))
                        tiresRemove.Add(tires);
            }

            foreach (var tires in tiresRemove)
            {
                MainCollectionViewModel._Tires.Remove(tires);
            }
        }

        public ICommand ResetTiresFilter
        {
            get
            {
                return new RelayCommand(_ResetTiresFilter, null);
            }
        }

        /// <summary>
        /// Reset the filter for the tires
        /// </summary>
        public void _ResetTiresFilter()
        {
            _CleanData();
            ReloadAllTire();
        }
    }
}
