using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Windows.Controls.Image;

namespace _13
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string opendFile = "";
        private string text;
        DispatcherTimer timer = new DispatcherTimer();
        private ListBox listBox = new ListBox();

        bool isPlaying = false;
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval=new TimeSpan(0,0,1);
            timer.Start();
            b1.Content = "Пауза";
            player.Play();
            isPlaying = true;

        }
        List<string> filenames = new List<string>();
        Image image = new Image();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Відео файли (*.mp4, *.avi)|*.mp4;*.avi|Аудіо файли (*.mp3, *.wav)|*.mp3;*.wav|Всі файли (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                if (openFileDialog.ShowDialog() == true)
                {
                    var filename = openFileDialog.FileName;
                    string fileExtension = System.IO.Path.GetExtension(filename);
                    if (fileExtension == ".mp4" || fileExtension == ".avi")
                    {
                        m2.Source = null;

                        player.Source = new Uri(filename);

                        filenames.Add(filename);

                        listBox.ItemsSource = filenames;

                        expander.Header = $"{filenames.Count} файлів";
                        listBox.DisplayMemberPath = ".";
                        expander.Content = listBox;
                    }
                    else if (fileExtension == ".mp3" || fileExtension == ".wav")
                    {                        
                        string s= @"C:\Users\dvlad\source\repos\13\1.jpeg";
                        player.Source = new Uri(filename);
                        m2.Source = new Uri(s);
                        filenames.Add(filename);

                        listBox.ItemsSource = filenames;

                        expander.Header = $"{filenames.Count} файлів";
                        listBox.DisplayMemberPath = ".";
                        expander.Content = listBox;
                        
                    }
                    else
                    {
                        
                        MessageBox.Show("Тип файлу не підходить");
                    }
                }
            }
            catch (Exception ex)
            {               
                MessageBox.Show($"Файл пошкоджений {ex.ToString()}");
            }


        }
        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (isPlaying)
            {
                b1.Content = "Відтворити";
                player.Stop();
                timer.Stop();
                isPlaying = false;
            }
            else
            {
                b1.Content = "Пауза";
                player.Play();
                isPlaying=true;
                timer.Stop();
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            player.SpeedRatio = 0.5;
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            player.SpeedRatio= 1;
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            player.SpeedRatio= 1.25;
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            player.SpeedRatio = 1.50;
        }

        private void RadioButton_Checked_4(object sender, RoutedEventArgs e)
        {
            player.SpeedRatio = 1.75;
        }

        private void RadioButton_Checked_5(object sender, RoutedEventArgs e)
        {
            player.SpeedRatio = 2;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
                player.Position = player.Position.Add(TimeSpan.FromSeconds(10));
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            
                player.Position = player.Position.Add(TimeSpan.FromSeconds(-10));
            
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                string filePath = ((string)((ListBoxItem)listBox.SelectedItem).Tag);
                player.Source = new Uri(filePath);
                player.Play();
            }
        }

        private void expander_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listBox.SelectedItem != null)
            {

                    string filePath = (string)listBox.SelectedItem;
                if (player.HasVideo)
                {
                    m2.Source = null;
                    player.Source = new Uri(filePath);
                    player.Play();
                }
                if (player.HasAudio)
                {
                    string s = @"C:\Users\dvlad\source\repos\13\1.jpeg";
                    player.Source = new Uri(filePath);
                    m2.Source = new Uri(s);
                    player.Play();
                }

                    
                //player.Source = new Uri(filePath);
            }
        }
    }
}
