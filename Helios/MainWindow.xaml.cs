using Microsoft.Win32;
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
using System.Data.SqlClient;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;

namespace Helios
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //string wilaya = "alger";    //wilaya par defaut: Alger
        string wilaya = File.ReadAllText(@"wilaya.txt");    //wilaya par defaut: Alger

        InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root();

        public MainWindow()
        {
            wilaya = wilaya.Trim(new Char[] { ' ', '\r', '\n', '\t' });

            InitializeComponent();

            /******************** date dynamique ******************************/
            System.Windows.Threading.DispatcherTimer monTimer = new System.Windows.Threading.DispatcherTimer();
            monTimer.Tick += (sender, eventArgs) =>
            {
                //on recupere la date actuelle
                DateTime date1 = DateTime.Now;
                //on chois notre format d'affichage
                string dt, dtt;
                dt = String.Format("{0:dddd ,dd MMMM yyyy}", date1);
                dtt = String.Format("{0: HH:mm: ss}", date1);
                // on ecrit sur le label, et 'est actualisé chaque seconde :ccool:
                //date_meteo.Content = dt.ToString();
                date_meteo.Content = dtt.ToString();
            };
            monTimer.Interval = TimeSpan.FromTicks(100);
            monTimer.Start();
            /*******************************fin date dynamique************************/

        }

        private void main_load(object sender, RoutedEventArgs e)
        {
            if (!CheckDatabaseExist())
            {
                //GenerateDatabase();
            }
        }
        //verifier si la BDD existe
        private bool CheckDatabaseExist()
        {
            SqlConnection connection = new SqlConnection(@"Data Source = .\SQLEXPRESS;Initial Catalog=weather;Integrated Security=True");
            try
            {
                connection.Open();
                return true;
            }
            catch
            {

                SqlCommand command = new SqlCommand();
                command.Connection = new SqlConnection(@"Data Source = .\SQLEXPRESS;Initial Catalog=master;Integrated Security=True");
                command.CommandType = System.Data.CommandType.Text;
                command.Connection.Open();
                command.CommandText = "CREATE DATABASE weather ON (FILENAME = '"+ Directory.GetCurrentDirectory()+ "\\weather.mdf'), (FILENAME = '"+ Directory.GetCurrentDirectory()+ "\\weather_log.ldf') FOR ATTACH;";
                command.ExecuteNonQuery();
                return true;
                //return false;
            }
        }

        private void DoWorkButton_Click(object sender, RoutedEventArgs e)
        {
            btnProgress.Visibility = Visibility.Hidden;
            ProgressTextBlock.Visibility = Visibility.Visible;
            List<string> cmds = new List<string>();
            if (File.Exists(Directory.GetCurrentDirectory() + "\\script.sql"))
            {
                TextReader tr = new StreamReader(Directory.GetCurrentDirectory() + "\\script.sql");

                string line = "";
                string cmd = "";

                while ((line = tr.ReadLine()) != null)
                {
                    
                    progressBar.Dispatcher.Invoke(new ProgressBarDelegate(UpdateProgress), DispatcherPriority.Background);

                    ProgressTextBlock.Text = cmd;
                    if (line.Trim().ToUpper() == "GO")
                    {
                        cmds.Add(cmd);
                        cmd = "";
                    }
                    else
                    {
                        cmd += line + "\r\n";
                    }

                }

                if (cmd.Length > 0)
                {

                    cmds.Add(cmd);
                    cmd = "";
                }


                tr.Close();
            }
            if (cmds.Count > 0)
            {
                SqlCommand command = new SqlCommand();
                command.Connection = new SqlConnection(@"Data Source = .\SQLEXPRESS;Initial Catalog=master;Integrated Security=True");
                command.CommandType = System.Data.CommandType.Text;
                command.Connection.Open();
                for (int i = 0; i < cmds.Count; i++)
                {
                    command.CommandText = cmds[i];
                    command.ExecuteNonQuery();
                }
            }
            progressBar.Visibility = Visibility.Hidden;
            ProgressTextBlock.Visibility = Visibility.Hidden;
            demarre.Visibility = Visibility.Visible;
        }
        private delegate void ProgressBarDelegate();

        private void UpdateProgress()
        {
            progressBar.Value += 1;

        }

       
        //Regénerer la BDD si elle n'existe pas
        private void GenerateDatabase()
        {
            demarre.Visibility = Visibility.Hidden;
            WpfMessageBox.Show(" Information ", "La base de données n'existe pas. Pour la générer  cliquez sur 'Création BDD'", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Information);
            btnProgress.Visibility = Visibility.Visible;
            progressBar.Visibility = Visibility.Visible;
            ProgressTextBlock.Visibility = Visibility.Visible;

        }


        private void ButtonPopUpLogout_Clickk(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // L'événement du bouton démarrer ---> Accueil
        private void demarrer(object sender, RoutedEventArgs e)
        {
            StreamReader sr = new StreamReader(@"son.txt");
            string str = sr.ReadLine();
            sr.Close();
            if (str == "Activé")
            {
                MediaPlayer player = new MediaPlayer();
                player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
                player.Play();
            }
            accueil nb = new accueil(wilaya, output_out);
            nb.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            nb.Show();
            this.Close();
        }
        
        private void conta(object sender, RoutedEventArgs e)
        {
            Contact cont = new Contact(wilaya, output_out);
            cont.Show();
            this.Close();
        }
        private void power_click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void minimize_click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        // Création de la capture d'écran automatique : 
        private void CreateScreenShot(UIElement visual, string file)

        {

            double width = Convert.ToDouble(visual.GetValue(FrameworkElement.WidthProperty));

            double height = Convert.ToDouble(visual.GetValue(FrameworkElement.HeightProperty));



            if (double.IsNaN(width) || double.IsNaN(height))

            {

                throw new FormatException("Erreur !!");

            }



            RenderTargetBitmap render = new RenderTargetBitmap(

               Convert.ToInt32(width),

               Convert.ToInt32(visual.GetValue(FrameworkElement.HeightProperty)),

               96,

               96,

               PixelFormats.Pbgra32);



            // Indicate which control to render in the image

            render.Render(visual);


            try
            {
                using (System.IO.FileStream stream = new FileStream(file, FileMode.Create))

                {

                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(render));
                    encoder.Save(stream);

                }

            }
            catch (Exception)
            {
                WpfMessageBox.Show("Erreur", "Echec de la sauvegarde !", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Error);
            }
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            StreamReader sr = new StreamReader(@"son.txt");
            string str = sr.ReadLine();
            sr.Close();
            if (str == "Activé")
            {
                MediaPlayer player = new MediaPlayer();
                player.Open(new Uri(@"..\..\screen.mp3", UriKind.RelativeOrAbsolute));
                player.Play();
            }
            //déclaration et instanciation de la fenêtre parcourir
            SaveFileDialog parcourir = new SaveFileDialog();
            parcourir.DefaultExt = "png";

            //je spécifie que seul les images .png sont selectionnables
            parcourir.Filter = " Fichier PNG (*.PNG)|*.png";

            //ouverture de la fenêtre parcourir
            parcourir.ShowDialog();

            CreateScreenShot(this, parcourir.FileName);

        }
        private void mise_ajr(object sender, RoutedEventArgs e)
        {
            StreamReader sr = new StreamReader(@"son.txt");
            string str = sr.ReadLine();
            sr.Close();
            if (str == "Activé")
            {
                MediaPlayer player = new MediaPlayer();
                player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
                player.Play();
            }
            mise_a_jour mise = new mise_a_jour(wilaya, output_out);
            mise.Show();
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                StreamReader sr = new StreamReader(@"son.txt");
                string str = sr.ReadLine();
                sr.Close();
                if (str == "Activé")
                {
                    MediaPlayer player = new MediaPlayer();
                    player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
                    player.Play();
                }
                output_out.nbreJours = 1;
                accueil nb = new accueil(wilaya, output_out);
                nb.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

                nb.Show();
                this.Close();
            }
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Directory.GetCurrentDirectory() + "\\html5up-overflow\\index.html");
        }

    }
}
