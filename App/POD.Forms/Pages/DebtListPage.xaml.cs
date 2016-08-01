using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POD.Forms.ViewModels;
using Xamarin.Forms;

namespace POD.Forms.Pages
{
    public partial class DebtListPage : DebtListPageXaml
    {
        public DebtListPage()
        {
            InitializeComponent();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            ViewModel.LoadDebtsCommand.Execute(null);
        }
    }

    // Just used to declare in XAML
    public class DebtListPageXaml : BaseContentPage<DebtListViewModel>
    {
    }
}
