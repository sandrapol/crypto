using System;
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
                    Cipher cipher;
                    if (rbM1.IsChecked == true)
                    {
                        cipher = new Cipher(tbPlainText.Text, tbKey.Text);
                        cipher.code();
                        tbSummary.Text += cipher.getResult();
                    }
                    else
                    {
                        cipher = new Cipher(tbCipherText.Text, tbKey.Text);
                        cipher.decode();
                        tbSummary.Text += cipher.getDecodeResult();
                    }

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
                    ColumnTransposition coder = new ColumnTransposition(tbPlainText.Text, tbKey.Text);
                    coder.code();
                    tbSummary.Text += coder.GetCode();
                }

                if (rbM2.IsChecked == true)
                {
                    ColumnTransposition coder = new ColumnTransposition(tbCipherText.Text, tbKey.Text);
                    coder.decode();
                    tbSummary.Text += coder.GetDecode();
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
                JuliuszCezar juliusz = new JuliuszCezar();
                if (rb2M1.IsChecked == true)
                {
                    string plainText = tbPlainText2.Text;
                    int key = int.Parse(tbKey2.Text);

                    string cipherText = juliusz.Encipher(plainText, key);

                    tbSummary2.Text += cipherText;
                }
                else
                {
                    string cipherText = tbCipherText2.Text;
                    int key = int.Parse(tbKey2.Text);

                    string plainText = juliusz.Decipher(cipherText, key);

                    tbSummary2.Text += plainText;
                }

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

        ////////// -- Stream -- //////////
        ////////// --- tab3 --- //////////

        private void Clear1a(object sender, RoutedEventArgs e)
        {
            tbInput.Text = "";
            tbInput.Focus();
        }

        private void Clear2a(object sender, RoutedEventArgs e)
        {
            tbKey3.Text = "";
            tbKey3.Focus();
        }

        private void Clear3a(object sender, RoutedEventArgs e)
        {
            tbFile.Text = "";
            tbFile.Focus();
        }

        private void Method_Click3(object sender, RoutedEventArgs e)
        {
            if (rA1.IsChecked == true)
            {
                lKey3.IsEnabled = true;
                tbKey3.IsEnabled = true;
                bKey3.IsEnabled = true;

                lFile.IsEnabled = false;
                tbFile.IsEnabled = false;
                bFile.IsEnabled = false;

                rM2.IsEnabled = false;
            }
            else
            {
                lKey3.IsEnabled = false;
                tbKey3.IsEnabled = false;
                bKey3.IsEnabled = false;

                if (rA3.IsChecked==true)
                {
                    lKey3.IsEnabled = true;
                    tbKey3.IsEnabled = true;
                    bKey3.IsEnabled = true;
                }

                lFile.IsEnabled = true;
                tbFile.IsEnabled = true;
                bFile.IsEnabled = true;

                rM2.IsEnabled = true;
            }
        }

        private void LoadFile(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

            Nullable<bool> result = openFileDlg.ShowDialog();

            if (result == true)
            {
                tbFile.Text = openFileDlg.FileName;
                tbSummary3.Text = "Plik wczytano prawidłowo.";
            }
        }


        List<int> sscBinary;
        List<int> lfsrBinary;
        List<int> fileBinary;

        private void LetsDoThis3(object sender, RoutedEventArgs e)
        {
            tbSummary3.Text = "Result\n--\n";

            if (rA1.IsChecked == true)
            {
                try
                {
                    LFSR lfsr = new LFSR();

                    if (rM1.IsChecked == true)
                    {
                        string iteretations = "", output = "";
                        string LFSRdegree = tbInput.Text;

                        if (!string.IsNullOrEmpty(LFSRdegree) && int.TryParse(tbKey3.Text, out int lfsrLength))
                        {
                            iteretations = "\n--\n";
                            output = lfsr.Encrypt(LFSRdegree, lfsrLength, ref iteretations);
                        }

                        tbSummary3.Text += output + iteretations;
                    }
                }
                catch (FormatException ex)
                {
                    tbSummary3.Text = ex.Message;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    tbSummary3.Text = ex.Message;
                }
            }
            else if (rA2.IsChecked == true)
            {
                SynchronousStreamCipher ssc = new SynchronousStreamCipher(tbInput.Text, tbFile.Text);

                try
                {
                    if (rM1.IsChecked == true)
                    {
                        ssc.Encrypt(ref lfsrBinary, ref sscBinary, ref fileBinary);

                        tbSummary3.Text += "LFSR binary:\n" + ssc.ToString(lfsrBinary) + "\nFile binary:\n" + ssc.ToString(fileBinary) + "\nSSC binary:\n" + ssc.ToString(sscBinary);
                    }
                    else
                    {
                        tbSummary3.Text += ssc.Decrypt(ref lfsrBinary, ref sscBinary);
                    }

                }
                catch (ArgumentException ex)
                {
                    tbSummary3.Text = ex.Message;
                }
                catch (InvalidOperationException ex)
                {
                    tbSummary3.Text = ex.Message;
                }
                catch (FormatException ex)
                {
                    tbSummary3.Text = ex.Message;
                }
                catch (System.IO.FileNotFoundException ex)
                {
                    tbSummary3.Text = ex.Message;
                }
            }
            else if (rA3.IsChecked == true)
            {

                if (rM1.IsChecked == true)
                {
                    Stream3 stream = new Stream3(tbInput.Text, tbKey3.Text, tbFile.Text);
                    List<int> sth = stream.Code();
                    tbSummary3.Text = "Klucz do odkodowania: \n";
                    foreach (var it in sth)
                        tbSummary3.Text += it;
                    tbSummary3.Text += "\n" + stream.getDestination();
                }
                else
                {
                    Stream3 stream = new Stream3(tbInput.Text, tbKey3.Text, tbFile.Text);
                    tbSummary3.Text = stream.Decode();
                }
            }
        }

        ////////// --- DES  --- //////////
        ////////// --- tab4 --- //////////

        private void ClearDES1(object sender, RoutedEventArgs e)
        {
            tbPlainTextDES.Text = "";
            tbPlainTextDES.Focus();
        }

        private void ClearDES2(object sender, RoutedEventArgs e)
        {
            tbKeyDES.Text = "";
            tbKeyDES.Focus();
        }

        private void ClearDES3(object sender, RoutedEventArgs e)
        {
            tbCipherTextDES.Text = "";
            tbCipherTextDES.Focus();
        }

        private void Method_Click4(object sender, RoutedEventArgs e)
        {
            if (rb1.IsChecked == true)
            {
                lCipherTextDES.IsEnabled = false;
                tbCipherTextDES.IsEnabled = false;
                bCipherTextDES.IsEnabled = false;
                lPlainTextDES.IsEnabled = true;
                tbPlainTextDES.IsEnabled = true;
                bPlainTextDES.IsEnabled = true;
            }
            else if (rb2.IsChecked == true)
            {
                lCipherTextDES.IsEnabled = true;
                tbCipherTextDES.IsEnabled = true;
                bCipherTextDES.IsEnabled = true;
                lPlainTextDES.IsEnabled = false;
                tbPlainTextDES.IsEnabled = false;
                bPlainTextDES.IsEnabled = false;
            }
        }

        private void DES(object sender, RoutedEventArgs e)
        {
            tbSummaryDES.Text = "Result\n--\n";

            DESpacito des = new DESpacito();

            if (rb1.IsChecked == true)
            {
                string plainText = tbPlainTextDES.Text;
                string key = tbKeyDES.Text;

                string cipherText = des.Encrypt(plainText, key);

                tbSummaryDES.Text += cipherText; 
            }
            else
            {
                string cipherText = tbCipherTextDES.Text;
                string key = tbKeyDES.Text;

                string plainText = des.Decrypt(cipherText, key);

                tbSummaryDES.Text += plainText;
            }
        }
    }
}
