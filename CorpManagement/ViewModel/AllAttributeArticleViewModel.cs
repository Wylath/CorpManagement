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
    class AllAttributeArticleViewModel : ObservableObject
    {
        private readonly RequestDBArticleAttribution RAA;
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
        private DetailViewModel<ArticleAttributionModel> _SelectedAttributionItem;

        public DetailViewModel<ArticleAttributionModel> SelectedAttributionItem
        {
            get
            {
                return _SelectedAttributionItem;
            }
            set
            {
                if (value != _SelectedAttributionItem)
                {
                    _SelectedAttributionItem = value;
                    if (value != null)
                    {
                        SelectedDescriptionItem = value.Detail.description;
                        SelectedSerialItem = value.Detail.serialnumber;
                        SelectedSpecialNumberItem = value.Detail.specialnumber;
                    }
                    else
                    {
                        SelectedDescriptionItem = string.Empty;
                        SelectedSerialItem = string.Empty;
                        SelectedSpecialNumberItem = string.Empty;
                    }
                    RaisePropertyChanged(() => SelectedAttributionItem);
                }
            }
        }

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
                    RaisePropertyChanged(() => SelectedArticleItem);
                }
            }
        }

        private DetailViewModel<ArticleTypeModel> _SelectedArticleTypeItem;

        public DetailViewModel<ArticleTypeModel> SelectedArticleTypeItem
        {
            get
            {
                return _SelectedArticleTypeItem;
            }
            set
            {
                if (value != _SelectedArticleTypeItem)
                {
                    _SelectedArticleTypeItem = value;
                    if (value != null)
                    {
                        MainCollectionViewModel._BaseArticle.Clear();
                        MainCollectionViewModel._Attribution.Clear();
                        foreach (var article in MainCollectionViewModel._Article)
                        {
                            if (article.Detail.idtype.id == value.Detail.id)
                                MainCollectionViewModel._BaseArticle.Add(article);
                        }
                        foreach (var attribution in MainCollectionViewModel._BaseAttribution)
                        {
                            if (attribution.Detail.idarticle.idtype.id == value.Detail.id)
                                MainCollectionViewModel._Attribution.Add(attribution);
                        }
                        MainCollectionViewModel.typeArticleAttribution = value.Detail.id;
                    } else
                    {
                        MainCollectionViewModel.typeArticleAttribution = 0;
                    }
                    RaisePropertyChanged(() => SelectedArticleTypeItem);
                }
            }
        }

        private DetailViewModel<UserModel> _SelectedUserItem;

        public DetailViewModel<UserModel> SelectedUserItem
        {
            get
            {
                return _SelectedUserItem;
            }
            set
            {
                if (value != _SelectedUserItem)
                {
                    _SelectedUserItem = value;
                    RaisePropertyChanged(() => SelectedUserItem);
                }
            }
        }

        private string _SelectedSerialItem;

        public string SelectedSerialItem
        {
            get
            {
                return _SelectedSerialItem;
            }
            set
            {
                if (value != _SelectedSerialItem)
                {
                    _SelectedSerialItem = value;
                    RaisePropertyChanged(() => SelectedSerialItem);
                }
            }
        }

        private string _SelectedSpecialNumberItem;

        public string SelectedSpecialNumberItem
        {
            get
            {
                return _SelectedSpecialNumberItem;
            }
            set
            {
                if (value != _SelectedSpecialNumberItem)
                {
                    _SelectedSpecialNumberItem = value;
                    RaisePropertyChanged(() => SelectedSpecialNumberItem);
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

        private DetailViewModel<StateArticleAttributionModel> _SelectedStateItem;

        public DetailViewModel<StateArticleAttributionModel > SelectedStateItem
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
        /// Constructor AllAtributeArticle
        /// </summary>
        public AllAttributeArticleViewModel()
        {
            // Initialize the request DB
            RAA = new RequestDBArticleAttribution();

            // Initialize other class checker
            checker = new CheckerForFolder();
            checkAccess = new CheckRightAccess();

            // Initialize timer for the messages in the window
            dtResetMessage = new DispatcherTimer();
        }

        /// <summary>
        /// Return the selected user
        /// </summary>
        /// <returns></returns>
        public User GetSelectedUser()
        {
            return (SelectedUserItem != null) ? new User(SelectedUserItem.Detail.id, SelectedUserItem.Detail.lastname, SelectedUserItem.Detail.firstname, SelectedUserItem.Detail.matricule, SelectedUserItem.Detail.idprofilelevel, SelectedUserItem.Detail.pointarticle, SelectedUserItem.Detail.gradepoint, SelectedUserItem.Detail.status, SelectedUserItem.Detail.lastupdatepoint) : null;
        }

        /// <summary>
        /// Return the selected article
        /// </summary>
        /// <returns></returns>
        public Article GetSelectedArticle()
        {
            return (SelectedArticleItem != null) ? new Article(SelectedArticleItem.Detail.id) : null;
        }

        /// <summary>
        /// Return the selected StateArticleAttribution
        /// </summary>
        /// <returns></returns>
        public StateArticleAttribution GetSelectedState()
        {
            return (SelectedStateItem != null) ? new StateArticleAttribution(SelectedStateItem.Detail.id, SelectedStateItem.Detail.name) : null;
        }

        /// <summary>
        /// Return the selected ArticleAttribution
        /// </summary>
        /// <returns></returns>
        public ArticleAttribution GetSelectedAttribution()
        {
            return (SelectedAttributionItem != null) ? new ArticleAttribution(SelectedAttributionItem.Detail.id, SelectedAttributionItem.Detail.iduser, SelectedAttributionItem.Detail.idarticle, SelectedAttributionItem.Detail.serialnumber, SelectedAttributionItem.Detail.specialnumber, SelectedAttributionItem.Detail.description, SelectedAttributionItem.Detail.state) : null;
        }

        /// <summary>
        /// Return the selected article type
        /// </summary>
        /// <returns></returns>
        public ArticleType GetSelectedArticleType()
        {
            return (SelectedArticleTypeItem != null) ? new ArticleType(SelectedArticleTypeItem.Detail.id, SelectedArticleTypeItem.Detail.name) : null;
        }

        /// <summary>
        /// Clean the different data information
        /// </summary>
        public void _CleanData()
        {
            SelectedAttributionItem = null;
            SelectedUserItem = null;
            SelectedArticleItem = null;
            SelectedStateItem = null;
            SelectedDescriptionItem = string.Empty;
            SelectedSerialItem = string.Empty;
            SelectedSpecialNumberItem = string.Empty;
            //SelectedArticleTypeItem = null;
        }

        /// <summary>
        /// Reload all attribution in observableCollection
        /// </summary>
        public void ReloadAllAttribution(bool OpenLastAttributionEncoded = false)
        {
            MainCollectionViewModel._Attribution.Clear();

            foreach (var attribution in RAA.SelectAllElement())
            {
                if (attribution.idarticle.idtype.id == MainCollectionViewModel.typeArticleAttribution)
                    MainCollectionViewModel._Attribution.Add(new DetailViewModel<ArticleAttributionModel>(new ArticleAttributionModel(attribution)));
                else if (MainCollectionViewModel.typeArticleAttribution == 0)
                    MainCollectionViewModel._Attribution.Add(new DetailViewModel<ArticleAttributionModel>(new ArticleAttributionModel(attribution)));
            }

            if (OpenLastAttributionEncoded)
            {
                SelectedAttributionItem = MainCollectionViewModel._Attribution.Last();
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

        public bool _CanExecuteInsertAttribution()
        {
            return SelectedArticleItem != null && SelectedStateItem != null && SelectedArticleTypeItem != null && (checkAccess.AuthorizedAccess(SelectedArticleTypeItem.Detail.name, checkAccess.getLetterAdd) || checkAccess.AuthorizedAccess(checkAccess.tabName["AttributeArticle"], checkAccess.getLetterAdd));
        }

        public ICommand InsertAttribution
        {
            get
            {
                return new RelayCommand(_InsertAttribution, _CanExecuteInsertAttribution);
            }
        }

        /// <summary>
        /// Insert new Attribution
        /// </summary>
        public void _InsertAttribution()
        {
            // Initialize the variables
            // Get value by selected item
            User user = GetSelectedUser();
            Article article = GetSelectedArticle();
            string serial = !string.IsNullOrEmpty(SelectedSerialItem) ? SelectedSerialItem : string.Empty;
            string special = !string.IsNullOrEmpty(SelectedSpecialNumberItem) ? SelectedSpecialNumberItem : string.Empty;
            string description = !string.IsNullOrEmpty(SelectedDescriptionItem) ? SelectedDescriptionItem : string.Empty;
            StateArticleAttribution state = GetSelectedState();

            ArticleAttribution attribution = new ArticleAttribution(user, article, serial, special, description, state);

            if (RAA.InsertNewElement(attribution))
            {
                MessageStatusRequestToDB = SpecialInformationMessage.addstatusmessage;
                setTimerStatus();
                _CleanData();
                ReloadAllAttribution(true);
            }
        }

        public bool _CanExecuteUpdateAttribution()
        {
            return SelectedAttributionItem != null && SelectedArticleItem != null && SelectedStateItem != null && SelectedArticleTypeItem != null && (checkAccess.AuthorizedAccess(SelectedArticleTypeItem.Detail.name, checkAccess.getLetterUpd) || checkAccess.AuthorizedAccess(checkAccess.tabName["AttributeArticle"], checkAccess.getLetterUpd));
        }

        public ICommand UpdateAttribution
        {
            get
            {
                return new RelayCommand(_UpdateAttribution, _CanExecuteUpdateAttribution);
            }
        }

        /// <summary>
        /// Update Attribution
        /// </summary>
        public void _UpdateAttribution()
        {
            // Initialize the variables
            // Get value by selected item
            int id = GetSelectedAttribution().id;
            User user = GetSelectedUser() ?? GetSelectedAttribution().iduser;
            Article article = GetSelectedArticle() ?? GetSelectedAttribution().idarticle;
            string serial = !string.IsNullOrEmpty(SelectedSerialItem) ? SelectedSerialItem : GetSelectedAttribution().serialnumber;
            string special = !string.IsNullOrEmpty(SelectedSpecialNumberItem) ? SelectedSpecialNumberItem : GetSelectedAttribution().specialnumber;
            string description = !string.IsNullOrEmpty(SelectedDescriptionItem) ? SelectedDescriptionItem : GetSelectedAttribution().description;
            StateArticleAttribution state = GetSelectedState() ?? GetSelectedAttribution().state;

            ArticleAttribution attribution = new ArticleAttribution(id, user, article, serial, special, description, state);

            if (RAA.UpdateElement(attribution))
            {
                MessageStatusRequestToDB = SpecialInformationMessage.updatestatusmessage;
                setTimerStatus();
                _CleanData();
                ReloadAllAttribution();
            }
        }

        public bool _CanExecuteDeleteAttribution()
        {
            return SelectedAttributionItem != null && SelectedArticleTypeItem != null && (checkAccess.AuthorizedAccess(SelectedArticleTypeItem.Detail.name, checkAccess.getLetterDel) || checkAccess.AuthorizedAccess(checkAccess.tabName["AttributeArticle"], checkAccess.getLetterDel));
        }

        public ICommand DeleteAttribution
        {
            get
            {
                return new RelayCommand(_DeleteAttribution, _CanExecuteDeleteAttribution);
            }
        }

        /// <summary>
        /// Delete Attribution
        /// </summary>
        public void _DeleteAttribution()
        {
            // Initialize the variables
            ArticleAttribution attribution = GetSelectedAttribution();

            // Configure the message box to be displayed
            string messageBoxText = string.Format("Voulez-vous vraiment supprimer l'attribution de l'objet {0} ?", attribution.idarticle.name);
            string caption = "Suppression de l'attribution";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;

            // Display message box
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        if (RAA.DeleteElement(attribution))
                        {
                            MessageStatusRequestToDB = SpecialInformationMessage.deletestatusmessage;
                            setTimerStatus();
                            _CleanData();
                            ReloadAllAttribution();
                        }
                        return;
                    }
                case MessageBoxResult.No:
                    return;
                case MessageBoxResult.Cancel:
                    return;
            }
        }

        //public bool _CanExecuteAttributionFilter()
        //{
        //    return SelectedArticleTypeItem != null;
        //}

        public ICommand AttributionFilter
        {
            get
            {
                return new RelayCommand(_AttributionFilter, null);
            }
        }

        /// <summary>
        /// Filter for the Attribution
        /// </summary>
        public void _AttributionFilter()
        {
            // Initialize the variables
            List<DetailViewModel<ArticleAttributionModel>> attributionRemove = new List<DetailViewModel<ArticleAttributionModel>>();

            // Get value for the different variables
            User user = GetSelectedUser();
            Article article = GetSelectedArticle();
            string serial = SelectedSerialItem;
            string special = SelectedSpecialNumberItem;
            string description = SelectedDescriptionItem;
            StateArticleAttribution state = GetSelectedState();
            ArticleType type = GetSelectedArticleType();

            foreach (var attribution in MainCollectionViewModel._Attribution)
            {
                if (user != null)
                {
                    if (attribution.Detail.iduser == null)
                        attributionRemove.Add(attribution);
                    else if (attribution.Detail.iduser.id != user.id)
                        attributionRemove.Add(attribution);
                }

                if (article != null)
                {
                    if (attribution.Detail.idarticle == null)
                        attributionRemove.Add(attribution);
                    else if (attribution.Detail.idarticle.id != article.id)
                        attributionRemove.Add(attribution);
                }

                if (state != null)
                {
                    if (attribution.Detail.state == null)
                        attributionRemove.Add(attribution);
                    else if (attribution.Detail.state.id != state.id)
                        attributionRemove.Add(attribution);
                }

                if (type != null)
                    if (attribution.Detail.idarticle.idtype.id != type.id)
                        attributionRemove.Add(attribution);

                if (!string.IsNullOrEmpty(serial))
                    if (!attribution.Detail.serialnumber.ToLower().Contains(serial.ToLower()))
                        attributionRemove.Add(attribution);

                if (!string.IsNullOrEmpty(special))
                    if (!attribution.Detail.specialnumber.ToLower().Contains(special.ToLower()))
                        attributionRemove.Add(attribution);

                if (!string.IsNullOrEmpty(description))
                    if (!attribution.Detail.description.ToLower().Contains(description.ToLower()))
                        attributionRemove.Add(attribution);
            }

            foreach (var attribution in attributionRemove)
            {
                MainCollectionViewModel._Attribution.Remove(attribution);
            }
        }

        //public bool _CanExecuteResetAttributionFilter()
        //{
        //    return SelectedArticleTypeItem != null;
        //}

        public ICommand ResetInvoiceFilter
        {
            get
            {
                return new RelayCommand(_ResetAttributionFilter, null);
            }
        }

        /// <summary>
        /// Reset the filter for the Attribution
        /// </summary>
        public void _ResetAttributionFilter()
        {
            _CleanData();
            ReloadAllAttribution();
        }

        public bool _CanExecuteJoinFilesToAttribution()
        {
            return SelectedAttributionItem != null;
        }

        public ICommand JoinFilesToAttribution
        {
            get
            {
                return new RelayCommand(_JoinFilesToAttribution, _CanExecuteJoinFilesToAttribution);
            }
        }

        /// <summary>
        /// Open dialog for join files to Attribution selected
        /// </summary>
        public void _JoinFilesToAttribution()
        {
            string directory = ConfigurationManager.AppSettings[SpecialInformationMessage.FileDirectoryEquipment].ToString();

            if (!string.IsNullOrEmpty(directory))
            {
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                if (Directory.Exists(directory))
                {
                    string directoryElement = directory + @"\" + SelectedAttributionItem.Detail.id;

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

        public bool _CanExecuteDisplayFilesAttribution()
        {
            return SelectedAttributionItem != null && checker.CheckIfFolderExist(SpecialInformationMessage.FileDirectoryEquipment, SelectedAttributionItem.Detail.id);
        }

        public ICommand DisplayFilesAttribution
        {
            get
            {
                return new RelayCommand(_DisplayFilesAttribution, _CanExecuteDisplayFilesAttribution);
            }
        }

        /// <summary>
        /// Display the files for the Attribution selected
        /// </summary>
        public void _DisplayFilesAttribution()
        {
            string directory = ConfigurationManager.AppSettings[SpecialInformationMessage.FileDirectoryEquipment].ToString();

            if (!string.IsNullOrEmpty(directory))
            {
                if (Directory.Exists(directory))
                {
                    string directoryElement = directory + @"\" + SelectedAttributionItem.Detail.id;

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
