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

        ////////// Sandra Polska //////////
        ////////// --- tab1 --- //////////

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
                try
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
                catch (FormatException ex)
                {
                    tbSummary.Text += ex.Message;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    tbSummary.Text += ex.Message;
                }
                catch (OverflowException ex)
                {
                    tbSummary.Text += ex.Message;
                }
            }
            else if (rbA2.IsChecked == true)
            {
                try
                {
                    Cipher cipher = new Cipher(tbPlainText.Text, tbKey.Text);
                    if (rbM1.IsChecked == true)
                    {
                        cipher.code();
                    }
                    else
                        cipher.decode();
                    tbSummary.Text += cipher.getResult();
                }
                catch (ArgumentException ex)
                {
                    tbSummary.Text += ex.Message;
                }

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
                    char padChar = ' ';

                    string plainText = ct.Decipher(cipherText, key, padChar);
                    plainText = Regex.Replace(plainText, @"\s", "");
                    tbSummary.Text += plainText;
                }
            }
        }

        ////////// Sandra Polska //////////
        ////////// --- tab2 --- //////////

        private void Clear1sub(object sender, RoutedEventArgs e)
        {
            tbPlainText2.Text = "";
            tbPlainText2.Focus();
        }

        private void Clear2sub(object sender, RoutedEventArgs e)
        {
            tbKey2.Text = "";
            tbKey2.Focus();
        }

        private void Clear3sub(object sender, RoutedEventArgs e)
        {
            tbCipherText2.Text = "";
            tbCipherText2.Focus();
        }

        private void Method_Click2(object sender, RoutedEventArgs e)
        {
            if (rb2M1.IsChecked == true)
            {
                CipherText2.IsEnabled = false;
                tbCipherText2.IsEnabled = false;
                bCipherText2.IsEnabled = false;
                PlainText2.IsEnabled = true;
                tbPlainText2.IsEnabled = true;
                bPlainText2.IsEnabled = true;
            }
            else if (rb2M2.IsChecked == true)
            {
                CipherText2.IsEnabled = true;
                tbCipherText2.IsEnabled = true;
                bCipherText2.IsEnabled = true;
                PlainText2.IsEnabled = false;
                tbPlainText2.IsEnabled = false;
                bPlainText2.IsEnabled = false;
            }
        }

        private void LetsDoThis2(object sender, RoutedEventArgs e)
        {
            tbSummary2.Text = "Result\n--\n";

            if (rb2A1.IsChecked == true)
            {
                if (rb2M1.IsChecked == true)
                {
                    ColumnTransposition2 coder = new ColumnTransposition2(tbPlainText2.Text, tbKey2.Text);
                    coder.code();
                    tbSummary2.Text += coder.GetCode();
                }
                if (rb2M2.IsChecked == true)
                {

                    ColumnTransposition2 coder = new ColumnTransposition2(tbCipherText2.Text, tbKey2.Text);
                    coder.decode();
                    tbSummary2.Text += coder.GetDecode();
                }

            }
            else if (rb2A2.IsChecked == true)
            {

            }
            else if (rb2A3.IsChecked == true)
            {
                Vigenere vig = new Vigenere();

                if (rb2M1.IsChecked == true)
                {
                    string plainText = tbPlainText2.Text;
                    string key = tbKey2.Text;

                    string cipherText = vig.Cipher(plainText, key, true);

                    tbSummary2.Text += cipherText;
                }
                else
                {
                    string cipherText = tbCipherText2.Text;
                    string key = tbKey2.Text;

                    string plainText = vig.Cipher(cipherText, key, false);

                    tbSummary2.Text += plainText;
                }
            }
        }
    }
}
