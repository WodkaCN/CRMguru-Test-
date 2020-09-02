﻿using System;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CountryChecker__CRMguru_TEST_
{
    public partial class MainWindow : Window
    {
        private DataTable DT;
        private string[] Data = new string[6];
        public MainWindow()
        {
            InitializeComponent();
        }
        //----Drag Move----
        private void Win_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        //----Core Controls----
        private void X_Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void Min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        //----Simple WaterMark----
        private void CountryBox_MouseEnter(object sender, MouseEventArgs e)
        {
            CountryBox.Foreground = Brushes.Black;
            if (CountryBox.Text == "Enter Country Name...") CountryBox.Text = "";
        }
        private void CountryBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (CountryBox.Text == "" && !CountryBox.IsFocused)
            {
                CountryBox.Foreground = Brushes.LightGray;
                CountryBox.Text = "Enter Country Name...";
            }
        }
        //----Anti SQLInjection----
        private void CountryBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            for (int i = 0; i < CountryBox.Text.Length; i++)
            {
                if (CountryBox.Text[i] == ';')
                {
                    CC_Button.IsEnabled = false;
                    UnacceptedSLabel.Content = $"Unaccepted symbol \'{CountryBox.Text[i]}\'";
                }
                else
                {
                    if (CountryBox.Text != "Enter Country Name...")
                    {
                        CC_Button.IsEnabled = true;
                        UnacceptedSLabel.Content = "";
                    }
                }
            }
        }
        //----Main Function----
        private void CC_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GetCountry.GetDataFromSite(CountryBox.Text);
                for (byte i = 0; i != Data.Length; i++)
                {
                    Data[i] = null;
                }
                for (byte i = 0; i != Data.Length; i++)
                {
                    Data[i] = GetCountry.ReturnData(i);
                }
                if (Data[0] != null)
                {
                    NameL.Content = Data[0];
                    RegionL.Content = Data[1];
                    CapitalL.Content = Data[2];
                    CodeL.Content = Data[3];
                    AreaL.Content = Data[4];
                    PopulationL.Content = Data[5];
                    StatusL.Content = "Data Obtained";
                    SC_Button.IsEnabled = true;
                }
                else
                {
                    NameL.Content = "-";
                    RegionL.Content = "-";
                    CapitalL.Content = "-";
                    CodeL.Content = "-";
                    AreaL.Content = "-";
                    PopulationL.Content = "-";
                    StatusL.Content = $"ERROR 0x0 - Country \"{CountryBox.Text}\" not found";
                    SC_Button.IsEnabled = false;
                }
            }
            catch
            {
                StatusL.Content = $"ERROR 0x0 - Country \"{CountryBox.Text}\" not found";
                SC_Button.IsEnabled = false;
            }
            if ((string)StatusL.Content == "Data Obtained") StatusL.Foreground = Brushes.LightGreen;
            else if((string)StatusL.Content != "Data saved") StatusL.Foreground = Brushes.DarkRed;
        }

        //----SQL Controls----
        private void SC_Button_Click(object sender, RoutedEventArgs e)
        {
            SQLTasks.Connetion();

            SQLTasks.Insert(NameL.Content.ToString(), RegionL.Content.ToString(), CapitalL.Content.ToString(), CodeL.Content.ToString(), AreaL.Content.ToString(), PopulationL.Content.ToString());
            StatusL.Content = "Data saved";
            StatusL.Foreground = Brushes.LightSkyBlue;

            SQLTasks.ConClose();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            SQLTasks.Connetion();

            DT = SQLTasks.SelectAll();
            MDataGrid.ItemsSource = null;
            MDataGrid.ItemsSource = DT.AsDataView();

            SQLTasks.ConClose();
        }
    }
}
