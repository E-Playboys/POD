﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace POD.Forms.Views
{
    class ExtendedListView : ListView
    {
        private bool _isLoadingMore;

        #region Bindable Properties

        public static BindableProperty ItemClickCommandProperty = BindableProperty.Create("ItemClickCommand",
            typeof(ICommand), typeof(ExtendedListView));

        public static BindableProperty LoadMoreCommandProperty = BindableProperty.Create("LoadMoreCommand",
            typeof(ICommand), typeof(ExtendedListView));

        public static BindableProperty AllowSelectItemProperty = BindableProperty.Create("AllowSelectItem",
            typeof(bool), typeof(ExtendedListView), false);

        #endregion

        #region Properties

        public ICommand ItemClickCommand
        {
            get { return (ICommand)GetValue(ItemClickCommandProperty); }
            set { SetValue(ItemClickCommandProperty, value); }
        }

        public ICommand LoadMoreCommand
        {
            get { return (ICommand)GetValue(LoadMoreCommandProperty); }
            set { SetValue(LoadMoreCommandProperty, value); }
        }

        public bool AllowSelectItem
        {
            get { return (bool)GetValue(AllowSelectItemProperty); }
            set { SetValue(AllowSelectItemProperty, value); }
        }

        #endregion

        public ExtendedListView()
        {
            RegisterEvents();
        }

        public ExtendedListView(ListViewCachingStrategy cachingStrategy) : base(cachingStrategy)
        {
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            ItemTapped -= OnItemTapped;
            ItemAppearing -= OnItemAppearing;

            ItemTapped += OnItemTapped;
            ItemAppearing += OnItemAppearing;
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && ItemClickCommand != null && ItemClickCommand.CanExecute(e))
            {
                ItemClickCommand.Execute(e.Item);
            }

            if (!AllowSelectItem)
            {
                SelectedItem = null;
            }
        }

        private void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var items = ItemsSource as IList;
            if (_isLoadingMore || items == null || items.Count == 0 || LoadMoreCommand == null || !LoadMoreCommand.CanExecute(e))
                return;

            // Hit the bottom
            if (e.Item == items[items.Count - 1])
            {
                _isLoadingMore = true;

                LoadMoreCommand.Execute(null);

                _isLoadingMore = false;
            }
        }
    }
}
