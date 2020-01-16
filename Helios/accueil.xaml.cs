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
using System.Runtime.InteropServices;
using System.IO;
using Newtonsoft.Json;
using System.Net;
using LiveCharts;
using LiveCharts.Wpf;
using Brushes = System.Windows.Media.Brushes;
using System.Timers;
using LiveCharts.WinForms;
using Microsoft.Win32;

namespace Helios
{
    /// <summary>
    /// Logique d'interaction pour accueil.xaml
    /// </summary>


    public partial class accueil : Window

    {
        /* La page de l'accueil : contient la carte intéractive */
        String appid = "3310e252d2c293fd401e73dbc9eda3bc";
        string wilaya1 = File.ReadAllText(@"wilaya.txt"); // Récupération du nom de la wilaya par défaut pour lequel la prevision et la meteo du jour sera affichant dont le cas ou 
        // aucune selection n'a été faite auparvant 
        InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root();
        public accueil(string city, InfoJour.weatherinfo.Root output)
        {
            wilaya1 = wilaya1.Trim(new Char[] { ' ', '\r', '\n', '\t' }); // suppression de tout caractére bizarre lors de la récupération du fichier 
            InitializeComponent();
            CreateAndLoadRichTextBox(); // Création du bloc txt " Le saviez-vous " 
            output_out = output;
            /******************************************************************
             *     Construction de la carte 
             ******************************************************************/
            if (InfoJour.test_cnx() == 1) // Si il y a connection internet  
            {

                InfoJour.weatherinfo.Root[] outputlist = new InfoJour.weatherinfo.Root[15]; //Instancier les objets
                for (int i = 0; i < 12; i++)
                {
                    outputlist[i] = new InfoJour.weatherinfo.Root();
                }
                // Récuperation de la meteo du jour de 12 wilayas pour les afficher sur la carte
                InfoJour.weatherinfo.getweather("tamanrasset", appid, ref output);
                outputlist[0] = output;
                InfoJour.weatherinfo.getweather("adrar", appid, ref output);
                outputlist[1] = output;
                InfoJour.weatherinfo.getweather("oran", appid, ref output);
                outputlist[2] = output;
                InfoJour.weatherinfo.getweather("djelfa", appid, ref output);
                outputlist[3] = output;
                InfoJour.weatherinfo.getweather("khenchela", appid, ref output);
                outputlist[4] = output;
                InfoJour.weatherinfo.getweather("bechar", appid, ref output);
                outputlist[5] = output;
                InfoJour.weatherinfo.getweather("annaba", appid, ref output);
                outputlist[6] = output;
                InfoJour.weatherinfo.getweather("boumerdes", appid, ref output);
                outputlist[7] = output;
                InfoJour.weatherinfo.getweather("tindouf", appid, ref output);
                outputlist[8] = output;
                InfoJour.weatherinfo.getweather("illizi", appid, ref output);
                outputlist[9] = output;
                InfoJour.weatherinfo.getweather("el oued", appid, ref output);
                outputlist[10] = output;
                InfoJour.weatherinfo.getweather("ghardaia", appid, ref output);
                outputlist[11] = output;
                // Affichage de la carte intéractive tout dépend de l'heure si il fait nuit la carte sera sombre sinn claire
                BitmapImage imag;
                // 1. Mode journée :
                if ((DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].sys.sunset)) < 0) && (DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].sys.sunrise)) > 0))
                {
                    imag = new BitmapImage(new Uri("bateauMer.jpg", UriKind.Relative));
                    back.Source = imag;
                    imag = new BitmapImage(new Uri("cartecomp.png", UriKind.Relative));
                    cartecomp.Source = imag;
                    imag = new BitmapImage(new Uri("W_Adrar.png", UriKind.Relative));
                    adrar.Source = imag;
                    imag = new BitmapImage(new Uri("W_Tamanrasset.png", UriKind.Relative));
                    Tamanrasset.Source = imag;
                    imag = new BitmapImage(new Uri("W_Biskra.png", UriKind.Relative));
                    Biskra.Source = imag;
                    imag = new BitmapImage(new Uri("W_Laghouat.png", UriKind.Relative));
                    Laghouat.Source = imag;
                    imag = new BitmapImage(new Uri("W_Saida.png", UriKind.Relative));
                    Saida.Source = imag;
                    imag = new BitmapImage(new Uri("W_Ouargla.png", UriKind.Relative));
                    Ouargla.Source = imag;
                    imag = new BitmapImage(new Uri("W_Ghardaia.png", UriKind.Relative));
                    ghardaia.Source = imag;
                    imag = new BitmapImage(new Uri("W_Tindouf.png", UriKind.Relative));
                    Tindouf.Source = imag;
                    imag = new BitmapImage(new Uri("W_Nâama.png", UriKind.Relative));
                    Naama.Source = imag;
                    imag = new BitmapImage(new Uri("W_Khenchla.png", UriKind.Relative));
                    Khenchela.Source = imag;
                    imag = new BitmapImage(new Uri("W_Tebessa.png", UriKind.Relative));
                    Tebessa.Source = imag;
                    imag = new BitmapImage(new Uri("W_Msila.png", UriKind.Relative));
                    Msila.Source = imag;
                    imag = new BitmapImage(new Uri("W_Djelfa.png", UriKind.Relative));
                    Djelfa.Source = imag;
                    imag = new BitmapImage(new Uri("W_Tiaret.png", UriKind.Relative));
                    Tiaret.Source = imag;
                    imag = new BitmapImage(new Uri("W_Ain Defla.png", UriKind.Relative));
                    aindefla.Source = imag;
                    imag = new BitmapImage(new Uri("W_Alger.png", UriKind.Relative));
                    Alger.Source = imag;
                    imag = new BitmapImage(new Uri("W_Batna.png", UriKind.Relative));
                    batna.Source = imag;
                    imag = new BitmapImage(new Uri("W_Ain Timouchent.png", UriKind.Relative));
                    aittimouchent.Source = imag;
                    imag = new BitmapImage(new Uri("W_Annaba.png", UriKind.Relative));
                    annaba.Source = imag;
                    imag = new BitmapImage(new Uri("W_Bejaia.png", UriKind.Relative));
                    bejaia.Source = imag;
                    imag = new BitmapImage(new Uri("W_Blida.png", UriKind.Relative));
                    blida.Source = imag;
                    imag = new BitmapImage(new Uri("W_Bordj Bou Arreridj.png", UriKind.Relative));
                    borj.Source = imag;
                    imag = new BitmapImage(new Uri("W_Constantine.png", UriKind.Relative));
                    constantine.Source = imag;
                    imag = new BitmapImage(new Uri("W_Bouira.png", UriKind.Relative));
                    bouira.Source = imag;
                    imag = new BitmapImage(new Uri("W_Boumerdes.png", UriKind.Relative));
                    Boumerdes.Source = imag;
                    imag = new BitmapImage(new Uri("W_Chlef.png", UriKind.Relative));
                    Chlef.Source = imag;
                    imag = new BitmapImage(new Uri("W_El Tarf.png", UriKind.Relative));
                    eltaref.Source = imag;
                    imag = new BitmapImage(new Uri("W_El Bayedh.png", UriKind.Relative));
                    elbayedh.Source = imag;
                    imag = new BitmapImage(new Uri("W_Jijel.png", UriKind.Relative));
                    jijel.Source = imag;
                    imag = new BitmapImage(new Uri("W_Guelma.png", UriKind.Relative));
                    guelma.Source = imag;
                    imag = new BitmapImage(new Uri("W_El Oued.png", UriKind.Relative));
                    eloued.Source = imag;
                    imag = new BitmapImage(new Uri("W_Mascara.png", UriKind.Relative));
                    mascara.Source = imag;
                    imag = new BitmapImage(new Uri("W_Médéa.png", UriKind.Relative));
                    medea.Source = imag;
                    imag = new BitmapImage(new Uri("W_Mila.png", UriKind.Relative));
                    mila.Source = imag;
                    imag = new BitmapImage(new Uri("W_Mostghanem.png", UriKind.Relative));
                    mostaganem.Source = imag;
                    imag = new BitmapImage(new Uri("W_Oum El Bouaghi.png", UriKind.Relative));
                    elbouaghi.Source = imag;
                    imag = new BitmapImage(new Uri("W_Tipaza.png", UriKind.Relative));
                    tipaza.Source = imag;
                    imag = new BitmapImage(new Uri("W_Tissemsilt.png", UriKind.Relative));
                    Tissemsilt.Source = imag;
                    imag = new BitmapImage(new Uri("W_Oran.png", UriKind.Relative));
                    oran.Source = imag;
                    imag = new BitmapImage(new Uri("W_Relizane.png", UriKind.Relative));
                    relizane.Source = imag;
                    imag = new BitmapImage(new Uri("W_Sétif.png", UriKind.Relative));
                    setif.Source = imag;
                    imag = new BitmapImage(new Uri("W_Sidi Belabbes.png", UriKind.Relative));
                    sba.Source = imag;
                    imag = new BitmapImage(new Uri("W_Skikda.png", UriKind.Relative));
                    skikda.Source = imag;
                    imag = new BitmapImage(new Uri("W_Souk Ahras.png", UriKind.Relative));
                    soukahras.Source = imag;
                    imag = new BitmapImage(new Uri("W_Tizi Ouzou.png", UriKind.Relative));
                    tizi.Source = imag;
                    imag = new BitmapImage(new Uri("W_Tlemcen.png", UriKind.Relative));
                    tlemcen.Source = imag;
                    imag = new BitmapImage(new Uri("W_Illizi.png", UriKind.Relative));
                    Illizi.Source = imag;
                    imag = new BitmapImage(new Uri("W_Béchar.png", UriKind.Relative));
                    Bechar.Source = imag;


                }
                // 2. Mode nuit : 
                else if ((DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].sys.sunset)) > 0) || (DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].sys.sunrise)) < 0))
                {
                    imag = new BitmapImage(new Uri("2.jpg", UriKind.Relative));
                    back.Opacity = 1;
                    back.Source = imag;
                    imag = new BitmapImage(new Uri("cartecompN.png", UriKind.Relative));
                    cartecomp.Opacity = 0.8;
                    cartecomp.Source = imag;


                    imag = new BitmapImage(new Uri("W_AdrarN.png", UriKind.Relative));
                    adrar.Source = imag;
                    imag = new BitmapImage(new Uri("W_TamanrassetN.png", UriKind.Relative));
                    Tamanrasset.Source = imag;
                    imag = new BitmapImage(new Uri("W_BiskraN.png", UriKind.Relative));
                    Biskra.Source = imag;
                    imag = new BitmapImage(new Uri("W_LaghouatN.png", UriKind.Relative));
                    Laghouat.Source = imag;
                    imag = new BitmapImage(new Uri("W_SaidaN.png", UriKind.Relative));
                    Saida.Source = imag;
                    imag = new BitmapImage(new Uri("W_OuarglaN.png", UriKind.Relative));
                    Ouargla.Source = imag;
                    imag = new BitmapImage(new Uri("W_GhardaiaN.png", UriKind.Relative));
                    ghardaia.Source = imag;
                    imag = new BitmapImage(new Uri("W_TindoufN.png", UriKind.Relative));
                    Tindouf.Source = imag;
                    imag = new BitmapImage(new Uri("W_NâamaN.png", UriKind.Relative));
                    Naama.Source = imag;
                    imag = new BitmapImage(new Uri("W_KhenchlaN.png", UriKind.Relative));
                    Khenchela.Source = imag;
                    imag = new BitmapImage(new Uri("W_TebessaN.png", UriKind.Relative));
                    Tebessa.Source = imag;
                    imag = new BitmapImage(new Uri("W_MsilaN.png", UriKind.Relative));
                    Msila.Source = imag;
                    imag = new BitmapImage(new Uri("W_DjelfaN.png", UriKind.Relative));
                    Djelfa.Source = imag;
                    imag = new BitmapImage(new Uri("W_TiaretN.png", UriKind.Relative));
                    Tiaret.Source = imag;
                    imag = new BitmapImage(new Uri("W_Ain DeflaN.png", UriKind.Relative));
                    aindefla.Source = imag;
                    imag = new BitmapImage(new Uri("W_AlgerN.png", UriKind.Relative));
                    Alger.Source = imag;
                    imag = new BitmapImage(new Uri("W_BatnaN.png", UriKind.Relative));
                    batna.Source = imag;
                    imag = new BitmapImage(new Uri("W_Ain TimouchentN.png", UriKind.Relative));
                    aittimouchent.Source = imag;
                    imag = new BitmapImage(new Uri("W_AnnabaN.png", UriKind.Relative));
                    annaba.Source = imag;
                    imag = new BitmapImage(new Uri("W_BejaiaN.png", UriKind.Relative));
                    bejaia.Source = imag;
                    imag = new BitmapImage(new Uri("W_BlidaN.png", UriKind.Relative));
                    blida.Source = imag;
                    imag = new BitmapImage(new Uri("W_Bordj Bou ArreridjN.png", UriKind.Relative));
                    borj.Source = imag;
                    imag = new BitmapImage(new Uri("W_ConstantineN.png", UriKind.Relative));
                    constantine.Source = imag;
                    imag = new BitmapImage(new Uri("W_BouiraN.png", UriKind.Relative));
                    bouira.Source = imag;
                    imag = new BitmapImage(new Uri("W_BoumerdesN.png", UriKind.Relative));
                    Boumerdes.Source = imag;
                    imag = new BitmapImage(new Uri("W_ChlefN.png", UriKind.Relative));
                    Chlef.Source = imag;
                    imag = new BitmapImage(new Uri("W_El TarfN.png", UriKind.Relative));
                    eltaref.Source = imag;
                    imag = new BitmapImage(new Uri("W_El BayedhN.png", UriKind.Relative));
                    elbayedh.Source = imag;
                    imag = new BitmapImage(new Uri("W_JijelN.png", UriKind.Relative));
                    jijel.Source = imag;
                    imag = new BitmapImage(new Uri("W_GuelmaN.png", UriKind.Relative));
                    guelma.Source = imag;
                    imag = new BitmapImage(new Uri("W_El OuedN.png", UriKind.Relative));
                    eloued.Source = imag;
                    imag = new BitmapImage(new Uri("W_MascaraN.png", UriKind.Relative));
                    mascara.Source = imag;
                    imag = new BitmapImage(new Uri("W_MédéaN.png", UriKind.Relative));
                    medea.Source = imag;
                    imag = new BitmapImage(new Uri("W_MilaN.png", UriKind.Relative));
                    mila.Source = imag;
                    imag = new BitmapImage(new Uri("W_MostghanemN.png", UriKind.Relative));
                    mostaganem.Source = imag;
                    imag = new BitmapImage(new Uri("W_Oum El BouaghiN.png", UriKind.Relative));
                    elbouaghi.Source = imag;
                    imag = new BitmapImage(new Uri("W_TipazaN.png", UriKind.Relative));
                    tipaza.Source = imag;
                    imag = new BitmapImage(new Uri("W_TissemsiltN.png", UriKind.Relative));
                    Tissemsilt.Source = imag;
                    imag = new BitmapImage(new Uri("W_OranN.png", UriKind.Relative));
                    oran.Source = imag;
                    imag = new BitmapImage(new Uri("W_RelizaneN.png", UriKind.Relative));
                    relizane.Source = imag;
                    imag = new BitmapImage(new Uri("W_SétifN.png", UriKind.Relative));
                    setif.Source = imag;
                    imag = new BitmapImage(new Uri("W_Sidi BelabbesN.png", UriKind.Relative));
                    sba.Source = imag;
                    imag = new BitmapImage(new Uri("W_SkikdaN.png", UriKind.Relative));
                    skikda.Source = imag;
                    imag = new BitmapImage(new Uri("W_Souk AhrasN.png", UriKind.Relative));
                    soukahras.Source = imag;
                    imag = new BitmapImage(new Uri("W_Tizi OuzouN.png", UriKind.Relative));
                    tizi.Source = imag;
                    imag = new BitmapImage(new Uri("W_TlemcenN.png", UriKind.Relative));
                    tlemcen.Source = imag;
                    imag = new BitmapImage(new Uri("W_IlliziN.png", UriKind.Relative));
                    Illizi.Source = imag;
                    imag = new BitmapImage(new Uri("W_BécharN.png", UriKind.Relative));
                    Bechar.Source = imag;

                }


                ImageSource[] tab = new ImageSource[12];
                string desc;
                for (int i = 0; i < 12; i++)
                {
                    // tab[i] = new ImageSource(); 
                    desc = string.Format("{0}", outputlist[i].weather[0].description);
                    // Définir les icones pour chaque conditions : 
                    if (InfoJour.test_cnx() == 1)
                    {
                        switch (desc)
                        {
                            case "light rain":
                            case "moderate rain":
                            case "heavy intensity rain":
                            case "very heavy rain":
                            case "extreme rain":
                            case "light intensity drizzle":
                            case "drizzle":
                            case "heavy intensity drizzle":
                            case "light intensity drizzle rain":
                            case "drizzle rain":
                            case "heavy intensity drizzle rain":
                            case "shower rain and drizzle":
                            case "heavy shower rain and drizzle":
                            case "shower drizzle":



                                {

                                    if ((DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].sys.sunset)) < 0) && (DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].sys.sunrise)) > 0))
                                    {
                                        tab[i] = new BitmapImage(new Uri("41.png", UriKind.Relative));
                                    }
                                    if ((DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].sys.sunset)) > 0) || (DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].sys.sunrise)) < 0))
                                    {
                                        tab[i] = new BitmapImage(new Uri("46.png", UriKind.Relative));

                                    }
                                }
                                break;
                            case "few clouds":
                            case "scattered clouds":
                            case "broken clouds":
                            case "overcast clouds":

                                {
                                    if ((DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].sys.sunset)) < 0) && (DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].sys.sunrise)) > 0))
                                    {

                                        tab[i] = new BitmapImage(new Uri("28.png", UriKind.Relative));

                                    }
                                    if ((DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].sys.sunset)) > 0) || (DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].sys.sunrise)) < 0))
                                    {
                                        tab[i] = new BitmapImage(new Uri("27.png", UriKind.Relative));
                                    }
                                }
                                break;
                            case "clear sky":

                                {
                                    if ((DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].sys.sunset)) < 0) && (DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].sys.sunrise)) > 0))
                                    {

                                        tab[i] = new BitmapImage(new Uri("32.png", UriKind.Relative));
                                    }
                                    if ((DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].sys.sunset)) > 0) || (DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(outputlist[7].sys.sunrise)) < 0))
                                    {
                                        tab[i] = new BitmapImage(new Uri("31.png", UriKind.Relative));
                                    }

                                }
                                break;
                            case "rain":
                            case "freezing rain":
                            case "light intensity shower rain":
                            case "shower rain":
                            case "heavy intensity shower rain":
                            case "ragged shower rain":
                                {
                                    tab[i] = new BitmapImage(new Uri("10.png", UriKind.Relative));
                                }
                                break;
                            case "thunderstorm with light rain":
                            case "thunderstorm with rain":
                            case "thunderstorm with heavy rain":
                            case "light thunderstorm":
                            case "thunderstorm":
                            case "heavy thunderstorm":
                            case "ragged thunderstorm":
                            case "thunderstorm with light drizzle":
                            case "thunderstorm with drizzle":
                            case "thunderstorm with heavy drizzle":


                                {
                                    tab[i] = new BitmapImage(new Uri("4.png", UriKind.Relative));

                                }
                                break;

                            case "mist":
                            case "smoke":
                            case "haze":
                            case "sand, dust whirls":
                            case "fog":
                            case "sand":
                            case "dust":
                            case "volcanic ash":
                            case "squalls":
                            case "tornado":

                                {
                                    tab[i] = new BitmapImage(new Uri("24.png", UriKind.Relative));
                                }
                                break;

                            case "light snow":
                            case "Heavy snow":
                            case "Light shower sleet":
                            case "Shower sleet":
                            case "Light rain and snow":
                            case "Rain and snow":
                            case "Light shower snow":
                            case "Shower snow":
                            case "Heavy shower snow":
                                {
                                    tab[i] = new BitmapImage(new Uri("14.png", UriKind.Relative));
                                }
                                break;
                        }

                        adr.Source = tab[1];
                        orann.Source = tab[2];
                        Dje.Source = tab[3];
                        ana.Source = tab[6];
                        boumer.Source = tab[7];
                        tind.Source = tab[8];
                        ilizi.Source = tab[9];
                        elouede.Source = tab[10];
                        ghar.Source = tab[11];
                        Becharr.Source = tab[5];

                        taman.Source = tab[0];

                    }
                }
            }
            else // En cas ou il n'y a pas de connexion internet :
            {
                BitmapImage imag;
                imag = new BitmapImage(new Uri("bateauMer.jpg", UriKind.Relative));
                back.Source = imag;
                imag = new BitmapImage(new Uri("cartecomp.png", UriKind.Relative));
                cartecomp.Source = imag;
                imag = new BitmapImage(new Uri("W_Adrar.png", UriKind.Relative));
                adrar.Source = imag;
                imag = new BitmapImage(new Uri("W_Tamanrasset.png", UriKind.Relative));
                Tamanrasset.Source = imag;
                imag = new BitmapImage(new Uri("W_Biskra.png", UriKind.Relative));
                Biskra.Source = imag;
                imag = new BitmapImage(new Uri("W_Laghouat.png", UriKind.Relative));
                Laghouat.Source = imag;
                imag = new BitmapImage(new Uri("W_Saida.png", UriKind.Relative));
                Saida.Source = imag;
                imag = new BitmapImage(new Uri("W_Ouargla.png", UriKind.Relative));
                Ouargla.Source = imag;
                imag = new BitmapImage(new Uri("W_Ghardaia.png", UriKind.Relative));
                ghardaia.Source = imag;
                imag = new BitmapImage(new Uri("W_Tindouf.png", UriKind.Relative));
                Tindouf.Source = imag;
                imag = new BitmapImage(new Uri("W_Nâama.png", UriKind.Relative));
                Naama.Source = imag;
                imag = new BitmapImage(new Uri("W_Khenchla.png", UriKind.Relative));
                Khenchela.Source = imag;
                imag = new BitmapImage(new Uri("W_Tebessa.png", UriKind.Relative));
                Tebessa.Source = imag;
                imag = new BitmapImage(new Uri("W_Msila.png", UriKind.Relative));
                Msila.Source = imag;
                imag = new BitmapImage(new Uri("W_Djelfa.png", UriKind.Relative));
                Djelfa.Source = imag;
                imag = new BitmapImage(new Uri("W_Tiaret.png", UriKind.Relative));
                Tiaret.Source = imag;
                imag = new BitmapImage(new Uri("W_Ain Defla.png", UriKind.Relative));
                aindefla.Source = imag;
                imag = new BitmapImage(new Uri("W_Alger.png", UriKind.Relative));
                Alger.Source = imag;
                imag = new BitmapImage(new Uri("W_Batna.png", UriKind.Relative));
                batna.Source = imag;
                imag = new BitmapImage(new Uri("W_Ain Timouchent.png", UriKind.Relative));
                aittimouchent.Source = imag;
                imag = new BitmapImage(new Uri("W_Annaba.png", UriKind.Relative));
                annaba.Source = imag;
                imag = new BitmapImage(new Uri("W_Bejaia.png", UriKind.Relative));
                bejaia.Source = imag;
                imag = new BitmapImage(new Uri("W_Blida.png", UriKind.Relative));
                blida.Source = imag;
                imag = new BitmapImage(new Uri("W_Bordj Bou Arreridj.png", UriKind.Relative));
                borj.Source = imag;
                imag = new BitmapImage(new Uri("W_Constantine.png", UriKind.Relative));
                constantine.Source = imag;
                imag = new BitmapImage(new Uri("W_Bouira.png", UriKind.Relative));
                bouira.Source = imag;
                imag = new BitmapImage(new Uri("W_Boumerdes.png", UriKind.Relative));
                Boumerdes.Source = imag;
                imag = new BitmapImage(new Uri("W_Chlef.png", UriKind.Relative));
                Chlef.Source = imag;
                imag = new BitmapImage(new Uri("W_El Tarf.png", UriKind.Relative));
                eltaref.Source = imag;
                imag = new BitmapImage(new Uri("W_El Bayedh.png", UriKind.Relative));
                elbayedh.Source = imag;
                imag = new BitmapImage(new Uri("W_Jijel.png", UriKind.Relative));
                jijel.Source = imag;
                imag = new BitmapImage(new Uri("W_Guelma.png", UriKind.Relative));
                guelma.Source = imag;
                imag = new BitmapImage(new Uri("W_El Oued.png", UriKind.Relative));
                eloued.Source = imag;
                imag = new BitmapImage(new Uri("W_Mascara.png", UriKind.Relative));
                mascara.Source = imag;
                imag = new BitmapImage(new Uri("W_Médéa.png", UriKind.Relative));
                medea.Source = imag;
                imag = new BitmapImage(new Uri("W_Mila.png", UriKind.Relative));
                mila.Source = imag;
                imag = new BitmapImage(new Uri("W_Mostghanem.png", UriKind.Relative));
                mostaganem.Source = imag;
                imag = new BitmapImage(new Uri("W_Oum El Bouaghi.png", UriKind.Relative));
                elbouaghi.Source = imag;
                imag = new BitmapImage(new Uri("W_Tipaza.png", UriKind.Relative));
                tipaza.Source = imag;
                imag = new BitmapImage(new Uri("W_Tissemsilt.png", UriKind.Relative));
                Tissemsilt.Source = imag;
                imag = new BitmapImage(new Uri("W_Oran.png", UriKind.Relative));
                oran.Source = imag;
                imag = new BitmapImage(new Uri("W_Relizane.png", UriKind.Relative));
                relizane.Source = imag;
                imag = new BitmapImage(new Uri("W_Sétif.png", UriKind.Relative));
                setif.Source = imag;
                imag = new BitmapImage(new Uri("W_Sidi Belabbes.png", UriKind.Relative));
                sba.Source = imag;
                imag = new BitmapImage(new Uri("W_Skikda.png", UriKind.Relative));
                skikda.Source = imag;
                imag = new BitmapImage(new Uri("W_Souk Ahras.png", UriKind.Relative));
                soukahras.Source = imag;
                imag = new BitmapImage(new Uri("W_Tizi Ouzou.png", UriKind.Relative));
                tizi.Source = imag;
                imag = new BitmapImage(new Uri("W_Tlemcen.png", UriKind.Relative));
                tlemcen.Source = imag;
                imag = new BitmapImage(new Uri("W_Illizi.png", UriKind.Relative));
                Illizi.Source = imag;
                imag = new BitmapImage(new Uri("W_Béchar.png", UriKind.Relative));
                Bechar.Source = imag;

            }
            wilaya1 = city;
        }
        // Construction des tooltip qui contient l'evolution == schémas illustrant la temparture de chaque wilaya durant les dix derniéres années 
        LineSeries col = new LineSeries() //déclarer le types des graphes (courbes)
        {
            Title = "Temperature",
            DataLabels = true,
            Values = new ChartValues<int>(),
            LabelPoint = Point => Point.Y.ToString(),
            Foreground = Brushes.White
                           ,
            AreaLimit = 10,
            LineSmoothness = 0,
            Stroke = Brushes.MidnightBlue,
            PointForeground = Brushes.White,
            PointGeometrySize = 8,
            Opacity = 55
        };
        LineSeries col2 = new LineSeries()
        {
            Title = "Humidité",
            DataLabels = true,
            Values = new ChartValues<int>(),
            LabelPoint = Point => Point.Y.ToString(),
            Foreground = Brushes.Black
                    ,
            AreaLimit = 10,
            LineSmoothness = 0,
            Stroke = Brushes.MidnightBlue,
            PointForeground = Brushes.White,
            PointGeometrySize = 8,
            Opacity = 55
        };
        LineSeries col3 = new LineSeries()
        {
            Title = "Précipitation",
            DataLabels = false,
            Values = new ChartValues<double>(),
            LabelPoint = Point => Point.Y.ToString(),
            Foreground = Brushes.Black
                    ,
            AreaLimit = 10,
            LineSmoothness = 0,
            Stroke = Brushes.MidnightBlue,
            PointForeground = Brushes.White,
            PointGeometrySize = 8,
            Opacity = 55
        };

        // Construction de l'axe des années 
        Axis ax1 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax3 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax4 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax5 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax6 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax7 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax8 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax9 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax10 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax11 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax12 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax13 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax14 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax15 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax16 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax17 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax18 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax19 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax20 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax21 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax22 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax23 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax24 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax25 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax26 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax27 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax28 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax29 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax30 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax31 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax32 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax33 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax34 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax35 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax36 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax37 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax38 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax39 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax40 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax41 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax42 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax43 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax44 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax45 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax46 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax47 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
        Axis ax48 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };

        private void wilaya(string str)
        {
            //Récuperation des données de chaque table tout depende l'entrée str == wilaya à l'aide du model entity afin de pourvoir dessiner les schémas 
            WeatherEntities db = new WeatherEntities();
            using (db)
            {
                col.Values.Clear(); // il faut supprimer les graphes qui sont déja déssiner , il y a 3 graphes pour la temperature l'humidité et la précipitation

                if (str == "Adrar")
                {
                    var data1 = db.GetTempAdrar();//lire les données en utilisant des procédures dans la bdd
                    var data2 = db.GetHumadrar();
                    var data3 = db.GetPreadrar();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Annaba")
                {
                    var data1 = db.GetTempAdrar();
                    var data2 = db.GetHumannaba();
                    var data3 = db.GetPreannaba();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Alger")
                {
                    var data1 = db.GetTempAlger();
                    var data2 = db.GetHumalger();
                    var data3 = db.GetPrealger();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Ain Defla")
                {
                    var data1 = db.GetTempaindefla();
                    var data2 = db.GetHumaindefla();
                    var data3 = db.GetPreaindefla();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Batna")
                {
                    var data1 = db.GetTempBatna();
                    var data2 = db.GetHumbatna();
                    var data3 = db.GetPrebatna();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Blida")
                {
                    var data1 = db.GetTempBlida();
                    var data2 = db.GetHumblida();
                    var data3 = db.GetPreblida();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Ain Témouchent")
                {
                    var data1 = db.GetTempAintemouchent();
                    var data2 = db.GetHumaintemouchent();
                    var data3 = db.GetPreaintemouchent();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Béchar")
                {
                    var data1 = db.GetTempBechar();
                    var data2 = db.GetHumbechar();
                    var data3 = db.GetPrebechar();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Béjaia")
                {
                    var data1 = db.GetTempBejaia();
                    var data2 = db.GetHumbejaia();
                    var data3 = db.GetPrebejaia();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Biskra")
                {
                    var data1 = db.GetTempBiskra();
                    var data2 = db.GetHumbiskra();
                    var data3 = db.GetPrebiskra();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Bordj Bou Arréridj")
                {
                    var data1 = db.GetTempBordj();
                    var data2 = db.GetHumbordj();
                    var data3 = db.GetPrebordj();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Bouira")
                {
                    var data1 = db.GetTempBouira();
                    var data2 = db.GetHumbouira();
                    var data3 = db.GetPrebouira();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "El Bayadh")
                {
                    var data1 = db.GetTempByadh();
                    var data2 = db.GetHumbayadh();
                    var data3 = db.GetPrebyadh();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Chlef")
                {
                    var data1 = db.GetTempChlef();
                    var data2 = db.GetHumchlef();
                    var data3 = db.GetPrechlef();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Constantine")
                {
                    var data1 = db.GetTempConsto();
                    var data2 = db.GetHumconsto();
                    var data3 = db.GetPreconsto();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Djelfa")
                {
                    var data1 = db.GetTempDjelfa();
                    var data2 = db.GetHumdjelfa();
                    var data3 = db.GetPredjelfa();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Ghardaia")
                {
                    var data1 = db.GetTempGhardaia();
                    var data2 = db.GetHumghardaia();
                    var data3 = db.GetPreghardaia();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Guelma")
                {
                    var data1 = db.GetTempGuelma();
                    var data2 = db.GetHumguelma();
                    var data3 = db.GetPreguelma();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Illizi")
                {
                    var data1 = db.GetTempIllizi();
                    var data2 = db.GetHumillizi();
                    var data3 = db.GetPreillizi();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Jijel")
                {
                    var data1 = db.GetTempJijel();
                    var data2 = db.GetHumjijel();
                    var data3 = db.GetPrejijel();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Khenchela")
                {
                    var data1 = db.GetTempKhench();
                    var data2 = db.GetHumkhench();
                    var data3 = db.GetPrekhench();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Laghouat")
                {
                    var data1 = db.GetTempLaghouat();
                    var data2 = db.GetHumlaghouat();
                    var data3 = db.GetPrelaghouat();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Mascara")
                {
                    var data1 = db.GetTempMascara();
                    var data2 = db.GetHummascara();
                    var data3 = db.GetPremascara();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Médéa")
                {
                    var data1 = db.GetTempMedea();
                    var data2 = db.GetHummedea();
                    var data3 = db.GetPremedea();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Mila")
                {
                    var data1 = db.GetTempMila();
                    var data2 = db.GetHummila();
                    var data3 = db.GetPremila();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Mostaganem")
                {
                    var data1 = db.GetTempAdrar();//////
                    var data2 = db.GetHummosta();
                    var data3 = db.GetPremila();///////
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "M'Sila")
                {
                    var data1 = db.GetTempNaama();
                    var data2 = db.GetHumnaama();
                    var data3 = db.GetPrenaama();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Naâma")
                {
                    var data1 = db.GetTempNaama();
                    var data2 = db.GetHumnaama();
                    var data3 = db.GetPrealger();/////
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Oran")
                {
                    var data1 = db.GetTempOran();
                    var data2 = db.GetHumoran();
                    var data3 = db.GetPreoran();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "El-Oued")
                {
                    var data1 = db.GetTempOued();
                    var data2 = db.GetHumoued();
                    var data3 = db.GetPreoued();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Ouargla")
                {
                    var data1 = db.GetTempOuerg();
                    var data2 = db.GetHumouerg();
                    var data3 = db.GetPreouerg();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Oum El Bouaghi")
                {
                    var data1 = db.GetTempOum();
                    var data2 = db.GetHumoum();
                    var data3 = db.GetPreoum();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Relizane")
                {
                    var data1 = db.GetTempReliz();
                    var data2 = db.GetHureliz();
                    var data3 = db.GetPrereliz();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Saida")
                {
                    var data1 = db.GetTempSaida();
                    var data2 = db.GetHumsaida();
                    var data3 = db.GetPresaida();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Sétif")
                {
                    var data1 = db.GetTempSetif();
                    var data2 = db.GetHumsetif();
                    var data3 = db.GetPresetif();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Sidi BelAbbès")
                {
                    var data1 = db.GetTempSidiabbes();
                    var data2 = db.GetHumsidiabbes();
                    var data3 = db.GetPresidiabbes();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Skikda")
                {
                    var data1 = db.GetTempSkikda();
                    var data2 = db.GetHumskikda();
                    var data3 = db.GetPreskikda();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Souk Ahras")
                {
                    var data1 = db.GetTempSouk();
                    var data2 = db.GetHumsouk();
                    var data3 = db.GetPresouk();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Tamanrasset")
                {
                    var data1 = db.GetTemptamen();
                    var data2 = db.GetHumtamm();
                    var data3 = db.GetPretam();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "El Taref")
                {
                    var data1 = db.GetTempTarf();
                    var data2 = db.GetHutaref();
                    var data3 = db.GetPretaref();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Tébessa")
                {

                    var data1 = db.GetTempSouk();
                    var data2 = db.GetHumsouk();
                    var data3 = db.GetPresouk();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Tiaret")
                {
                    var data1 = db.GetTempTiaret();
                    var data2 = db.GetHumtiaret();
                    var data3 = db.GetPretiaret();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Tindouf")
                {
                    var data1 = db.gettemptindouf();
                    var data2 = db.gethumtindouf();
                    var data3 = db.getpretindouf();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Tipaza")
                {
                    var data1 = db.GetTempTipaza();
                    var data2 = db.GetHumtipaza();
                    var data3 = db.GetPretipaza();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Tissemsilt")
                {
                    var data1 = db.GetTempTisse();
                    var data2 = db.GetHumtissem();
                    var data3 = db.GetPretiss();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Tizi-Ouzou")
                {
                    var data1 = db.GetTempTizi();
                    var data2 = db.GetHumtizi();
                    var data3 = db.GetPretizi();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);




                    }
                }
                if (str == "Tlemcen")
                {
                    var data1 = db.GetTempTlemcen();
                    var data2 = db.GetHumtlemcen();
                    var data3 = db.GetPretlemcen();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
                if (str == "Boumerdès")
                {
                    var data1 = db.GetTempBoumerdes();
                    var data2 = db.GetHumboumerdes();
                    var data3 = db.GetPreboumerdes();
                    foreach (var x in data1)
                    {

                        col.Values.Add(x.total.Value);//ajouter les valeurs au graphes 

                    }
                    foreach (var x in data2)
                    {

                        col2.Values.Add(x.total.Value);


                    }
                    foreach (var x in data3)
                    {
                        col3.Values.Add(x.total.Value);


                    }
                }
            }


        }
        Axis ax2 = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };

        private void accueil_Load(object sender, EventArgs e)
        {
            // Chargement de la fenetre
            WeatherEntities db = new
                WeatherEntities();

            using (db)
            {

                var data3 = db.GetTempAlger();
                ax1.Labels = new List<String>();
                ax2.Labels = new List<String>();
                ax3.Labels = new List<String>();
                ax4.Labels = new List<String>();
                ax5.Labels = new List<String>();
                ax6.Labels = new List<String>();
                ax7.Labels = new List<String>();
                ax8.Labels = new List<String>();
                ax9.Labels = new List<String>();
                ax10.Labels = new List<String>();
                ax11.Labels = new List<String>();
                ax12.Labels = new List<String>();
                ax13.Labels = new List<String>();
                ax14.Labels = new List<String>();
                ax15.Labels = new List<String>();
                ax16.Labels = new List<String>();
                ax17.Labels = new List<String>();
                ax18.Labels = new List<String>();
                ax19.Labels = new List<String>();
                ax20.Labels = new List<String>();
                ax21.Labels = new List<String>();
                ax22.Labels = new List<String>();
                ax23.Labels = new List<String>();
                ax24.Labels = new List<String>();
                ax25.Labels = new List<String>();
                ax26.Labels = new List<String>();
                ax27.Labels = new List<String>();
                ax28.Labels = new List<String>();
                ax29.Labels = new List<String>();
                ax30.Labels = new List<String>();
                ax31.Labels = new List<String>();
                ax32.Labels = new List<String>();
                ax33.Labels = new List<String>();
                ax34.Labels = new List<String>();
                ax35.Labels = new List<String>();
                ax36.Labels = new List<String>();
                ax37.Labels = new List<String>();
                ax38.Labels = new List<String>();
                ax39.Labels = new List<String>();
                ax40.Labels = new List<String>();
                ax41.Labels = new List<String>();
                ax42.Labels = new List<String>();
                ax43.Labels = new List<String>();
                ax44.Labels = new List<String>();
                ax45.Labels = new List<String>();
                ax46.Labels = new List<String>();
                ax47.Labels = new List<String>();
                ax48.Labels = new List<String>();
                foreach (var x in data3)
                {
                    ax1.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax2.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax3.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax4.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax5.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax6.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax7.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax8.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax9.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax10.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax11.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax12.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax13.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax14.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax15.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax16.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax17.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax18.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax19.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax20.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax21.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax22.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax23.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax24.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax25.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax26.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax27.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax28.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax29.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax30.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax31.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax32.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax33.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax34.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax35.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax36.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax37.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax38.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax39.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax40.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax41.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax42.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax43.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax44.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax45.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax46.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax47.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                    ax48.Labels.Add(x.year.ToString().Substring(x.year.ToString().Length - 4, 4));
                }
                cartesianchart1.AxisX.Add(ax1);
                cartesianchart2.AxisX.Add(ax2);
                cartesianchart3.AxisX.Add(ax3);
                cartesianchart4.AxisX.Add(ax4);
                cartesianchart5.AxisX.Add(ax5);
                cartesianchart6.AxisX.Add(ax6);
                cartesianchart8.AxisX.Add(ax8);
                cartesianchart9.AxisX.Add(ax9);
                cartesianchart10.AxisX.Add(ax10);
                cartesianchart11.AxisX.Add(ax11);
                cartesianchart12.AxisX.Add(ax12);
                cartesianchart13.AxisX.Add(ax13);
                cartesianchart14.AxisX.Add(ax14);
                cartesianchart15.AxisX.Add(ax15);
                cartesianchart16.AxisX.Add(ax16);
                cartesianchart17.AxisX.Add(ax17);
                cartesianchart18.AxisX.Add(ax18);
                cartesianchart19.AxisX.Add(ax19);
                cartesianchart20.AxisX.Add(ax20);
                cartesianchart21.AxisX.Add(ax21);
                cartesianchart23.AxisX.Add(ax23);
                cartesianchart24.AxisX.Add(ax24);
                cartesianchart25.AxisX.Add(ax25);
                cartesianchart26.AxisX.Add(ax26);
                cartesianchart27.AxisX.Add(ax27);
                cartesianchart28.AxisX.Add(ax28);
                cartesianchart29.AxisX.Add(ax29);
                cartesianchart30.AxisX.Add(ax30);
                cartesianchart31.AxisX.Add(ax31);
                cartesianchart32.AxisX.Add(ax32);
                cartesianchart33.AxisX.Add(ax33);
                cartesianchart34.AxisX.Add(ax34);
                cartesianchart35.AxisX.Add(ax35);
                cartesianchart36.AxisX.Add(ax36);
                cartesianchart37.AxisX.Add(ax37);
                cartesianchart38.AxisX.Add(ax38);
                cartesianchart39.AxisX.Add(ax39);
                cartesianchart40.AxisX.Add(ax40);
                cartesianchart41.AxisX.Add(ax41);
                cartesianchart42.AxisX.Add(ax42);
                cartesianchart43.AxisX.Add(ax43);
                cartesianchart44.AxisX.Add(ax44);
                cartesianchart45.AxisX.Add(ax45);
                cartesianchart46.AxisX.Add(ax46);
                cartesianchart47.AxisX.Add(ax47);
                cartesianchart48.AxisX.Add(ax48);
                cartesianchart22.AxisX.Add(ax22);
                cartesianchart7.AxisX.Add(ax7);
            }


        }

        /*************************************************************************************************************
        * Les evenements de l'affichage de l'évolution : lorsque la souris passe par une wilaya : un schéma s'affiche 
        * ***********************************************************************************************************/
        // 48 evenements :
        private void tool1_Click(object sender, RoutedEventArgs e)
        {
            wilaya("Adrar");
            cartesianchart1.Series.Add(col);
        }
        private void tool2_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Ain Defla");
            cartesianchart2.Series.Add(col);
        }
        private void tool3_Click(object sender, RoutedEventArgs e)
        {
            wilaya("Boumerdès");

            cartesianchart3.Series.Add(col);
        }
        private void tool4_Click(object sender, RoutedEventArgs e)
        {
            wilaya("Ain Témouchent");

            cartesianchart4.Series.Add(col);
        }
        private void tool5_Click(object sender, RoutedEventArgs e)
        {
            wilaya("Alger");

            cartesianchart4.Series.Add(col);
        }
        private void tool6_Click(object sender, RoutedEventArgs e)
        {
            wilaya("Annaba");

            cartesianchart6.Series.Add(col);
        }
        private void tool7_Click(object sender, RoutedEventArgs e)
        {
            wilaya("Batna");

            cartesianchart7.Series.Add(col);
        }
        private void tool8_Click(object sender, RoutedEventArgs e)
        {
            wilaya("Béchar");

            cartesianchart8.Series.Add(col);
        }
        private void tool9_Click(object sender, RoutedEventArgs e)
        {
            wilaya("Béjaia");

            cartesianchart9.Series.Add(col);
        }
        private void tool10_Click(object sender, RoutedEventArgs e)
        {
            wilaya("Biskra");

            cartesianchart10.Series.Add(col);
        }
        private void tool11_Click(object sender, RoutedEventArgs e)
        {
            wilaya("Blida");

            cartesianchart11.Series.Add(col);
        }
        private void tool12_Click(object sender, RoutedEventArgs e)
        {
            wilaya("Bordj Bou Arréridj");

            cartesianchart12.Series.Add(col);
        }
        private void tool13_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Bouira");
            cartesianchart13.Series.Add(col);
        }
        private void tool14_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Chlef");
            cartesianchart14.Series.Add(col);
        }
        private void tool15_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Constantine");
            cartesianchart15.Series.Add(col);
        }
        private void tool16_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Djelfa");
            cartesianchart16.Series.Add(col);
        }
        private void tool17_Click(object sender, RoutedEventArgs e)
        {

            wilaya("El Bayadh");
            cartesianchart17.Series.Add(col);
        }
        private void tool18_Click(object sender, RoutedEventArgs e)
        {

            wilaya("El Taref");
            cartesianchart18.Series.Add(col);
        }
        private void tool19_Click(object sender, RoutedEventArgs e)
        {

            wilaya("El-Oued");
            cartesianchart19.Series.Add(col);
        }
        private void tool20_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Ghardaia");
            cartesianchart20.Series.Add(col);
        }
        private void tool21_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Guelma");
            cartesianchart21.Series.Add(col);
        }
        private void tool22_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Illizi");
            cartesianchart22.Series.Add(col);
        }
        private void tool23_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Jijel");
            cartesianchart23.Series.Add(col);
        }
        private void tool24_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Khenchela");
            cartesianchart24.Series.Add(col);
        }
        private void tool25_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Laghouat");
            cartesianchart25.Series.Add(col);
        }
        private void tool26_Click(object sender, RoutedEventArgs e)
        {

            wilaya("M'sila");
            cartesianchart26.Series.Add(col);
        }
        private void tool27_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Mascara");
            cartesianchart27.Series.Add(col);
        }
        private void tool28_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Médéa");
            cartesianchart28.Series.Add(col);
        }
        private void tool29_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Mila");
            cartesianchart29.Series.Add(col);
        }
        private void tool30_Click(object sender, RoutedEventArgs e)
        {
            wilaya("Mostaganem");
            cartesianchart30.Series.Add(col);
        }
        private void tool31_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Naâma");
            cartesianchart31.Series.Add(col);
        }
        private void tool32_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Oran");
            cartesianchart32.Series.Add(col);
        }
        private void tool33_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Ouargla");
            cartesianchart33.Series.Add(col);
        }
        private void tool34_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Oum El Bouaghi");
            cartesianchart34.Series.Add(col);
        }
        private void tool35_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Relizane");
            cartesianchart35.Series.Add(col);
        }
        private void tool36_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Saida");
            cartesianchart36.Series.Add(col);
        }
        private void tool37_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Sidi BelAbbès");
            cartesianchart37.Series.Add(col);
        }
        private void tool38_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Souk Ahras");
            cartesianchart38.Series.Add(col);
        }
        private void tool39_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Tamanrasset");
            cartesianchart39.Series.Add(col);
        }
        private void tool40_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Tébessa");
            cartesianchart40.Series.Add(col);
        }
        private void tool41_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Tiaret");
            cartesianchart41.Series.Add(col);
        }
        private void tool42_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Adrar");
            cartesianchart42.Series.Add(col);
        }
        private void tool43_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Tipaza");
            cartesianchart43.Series.Add(col);
        }
        private void tool44_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Tissemsilt");
            cartesianchart44.Series.Add(col);
        }
        private void tool45_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Tizi-Ouzou");
            cartesianchart45.Series.Add(col);
        }
        private void tool46_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Tlemcen");
            cartesianchart46.Series.Add(col);
        }
        private void tool47_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Sétif");
            cartesianchart47.Series.Add(col);
        }
        private void tool48_Click(object sender, RoutedEventArgs e)
        {

            wilaya("Skikda");
            cartesianchart48.Series.Add(col);
        }
        /*******************************************************************************************/

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

        private void ComboBox1_DropDownClosed(object sender, EventArgs e)
        {
            // Le choix d'une wilaya à partir de la liste déroulante : 
            ComboBox1.BorderBrush = Brushes.White;
            string str = ComboBox1.Text;
            // En cas de connection la selection d'un wilaya renvoie ---> méteo du jour 
            // En cas ou il n'y a pas de connexion ---> Prévision 

            switch (str)
            {
                case "Adrar":
                    wilaya1 = "adrar";
                    if (this.output_out.name != wilaya1) output_out.used = false;

                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Ghardaia":
                    wilaya1 = "ghardaia";
                    if (this.output_out.name != wilaya1) output_out.used = false;

                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Ain Defla":
                    wilaya1 = "ain-defla";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Ain Témouchent":
                    wilaya1 = "ain-temouchent";
                    if (this.output_out.name != wilaya1) output_out.used = false;

                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Alger":
                    wilaya1 = "alger";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Annaba":
                    wilaya1 = "annaba";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Batna":
                    wilaya1 = "batna";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Béchar":
                    wilaya1 = "bechar";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Béjaia":
                    wilaya1 = "bejaia";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Biskra":
                    wilaya1 = "biskra";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Blida":
                    wilaya1 = "blida";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Bordj Bou Arréridj":
                    wilaya1 = "bordj-bou-arreridj";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Bouira":
                    wilaya1 = "bouira";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Chlef":
                    wilaya1 = "chlef";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Boumerdès":
                    wilaya1 = "boumerdes";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Constantine":
                    wilaya1 = "constantine";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Djelfa":
                    wilaya1 = "djelfa";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "El Bayadh":
                    wilaya1 = "el-bayadh";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "El Taref":
                    wilaya1 = "el-tarf";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "El-Oued":
                    wilaya1 = "el-oued";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Guelma":
                    wilaya1 = "guelma";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Illizi":
                    wilaya1 = "illizi";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Jijel":
                    wilaya1 = "jijel";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Laghouat":
                    wilaya1 = "laghouat";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Khenchela":
                    wilaya1 = "khenchela";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "M’Sila":
                    wilaya1 = "msila";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Mascara":
                    wilaya1 = "mascara";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Médéa":
                    wilaya1 = "medea";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Mila":
                    wilaya1 = "mila";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Mostaganem":
                    wilaya1 = "mostaganem";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Naâma":
                    wilaya1 = "naama";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Oran":
                    wilaya1 = "oran";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Ouargla":
                    wilaya1 = "ouargla";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Oum El Bouaghi":
                    wilaya1 = "oum-el-bouaghi";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Relizane":
                    wilaya1 = "relizane";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Saida":
                    wilaya1 = "saida";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Sidi BelAbbès":
                    wilaya1 = "sidi-bel-abbes";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Skikda":
                    wilaya1 = "skikda";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Souk Ahras":
                    wilaya1 = "souk-ahras";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Tamanrasset":
                    wilaya1 = "tamanrasset";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Tiaret":
                    wilaya1 = "tiaret";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Tindouf":
                    wilaya1 = "tindouf";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Tébessa":
                    wilaya1 = "tebessa";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Tipaza":
                    wilaya1 = "tipaza";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Tissemsilt":
                    wilaya1 = "tissemssilt";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Tizi-Ouzou":
                    wilaya1 = "tizi-ouzou";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Tlemcen":
                    wilaya1 = "tlemcen";
                    if (this.output_out.name != wilaya1) output_out.used = false;
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya1, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;


            }
            // wilaya1 sera l'entrée de prévision en cas ou pas connex et de méteo du jour si y a de la conex
        }

        private void CreateScreenShot(UIElement visual, string file)

        {
            // Role : Capturer l'ecran automatique == Sauvergarde 
            double width = Convert.ToDouble(visual.GetValue(FrameworkElement.WidthProperty));
            double height = Convert.ToDouble(visual.GetValue(FrameworkElement.HeightProperty));
            if (double.IsNaN(width) || double.IsNaN(height))

            {
                throw new FormatException("Erreur !!");
            }
            RenderTargetBitmap render = new RenderTargetBitmap(
            Convert.ToInt32(width),
            Convert.ToInt32(visual.GetValue(FrameworkElement.HeightProperty)), 96, 96, PixelFormats.Pbgra32);
            // Indicate which control to render in the image
            render.Render(visual);
            try
            {
                using (FileStream stream = new FileStream(file, FileMode.Create))

                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(render));
                    encoder.Save(stream);
                    WpfMessageBox.Show("Réussi", "Capture prise avec succés", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Information);

                }

            }
            catch (Exception)
            {
                WpfMessageBox.Show("Erreur", "Echec de la sauvegarde !", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Error);


            }
        }

        /***********************************************************************************************************
       *                         création du txtbox qui contient le saviez-vous 
       * *********************************************************************************************************/
        private void CreateAndLoadRichTextBox()
        {

            //Le contenu change à chaque fois tout depend du random 

            string texte = "";
            Random aleatoire = new Random();
            // On a plusieurs informations à propos de la météo en général 
            int numTxt = aleatoire.Next(1, 8); // En utilisant numTxt qui un nombre aleatoire on aura l'information choisie aléatoirement 
            switch (numTxt)
            {
                case 1:
                    texte = "Jusqu'à présent, la température la plus basse jamais enregistrée était de - 93.2 °C, relevée à la station russe Vostok (Antarctique), le 10 août 2010";
                    break;
                case 2:
                    texte = "le record officiel de la température la plus élevée jamais observée à la surface du globe s'établit à 56,7 °C et a été enregistré le 10 juillet 1913 à Greenland Ranch (Vallée de la mort), en Californie, aux États-Unis d'Amérique";
                    break;
                case 3:
                    texte = "173 est le nombre de mois d'affilés durant lesquels il n'a pas plu à Arica au Chili. En effet, cette ville se situe dans le désert d'Atacama. Par ailleurs, Arica est connue pour sonrecord de précipitation au niveau mondial, puisqu'il ne tombe pas plus de... 0.76 mm par an.";
                    break;
                case 4:
                    texte = "173 est le nombre de mois d'affilés durant lesquels il n'a pas plu à Arica au Chili. En effet, cette ville se situe dans le désert d'Atacama. Par ailleurs, Arica est connue pour sonrecord de précipitation au niveau mondial, puisqu'il ne tombe pas plus de... 0.76 mm par an.";
                    break;
                case 5:
                    texte = "26 470 est la quantité de précipitation en millimètres tombée en une année en Inde en 1995. Ceci constitue le record mondial de précipitation en une année";
                    break;
                case 6:
                    texte = "La valeur 1083.8 hPa constituent actuellement le record de hautes pressions sur notre Terre. Ils ont été mesurés à Agata en Sibérie le 31 décembre 1968.";
                    break;
                case 7:
                    texte = "26 470 est la quantité de précipitation en millimètres tombée en une année en Inde en 1995. Ceci constitue le record mondial de précipitation en une année.";
                    break;
                case 8:
                    texte = "Xynthia est le nom de la tempête qui a frappé plusieurs pays dont la France dans la nuit du 27 au 28 février 2010, causant la mort de 53 personnes, avec des vents entre 130 et 160 km/h.";
                    break;

            }

            FlowDocument mcFlowDoc = new FlowDocument();
            Paragraph para = new Paragraph();
            Paragraph para1 = new Paragraph();
            /**** Design du Textbox ****/
            para1.Inlines.Add(new Bold(new Run("Saviez-vous que :")));
            para.Inlines.Add(new Bold(new Run(texte)));
            para.FontFamily = new FontFamily("Roboto");
            para.FontSize = 18;
            para.Foreground = Brushes.White;
            para1.FontFamily = new FontFamily("Roboto");
            para1.FontSize = 26;
            para1.Foreground = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFB107");
            mcFlowDoc.Blocks.Add(para1);
            mcFlowDoc.Blocks.Add(para);
            RichTextBox mcRTB = new RichTextBox();
            mcRTB.BorderBrush = Brushes.Transparent;
            mcRTB.Margin = new Thickness(20, 20, 0, 0);
            mcRTB.HorizontalAlignment = HorizontalAlignment.Center;
            mcRTB.Background = Brushes.Transparent;
            mcRTB.SelectionBrush = Brushes.Transparent;
            mcRTB.Document = mcFlowDoc;
            gridfull.Children.Add(mcRTB);


        }

        //  Les evenements de la selction d'une wilaya tout comme la liste déroulante : ça renvoie ---> meteo du jour en cas de connexion --> prévision dans le cas ou il n'y a pas de 
        //  connexion ==> On na 48 evenement == pour chaque wilaya --> un evenement de selection 
        private void Adrar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root();
            wilaya1 = "adrar";
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Tamanrasset_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            wilaya1 = "tamanrasset";
            if (this.output_out.name != wilaya1) output_out.used = false;

            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Biskra_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "biskra";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Laghouat_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "laghouat";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Saida_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "saida";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Msila_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "msila";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Tebessa_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "tebessa";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Khenchela_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "khenchela";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Naama_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "naama";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Illizi_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "illizi";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Tindouf_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "tindouf";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Ghardaia_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "ghardaia";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Djelfa_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "djelfa";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Tiaret_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "tiaret";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Aindefla_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "ain-defla";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Alger_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "alger";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Batna_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "batna";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Aittimouchent_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "ain-temouchent";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Annaba_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "annaba";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Bejaia_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "bejaia";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Blida_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "blida";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Borj_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "bordj-bou-arreridj";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Constantine_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "constantine";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Bouira_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "bouira";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Boumerdes_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "boumerdes";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Chlef_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "chlef";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Eltaref_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "el-tarf";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Elbayedh_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "el-bayadh";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Jijel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "jijel";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Guelma_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "guelma";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Eloued_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "el-oued";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Mascara_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "mascara";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Medea_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "medea";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Mila_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "mila";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Mostaganem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "mostaganem";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Elbouaghi_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "oum-el-bouaghi";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Tipaza_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "tipaza";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Tissemsilt_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "tissemssilt";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Oran_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "oran";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Relizane_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "relizane";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Setif_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "setif";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Sba_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "sidi-bel-abbes";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Skikda_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "skikda";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Soukahras_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            wilaya1 = "souk-ahras";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Tlemcen_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            wilaya1 = "tlemcen";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Tizi_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            wilaya1 = "tizi-ouzou";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Ouargla_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            wilaya1 = "ouargla";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }
        private void Bechar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            wilaya1 = "bechar";
            if (this.output_out.name != wilaya1) output_out.used = false;
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya1, output_out);
                prv.Show();
                this.Close();
            }
        }

        /***********************************************************************************************************
         *          Les évenements des bouttons du menu : prevision, evolution , cntactez-nous ...... mise à jour 
         * *********************************************************************************************************/

        private void conta(object sender, RoutedEventArgs e)
        {
            // ça renvoie vers contactez-nous 
            StreamReader sr = new StreamReader(@"son.txt");
            string str = sr.ReadLine();
            sr.Close();
            if (str == "Activé") // en cas ou le son activé le click fait un sonn
            {
                MediaPlayer player = new MediaPlayer();
                player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
                player.Play();
            }
            Contact cont = new Contact(wilaya1, output_out);
            cont.Show();
            this.Close();
        }
        private void meteo(object sender, RoutedEventArgs e)
        {// ça renvoie vers météo du jour
            StreamReader sr = new StreamReader(@"son.txt");
            string str = sr.ReadLine();
            sr.Close();
            if (str == "Activé")
            {
                MediaPlayer player = new MediaPlayer();
                player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
                player.Play();
            }

            if (InfoJour.test_cnx() == 1)   // si connexion ==> météo du jour
            {
                meteo_jour nvv = new meteo_jour(wilaya1, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                WpfMessageBox.Show("Vous ne disposez pas de connexion internet!");

            }

        }
        private void prev(object sender, RoutedEventArgs e)
        {
            // ça renvoie vers prévision 
            this.Visibility = Visibility.Hidden;
            StreamReader sr = new StreamReader(@"son.txt");
            string str = sr.ReadLine();
            sr.Close();
            if (str == "Activé")
            {
                MediaPlayer player = new MediaPlayer();
                player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
                player.Play();
            }
            this.Visibility = Visibility.Hidden;
            prevision prv = new prevision(wilaya1, output_out);
            prv.Show();
            this.Close();
        }
        private void aprop(object sender, RoutedEventArgs e)
        {
            // ça renvoie vers contactez-nousà propos
            StreamReader sr = new StreamReader(@"son.txt");
            string str = sr.ReadLine();
            sr.Close();
            if (str == "Activé")
            {
                MediaPlayer player = new MediaPlayer();
                player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
                player.Play();
            }
            apropos ap = new apropos(wilaya1, output_out);
            ap.Show();
            this.Close();
        }
        private void evolut(object sender, RoutedEventArgs e)
        {
            // ça renvoie vers évolution 
            StreamReader sr = new StreamReader(@"son.txt");
            string str = sr.ReadLine();
            sr.Close();
            if (str == "Activé")
            {
                MediaPlayer player = new MediaPlayer();
                player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
                player.Play();
            }
            evolution ev = new evolution(wilaya1, output_out);
            ev.Show();
            this.Close();
        }
        private void generall(object sender, RoutedEventArgs e)
        {
            // ça renvoie vers général
            StreamReader sr = new StreamReader(@"son.txt");
            string str = sr.ReadLine();
            sr.Close();
            if (str == "Activé")
            {
                MediaPlayer player = new MediaPlayer();
                player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
                player.Play();
            }
            parametre cr = new parametre(wilaya1, output_out);
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
            mise_a_jour mise = new mise_a_jour(wilaya1, output_out);
            mise.Show();
            this.Close();
            //this.Close();
        }
        private void carte(object sender, RoutedEventArgs e)
        {
            // ça renvoi vers la carte 
            StreamReader sr = new StreamReader(@"son.txt");
            string str = sr.ReadLine();
            sr.Close();
            if (str == "Activé")
            {
                MediaPlayer player = new MediaPlayer();
                player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
                player.Play();
            }
            accueil ac = new accueil(wilaya1, output_out);
            ac.Show();
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
        // L'évent du boutton de la capture d'écan 
        private void Screen_Click(object sender, RoutedEventArgs e)
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
        /******************************************************************************
         *         Les raccourcis clavier 
         ******************************************************************************/
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            WpfMessageBox.Show("hghhgh" + e.Key.ToString());
            switch (e.Key)
            {
                case Key.A:
                    {
                        accueil ev = new accueil(wilaya1, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.M:
                    {
                        if (InfoJour.test_cnx() == 1)   // si connexion ==> météo du jour
                        {
                            meteo_jour nvv = new meteo_jour(wilaya1, output_out);
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
                        prevision ev = new prevision(wilaya1, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.E:
                    {
                        evolution ev = new evolution(wilaya1, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.T:
                    {
                        Contact ev = new Contact(wilaya1, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.R:
                    {
                        parametre ev = new parametre(wilaya1, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.O:
                    {
                        apropos ev = new apropos(wilaya1, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.D:
                    {
                        credit ev = new credit(wilaya1, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.Z:
                    {
                        mise_a_jour mise = new mise_a_jour(wilaya1, output_out);
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Directory.GetCurrentDirectory() + "\\html5up-overflow\\index.html");
        }


        private void Button_Click()
        {

        }
    }
}
