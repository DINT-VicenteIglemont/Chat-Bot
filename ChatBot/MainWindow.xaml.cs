using Microsoft.Azure.CognitiveServices.Knowledge.QnAMaker;
using Microsoft.Azure.CognitiveServices.Knowledge.QnAMaker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ChatBot
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ObservableCollection<Mensaje> mensajes = new ObservableCollection<Mensaje>();
        Boolean disponible = true;

        public MainWindow()
        {
            InitializeComponent();
            ChatWindow.DataContext = mensajes;
        }

        private void CommandBinding_Executed_Nuevo(object sender, ExecutedRoutedEventArgs e)
        {
            mensajes.Clear();
        }

        private void CommandBinding_CanExecute_Nuevo(object sender, CanExecuteRoutedEventArgs e)
        {
            if(mensajes.Count > 0)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private async void CommandBinding_Executed_SendMessage(object sender, ExecutedRoutedEventArgs e)
        {
            Mensaje m = new Mensaje("Persona", MensajeTextBox.Text);
            mensajes.Add(m);
            MensajeTextBox.Text = "";

            string EndPoint = Properties.Settings.Default.EndPoint;
            string Key = Properties.Settings.Default.Key;
            string Id = Properties.Settings.Default.Id;

            QnAMakerRuntimeClient cliente = new QnAMakerRuntimeClient(new EndpointKeyServiceClientCredentials(Key)) { RuntimeEndpoint = EndPoint };

            Mensaje mensajeRobot = new Mensaje("Robot", "Pensando...");
            mensajes.Add(mensajeRobot);
            disponible = false;
            string respuesta = "";

            try
            {
                string pregunta = m.Contenido;
                QnASearchResultList response = await cliente.Runtime.GenerateAnswerAsync(Id, new QueryDTO { Question = pregunta });
                respuesta = response.Answers[0].Answer;
            }
            catch (Exception)
            {
                respuesta = "Estoy ocupado ahora mismo";
            }

            mensajes.Remove(mensajeRobot);
            mensajeRobot.Contenido = respuesta;
            mensajes.Add(mensajeRobot);
            disponible = true;
            ScrollViewer.ScrollToEnd();
        }

        private void CommandBinding_CanExecute_SendMessage(object sender, CanExecuteRoutedEventArgs e)
        {
            if (disponible)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private async void CommandBinding_Executed_CheckConnection(object sender, ExecutedRoutedEventArgs e)
        {
            string EndPoint = Properties.Settings.Default.EndPoint;
            string Key = Properties.Settings.Default.Key;
            string Id = Properties.Settings.Default.Id;

            QnAMakerRuntimeClient client = new QnAMakerRuntimeClient(new EndpointKeyServiceClientCredentials(Key)) { RuntimeEndpoint = EndPoint };

            try
            {
                string pregunta = "hola";
                QnASearchResultList response = await client.Runtime.GenerateAnswerAsync(Id, new QueryDTO { Question = pregunta });
                string respuesta = response.Answers[0].Answer;

                MessageBox.Show("Conexion realizada con exito", "Comprobar conexion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("No se ha podido completar la conexion...", "Comprobar conexion", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CommandBinding_CanExecute_CheckConnection(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed_Save(object sender, ExecutedRoutedEventArgs e)
        {

            string logConversacion = "";

            foreach (Mensaje m in mensajes)
            {
                logConversacion += m.ToString();
            }

            Microsoft.Win32.SaveFileDialog ventanaGuardar = new Microsoft.Win32.SaveFileDialog();

            ventanaGuardar.Filter = "Archivos de texto|*.txt";
            ventanaGuardar.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if(ventanaGuardar.ShowDialog() == true)
            {
                File.WriteAllText(ventanaGuardar.FileName, logConversacion);
            }

        }

        private void CommandBinding_CanExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {
            if(mensajes.Count > 0)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void CommandBinding_Executed_OpenSettings(object sender, ExecutedRoutedEventArgs e)
        {
            SettingsWindow settings = new SettingsWindow();
            
            if(settings.ShowDialog() == true)
            {
                Properties.Settings.Default.ColorFondo = settings.Fondo;
                Properties.Settings.Default.ColorPersona = settings.Humano;
                Properties.Settings.Default.ColorRobot = settings.Robot;
                Properties.Settings.Default.Save();
            }
        }

        private void CommandBinding_CanExecute_OpenSettings(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        
        private void CommandBinding_Executed_Close(object sender, ExecutedRoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void CommandBinding_CanExecute_Close(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        
    }
}
