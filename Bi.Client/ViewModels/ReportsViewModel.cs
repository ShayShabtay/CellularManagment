﻿using Bi.Client.BL;
using Bi.Client.Halpers;
using Bi.Client.Utils;
using Common.Models;
using Common.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Bi.Client.ViewModels
{
    public class ReportsViewModel : ViewModelPropertyChanged
    {
        private IFrameNavigationService _frameNavigationService;
        private ReportsManager _reportsManager;

        public ICommand LogoutCommand { get; set; }
        public ICommand GeneratePaymentsCommand { get; set; }


        private ObservableCollection<MostCallCustomerDTO> _mostCallCustomes;
        public ObservableCollection<MostCallCustomerDTO> MostCallCustomes
        {
            get { return _mostCallCustomes; }
            set
            {
                _mostCallCustomes = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<EmployeeBiDTO> _bestSellers;
        public ObservableCollection<EmployeeBiDTO> BestSellers
        {
            get { return _bestSellers; }
            set
            {
                _bestSellers = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ProfitableCustomerDTO> _ProfitableCustomers;
        public ObservableCollection<ProfitableCustomerDTO> ProfitableCustomers
        {
            get { return _ProfitableCustomers; }
            set
            {
                _ProfitableCustomers = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CustomerBiDTO> _disconnectionRiskClient;
        public ObservableCollection<CustomerBiDTO> DisconnectionRiskClient

        {
            get { return _disconnectionRiskClient; }
            set
            {
                _disconnectionRiskClient = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CustomerBiDTO> _customersOpinionLeaders;
        public ObservableCollection<CustomerBiDTO> CustomersOpinionLeaders

        {
            get { return _customersOpinionLeaders; }
            set
            {
                _customersOpinionLeaders = value;
                OnPropertyChanged();
            }
        }

        // ctor
        public ReportsViewModel(IFrameNavigationService frameNavigationService)
        {
            _frameNavigationService = frameNavigationService;
            _reportsManager = new ReportsManager();
            LogoutCommand = new RelayCommand(() =>
            {
                ViewModelLocator.UnRegisterReportsViewModel();
                _frameNavigationService.NavigateTo("Login");
            });
            GeneratePaymentsCommand = new RelayCommand(() =>
            {
                _reportsManager.GeneratePayments();
            });
            GetReports();
        }

        private void GetReports()
        {
            try
            {
                MostCallCustomes = new ObservableCollection<MostCallCustomerDTO>(_reportsManager.GetReports<MostCallCustomerDTO>("mostCallingToCenterCustomers"));
                BestSellers = new ObservableCollection<EmployeeBiDTO>(_reportsManager.GetReports<EmployeeBiDTO>("bestSellerEmployees"));
                ProfitableCustomers = new ObservableCollection<ProfitableCustomerDTO>(_reportsManager.GetReports<ProfitableCustomerDTO>("mostProfitableCustomers"));
                DisconnectionRiskClient = new ObservableCollection<CustomerBiDTO>(_reportsManager.GetReports<CustomerBiDTO>("linesAtRiskOfAbandonment"));
                CustomersOpinionLeaders = new ObservableCollection<CustomerBiDTO>(_reportsManager.GetReports<CustomerBiDTO>("opinionLeadersCustomers"));
            }
            catch (Exception)
            {
                MessageBox.Show("failed to get the date");
            }
        }
    }
}
