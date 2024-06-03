using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.ToolBox;

namespace CorpManagement.Model
{
    class OrderStatusModel : ObservableObject
    {
        private readonly OrderStatus _OrderStatus;

        public OrderStatusModel(OrderStatus OrderStatus)
        {
            _OrderStatus = OrderStatus;
            RaisePropertyChanged();
        }

        public int id
        {
            get
            {
                return _OrderStatus.id;
            }
            set
            {
                if (_OrderStatus.id != value)
                    _OrderStatus.id = value;
                RaisePropertyChanged(() => id);
            }
        }

        public string name
        {
            get
            {
                return _OrderStatus.name;
            }
            set
            {
                if (_OrderStatus.name != value)
                    _OrderStatus.name = value;
                RaisePropertyChanged(() => name);
            }
        }
    }
}
