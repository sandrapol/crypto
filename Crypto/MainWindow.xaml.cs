﻿using System;
using System.Collections.Generic;
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
using System.Text.RegularExpressions;

namespace Crypto
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ColumnTransposition ct = new ColumnTransposition();

        private void Clear1(object sender, RoutedEventArgs e)
        {
            tbPlainText.Text = "";
            tbPlainText.Focus();
        }

        private void Clear2(object sender, RoutedEventArgs e)
        {
            tbKey.Text = "";
            tbKey.Focus();
        }

        private void Clear3(object sender, RoutedEventArgs e)
        {
            tbCipherText.Text = "";
            tbCipherText.Focus();
        }

        private void Method_Click(object sender, RoutedEventArgs e)
        {
            if (rbM1.IsChecked == true)
            {
                lCipherText.IsEnabled = false;
                tbCipherText.IsEnabled = false;
                bCipherText.IsEnabled = false;
                lPlainText.IsEnabled = true;
                tbPlainText.IsEnabled = true;
                bPlainText.IsEnabled = true;
            }
            else if (rbM2.IsChecked == true)
            {
                lCipherText.IsEnabled = true;
                tbCipherText.IsEnabled = true;
                bCipherText.IsEnabled = true;
                lPlainText.IsEnabled = false;
                tbPlainText.IsEnabled = false;
                bPlainText.IsEnabled = false;
            }
        }

        private void LetsDoThis(object sender, RoutedEventArgs e)
        {
            tbSummary.Text = "Result\n--\n";

            if (rbA1.IsChecked == true)
            {
                RailFence rf = new RailFence();
               

                if (rbM1.IsChecked == true)
                {
                    string plainText = tbPlainText.Text;
                    int key = int.Parse(tbKey.Text);

                    string cipherText = rf.Encrypt(plainText, key);

                    tbSummary.Text += cipherText;
                }
                else
                {
                    string cipherText = tbCipherText.Text;
                    int key = int.Parse(tbKey.Text);

                    string plainText = rf.Decrypt(cipherText, key);

                    tbSummary.Text += plainText;
                }
            }
            else if (rbA2.IsChecked == true)
            {
                

            }
            else if (rbA3.IsChecked == true)
            {
                if (rbM1.IsChecked == true)
                { 
                string plainText = tbPlainText.Text;
                string key = tbKey.Text;
                char padChar = ' ';

                string cipherText = ct.Encipher(plainText, key, padChar);
                cipherText = Regex.Replace(cipherText, @"\s", "");

                tbSummary.Text += cipherText;
                }

                else
                {
                    string cipherText = tbCipherText.Text;
                    string key = tbKey.Text;

                    string plainText = ct.Decipher(cipherText, key);

                    plainText = Regex.Replace(plainText, @"\s", "");
                    tbSummary.Text += plainText;
                }
            }
        }
    }
}
