using CorpManagement.Toolbox;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using CorpManagement.DB;
using CorpManagement.Model;
using CorpManagement.ToolBox;
using System.Windows;
using System.Windows.Threading;
using System.Data.SqlTypes;
using CorpManagement.View;

namespace CorpManagement.ViewModel
{
    class AllUserViewModel : ObservableObject
    {
        private readonly RequestDBUser RU;
        private readonly CheckRightAccess checkAccess;
        private readonly DispatcherTimer dtResetMessage;
        private const int profileLevelAdmin = 1;
        private const int gradeAdmin = 3;
        private static string method = string.Empty;
        private static int line = 0;
        Window userWindow = null;
        Window policelocalityWindow = null;

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
                    if(value != null)
                    {
                        SelectedCreditArticleItem = value.Detail.pointarticle;
                        SelectedStatusUserItem = value.Detail.status;
                    }
                    else
                    {
                        SelectedCreditArticleItem = 0;
                        SelectedStatusUserItem = false;
                    }
                    RaisePropertyChanged(() => SelectedUserItem);
                }
            }
        }

        private int _SelectedMatriculeItem;

        public int SelectedMatriculeItem
        {
            get
            {
                return _SelectedMatriculeItem;
            }
            set
            {
                if (value != _SelectedMatriculeItem)
                {
                    _SelectedMatriculeItem = value;
                    RaisePropertyChanged(() => SelectedMatriculeItem);
                }
            }
        }

        private string _SelectedFirstNameItem;

        public string SelectedFirstNameItem
        {
            get
            {
                return _SelectedFirstNameItem;
            }
            set
            {
                if (value != _SelectedFirstNameItem)
                {
                    _SelectedFirstNameItem = value;
                    RaisePropertyChanged(() => SelectedFirstNameItem);
                }
            }
        }

        private string _SelectedLastNameItem;

        public string SelectedLastNameItem
        {
            get
            {
                return _SelectedLastNameItem;
            }
            set
            {
                if (value != _SelectedLastNameItem)
                {
                    _SelectedLastNameItem = value;
                    RaisePropertyChanged(() => SelectedLastNameItem);
                }
            }
        }

        private int _SelectedCreditArticleItem;

        public int SelectedCreditArticleItem
        {
            get
            {
                return _SelectedCreditArticleItem;
            }
            set
            {
                if (value != _SelectedCreditArticleItem)
                {
                    _SelectedCreditArticleItem = value;
                    RaisePropertyChanged(() => SelectedCreditArticleItem);
                }
            }
        }

        private DetailViewModel<GradePointModel> _SelectedGradePointItem;

        public DetailViewModel<GradePointModel> SelectedGradePointItem
        {
            get
            {
                return _SelectedGradePointItem;
            }
            set
            {
                if (value != _SelectedGradePointItem)
                {
                    _SelectedGradePointItem = value;
                    RaisePropertyChanged(() => SelectedGradePointItem);
                }
            }
        }

        private DetailViewModel<ProfileLevelModel> _SelectedProfileLevelItem;

        public DetailViewModel<ProfileLevelModel> SelectedProfileLevelItem
        {
            get
            {
                return _SelectedProfileLevelItem;
            }
            set
            {
                if (value != _SelectedProfileLevelItem)
                {
                    _SelectedProfileLevelItem = value;
                    RaisePropertyChanged(() => SelectedProfileLevelItem);
                }
            }
        }

        private bool _SelectedStatusUserItem;

        public bool SelectedStatusUserItem
        {
            get
            {
                return _SelectedStatusUserItem;
            }
            set
            {
                if (value != _SelectedStatusUserItem)
                {
                    _SelectedStatusUserItem = value;
                    RaisePropertyChanged(() => SelectedStatusUserItem);
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

        private string _MessageStatusUser;

        public string MessageStatusUser
        {
            get
            {
                return _MessageStatusUser;
            }
            set
            {
                if (value != _MessageStatusUser)
                {
                    _MessageStatusUser = value;
                    RaisePropertyChanged(() => MessageStatusUser);
                }
            }
        }
        #endregion

        #region VisibilityForView
        private Visibility _GetVisibilityForUser;

        public Visibility GetVisibilityForUser
        {
            get
            {
                return _GetVisibilityForUser;
            }
            set
            {
                if (value != _GetVisibilityForUser)
                {
                    _GetVisibilityForUser = value;
                    RaisePropertyChanged(() => GetVisibilityForUser);
                }
            }
        }

        private Visibility _GetVisibilityForService;

        public Visibility GetVisibilityForService
        {
            get
            {
                return _GetVisibilityForService;
            }
            set
            {
                if (value != _GetVisibilityForService)
                {
                    _GetVisibilityForService = value;
                    RaisePropertyChanged(() => GetVisibilityForService);
                }
            }
        }

        private Visibility _GetVisibilityForVehicle;

        public Visibility GetVisibilityForVehicle
        {
            get
            {
                return _GetVisibilityForVehicle;
            }
            set
            {
                if (value != _GetVisibilityForVehicle)
                {
                    _GetVisibilityForVehicle = value;
                    RaisePropertyChanged(() => GetVisibilityForVehicle);
                }
            }
        }

        private Visibility _GetVisibilityForTires;

        public Visibility GetVisibilityForTires
        {
            get
            {
                return _GetVisibilityForTires;
            }
            set
            {
                if (value != _GetVisibilityForTires)
                {
                    _GetVisibilityForTires = value;
                    RaisePropertyChanged(() => GetVisibilityForTires);
                }
            }
        }

        private Visibility _GetVisibilityForArticle;

        public Visibility GetVisibilityForArticle
        {
            get
            {
                return _GetVisibilityForArticle;
            }
            set
            {
                if (value != _GetVisibilityForArticle)
                {
                    _GetVisibilityForArticle = value;
                    RaisePropertyChanged(() => GetVisibilityForArticle);
                }
            }
        }

        private Visibility _GetVisibilityForInvoice;

        public Visibility GetVisibilityForInvoice
        {
            get
            {
                return _GetVisibilityForInvoice;
            }
            set
            {
                if (value != _GetVisibilityForInvoice)
                {
                    _GetVisibilityForInvoice = value;
                    RaisePropertyChanged(() => GetVisibilityForInvoice);
                }
            }
        }

        private Visibility _GetVisibilityForProvider;

        public Visibility GetVisibilityForProvider
        {
            get
            {
                return _GetVisibilityForProvider;
            }
            set
            {
                if (value != _GetVisibilityForProvider)
                {
                    _GetVisibilityForProvider = value;
                    RaisePropertyChanged(() => GetVisibilityForProvider);
                }
            }
        }

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

        private Visibility _GetVisibilityForPoliceLocality;

        public Visibility GetVisibilityForPoliceLocality
        {
            get
            {
                return _GetVisibilityForPoliceLocality;
            }
            set
            {
                if (value != _GetVisibilityForPoliceLocality)
                {
                    _GetVisibilityForPoliceLocality = value;
                    RaisePropertyChanged(() => GetVisibilityForPoliceLocality);
                }
            }
        }

        private Visibility _GetVisibilityForAttributeArticle;

        public Visibility GetVisibilityForAttributeArticle
        {
            get
            {
                return _GetVisibilityForAttributeArticle;
            }
            set
            {
                if (value != _GetVisibilityForAttributeArticle)
                {
                    _GetVisibilityForAttributeArticle = value;
                    RaisePropertyChanged(() => GetVisibilityForAttributeArticle);
                }
            }
        }
        #endregion

        /// <summary>
        /// Constructor AllUser
        /// </summary>
        public AllUserViewModel()
        {
            // Initialize the request DB
            RU = new RequestDBUser();

            // Initialize other class checker
            checkAccess = new CheckRightAccess();

            // Initialize timer for the messages in the window
            dtResetMessage = new DispatcherTimer();

            string username = "11223344";

            bool isAdmin = CheckIfAdminAccess(username);
            int matricule = GetLoginForApp(username);

            if (CurrentUser.userId == null)
            {
                User user = null;
                SelectedUserItem = null;
                if (isAdmin)
                {
                    if (RU.SelectElementByMatricule(matricule) == null)
                    {
                        if (RU.InsertNewElement(new User(string.Empty, string.Empty, matricule, new ProfileLevel(profileLevelAdmin), SqlInt32.Zero.Value, new GradePoint(gradeAdmin), true, DateTime.Now)))
                            user = RU.SelectElementByMatricule(matricule);
                    }
                    else user = RU.SelectElementByMatricule(matricule);
                }
                else user = RU.SelectElementByMatricule(matricule);

                if (user != null)
                {
                    if (!user.status)
                        MessageStatusUser = SpecialInformationMessage.ErrorMessageUserNotActive;

                    if (user.status)
                    {
                        SelectedUserItem = new DetailViewModel<UserModel>(new UserModel(user));
                        CurrentUser.userId = user;
                        //Check for update point article
                        UpdateCreditUser();
                    }
                }
                else
                {
                    ShowDebugInfo();
                    throw new MyErrorException(SpecialInformationMessage.ErrorMessageUserNotFound, SpecialInformationMessage.ErrorTitleUserNotFound, GetType().Name, method, line);
                }
            }
        }

        /// <summary>
        /// Return if the current user is admin or not
        /// </summary>
        /// <returns></returns>
        private bool CheckIfAdminAccess(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                if (username.ToLower().Contains(checkAccess.getAccountAdminType.ToLower()))
                    return true;
                return false;
            }
            else
            {
                ShowDebugInfo();
                throw new MyErrorException(SpecialInformationMessage.ErrorMessageLogin, SpecialInformationMessage.ErrorTitleLogin, GetType().Name, method, line);
            }
        }

        /// <summary>
        /// Return the number of username
        /// </summary>
        /// <returns></returns>
        private int GetLoginForApp(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                int matricule = 0;

                try
                {
                    if (username.ToLower().Contains(checkAccess.getAccountAdminType.ToLower()))
                        matricule = Convert.ToInt32(username.ToLower().Replace(checkAccess.getAccountAdminType.ToLower(), string.Empty));
                    else
                        matricule = Convert.ToInt32(username);
                }
                catch (Exception ex)
                {
                    ShowDebugInfo();
                    throw new MyErrorException(ex.Message, GetType().Name, method, line);
                }
                return matricule;
            }
            else
            {
                ShowDebugInfo();
                throw new MyErrorException(SpecialInformationMessage.ErrorMessageLogin, SpecialInformationMessage.ErrorTitleLogin, GetType().Name, method, line);
            }
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
        /// Return the selected profile level
        /// </summary>
        /// <returns></returns>
        public ProfileLevel GetSelectedProfileLevel()
        {
            return (SelectedProfileLevelItem != null) ? new ProfileLevel(SelectedProfileLevelItem.Detail.id, SelectedProfileLevelItem.Detail.name) : null;
        }

        /// <summary>
        /// Return the selected grade point
        /// </summary>
        /// <returns></returns>
        public GradePoint GetSelectedGradePoint()
        {
            return (SelectedGradePointItem != null) ? new GradePoint(SelectedGradePointItem.Detail.id, SelectedGradePointItem.Detail.name, SelectedGradePointItem.Detail.totalpoint) : null;
        }

        /// <summary>
        /// Clean the different data information
        /// </summary>
        public void _CleanData()
        {
            SelectedUserItem = null;
            SelectedMatriculeItem = 0;
            SelectedFirstNameItem = string.Empty;
            SelectedLastNameItem = string.Empty;
            SelectedCreditArticleItem = 0;
            SelectedGradePointItem = null;
            SelectedProfileLevelItem = null;
            SelectedStatusUserItem = true;
        }

        /// <summary>
        /// Reload all user in observableCollection User
        /// </summary>
        public void ReloadAllUser(bool OpenLastUserEncoded = false)
        {
            MainCollectionViewModel._User.Clear();

            foreach (var user in RU.SelectAllElement())
            {
                MainCollectionViewModel._User.Add(new DetailViewModel<UserModel>(new UserModel(user)));
            }

            if (OpenLastUserEncoded)
            {
                SelectedUserItem = MainCollectionViewModel._User.Last();
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

        public bool _CanExecuteOpenServicing()
        {
            return SelectedUserItem != null;
        }

        public ICommand OpenServicing
        {
            get
            {
                return new RelayCommand(_OpenServicing, _CanExecuteOpenServicing);
            }
        }

        /// <summary>
        /// Open view servicing
        /// </summary>
        public void _OpenServicing()
        {
            SwitchWindows sw = new SwitchWindows();
            User user = CurrentUser.userId;

            if (CurrentUser.userId.status)
            {
                if (user.idprofilelevel != null)
                {
                    setViewVisibility();
                    // Change content main windows with the AllView
                    sw.ChangeViewWindows("AllView", user);
                    SelectedUserItem = null;
                }
                else
                {
                    ShowDebugInfo();
                    throw new MyErrorException(SpecialInformationMessage.ErrorMessageUserProfile, SpecialInformationMessage.ErrorTitleUserProfile, GetType().Name, method, line);
                }
            }
            else
            {
                ShowDebugInfo();
                throw new MyErrorException(SpecialInformationMessage.ErrorMessageUserNotActive, SpecialInformationMessage.ErrorTitleUserNotActive, GetType().Name, method, line);
            }
        }

        /// <summary>
        /// Update the pointarticle with the grade point for the new year
        /// </summary>
        public void UpdateCreditUser()
        {
            DateTime actualdate = DateTime.Now;

            foreach (var user in RU.SelectAllElement())
            {
                if (user.lastupdatepoint.Year < actualdate.Year)
                {
                    user.pointarticle += user.gradepoint.totalpoint;
                    user.lastupdatepoint = actualdate;
                    _ = RU.UpdateElement(user);
                }
            }
        }

        public bool _CanExecuteAddUser()
        {
            return SelectedMatriculeItem > 0 && GetSelectedProfileLevel() != null && GetSelectedGradePoint() != null && checkAccess.AuthorizedAccess(checkAccess.tabName["Users"], checkAccess.getLetterAdd);
        }

        public ICommand AddUser
        {
            get
            {
                return new RelayCommand(_AddUser, _CanExecuteAddUser);
            }
        }

        /// <summary>
        /// Add user
        /// </summary>
        public void _AddUser()
        {
            // Initialize the variables
            DateTime lastupdatepoint = DateTime.Now; // Default value, not change
            // Get value by selected item
            int matricule = SelectedMatriculeItem;
            string firstname = SelectedFirstNameItem;
            string lastname = SelectedLastNameItem;
            int creditarticle = SelectedCreditArticleItem;
            GradePoint gradepoint = GetSelectedGradePoint();
            ProfileLevel profilelevel = GetSelectedProfileLevel();
            bool status = SelectedStatusUserItem;

            User user = new User(lastname, firstname, matricule, profilelevel, creditarticle, gradepoint, status, lastupdatepoint);

            if (RU.InsertNewElement(user))
            {
                MessageStatusRequestToDB = SpecialInformationMessage.addstatusmessage;
                setTimerStatus();
                _CleanData();
                ReloadAllUser(true);
            }
        }

        public bool _CanExecuteUpdateUser()
        {
            return SelectedUserItem != null && SelectedMatriculeItem > 0 && GetSelectedProfileLevel() != null && GetSelectedGradePoint() != null && checkAccess.AuthorizedAccess(checkAccess.tabName["Users"], checkAccess.getLetterUpd);
        }

        public ICommand UpdateUser
        {
            get
            {
                return new RelayCommand(_UpdateUser, _CanExecuteUpdateUser);
            }
        }

        /// <summary>
        /// Update user
        /// </summary>
        public void _UpdateUser()
        {
            // Initialize the variables
            int creditarticle = 0;
            DateTime lastupdatepoint = GetSelectedUser().lastupdatepoint; // default date is current date on user
            // Get value by selected item
            int id = GetSelectedUser().id;
            int matricule = SelectedMatriculeItem > 0 ? SelectedMatriculeItem : GetSelectedUser().matricule;
            string firstname = !string.IsNullOrEmpty(SelectedFirstNameItem) ? SelectedFirstNameItem : GetSelectedUser().firstname;
            string lastname = !string.IsNullOrEmpty(SelectedLastNameItem) ? SelectedLastNameItem : GetSelectedUser().lastname;
            if (SelectedCreditArticleItem > 0 && SelectedCreditArticleItem != GetSelectedUser().pointarticle)
            {
                creditarticle = SelectedCreditArticleItem;
                lastupdatepoint = DateTime.Now; // Update date
            }
            else
            {
                creditarticle = GetSelectedUser().pointarticle;
            }
            GradePoint gradepoint = GetSelectedGradePoint() ?? GetSelectedUser().gradepoint;
            ProfileLevel profilelevel = GetSelectedProfileLevel() ?? GetSelectedUser().idprofilelevel;
            bool status = SelectedStatusUserItem != GetSelectedUser().status ? SelectedStatusUserItem : GetSelectedUser().status;

            User user = new User(id, lastname, firstname, matricule, profilelevel, creditarticle, gradepoint, status, lastupdatepoint);

            if (RU.UpdateElement(user))
            {
                MessageStatusRequestToDB = SpecialInformationMessage.updatestatusmessage;
                setTimerStatus();
                _CleanData();
                ReloadAllUser();
            }
        }

        public bool _CanExecuteDeleteUser()
        {
            return SelectedUserItem != null && checkAccess.AuthorizedAccess(checkAccess.tabName["Users"], checkAccess.getLetterDel);
        }

        public ICommand DeleteUser
        {
            get
            {
                return new RelayCommand(_DeleteUser, _CanExecuteDeleteUser);
            }
        }

        /// <summary>
        /// Delete user
        /// </summary>
        public void _DeleteUser()
        {
            // Initialize the variables
            User user = GetSelectedUser();

            // Configure the message box to be displayed
            string messageBoxText = string.Format("Voulez-vous vraiment supprimer l'utilisateur {0} {1} ({2}) ?", user.firstname, user.lastname, user.matricule);
            string caption = "Suppression de l'utilisateur";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;

            // Display message box
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        if (RU.DeleteElement(user))
                        {
                            MessageStatusRequestToDB = SpecialInformationMessage.deletestatusmessage;
                            setTimerStatus();
                            _CleanData();
                            ReloadAllUser();
                        }
                        return;
                    }
                case MessageBoxResult.No:
                    return;
                case MessageBoxResult.Cancel:
                    return;
            }
        }

        public ICommand CloseWindowUser
        {
            get
            {
                return new RelayCommand(_CloseWindowUser, null);
            }
        }

        /// <summary>
        /// Close the window with the users
        /// </summary>
        /// <param name="parameter"></param>
        public void _CloseWindowUser(object parameter)
        {
            if (parameter != null)
            {
                _CleanData();
                Window win = (Window)parameter;
                win.Close();
            }
        }

        public ICommand ChangeStatusUser
        {
            get
            {
                return new RelayCommand(_ChangeStatusUser, null);
            }
        }

        /// <summary>
        /// Switch value status user
        /// </summary>
        public void _ChangeStatusUser()
        {
            SelectedStatusUserItem = SelectedStatusUserItem ? true : false;
            if (SelectedUserItem != null)
            {
                SelectedUserItem.Detail.status = SelectedStatusUserItem;
            }
        }

        public bool _CanExecuteChangeStatusUserInDG()
        {
            return SelectedUserItem != null;
        }

        public ICommand ChangeStatusUserInDG
        {
            get
            {
                return new RelayCommand(_ChangeStatusUserInDG, _CanExecuteChangeStatusUserInDG);
            }
        }

        /// <summary>
        /// Switch value status user
        /// </summary>
        public void _ChangeStatusUserInDG()
        {
            SelectedStatusUserItem = SelectedUserItem.Detail.status;
        }

        public ICommand UserFilter
        {
            get
            {
                return new RelayCommand(_UserFilter, null);
            }
        }

        /// <summary>
        /// Filter for the user
        /// </summary>
        public void _UserFilter()
        {
            // Initialize the variables
            List<DetailViewModel<UserModel>> userRemove = new List<DetailViewModel<UserModel>>();

            // Get value for the different variables
            int matricule = SelectedMatriculeItem;
            string firstname = SelectedFirstNameItem;
            string lastname = SelectedLastNameItem;
            int creditarticle = SelectedCreditArticleItem;
            GradePoint gradepoint = GetSelectedGradePoint();
            ProfileLevel profilelevel = GetSelectedProfileLevel();

            foreach (var user in MainCollectionViewModel._User)
            {
                if (matricule > 0)
                    if (user.Detail.matricule != matricule)
                        userRemove.Add(user);

                if (!string.IsNullOrEmpty(firstname))
                    if (!user.Detail.firstname.ToLower().Contains(firstname.ToLower()))
                        userRemove.Add(user);

                if (!string.IsNullOrEmpty(lastname))
                    if (!user.Detail.lastname.ToLower().Contains(lastname.ToLower()))
                        userRemove.Add(user);

                if (creditarticle > 0)
                    if (user.Detail.pointarticle != creditarticle)
                        userRemove.Add(user);

                if (gradepoint != null)
                    if (user.Detail.gradepoint.id != gradepoint.id)
                        userRemove.Add(user);

                if (profilelevel != null)
                    if (user.Detail.idprofilelevel.id != profilelevel.id)
                        userRemove.Add(user);
            }

            foreach (var user in userRemove)
            {
                MainCollectionViewModel._User.Remove(user);
            }
        }

        public ICommand ResetUserFilter
        {
            get
            {
                return new RelayCommand(_ResetUserFilter, null);
            }
        }

        /// <summary>
        /// Reset the filter for the provider
        /// </summary>
        public void _ResetUserFilter()
        {
            _CleanData();
            ReloadAllUser();
        }

        /// <summary>
        /// Set all view visibility for current user
        /// </summary>
        private void setViewVisibility()
        {
            GetVisibilityForUser = Visibility.Collapsed;
            GetVisibilityForArticle = Visibility.Collapsed;
            GetVisibilityForInsurance = Visibility.Collapsed;
            GetVisibilityForInvoice = Visibility.Collapsed;
            GetVisibilityForProvider = Visibility.Collapsed;
            GetVisibilityForService = Visibility.Collapsed;
            GetVisibilityForTires = Visibility.Collapsed;
            GetVisibilityForVehicle = Visibility.Collapsed;
            GetVisibilityForPoliceLocality = Visibility.Collapsed;
            GetVisibilityForAttributeArticle = Visibility.Collapsed;

            if (checkAccess.AuthorizedAccess(checkAccess.tabName["All"], checkAccess.getLetterVis))
            {
                GetVisibilityForUser = Visibility.Visible;
                GetVisibilityForArticle = Visibility.Visible;
                GetVisibilityForInsurance = Visibility.Visible;
                GetVisibilityForInvoice = Visibility.Visible;
                GetVisibilityForProvider = Visibility.Visible;
                GetVisibilityForService = Visibility.Visible;
                GetVisibilityForTires = Visibility.Visible;
                GetVisibilityForVehicle = Visibility.Visible;
                GetVisibilityForPoliceLocality = Visibility.Visible;
                GetVisibilityForAttributeArticle = Visibility.Visible;
            }
            if (checkAccess.AuthorizedAccess(checkAccess.tabName["Users"], checkAccess.getLetterVis))
                GetVisibilityForUser = Visibility.Visible;
            if (checkAccess.AuthorizedAccess(checkAccess.tabName["Article"], checkAccess.getLetterVis))
                GetVisibilityForArticle = Visibility.Visible;
            if (checkAccess.AuthorizedAccess(checkAccess.tabName["Insurance"], checkAccess.getLetterVis))
                GetVisibilityForInsurance = Visibility.Visible;
            if (checkAccess.AuthorizedAccess(checkAccess.tabName["Invoice"], checkAccess.getLetterVis))
                GetVisibilityForInvoice = Visibility.Visible;
            if (checkAccess.AuthorizedAccess(checkAccess.tabName["Provider"], checkAccess.getLetterVis))
                GetVisibilityForProvider = Visibility.Visible;
            if (checkAccess.AuthorizedAccess(checkAccess.tabName["Servicing"], checkAccess.getLetterVis))
                GetVisibilityForService = Visibility.Visible;
            if (checkAccess.AuthorizedAccess(checkAccess.tabName["Tires"], checkAccess.getLetterVis))
                GetVisibilityForTires = Visibility.Visible;
            if (checkAccess.AuthorizedAccess(checkAccess.tabName["Vehicle"], checkAccess.getLetterVis))
                GetVisibilityForVehicle = Visibility.Visible;
            if (checkAccess.AuthorizedAccess(checkAccess.tabName["PoliceLocality"], checkAccess.getLetterVis))
                GetVisibilityForPoliceLocality = Visibility.Visible;
            if (checkAccess.AuthorizedAccess(checkAccess.tabName["AttributeArticle"], checkAccess.getLetterVis))
                GetVisibilityForAttributeArticle = Visibility.Visible;
        }

        public ICommand OpenUserList
        {
            get
            {
                return new RelayCommand(_OpenUserList, null);
            }
        }

        /// <summary>
        /// Open the user window
        /// </summary>
        public void _OpenUserList()
        {
            userWindow = new AllUsers
            {
                DataContext = this
            };
            userWindow.Show();
        }

        public ICommand OpenPoliceLocalityList
        {
            get
            {
                return new RelayCommand(_OpenPoliceLocalityList, null);
            }
        }

        /// <summary>
        /// Open the police locality window
        /// </summary>
        public void _OpenPoliceLocalityList()
        {
            policelocalityWindow = new AllPoliceLocality();
            policelocalityWindow.DataContext = new AllPoliceLocalityViewModel();
            policelocalityWindow.Show();
        }
    }

    // Current user on application
    static class CurrentUser
    {
        private static User _userId = null;
        public static User userId { get { return _userId; } set { if (_userId != value) _userId = value; } }
    }
}
