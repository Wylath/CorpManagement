using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using CorpManagement.DB;
using CorpManagement.Model;
using CorpManagement.ToolBox;

namespace CorpManagement.ViewModel
{
    class AllProviderViewModel : ObservableObject
    {
        private readonly RequestDBProvider RP;
        private readonly CheckRightAccess checkAccess;
        private readonly DispatcherTimer dtResetMessage;

        #region SelectedItem
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
                    if (value != null)
                    {
                        SelectedPhoneItem = value.Detail.phone;
                        SelectedMailItem = value.Detail.mail;
                        SelectedDescriptionItem = value.Detail.description;
                        SelectedHouseNumberItem = value.Detail.housenumber;
                        SelectedStreetItem = value.Detail.street;
                        SelectedPostalCodeItem = value.Detail.postalcode;
                        SelectedTownItem = value.Detail.town;
                    }
                    else
                    {
                        SelectedPhoneItem = 0;
                        SelectedMailItem = string.Empty;
                        SelectedDescriptionItem = string.Empty;
                        SelectedHouseNumberItem = string.Empty;
                        SelectedStreetItem = string.Empty;
                        SelectedPostalCodeItem = string.Empty;
                        SelectedTownItem = string.Empty;
                    }
                    RaisePropertyChanged(() => SelectedProviderItem);
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

        private int _SelectedPhoneItem;

        public int SelectedPhoneItem
        {
            get
            {
                return _SelectedPhoneItem;
            }
            set
            {
                if (value != _SelectedPhoneItem)
                {
                    _SelectedPhoneItem = value;
                    RaisePropertyChanged(() => SelectedPhoneItem);
                }
            }
        }

        private string _SelectedMailItem;

        public string SelectedMailItem
        {
            get
            {
                return _SelectedMailItem;
            }
            set
            {
                if (value != _SelectedMailItem)
                {
                    _SelectedMailItem = value;
                    RaisePropertyChanged(() => SelectedMailItem);
                }
            }
        }

        private string _SelectedHouseNumberItem;

        public string SelectedHouseNumberItem
        {
            get
            {
                return _SelectedHouseNumberItem;
            }
            set
            {
                if (value != _SelectedHouseNumberItem)
                {
                    _SelectedHouseNumberItem = value;
                    RaisePropertyChanged(() => SelectedHouseNumberItem);
                }
            }
        }

        private string _SelectedStreetItem;

        public string SelectedStreetItem
        {
            get
            {
                return _SelectedStreetItem;
            }
            set
            {
                if (value != _SelectedStreetItem)
                {
                    _SelectedStreetItem = value;
                    RaisePropertyChanged(() => SelectedStreetItem);
                }
            }
        }

        private string _SelectedPostalCodeItem;

        public string SelectedPostalCodeItem
        {
            get
            {
                return _SelectedPostalCodeItem;
            }
            set
            {
                if (value != _SelectedPostalCodeItem)
                {
                    _SelectedPostalCodeItem = value;
                    RaisePropertyChanged(() => SelectedPostalCodeItem);
                }
            }
        }

        private string _SelectedTownItem;

        public string SelectedTownItem
        {
            get
            {
                return _SelectedTownItem;
            }
            set
            {
                if (value != _SelectedTownItem)
                {
                    _SelectedTownItem = value;
                    RaisePropertyChanged(() => SelectedTownItem);
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

        private DetailViewModel<ProviderTypeModel> _SelectedProviderTypeItem;

        public DetailViewModel<ProviderTypeModel> SelectedProviderTypeItem
        {
            get
            {
                return _SelectedProviderTypeItem;
            }
            set
            {
                if (value != _SelectedProviderTypeItem)
                {
                    _SelectedProviderTypeItem = value;
                    RaisePropertyChanged(() => SelectedProviderTypeItem);
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
        /// Constructor AllProvider
        /// </summary>
        public AllProviderViewModel()
        {
            // Initialize the request DB
            RP = new RequestDBProvider();

            // Initialize other class checker
            checkAccess = new CheckRightAccess();

            // Initialize timer for the messages in the window
            dtResetMessage = new DispatcherTimer();
        }

        /// <summary>
        /// Return the selected provider
        /// </summary>
        /// <returns></returns>
        public Provider GetSelectedProvider()
        {
            return (SelectedProviderItem != null) ? new Provider(SelectedProviderItem.Detail.id, SelectedProviderItem.Detail.name, SelectedProviderItem.Detail.phone, SelectedProviderItem.Detail.mail, SelectedProviderItem.Detail.street, SelectedProviderItem.Detail.housenumber, SelectedProviderItem.Detail.postalcode, SelectedProviderItem.Detail.town, SelectedProviderItem.Detail.description, SelectedProviderItem.Detail.idtype) : null;
        }

        /// <summary>
        /// Return the selected provider type
        /// </summary>
        /// <returns></returns>
        public ProviderType GetSelectedProviderType()
        {
            return (SelectedProviderTypeItem != null) ? new ProviderType(SelectedProviderTypeItem.Detail.id, SelectedProviderTypeItem.Detail.name) : null;
        }

        /// <summary>
        /// Clean the different data information
        /// </summary>
        public void _CleanData()
        {
            SelectedProviderItem = null;
            SelectedNameItem = string.Empty;
            SelectedPhoneItem = 0;
            SelectedMailItem = string.Empty;
            SelectedHouseNumberItem = string.Empty;
            SelectedStreetItem = string.Empty;
            SelectedPostalCodeItem = string.Empty;
            SelectedTownItem = string.Empty;
            SelectedProviderTypeItem = null;
            SelectedDescriptionItem = string.Empty;
        }

        /// <summary>
        /// Reload all tire in observableCollection Provider
        /// </summary>
        public void ReloadAllProvider(bool OpenLastProviderEncoded = false)
        {
            MainCollectionViewModel._Provider.Clear();

            foreach (var provider in RP.SelectAllElement())
            {
                MainCollectionViewModel._Provider.Add(new DetailViewModel<ProviderModel>(new ProviderModel(provider)));
            }

            if (OpenLastProviderEncoded)
            {
                SelectedProviderItem = MainCollectionViewModel._Provider.Last();
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

        public bool _CanExecuteAddProvider()
        {
            return !string.IsNullOrEmpty(SelectedNameItem) && GetSelectedProviderType() != null && checkAccess.AuthorizedAccess(checkAccess.tabName["Provider"], checkAccess.getLetterAdd);
        }

        public ICommand AddProvider
        {
            get
            {
                return new RelayCommand(_AddProvider, _CanExecuteAddProvider);
            }
        }

        /// <summary>
        /// Add provider
        /// </summary>
        public void _AddProvider()
        {
            // Initialize the variables
            // Get value by selected item
            string name = SelectedNameItem;
            int phone = SelectedPhoneItem;
            string mail = !string.IsNullOrEmpty(SelectedMailItem) ? SelectedMailItem : string.Empty;
            string housenumber = !string.IsNullOrEmpty(SelectedHouseNumberItem) ? SelectedHouseNumberItem : string.Empty;
            string street = !string.IsNullOrEmpty(SelectedStreetItem) ? SelectedStreetItem : string.Empty;
            string postalcode = !string.IsNullOrEmpty(SelectedPostalCodeItem) ? SelectedPostalCodeItem : string.Empty;
            string town = !string.IsNullOrEmpty(SelectedTownItem) ? SelectedTownItem : string.Empty;
            string description = !string.IsNullOrEmpty(SelectedDescriptionItem) ? SelectedDescriptionItem : string.Empty;
            ProviderType idtype = GetSelectedProviderType();

            Provider provider = new Provider(name, phone, mail, street, housenumber, postalcode, town, description, idtype);

            if (RP.InsertNewElement(provider))
            {
                MessageStatusRequestToDB = SpecialInformationMessage.addstatusmessage;
                setTimerStatus();
                _CleanData();
                ReloadAllProvider(true);
            }
        }

        public bool _CanExecuteUpdateProvider()
        {
            return SelectedProviderItem != null && !string.IsNullOrEmpty(SelectedNameItem) && GetSelectedProviderType() != null && checkAccess.AuthorizedAccess(checkAccess.tabName["Provider"], checkAccess.getLetterUpd);
        }

        public ICommand UpdateProvider
        {
            get
            {
                return new RelayCommand(_UpdateProvider, _CanExecuteUpdateProvider);
            }
        }

        /// <summary>
        /// Update provider
        /// </summary>
        public void _UpdateProvider()
        {
            // Initialize the variables
            // Get value by selected item
            int id = GetSelectedProvider().id;
            string name = !string.IsNullOrEmpty(SelectedNameItem) ? SelectedNameItem : GetSelectedProvider().name;
            int phone = SelectedPhoneItem > 0 ? SelectedPhoneItem : GetSelectedProvider().phone;
            string mail = !string.IsNullOrEmpty(SelectedMailItem) ? SelectedMailItem : GetSelectedProvider().mail;
            string housenumber = !string.IsNullOrEmpty(SelectedHouseNumberItem) ? SelectedHouseNumberItem : GetSelectedProvider().housenumber;
            string street = !string.IsNullOrEmpty(SelectedStreetItem) ? SelectedStreetItem : GetSelectedProvider().street;
            string postalcode = !string.IsNullOrEmpty(SelectedPostalCodeItem) ? SelectedPostalCodeItem : GetSelectedProvider().postalcode;
            string town = !string.IsNullOrEmpty(SelectedTownItem) ? SelectedTownItem : GetSelectedProvider().town;
            string description = !string.IsNullOrEmpty(SelectedDescriptionItem) ? SelectedDescriptionItem : GetSelectedProvider().description;
            ProviderType idtype = GetSelectedProviderType() ?? GetSelectedProvider().idtype;

            Provider provider = new Provider(id, name, phone, mail, street, housenumber, postalcode, town, description, idtype);

            if (RP.UpdateElement(provider))
            {
                MessageStatusRequestToDB = SpecialInformationMessage.updatestatusmessage;
                setTimerStatus();
                _CleanData();
                ReloadAllProvider();
            }
        }

        public bool _CanExecuteDeleteProvider()
        {
            return SelectedProviderItem != null && checkAccess.AuthorizedAccess(checkAccess.tabName["Provider"], checkAccess.getLetterDel);
        }

        public ICommand DeleteProvider
        {
            get
            {
                return new RelayCommand(_DeleteProvider, _CanExecuteDeleteProvider);
            }
        }

        /// <summary>
        /// Delete provider
        /// </summary>
        public void _DeleteProvider()
        {
            // Initialize the variables
            Provider provider = GetSelectedProvider();

            // Configure the message box to be displayed
            string messageBoxText = string.Format("Voulez-vous vraiment supprimer l'entreprise {0} ?", provider.name);
            string caption = "Suppression de l'entreprise";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;

            // Display message box
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        if (RP.DeleteElement(provider))
                        {
                            MessageStatusRequestToDB = SpecialInformationMessage.deletestatusmessage;
                            setTimerStatus();
                            _CleanData();
                            ReloadAllProvider();
                        }
                        return;
                    }
                case MessageBoxResult.No:
                    return;
                case MessageBoxResult.Cancel:
                    return;
            }
        }

        public ICommand ProviderFilter
        {
            get
            {
                return new RelayCommand(_ProviderFilter, null);
            }
        }

        /// <summary>
        /// Filter for the vehicle
        /// </summary>
        public void _ProviderFilter()
        {
            // Initialize the variables
            List<DetailViewModel<ProviderModel>> providerRemove = new List<DetailViewModel<ProviderModel>>();

            // Get value for the different variables
            string name = SelectedNameItem;
            int phone = SelectedPhoneItem;
            string mail = SelectedMailItem;
            string housenumber = SelectedHouseNumberItem;
            string street = SelectedStreetItem;
            string postalcode = SelectedPostalCodeItem;
            string town = SelectedTownItem;
            string description = SelectedDescriptionItem;
            ProviderType idtype = GetSelectedProviderType();

            foreach (var provider in MainCollectionViewModel._Provider)
            {
                if (!string.IsNullOrEmpty(name))
                    if (!provider.Detail.name.ToLower().Contains(name.ToLower()))
                        providerRemove.Add(provider);

                if (phone > 0)
                    if (provider.Detail.phone != phone)
                        providerRemove.Add(provider);

                if (!string.IsNullOrEmpty(mail))
                    if (!provider.Detail.mail.ToLower().Contains(mail.ToLower()))
                        providerRemove.Add(provider);

                if (!string.IsNullOrEmpty(housenumber))
                    if (!provider.Detail.housenumber.ToLower().Contains(housenumber.ToLower()))
                        providerRemove.Add(provider);

                if (!string.IsNullOrEmpty(street))
                    if (!provider.Detail.street.ToLower().Contains(street.ToLower()))
                        providerRemove.Add(provider);

                if (!string.IsNullOrEmpty(postalcode))
                    if (!provider.Detail.postalcode.ToLower().Contains(postalcode.ToLower()))
                        providerRemove.Add(provider);

                if (!string.IsNullOrEmpty(town))
                    if (!provider.Detail.town.ToLower().Contains(town.ToLower()))
                        providerRemove.Add(provider);

                if (idtype != null)
                    if (provider.Detail.idtype.id != idtype.id)
                        providerRemove.Add(provider);

                if (!string.IsNullOrEmpty(description))
                    if (!provider.Detail.description.ToLower().Contains(description.ToLower()))
                        providerRemove.Add(provider);
            }

            foreach (var provider in providerRemove)
            {
                MainCollectionViewModel._Provider.Remove(provider);
            }
        }

        public ICommand ResetProviderFilter
        {
            get
            {
                return new RelayCommand(_ResetProviderFilter, null);
            }
        }

        /// <summary>
        /// Reset the filter for the provider
        /// </summary>
        public void _ResetProviderFilter()
        {
            _CleanData();
            ReloadAllProvider();
        }
    }
}
