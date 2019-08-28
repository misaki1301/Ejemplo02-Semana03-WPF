﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Semana03_ejemplo2WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataAccess obj = new DataAccess();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxYearListOrders.ItemsSource = obj.getYearofOrders().DefaultView;
            comboBoxYearListOrders.DisplayMemberPath = "years";
            comboBoxYearListOrders.SelectedValuePath = "years";
        }

        private void ComboBoxYearListOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Int32 cod = Convert.ToInt32(comboBoxYearListOrders.SelectedValue);
            comboBoxMonthListOrders.ItemsSource = obj.getMonthofOrdersByYear(cod).DefaultView;
            comboBoxMonthListOrders.DisplayMemberPath = "month_name";
            comboBoxMonthListOrders.SelectedValuePath = "month_number";
        }

        private void ComboBoxMonthListOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string year = comboBoxYearListOrders.SelectedValue.ToString();
            int month = Convert.ToInt32(comboBoxMonthListOrders.SelectedValue);
            string aux = "";
            if (month < 10 && month > 0)
            {
                aux = "0" + month.ToString();
            }
            else
            {
                aux = month.ToString();
            }
            comboBoxEmployees.ItemsSource = obj.getEmployeesByYearAndMonth(aux, year).DefaultView;
            comboBoxEmployees.DisplayMemberPath = "Nombre";
            comboBoxEmployees.SelectedValuePath = "IdEmpleado";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(this, "clicked image button");
            string year = comboBoxYearListOrders.SelectedValue.ToString();
            int month = Convert.ToInt32(comboBoxMonthListOrders.SelectedValue);
            string aux = "";
            if (month < 10 && month > 0)
            {
                aux = "0" + month.ToString();
            }
            else
            {
                aux = month.ToString();
            }
            int codEmployee = Convert.ToInt32(comboBoxEmployees.SelectedValue);
            dataGridCliente.ItemsSource = obj.getClientsByCustomerperYearAndMonth(aux, year,codEmployee).DefaultView;
        }

        private void DataGridCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string year = comboBoxYearListOrders.SelectedValue.ToString();
            int month = Convert.ToInt32(comboBoxMonthListOrders.SelectedValue);
            string aux = "";
            if (month < 10 && month > 0)
            {
                aux = "0" + month.ToString();
            }
            else
            {
                aux = month.ToString();
            }
            object item = dataGridCliente.SelectedItem;
            try
            {
                string ID = (dataGridCliente.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                int codEmployee = Convert.ToInt32(comboBoxEmployees.SelectedValue);
                DataGridPedido.ItemsSource = obj.getOrderByClientSelected(aux, year, codEmployee, ID).AsDataView();
                textBoxTotalPedidos.Text = (DataGridPedido.Items.Count - 1).ToString();
            }
            catch (Exception error)
            {
                MessageBox.Show("Se fue a la verga");
            }
            //int customerId = Convert.ToInt32((dataGridCliente.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text);
            
        }

        private void DataGridPedido_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = DataGridPedido.SelectedItem;
            string ID = (DataGridPedido.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            MessageBox.Show(ID);
        }
    }
}
