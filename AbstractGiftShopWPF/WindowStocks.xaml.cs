﻿using AbstractGiftShopServiceDAL.Interfaces;
using AbstractGiftShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Unity;

namespace AbstractGiftShopWPF
{
    /// <summary>
    /// Логика взаимодействия для WindowStocks.xaml
    /// </summary>
    public partial class WindowStocks : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly ISStockService service;

        public WindowStocks(ISStockService service)
        {
            InitializeComponent();
            Loaded += WindowStocks_Load;
            this.service = service;
        }

        private void WindowStocks_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                List<SStockViewModel> list = service.GetList();
                if (list != null)
                {
                    dataGridViewStocks.ItemsSource = list;
                    dataGridViewStocks.Columns[0].Visibility = Visibility.Hidden;
                    dataGridViewStocks.Columns[1].Width = DataGridLength.Auto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<WindowStock>();
            if (form.ShowDialog() == true)
                LoadData();
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewStocks.SelectedItem != null)
            {
                var form = Container.Resolve<WindowStock>();
                form.Id = ((SStockViewModel)dataGridViewStocks.SelectedItem).Id;
                if (form.ShowDialog() == true)
                {
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewStocks.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запись?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int id = ((SStockViewModel)dataGridViewStocks.SelectedItem).Id;
                    try
                    {
                        service.DelElement(id);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}