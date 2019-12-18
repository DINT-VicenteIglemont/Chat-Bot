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
using System.Windows.Shapes;
using System.Collections.ObjectModel;


namespace ChatBot
{
    /// <summary>
    /// Lógica de interacción para SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public string Fondo { get; set; }
        public string Humano { get; set; }
        public string Robot { get; set; }

        public SettingsWindow()
        {
            InitializeComponent();

            Fondo = Properties.Settings.Default.ColorFondo;
            Humano = Properties.Settings.Default.ColorPersona;
            Robot = Properties.Settings.Default.ColorRobot;

            FondoColorComboBox.ItemsSource = typeof(Colors).GetProperties();
            HumanoColorComboBox.ItemsSource = typeof(Colors).GetProperties();
            RobotColorCOmboBox.ItemsSource = typeof(Colors).GetProperties();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Salirbutton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
