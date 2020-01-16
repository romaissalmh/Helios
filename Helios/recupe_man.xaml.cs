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
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;
using System.Drawing;
using System.Windows.Interop;
using Color = System.Windows.Media.Color;
using Point = System.Windows.Point;
using ColorConverter = System.Windows.Media.ColorConverter;
using System.IO;
using static Helios.InfoJour.weatherinfo;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Microsoft.Win32;

namespace Helios
{
    /// summary
    /// Logique d'interaction pour recupman.xaml
    /// summary
    public partial class recupe_man : Window
    {


        double tempmi;
        double tempma;
        double visib;
        double tempve;
        int humidit;

        // string wilaya = "alger";    //wilaya par defaut: Alger
        String wilaya = File.ReadAllText(@"wilaya.txt");    //wilaya par defaut: Alger
                                                            // String wilaya = (@"wilaya.txt");
        bool enregistre = false;
        public bool otherWindow = false;
        InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root();

        public InfoJour.weatherinfo.Root output = new InfoJour.weatherinfo.Root();


        public recupe_man(string city)
        {
            try
            {
                wilaya = wilaya.Trim(new Char[] { ' ', '\r', '\n', '\t' });
                InitializeComponent();

                wilaya = city;
                output.name = wilaya;


            }
            catch (Exception)
            {
                WpfMessageBox.Show("Erreur", "Echec de la récupération manuelle !", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Error); throw;
            }

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void CreateScreenShot(UIElement visual, string file)

        {

            double width = Convert.ToDouble(visual.GetValue(FrameworkElement.WidthProperty));

            double height = Convert.ToDouble(visual.GetValue(FrameworkElement.HeightProperty));



            if (double.IsNaN(width) || double.IsNaN(height))

            {

                throw new FormatException("Erreur !! ");

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



                    WpfMessageBox.Show("Réussi", "Capture prise avec succés", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Information);
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

        private void meteo(object sender, RoutedEventArgs e)
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


        }
        private void prev(object sender, RoutedEventArgs e)
        {
            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
            player.Play();
            /* prevision prv = new prevision(wilaya, output_out);
             prv.Show();
             this.Close();*/
        }
        private void aprop(object sender, RoutedEventArgs e)
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
            otherWindow = true;
            apropos ap = new apropos(wilaya, output_out);
            ap.Show();
            this.Close();
        }
        private void evolut(object sender, RoutedEventArgs e)
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
            otherWindow = true;
            evolution ev = new evolution(wilaya, output_out);
            ev.Show();
            this.Close();
        }
        private void generall(object sender, RoutedEventArgs e)
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
            otherWindow = true;
            parametre cr = new parametre(wilaya, output_out);
            cr.Show();
            this.Close();
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
            otherWindow = true;

            mise_a_jour mise = new mise_a_jour(wilaya,output_out);
            mise.Show();
            this.Close();
            //connexion mise = new connexion(wilaya, output_out);
            //mise.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            //mise.ShowDialog();
            //this.Close();
        }
        private void carte(object sender, RoutedEventArgs e)
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
            otherWindow = true;
            accueil ac = new accueil(wilaya, output_out);
            ac.Show();
            this.Close();
        }
        private void conta(object sender, RoutedEventArgs e)
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
            otherWindow = true;
            Contact cont = new Contact(wilaya, output_out);
            cont.Show();
            this.Close();
        }


        //Contrôles de saisie au niveau des TextBox + Saut du curseur vers le prochain TextBox si la touche ENTRER est appuyée

        private void Tempmin_KeyDown(object sender, KeyEventArgs e)
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
            if (e.Key == Key.Enter)//Si ENTRER
            {
                double number1;
                double number2;
                if (Double.TryParse(this.tempmin.Text, out number1) == true) //Vérifier si la valeur entrée est un nombre décimal
                {
                    Double.TryParse(this.tempmax.Text, out number2); //Conversion en Double du contenu du textBox
                    if (this.tempmax.Text.Length == 0 || number1 <= number2) //Vérifier si la température minimale est inférieure à la maximale
                    {
                        if (number1 <= 100)// Ne pas entrer de valeur supérieure à 100°C
                        {
                            tempmi = number1; //Affectation de la valeur entée dans la textBox
                            TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next); //Saut au prochain textBox
                            UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;
                            if (keyboardFocus != null)
                            {
                                keyboardFocus.MoveFocus(tRequest);
                                this.tempmin.Background = null;
                                this.tempmin.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                                this.tempmax.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                                this.tempmax.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

                            }
                        }
                        else //Message d'erreur
                        {
                            this.tempmin.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                            WpfMessageBox.Show("Erreur", "La température extérieure ne doit pas dépasser les 100°C", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                            this.tempmin.Clear();
                            this.tempmin.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                            this.tempmin.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

                        }
                    }
                    else
                    {   
                            this.tempmin.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                            WpfMessageBox.Show("Erreur", "La température minimale doit être inférieure à la maximale", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                            this.tempmax.Clear();
                            this.tempmin.Clear();
                            this.tempmin.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                            this.tempmin.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));
                    }
                }
                else//Message d'erreur
                {
                    this.tempmin.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                    WpfMessageBox.Show("Erreur", "Veuillez entrer une valeur numérique.(Pour les valeurs décimales utilisez ',' et non pas '.')", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                    this.tempmin.Clear();
                    this.tempmin.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                    this.tempmin.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));


                }
            }
        }
        private void Tempmax_KeyDown(object sender, KeyEventArgs e)
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
            if (e.Key == Key.Enter )
            {
                double number1;
                double number2;
                if (Double.TryParse(this.tempmax.Text, out number2) == true)
                {
                    Double.TryParse(this.tempmin.Text, out number1);

                    if (this.tempmin.Text.Length == 0 || number1 <= number2)
                    {
                        if (number2 <= 100)
                        {
                            tempma = number2;
                            TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                            UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;
                            if (keyboardFocus != null)
                            {
                                keyboardFocus.MoveFocus(tRequest);
                                this.tempvent.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                                this.tempmax.Background = null;
                                this.tempmax.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

                                this.tempvent.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                                this.tempvent.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

                            }
                        }
                        else
                        {
                            this.tempmax.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                            WpfMessageBox.Show("Erreur", "La température extérieure ne doit pas dépasser les 100°C", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                            this.tempmax.Clear();
                            this.tempmax.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                            this.tempmax.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

                        }
                    }
                    else
                    {
                       
                            this.tempmax.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                            WpfMessageBox.Show("Erreur", "La température minimale doit être inférieure à la maximale", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                            this.tempmin.Clear();
                            this.tempmax.Clear();
                            this.tempmax.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                            this.tempmax.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

                    }
                }
                else
                {
                    this.tempmax.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                    WpfMessageBox.Show("Erreur", "Veuillez entrer une valeur numérique.(Pour les valeurs décimales utilisez ',' et non pas '.')", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                    this.tempmax.Clear();
                    this.tempmax.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                    this.tempmax.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

                }

            }
        }
        private void Tempvent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter )
            {
                double number;
                if (Double.TryParse(this.tempvent.Text, out number) == true)
                {
                    if (number <= 100)
                    {
                        tempve = number;
                        TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                        UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;
                        if (keyboardFocus != null)
                        {
                            keyboardFocus.MoveFocus(tRequest);
                            LinearGradientBrush back = new LinearGradientBrush();
                            this.tempvent.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77000000"));
                            this.GridHumide.Opacity = 0.9;
                            this.tempvent.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

                        }
                    }
                    else
                    {
                        this.tempvent.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                        WpfMessageBox.Show("Erreur", "La température du vent ne doit pas dépasser les 100°C", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                        this.tempvent.Clear();
                        this.tempvent.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                        this.tempvent.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

                    }
                }


                else
                {
                    this.tempvent.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                    WpfMessageBox.Show("Erreur", "Veuillez entrer une valeur numérique.(Pour les valeurs décimales utilisez ',' et non pas '.')", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                    this.tempvent.Clear();
                    this.tempvent.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                    this.tempvent.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

                }
            }
        }
        private void Humidite_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter )
            {
                int number;

                if (Int32.TryParse(this.humidite.Text, out number) == true)
                {
                    if (number <= 100 && number >= 0)
                    {
                        if (this.nonhumide.IsChecked == true)
                        {
                            if (number > 29)
                            {
                                this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                                WpfMessageBox.Show("Incohérence", "Vous avez séléctionné 'Temps sec' les valeurs sont donc comprises entre 0 % et 29 %", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                                this.humidite.Clear();
                                this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                                this.humidite.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

                            }
                            else
                            {
                                humidit = number;

                                TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                                UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;


                                if (keyboardFocus != null)
                                {

                                    keyboardFocus.MoveFocus(tRequest);

                                    this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77000000"));
                                    this.visibilite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                                    this.Grid_Visib.Opacity = 0.9;

                                }

                            }
                        }
                        else
                        {
                            if (this.peuhumide.IsChecked == true)
                            {
                                if (number > 59 || number < 30)
                                {
                                    this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                                    WpfMessageBox.Show("Incohérence", "Vous avez séléctionné 'Temps peu humide' les valeurs sont donc comprises entre 30 % et 59 %", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                                    this.humidite.Clear();
                                    this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                                    this.humidite.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

                                }
                                else
                                {

                                    humidit = number;
                                    TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);


                                    UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;


                                    if (keyboardFocus != null)
                                    {

                                        keyboardFocus.MoveFocus(tRequest);

                                        this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77000000"));

                                        this.visibilite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                                        this.Grid_Visib.Opacity = 0.9;

                                    }

                                }
                            }
                            else
                            {
                                if (this.moyhumide.IsChecked == true)
                                {
                                    if (number > 89 || number < 60)
                                    {
                                        this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                                        WpfMessageBox.Show("Incohérence", "Vous avez séléctionné 'Temps moyennement humide' les valeurs sont donc comprises entre 60 % et 89 %", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                                        this.humidite.Clear();
                                        this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                                        this.humidite.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

                                    }
                                    else
                                    {
                                        humidit = number;
                                        TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);


                                        UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;


                                        if (keyboardFocus != null)
                                        {

                                            keyboardFocus.MoveFocus(tRequest);

                                            this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77000000"));

                                            this.visibilite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                                            this.Grid_Visib.Opacity = 0.9;

                                        }
                                    }
                                }
                                else
                                {
                                    if (number < 90)
                                    {
                                        this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                                        WpfMessageBox.Show("Incohérence", "Vous avez séléctionné 'Temps très humide' les valeurs sont donc comprises entre 90 % et 100 %", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                                        this.humidite.Clear();
                                        this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                                        this.humidite.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

                                    }
                                    else
                                    {
                                        humidit = number;
                                        TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);


                                        UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;


                                        if (keyboardFocus != null)
                                        {

                                            keyboardFocus.MoveFocus(tRequest);

                                            this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77000000"));

                                            this.visibilite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                                            this.Grid_Visib.Opacity = 0.9;
                                            this.humidite.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));


                                        }

                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                        WpfMessageBox.Show("Erreur", "Le taux d'humidité  est compris entre 0% et 100%", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                        this.humidite.Clear();
                        this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                        this.humidite.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

                    }
                }
                else
                {
                    this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                    WpfMessageBox.Show("Erreur", "Veuillez entrer une valeur numérique.(Pour les valeurs décimales utilisez ',' et non pas '.')", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                    this.humidite.Clear();
                    this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                    this.humidite.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

                }
            }
        }
        private void Visibilite_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter )
            {
                double number;

                if (Double.TryParse(this.visibilite.Text, out number) == true)
                {
                    if (number <= 10 && number >= 0)
                    {

                        visib = number;
                        TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                        UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;
                        if (keyboardFocus != null)
                        {
                            keyboardFocus.MoveFocus(tRequest);
                            this.visibilite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77000000"));
                            this.Grid_Visib.Opacity = 0.7;
                            this.GridPrecip.Opacity = 0.9;
                            this.visibilite.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

                        }

                    }
                    else
                    {
                        this.visibilite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                        WpfMessageBox.Show("Erreur", "La visibilité est comprise entre 0 et 10", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                        this.visibilite.Clear();
                        this.visibilite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                        this.visibilite.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

                    }

                }
                else
                {
                    this.visibilite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                    WpfMessageBox.Show("Erreur", "Veuillez entrer une valeur numérique", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                    this.visibilite.Clear();
                    this.visibilite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                    this.visibilite.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

                }
            }
        }

        //CheckBox humidité : un seul champs ne peut être choisi à la fois 

        private void Treshumide_Checked(object sender, RoutedEventArgs e)
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
            this.nonhumide.IsEnabled = false;
            this.moyhumide.IsEnabled = false;
            this.peuhumide.IsEnabled = false;
            this.humidite.IsEnabled = true;

            this.Thumide.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            this.Thumide.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

        }
        private void Moyhumide_Checked(object sender, RoutedEventArgs e)
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
            this.nonhumide.IsEnabled = false;
            this.peuhumide.IsEnabled = false;
            this.treshumide.IsEnabled = false;
            this.humidite.IsEnabled = true;  // lorsqu'un champs est choisi le textBox humitie est activé      
            this.Mhumide.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            this.Mhumide.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));
        }
        private void Peuhumide_Checked(object sender, RoutedEventArgs e)
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

            this.nonhumide.IsEnabled = false;
            this.moyhumide.IsEnabled = false;
            this.treshumide.IsEnabled = false;
            this.humidite.IsEnabled = true;
            this.Phumide.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            this.Phumide.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));
        }
        private void Nonhumide_Checked(object sender, RoutedEventArgs e)
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
            this.peuhumide.IsEnabled = false;
            this.moyhumide.IsEnabled = false;
            this.treshumide.IsEnabled = false;
            this.humidite.IsEnabled = true;

            this.Nhumide.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            this.Nhumide.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));


        }

        //CheckBox humidité : Pour modifier son choix l'utilisateur uncheck le checkbox qu'il avait choisit

        private void Treshumide_Unchecked(object sender, RoutedEventArgs e)
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
            this.peuhumide.IsEnabled = true;
            this.moyhumide.IsEnabled = true;
            this.nonhumide.IsEnabled = true;
            this.humidite.IsEnabled = false;

            this.Thumide.Background = null;
            this.Thumide.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

        }
        private void Moyhumide_Unchecked(object sender, RoutedEventArgs e)
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

            this.peuhumide.IsEnabled = true;
            this.nonhumide.IsEnabled = true;
            this.treshumide.IsEnabled = true;
            this.humidite.IsEnabled = false; //Le textBlock humidité est désactivé
            this.Mhumide.Background = null;
            this.Mhumide.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

        }
        private void Peuhumide_Unchecked(object sender, RoutedEventArgs e)
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

            this.nonhumide.IsEnabled = true;
            this.moyhumide.IsEnabled = true;
            this.treshumide.IsEnabled = true;
            this.humidite.IsEnabled = false;
            this.Phumide.Background = null;
            this.Phumide.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));



        }
        private void Nonhumide_Unchecked(object sender, RoutedEventArgs e)
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
            this.peuhumide.IsEnabled = true;
            this.moyhumide.IsEnabled = true;
            this.treshumide.IsEnabled = true;
            this.humidite.IsEnabled = false;
            this.Nhumide.Background = null;
            this.Nhumide.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));



        }

        //CheckBox Pluie: un seul champs ne peut être choisi à la fois 
        private void Pluieintense_Checked(object sender, RoutedEventArgs e)
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
            this.pasdepluie.IsEnabled = false;
            this.pluieimportante.IsEnabled = false;
            this.pluiemoderee.IsEnabled = false;


            this.GridPrecip.Opacity = 0.7;
            this.GridVent.Opacity = 0.9;
            this.Pin.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            this.Pin.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));
        }
        private void Pluieimportante_Checked(object sender, RoutedEventArgs e)
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
            // LinearGradientBrush back = new LinearGradientBrush();
            this.pasdepluie.IsEnabled = false;
            this.pluieintense.IsEnabled = false;
            this.pluiemoderee.IsEnabled = false;


            this.GridPrecip.Opacity = 0.7;
            this.GridVent.Opacity = 0.9;
            this.Pim.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            this.Pim.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));
        }
        private void Pluiemoderee_Checked(object sender, RoutedEventArgs e)
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

            // LinearGradientBrush back = new LinearGradientBrush();
            this.pasdepluie.IsEnabled = false;
            this.pluieimportante.IsEnabled = false;
            this.pluieintense.IsEnabled = false;


            this.GridPrecip.Opacity = 0.7;
            this.GridVent.Opacity = 0.9;
            this.Pil.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            this.Pil.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));
        }
        private void Pasdepluie_Checked(object sender, RoutedEventArgs e)
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
            // LinearGradientBrush back = new LinearGradientBrush();
            this.pluieintense.IsEnabled = false;
            this.pluieimportante.IsEnabled = false;
            this.pluiemoderee.IsEnabled = false;
            this.GridPrecip.Opacity = 0.7;
            this.GridVent.Opacity = 0.9;
            this.Pdp.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            this.Pdp.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));
        }

        //CheckBox Pluie:  Pour modifier son choix l'utilisateur uncheck le checkbox qu'il avait choisit

        private void Pluieintense_Unchecked(object sender, RoutedEventArgs e)
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
            // LinearGradientBrush back = new LinearGradientBrush();
            this.pluieimportante.IsEnabled = true;
            this.pluiemoderee.IsEnabled = true;
            this.pasdepluie.IsEnabled = true;

            this.GridPrecip.Opacity = 0.9;
            this.GridVent.Opacity = 0.7;
            this.Pin.Background = null;
            this.Pin.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));


        }
        private void Pluieimportante_Unchecked(object sender, RoutedEventArgs e)
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

            // LinearGradientBrush back = new LinearGradientBrush();
            this.pluieintense.IsEnabled = true;
            this.pluiemoderee.IsEnabled = true;
            this.pasdepluie.IsEnabled = true;

            this.GridPrecip.Opacity = 0.9;
            this.GridVent.Opacity = 0.7;
            this.Pim.Background = null;
            this.Pim.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));


        }
        private void Pluiemoderee_Unchecked(object sender, RoutedEventArgs e)
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
            // LinearGradientBrush back = new LinearGradientBrush();
            this.pluieimportante.IsEnabled = true;
            this.pluieintense.IsEnabled = true;
            this.pasdepluie.IsEnabled = true;
            this.GridPrecip.Opacity = 0.9;
            this.GridVent.Opacity = 0.7;
            this.Pil.Background = null;
            this.Pil.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));


        }
        private void Pasdepluie_Unchecked(object sender, RoutedEventArgs e)
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
            //LinearGradientBrush back = new LinearGradientBrush();
            this.pluieimportante.IsEnabled = true;
            this.pluiemoderee.IsEnabled = true;
            this.pluieintense.IsEnabled = true;

            this.GridPrecip.Opacity = 0.9;
            this.GridVent.Opacity = 0.7;
            this.Pdp.Background = null;
            this.Pdp.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));


        }

        //CheckBox vitesse du vent: un seul champs ne peut être choisi à la fois 

        private void Case1_Checked(object sender, RoutedEventArgs e)
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


            // LinearGradientBrush back = new LinearGradientBrush();
            this.case2.IsEnabled = false;
            this.case3.IsEnabled = false;
            this.case4.IsEnabled = false;
            this.case1T.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            this.case1T.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));
        }
        private void Case2_Checked(object sender, RoutedEventArgs e)
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
            // LinearGradientBrush back = new LinearGradientBrush();
            this.case1.IsEnabled = false;
            this.case3.IsEnabled = false;
            this.case4.IsEnabled = false;

            this.case2T.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            this.case2T.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));
        }
        private void Case3_Checked(object sender, RoutedEventArgs e)
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
            //LinearGradientBrush back = new LinearGradientBrush();
            this.case2.IsEnabled = false;
            this.case1.IsEnabled = false;
            this.case4.IsEnabled = false;
            this.case3T.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            this.case3T.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));
        }
        private void Case4_Checked(object sender, RoutedEventArgs e)
        {
            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
            player.Play();
            //LinearGradientBrush back = new LinearGradientBrush();
            this.case2.IsEnabled = false;
            this.case3.IsEnabled = false;
            this.case1.IsEnabled = false;

            this.case4T.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            this.case4T.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));
        }

        //CheckBox vitesse du vent:  Pour modifier son choix l'utilisateur uncheck le checkbox qu'il avait choisit


        private void Case1_Unchecked(object sender, RoutedEventArgs e)
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
            //  LinearGradientBrush back = new LinearGradientBrush();
            this.case2.IsEnabled = true;
            this.case3.IsEnabled = true;
            this.case4.IsEnabled = true;

            this.case1T.Background = null;
            this.case1T.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

        }
        private void Case2_Unchecked(object sender, RoutedEventArgs e)
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
            //LinearGradientBrush back = new LinearGradientBrush();
            this.case1.IsEnabled = true;
            this.case3.IsEnabled = true;
            this.case4.IsEnabled = true;

            this.case2T.Background = null;
            this.case2T.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

        }
        private void Case3_Unchecked(object sender, RoutedEventArgs e)
        {
            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
            player.Play();
            // LinearGradientBrush back = new LinearGradientBrush();
            this.case2.IsEnabled = true;
            this.case1.IsEnabled = true;
            this.case4.IsEnabled = true;

            this.case3T.Background = null;
            this.case3T.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

        }
        private void Case4_Unchecked(object sender, RoutedEventArgs e)
        {
            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
            player.Play();
            //LinearGradientBrush back = new LinearGradientBrush();
            this.case2.IsEnabled = true;
            this.case3.IsEnabled = true;
            this.case1.IsEnabled = true;

            this.case4T.Background = null;
            this.case4T.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

        }

        //Changement d'opacité des Grid au passage de la souris

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            this.GridHumide.Opacity = 0.9;
        }
        private void GridTemp_MouseMove(object sender, MouseEventArgs e)
        {
            this.GridTemp.Opacity = 0.9;
        }
        private void GridPrecip_MouseMove(object sender, MouseEventArgs e)
        {
            this.GridPrecip.Opacity = 0.9;
        }
        private void GridVent_MouseMove_1(object sender, MouseEventArgs e)
        {
            this.GridVent.Opacity = 0.9;
        }
        private void Grid_Visib_MouseMove(object sender, MouseEventArgs e)
        {
            this.Grid_Visib.Opacity = 0.9;
        }

        //Changement d'opacité des Grid après passage de la souris

        private void GridHumide_MouseLeave(object sender, MouseEventArgs e)
        {
            this.GridHumide.Opacity = 0.7;
        }  
        private void GridTemp_MouseLeave(object sender, MouseEventArgs e)
        {
            this.GridTemp.Opacity = 0.7;
        }     
        private void GridPrecip_MouseLeave(object sender, MouseEventArgs e)
        {
            this.GridPrecip.Opacity = 0.7;
        }
        private void GridVent_MouseLeave_1(object sender, MouseEventArgs e)
        {
            this.GridVent.Opacity = 0.7;
        }
        private void Grid_Visib_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Grid_Visib.Opacity = 0.7;
        }


        //Changement opacité des bouton au passage de la souris

        private void Reinitialiser_MouseMove(object sender, MouseEventArgs e)
        {
            this.Reinitialiser.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            this.Reinitialiser.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));
        }
        private void Enregistrer_MouseMove(object sender, MouseEventArgs e)
        {
            this.Enregistrer.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            this.Enregistrer.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));
        }
        private void Reinitialiser_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Reinitialiser.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77D56C78"));
            this.Reinitialiser.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

        }
        private void Enregistrer_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Enregistrer.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77D56C78"));
            this.Enregistrer.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));


        }


        //Réunitialisation des données entrées 

        private void Reinitialiser_Click(object sender, RoutedEventArgs e)
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
            this.Reinitialiser.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            this.Reinitialiser.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));
            Case1_Unchecked(case1, null); // Chackbox réinitialisées
            Case2_Unchecked(case2, null);
            Case3_Unchecked(case3, null);
            Case4_Unchecked(case4, null);
            Peuhumide_Unchecked(peuhumide, null);
            Moyhumide_Unchecked(moyhumide, null);
            Treshumide_Unchecked(treshumide, null);
            Nonhumide_Unchecked(nonhumide, null);
            Pluieimportante_Unchecked(pluieimportante, null);
            Pluieintense_Unchecked(pluieintense, null);
            Pluiemoderee_Unchecked(pluiemoderee, null);
            Pasdepluie_Unchecked(pasdepluie, null);

            this.tempmax.Clear(); //TextBox réinitialisées
            this.tempmin.Clear();
            this.tempvent.Clear();
            this.visibilite.Clear();
            this.humidite.Clear();
            this.humidite.IsEnabled = false;
            this.case1.IsChecked = false;
            this.case2.IsChecked = false;
            this.case3.IsChecked = false;
            this.case4.IsChecked = false;
            this.peuhumide.IsChecked = false;
            this.moyhumide.IsChecked = false;
            this.treshumide.IsChecked = false;
            this.nonhumide.IsChecked = false;
            this.pluieimportante.IsChecked = false;
            this.pluieintense.IsChecked = false;
            this.pluiemoderee.IsChecked = false;
            this.pasdepluie.IsChecked = false;
           
            GridHumide_MouseLeave(GridHumide, null);
            GridPrecip_MouseLeave(GridPrecip, null);
            GridTemp_MouseLeave(GridTemp, null);
            GridVent_MouseLeave_1(GridVent, null);
        }

        //Enregistrement des données + tests

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
            player.Play();
            //Si un champs n'a pas été rempli : message d'erreur
            if (this.tempmax.Text.Length == 0 || this.humidite.Text.Length == 0 || this.tempmin.Text.Length == 0 || this.visibilite.Text.Length == 0 || this.tempvent.Text.Length == 0 || (this.case1.IsChecked == false && this.case2.IsChecked == false && this.case3.IsChecked == false && this.case4.IsChecked == false) || (this.peuhumide.IsChecked == false && this.treshumide.IsChecked == false && this.moyhumide.IsChecked == false && this.nonhumide.IsChecked == false) || (this.pluieimportante.IsChecked == false && this.pasdepluie.IsChecked == false && this.pluieintense.IsChecked == false && this.pluiemoderee.IsChecked == false))
            {
                WpfMessageBox.Show("Les données ne sont pas complètes", "Veuillez remplir tous les champs.", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                this.Enregistrer.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77D56C78"));
                this.Enregistrer.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            }
            else
            {
                //Si un textBox ne contient pas de valeur décimale : message d'erreur
                if (Double.TryParse(tempmin.Text, out tempmi) == false || Double.TryParse(tempmax.Text, out tempma) == false || Double.TryParse(tempvent.Text, out tempve) == false || Double.TryParse(visibilite.Text, out visib) == false || Int32.TryParse(humidite.Text, out humidit) == false)
                {
                    WpfMessageBox.Show("Erreur", "Veuillez entrer des valeurs numériques.(Pour les valeurs décimales utilisez ',' et non pas '.')", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                    this.Enregistrer.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77D56C78"));
                    this.Enregistrer.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                }
                else
                {
                    //Si la température minimale est supérieure à la maximale : message d'erreur
                    if (tempmi > tempma)
                    {
                        WpfMessageBox.Show("Erreur", "La température minimale est inférieure à la maximale", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Error);
                        GridTemp_MouseMove(GridTemp, null);
                        this.Enregistrer.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77D56C78"));
                        this.Enregistrer.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                    }
                    else
                    {
                        //Si les valeurs entrées dépasse la valeur 100
                        if (tempmi > 100 || tempma > 100 || tempve > 100)
                        {
                            WpfMessageBox.Show("Erreur", "Les valeurs de la température sont inférieures à 100°C", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                            GridTemp_MouseMove(GridTemp, null);
                            this.Enregistrer.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77D56C78"));
                            this.Enregistrer.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                        }

                        else
                        {
                            // Si la visibilité entrée n'est pas dans l'intervalle 0-10
                            if (visib > 10 || visib < 0)
                            {
                                WpfMessageBox.Show("Erreur", "La visibilité est comprise entre 0 et 10", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                                Grid_Visib_MouseMove(Grid_Visib, null);
                                this.Enregistrer.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77D56C78"));
                                this.Enregistrer.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                            }
                            else
                            {
                                if (humidit > 100 || humidit < 0)
                                {
                                    WpfMessageBox.Show("Erreur", "L'humidité est comprise entre 0% et 100%", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                                    Grid_MouseMove(GridHumide, null);
                                    this.Enregistrer.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77D56C78"));
                                    this.Enregistrer.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                                }
                                //Tests de cohérence entre la description choisie concernant l'humidité et la valeur entrée
                                else
                                {
                                    if (peuhumide.IsChecked == true && (humidit > 59 || humidit < 30))
                                    {
                                        WpfMessageBox.Show("Incohérence", "Vous avez séléctionné 'peu humide' le taux d'humidité est compris entre 30% et 59%", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                                        this.Enregistrer.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77D56C78"));
                                        this.Enregistrer.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

                                    }
                                    else
                                    {
                                        if (moyhumide.IsChecked == true && (humidit > 89 || humidit < 60))
                                        {
                                            WpfMessageBox.Show("Incohérence", "Vous avez séléctionné 'moyennement humide' le taux d'humidité est compris entre 60% et 89%", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                                            this.Enregistrer.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77D56C78"));
                                            this.Enregistrer.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

                                        }
                                        else
                                        {
                                            if (treshumide.IsChecked == true && (humidit < 90))
                                            {
                                                WpfMessageBox.Show("Incohérence", "Vous avez séléctionné 'très humide' le taux d'humidité est compris entre 90% et 100%", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                                                this.Enregistrer.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77D56C78"));
                                                this.Enregistrer.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

                                            }
                                            else
                                            {
                                                if (nonhumide.IsChecked == true && (humidit > 29))
                                                {
                                                    WpfMessageBox.Show("Incohérence", "Vous avez séléctionné 'temps sec' le taux d'humidité est compris entre 0% et 29%", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                                                    this.Enregistrer.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77D56C78"));
                                                    this.Enregistrer.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

                                                }
                                                else
                                                {
                                                    this.Enregistrer.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFBAA4B"));
                                                    WpfMessageBox.Show("", "Vos données ont été enregistrées!", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                                                    enregistre = true;
                                                    output.main.temp_max = Convert.ToDouble(this.tempmax.Text);
                                                    output.main.temp_min = Convert.ToDouble(this.tempmin.Text);
                                                    output.wind.deg = Convert.ToDouble(this.tempvent.Text);
                                                    output.main.humidity = Convert.ToDouble(this.humidite.Text);
                                                    output.visibility = Convert.ToDouble(this.visibilite.Text);

                                                    double rain = 0;
                                                    if (this.pluieintense.IsChecked == true) rain = 10;
                                                    else if (this.pluieimportante.IsChecked == true) rain = 7;
                                                    else if (this.pluiemoderee.IsChecked == true) rain = 2;
                                                    else rain = 0;
                                                    output.precip = Convert.ToDouble(rain);
                                                    output.used = true;

                                                    double windspeed = 0;
                                                    if (this.case1.IsChecked == true) windspeed = 8;
                                                    if (this.case2.IsChecked == true) windspeed = 23;
                                                    if (this.case3.IsChecked == true) windspeed = 32;
                                                    if (this.case4.IsChecked == true) windspeed = 50;
                                                    output.wind.speed = Convert.ToDouble(windspeed);

                                                    this.Reinitialiser.IsEnabled = false;
                                                    this.tempmax.IsEnabled = false;
                                                    this.tempmin.IsEnabled = false;
                                                    this.tempvent.IsEnabled = false;
                                                    this.humidite.IsEnabled = false;
                                                    this.visibilite.IsEnabled = false;
                                                    this.case1.IsEnabled = false;
                                                    this.case2.IsEnabled = false;
                                                    this.case3.IsEnabled = false;
                                                    this.case4.IsEnabled = false;
                                                    this.pluieimportante.IsEnabled = false;
                                                    this.pluiemoderee.IsEnabled = false;
                                                    this.pluieintense.IsEnabled = false;
                                                    this.pasdepluie.IsEnabled = false;
                                                    this.peuhumide.IsEnabled = false;
                                                    this.moyhumide.IsEnabled = false;
                                                    this.nonhumide.IsEnabled = false;
                                                    this.treshumide.IsEnabled = false;

                                                    this.Close();
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //TextBox MouseEnter et MouseLeave

        private void Tempmin_MouseEnter(object sender, MouseEventArgs e)
        {
            this.tempmin.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            this.tempmin.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

        }
        private void Tempmax_MouseEnter(object sender, MouseEventArgs e)
        {
             this.tempmax.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            this.tempmax.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));


        }
        private void Tempvent_MouseEnter(object sender, MouseEventArgs e)
        {
            this.tempvent.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            this.tempvent.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

        }
        private void Visibilite_MouseEnter(object sender, MouseEventArgs e)
        {
            this.visibilite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            this.visibilite.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

        }
        private void Humidite_MouseEnter(object sender, MouseEventArgs e)
        {
            this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            this.humidite.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

        }
        private void Tempmin_MouseLeave(object sender, MouseEventArgs e)
        {
            this.tempmin.Background = null;
            this.tempmin.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

        }
        private void Tempmax_MouseLeave(object sender, MouseEventArgs e)
        {

            this.tempmax.Background = null;
            this.tempmax.Foreground= new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
        }
        private void Tempvent_MouseLeave(object sender, MouseEventArgs e)
        {

            this.tempvent.Background = null;
            this.tempvent.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

        } 
        private void Visibilite_MouseLeave(object sender, MouseEventArgs e)
        {

            this.visibilite.Background = null;
            this.visibilite.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

        }     
        private void Humidite_MouseLeave(object sender, MouseEventArgs e)
        {

            this.humidite.Background = null;
            this.humidite.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

        }

        //Gestion fenêtre
        private void power_click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void minimize_click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.A:
                    {
                        accueil ev = new accueil(wilaya, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.M:
                    {
                        if (InfoJour.test_cnx() == 1)   // si connexion ==> météo du jour
                        {
                            meteo_jour nvv = new meteo_jour(wilaya, output_out);
                            nvv.Show();
                            this.Close();
                        }
                        else
                        {
                            WpfMessageBox.Show("Vous ne disposez pas de connexion internet!");

                        }
                    }
                    break;
                case Key.P:
                    {
                        recupe_man ev = new recupe_man(wilaya);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.E:
                    {
                        evolution ev = new evolution(wilaya, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.T:
                    {
                        Contact ev = new Contact(wilaya, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.R:
                    {
                        parametre ev = new parametre(wilaya, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.O:
                    {
                        apropos ev = new apropos(wilaya, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.D:
                    {
                        credit ev = new credit(wilaya, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.Z:
                    {
                        mise_a_jour mise = new mise_a_jour(wilaya, output_out);
                        mise.Show();
                        this.Close();
                    }
                    break;
                case Key.S:
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
                    break;
            }



        }
        
        //Aide en ligne

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("file:///C:\\Users\\user\\Desktop\\Projet\\OurProjectversion8.0\\Our_Project_ver2.0\\html5up-overflow\\index.html");

        }
        
    }
}
