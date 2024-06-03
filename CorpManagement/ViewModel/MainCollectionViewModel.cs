using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.DB;
using CorpManagement.Model;
using CorpManagement.ToolBox;

namespace CorpManagement.ViewModel
{
    class MainCollectionViewModel : ObservableObject
    {
        private readonly RequestDBServicing RS;
        private readonly RequestDBProvider RP;
        private readonly RequestDBTypeServicing RTS;
        private readonly RequestDBVehicle RV;
        private readonly RequestDBProviderType RPT;
        private readonly RequestDBTires RT;
        private readonly RequestDBPoliceLocality RPL;
        private readonly RequestDBFuel RF;
        private readonly RequestDBStatusVehicle RSV;
        private readonly RequestDBInsuranceVehicle RIV;
        private readonly RequestDBInvoice RI;
        private readonly RequestDBArticle RA;
        private readonly RequestDBArticleType RAT;
        private readonly RequestDBSizeClothing RSC;
        private readonly RequestDBOrderArticle ROA;
        private readonly RequestDBUser RU;
        private readonly RequestDBOrderStatus ROS;
        private readonly RequestDBStatusTires RST;
        private readonly RequestDBStateArticleAttribution RSAA;
        private readonly RequestDBArticleAttribution RAA;
        private readonly RequestDBProfileLevel RPLE;
        private readonly RequestDBGradePoint RGP;
        private readonly RequestDBInvoiceType RIT;
        private static int _typeArticleAttribution { get; set; }
        public static int typeArticleAttribution {
            get
            {
                return _typeArticleAttribution;
            }
            set
            {
                if (value != _typeArticleAttribution)
                    _typeArticleAttribution = value;
            }
        }

        #region ObservableCollection
        public static ObservableCollection<DetailViewModel<ServicingModel>> _Servicing;

        public static ObservableCollection<DetailViewModel<ServicingModel>> Servicing
        {
            get
            {
                return _Servicing;
            }
        }

        public static ObservableCollection<DetailViewModel<VehicleModel>> _Vehicle;

        public static ObservableCollection<DetailViewModel<VehicleModel>> Vehicle
        {
            get
            {
                return _Vehicle;
            }
        }

        public static ObservableCollection<DetailViewModel<ProviderModel>> _Provider;

        public static ObservableCollection<DetailViewModel<ProviderModel>> Provider
        {
            get
            {
                return _Provider;
            }
        }

        public static ObservableCollection<DetailViewModel<TypeServicingModel>> _TypeServicing;

        public static ObservableCollection<DetailViewModel<TypeServicingModel>> TypeServicing
        {
            get
            {
                return _TypeServicing;
            }
        }

        public static ObservableCollection<DetailViewModel<ProviderTypeModel>> _ProviderType;

        public static ObservableCollection<DetailViewModel<ProviderTypeModel>> ProviderType
        {
            get
            {
                return _ProviderType;
            }
        }

        public static ObservableCollection<DetailViewModel<PoliceLocalityModel>> _PoliceLocality;

        public static ObservableCollection<DetailViewModel<PoliceLocalityModel>> PoliceLocality
        {
            get
            {
                return _PoliceLocality;
            }
        }

        public static ObservableCollection<DetailViewModel<TiresModel>> _Tires;

        public static ObservableCollection<DetailViewModel<TiresModel>> Tires
        {
            get
            {
                return _Tires;
            }
        }

        public static ObservableCollection<DetailViewModel<FuelModel>> _Fuel;

        public static ObservableCollection<DetailViewModel<FuelModel>> Fuel
        {
            get
            {
                return _Fuel;
            }
        }

        public static ObservableCollection<DetailViewModel<StatusVehicleModel>> _StatusVehicle;

        public static ObservableCollection<DetailViewModel<StatusVehicleModel>> StatusVehicle
        {
            get
            {
                return _StatusVehicle;
            }
        }

        public static ObservableCollection<DetailViewModel<InsuranceVehicleModel>> _Insurance;

        public static ObservableCollection<DetailViewModel<InsuranceVehicleModel>> Insurance
        {
            get
            {
                return _Insurance;
            }
        }

        public static ObservableCollection<DetailViewModel<ArticleModel>> _Article;

        public static ObservableCollection<DetailViewModel<ArticleModel>> Article
        {
            get
            {
                return _Article;
            }
        }

        public static ObservableCollection<DetailViewModel<ArticleTypeModel>> _ArticleType;

        public static ObservableCollection<DetailViewModel<ArticleTypeModel>> ArticleType
        {
            get
            {
                return _ArticleType;
            }
        }

        public static ObservableCollection<DetailViewModel<SizeClothingModel>> _SizeClothing;

        public static ObservableCollection<DetailViewModel<SizeClothingModel>> SizeClothing
        {
            get
            {
                return _SizeClothing;
            }
        }

        public static ObservableCollection<DetailViewModel<OrderArticleModel>> _OrderArticle;

        public static ObservableCollection<DetailViewModel<OrderArticleModel>> OrderArticle
        {
            get
            {
                return _OrderArticle;
            }
        }

        public static ObservableCollection<DetailViewModel<UserModel>> _User;

        public static ObservableCollection<DetailViewModel<UserModel>> User
        {
            get
            {
                return _User;
            }
        }

        public static ObservableCollection<DetailViewModel<OrderStatusModel>> _OrderStatus;

        public static ObservableCollection<DetailViewModel<OrderStatusModel>> OrderStatus
        {
            get
            {
                return _OrderStatus;
            }
        }

        public static ObservableCollection<DetailViewModel<ArticleModel>> _ArticleForOrder;

        public static ObservableCollection<DetailViewModel<ArticleModel>> ArticleForOrder
        {
            get
            {
                return _ArticleForOrder;
            }
        }

        public static ObservableCollection<DetailViewModel<StatusTireModel>> _StatusTires;

        public static ObservableCollection<DetailViewModel<StatusTireModel>> StatusTires
        {
            get
            {
                return _StatusTires;
            }
        }

        public static ObservableCollection<DetailViewModel<ArticleAttributionModel>> _Attribution;

        public static ObservableCollection<DetailViewModel<ArticleAttributionModel>> Attribution
        {
            get
            {
                return _Attribution;
            }
        }

        public static ObservableCollection<DetailViewModel<ArticleAttributionModel>> _BaseAttribution;

        public static ObservableCollection<DetailViewModel<ArticleAttributionModel>> BaseAttribution
        {
            get
            {
                return _BaseAttribution;
            }
        }

        public static ObservableCollection<DetailViewModel<ArticleModel>> _BaseArticle;

        public static ObservableCollection<DetailViewModel<ArticleModel>> BaseArticle
        {
            get
            {
                return _BaseArticle;
            }
        }

        public static ObservableCollection<DetailViewModel<StateArticleAttributionModel>> _StateArticleAttribution;

        public static ObservableCollection<DetailViewModel<StateArticleAttributionModel>> StateArticleAttribution
        {
            get
            {
                return _StateArticleAttribution;
            }
        }

        public static ObservableCollection<DetailViewModel<ProfileLevelModel>> _ProfileLevel;

        public static ObservableCollection<DetailViewModel<ProfileLevelModel>> ProfileLevel
        {
            get
            {
                return _ProfileLevel;
            }
        }

        public static ObservableCollection<DetailViewModel<GradePointModel>> _GradePoint;

        public static ObservableCollection<DetailViewModel<GradePointModel>> GradePoint
        {
            get
            {
                return _GradePoint;
            }
        }

        public static ObservableCollection<DetailViewModel<InvoiceModel>> _Invoice;

        public static ObservableCollection<DetailViewModel<InvoiceModel>> Invoice
        {
            get
            {
                return _Invoice;
            }
        }

        public static ObservableCollection<DetailViewModel<ServicingModel>> _BaseServicing;

        public static ObservableCollection<DetailViewModel<ServicingModel>> BaseServicing
        {
            get
            {
                return _BaseServicing;
            }
        }

        public static ObservableCollection<DetailViewModel<InvoiceTypeModel>> _InvoiceType;

        public static ObservableCollection<DetailViewModel<InvoiceTypeModel>> InvoiceType
        {
            get
            {
                return _InvoiceType;
            }
        }
        #endregion

        public MainCollectionViewModel()
        {
            // Initialize observable collection
            _Servicing = new ObservableCollection<DetailViewModel<ServicingModel>>();
            _Provider = new ObservableCollection<DetailViewModel<ProviderModel>>();
            _TypeServicing = new ObservableCollection<DetailViewModel<TypeServicingModel>>();
            _Vehicle = new ObservableCollection<DetailViewModel<VehicleModel>>();
            _ProviderType = new ObservableCollection<DetailViewModel<ProviderTypeModel>>();
            _Tires = new ObservableCollection<DetailViewModel<TiresModel>>();
            _PoliceLocality = new ObservableCollection<DetailViewModel<PoliceLocalityModel>>();
            _Fuel = new ObservableCollection<DetailViewModel<FuelModel>>();
            _StatusVehicle = new ObservableCollection<DetailViewModel<StatusVehicleModel>>();
            _Insurance = new ObservableCollection<DetailViewModel<InsuranceVehicleModel>>();
            _Article = new ObservableCollection<DetailViewModel<ArticleModel>>();
            _ArticleType = new ObservableCollection<DetailViewModel<ArticleTypeModel>>();
            _SizeClothing = new ObservableCollection<DetailViewModel<SizeClothingModel>>();
            _OrderArticle = new ObservableCollection<DetailViewModel<OrderArticleModel>>();
            _User = new ObservableCollection<DetailViewModel<UserModel>>();
            _OrderStatus = new ObservableCollection<DetailViewModel<OrderStatusModel>>();
            _ArticleForOrder = new ObservableCollection<DetailViewModel<ArticleModel>>();
            _StatusTires = new ObservableCollection<DetailViewModel<StatusTireModel>>();
            _Attribution = new ObservableCollection<DetailViewModel<ArticleAttributionModel>>();
            _BaseAttribution = new ObservableCollection<DetailViewModel<ArticleAttributionModel>>();
            _StateArticleAttribution = new ObservableCollection<DetailViewModel<StateArticleAttributionModel>>();
            _BaseArticle = new ObservableCollection<DetailViewModel<ArticleModel>>();
            _ProfileLevel = new ObservableCollection<DetailViewModel<ProfileLevelModel>>();
            _GradePoint = new ObservableCollection<DetailViewModel<GradePointModel>>();
            _Invoice = new ObservableCollection<DetailViewModel<InvoiceModel>>();
            _BaseServicing = new ObservableCollection<DetailViewModel<ServicingModel>>();
            _InvoiceType = new ObservableCollection<DetailViewModel<InvoiceTypeModel>>();

            // Initialize the request DB
            RS = new RequestDBServicing();
            RP = new RequestDBProvider();
            RTS = new RequestDBTypeServicing();
            RV = new RequestDBVehicle();
            RPT = new RequestDBProviderType();
            RT = new RequestDBTires();
            RPL = new RequestDBPoliceLocality();
            RF = new RequestDBFuel();
            RSV = new RequestDBStatusVehicle();
            RIV = new RequestDBInsuranceVehicle();
            RA = new RequestDBArticle();
            RAT = new RequestDBArticleType();
            RSC = new RequestDBSizeClothing();
            ROA = new RequestDBOrderArticle();
            RU = new RequestDBUser();
            ROS = new RequestDBOrderStatus();
            RST = new RequestDBStatusTires();
            RSAA = new RequestDBStateArticleAttribution();
            RAA = new RequestDBArticleAttribution();
            RPLE = new RequestDBProfileLevel();
            RGP = new RequestDBGradePoint();
            RI = new RequestDBInvoice();
            RIT = new RequestDBInvoiceType();

            // Initizalize default value
            typeArticleAttribution = 0;

            foreach (var servicing in RS.SelectAllElement())
            {
                _Servicing.Add(new DetailViewModel<ServicingModel>(new ServicingModel(servicing)));
                _BaseServicing.Add(new DetailViewModel<ServicingModel>(new ServicingModel(servicing)));
            }
            foreach (var provider in RP.SelectAllElement())
            {
                _Provider.Add(new DetailViewModel<ProviderModel>(new ProviderModel(provider)));
            }
            foreach (var typeservicing in RTS.SelectAllElement())
            {
                _TypeServicing.Add(new DetailViewModel<TypeServicingModel>(new TypeServicingModel(typeservicing)));
            }
            foreach (var vehicle in RV.SelectAllElement())
            {
                _Vehicle.Add(new DetailViewModel<VehicleModel>(new VehicleModel(vehicle)));
            }
            foreach (var providertype in RPT.SelectAllElement())
            {
                _ProviderType.Add(new DetailViewModel<ProviderTypeModel>(new ProviderTypeModel(providertype)));
            }
            foreach (var tires in RT.SelectAllElement())
            {
                _Tires.Add(new DetailViewModel<TiresModel>(new TiresModel(tires)));
            }
            foreach (var policelocality in RPL.SelectAllElement())
            {
                _PoliceLocality.Add(new DetailViewModel<PoliceLocalityModel>(new PoliceLocalityModel(policelocality)));
            }
            foreach (var fuel in RF.SelectAllElement())
            {
                _Fuel.Add(new DetailViewModel<FuelModel>(new FuelModel(fuel)));
            }
            foreach (var statusvehicle in RSV.SelectAllElement())
            {
                _StatusVehicle.Add(new DetailViewModel<StatusVehicleModel>(new StatusVehicleModel(statusvehicle)));
            }
            foreach (var insurance in RIV.SelectAllElement())
            {
                _Insurance.Add(new DetailViewModel<InsuranceVehicleModel>(new InsuranceVehicleModel(insurance)));
            }
            foreach (var article in RA.SelectAllElement())
            {
                _Article.Add(new DetailViewModel<ArticleModel>(new ArticleModel(article)));
                _ArticleForOrder.Add(new DetailViewModel<ArticleModel>(new ArticleModel(article)));
                _BaseArticle.Add(new DetailViewModel<ArticleModel>(new ArticleModel(article)));
            }
            foreach (var articletype in RAT.SelectAllElement())
            {
                _ArticleType.Add(new DetailViewModel<ArticleTypeModel>(new ArticleTypeModel(articletype)));
            }
            foreach (var sizeclothing in RSC.SelectAllElement())
            {
                _SizeClothing.Add(new DetailViewModel<SizeClothingModel>(new SizeClothingModel(sizeclothing)));
            }
            foreach (var orderarticle in ROA.SelectAllElement())
            {
                _OrderArticle.Add(new DetailViewModel<OrderArticleModel>(new OrderArticleModel(orderarticle)));
            }
            foreach (var user in RU.SelectAllElement())
            {
                // Only the active users is loaded
                //if (user.status)
                    _User.Add(new DetailViewModel<UserModel>(new UserModel(user)));
            }
            foreach (var statusorder in ROS.SelectAllElement())
            {
                _OrderStatus.Add(new DetailViewModel<OrderStatusModel>(new OrderStatusModel(statusorder)));
            }
            foreach (var status in RST.SelectAllElement())
            {
                _StatusTires.Add(new DetailViewModel<StatusTireModel>(new StatusTireModel(status)));
            }
            foreach (var state in RSAA.SelectAllElement())
            {
                _StateArticleAttribution.Add(new DetailViewModel<StateArticleAttributionModel>(new StateArticleAttributionModel(state)));
            }
            foreach (var attribution in RAA.SelectAllElement())
            {
                _Attribution.Add(new DetailViewModel<ArticleAttributionModel>(new ArticleAttributionModel(attribution)));
                _BaseAttribution.Add(new DetailViewModel<ArticleAttributionModel>(new ArticleAttributionModel(attribution)));
            }
            foreach (var profileLevel in RPLE.SelectAllElement())
            {
                _ProfileLevel.Add(new DetailViewModel<ProfileLevelModel>(new ProfileLevelModel(profileLevel)));
            }
            foreach (var grade in RGP.SelectAllElement())
            {
                _GradePoint.Add(new DetailViewModel<GradePointModel>(new GradePointModel(grade)));
            }
            // Loaded when type selected
            //foreach (var invoice in RI.SelectAllElement())
            //{
            //    _Invoice.Add(new DetailViewModel<InvoiceModel>(new InvoiceModel(invoice)));
            //}
            foreach (var invoicetype in RIT.SelectAllElement())
            {
                _InvoiceType.Add(new DetailViewModel<InvoiceTypeModel>(new InvoiceTypeModel(invoicetype)));
            }
        }
    }
}
