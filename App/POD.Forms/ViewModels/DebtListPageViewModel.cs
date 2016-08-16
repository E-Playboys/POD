using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using POD.Forms.Utilities;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace POD.Forms.ViewModels
{
    public class DebtListPageViewModel : BaseViewModel
    {
        private List<DebtItemModel> _dummyData = new List<DebtItemModel>
        {
            new DebtItemModel
                {
                    Icon = "icon_house.png",
                    Color = Color.Maroon,
                    Name = "Buy House",
                    PaidPercent = 30,
                    PaidAmount = 4500,
                    CurrentBalance = 10000,
                    StartingDebtAmount = 50000,
                    PlannedMonthlyPayment = 5000,
                    EstimatedTimeLeft = "50 years",
                    LastPaymentDate = DateTime.Now
                },
                new DebtItemModel
                {
                    Icon = "icon_wife.png",
                    Color = Color.Navy,
                    Name = "Buy Wife",
                    PaidPercent = 80,
                    PaidAmount = 7500,
                    CurrentBalance = 7000,
                    StartingDebtAmount = 10000,
                    PlannedMonthlyPayment = 900,
                    EstimatedTimeLeft = "10 years",
                    LastPaymentDate = DateTime.Now
                },
                new DebtItemModel
                {
                    Icon = "icon_kid.png",
                    Color = Color.Yellow,
                    Name = "Buy Kids",
                    PaidPercent = 50,
                    PaidAmount = 9500,
                    CurrentBalance = 2000,
                    StartingDebtAmount = 30000,
                    PlannedMonthlyPayment = 1000,
                    EstimatedTimeLeft = "20 years",
                    LastPaymentDate = DateTime.Now
                },
                new DebtItemModel
                {
                    Icon = "icon_kid.png",
                    Color = Color.Yellow,
                    Name = "Buy Kids",
                    PaidPercent = 50,
                    PaidAmount = 9500,
                    CurrentBalance = 2000,
                    StartingDebtAmount = 30000,
                    PlannedMonthlyPayment = 1000,
                    EstimatedTimeLeft = "20 years",
                    LastPaymentDate = DateTime.Now
                }
        };

        private ObservableRangeCollection<DebtItemModel> _debts;
        public ObservableRangeCollection<DebtItemModel> Debts
        {
            get { return _debts ?? (_debts = new ObservableRangeCollection<DebtItemModel>()); }
            set { SetProperty(ref _debts, value); }
        }

        public ICommand LoadDebtsCommand { get; set; }

        public DebtListPageViewModel(IAppService appService) 
            : base(appService)
        {
            LoadDebtsCommand = new Command(async () => await ExecuteLoadDebtsCommand());
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            LoadDebtsCommand.Execute(null);
        }

        private async Task ExecuteLoadDebtsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            Debts.AddRange(_dummyData);
            Title = $"Debts ({Debts.Count})";

            IsBusy = false;
        }

        public class DebtItemModel
        {
            public string Icon { get; set; }
            public Color Color { get; set; }
            public string Name { get; set; }
            public double PaidPercent { get; set; }
            public double PaidAmount { get; set; }
            public double CurrentBalance { get; set; }
            public double StartingDebtAmount { get; set; }
            public double PlannedMonthlyPayment { get; set; }
            public string EstimatedTimeLeft { get; set; }
            public DateTime LastPaymentDate { get; set; }
        }
    }
}
