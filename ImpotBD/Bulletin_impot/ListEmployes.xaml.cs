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
using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;
using System.IO;

namespace Bulletin_impot
{
    /// <summary>
    /// Logique d'interaction pour ListEmployes.xaml
    /// </summary>
    public partial class ListEmployes : Window
    {
        private MySqlDataAdapter adaptateur;
        DataTable dt = new DataTable();
        double bruteFiscalAnnuel = 0;
        double abbatement = 0;
        double bruteFiscalApresAbbat = 0;
        double nbrePart = 0;
        double Impot = 0;
        double resulat = 0;
        double reduction = 0;
        double salaireNet = 0;
        NumberFormatInfo f = new NumberFormatInfo { NumberGroupSeparator = " " };

        public ListEmployes()
        {
            InitializeComponent();
        }

        private void But_modif_Empl_Click(object sender, RoutedEventArgs e)
        {

        }

        private void But_Supp_Emp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                System.Data.DataRowView selectedFile = (System.Data.DataRowView)listEmpl_Datagrid.SelectedItems[0];
                //Info sur label
                lab_nom.Content = "Nom : " + Convert.ToString(selectedFile.Row.ItemArray[1]);
                lab_prenom.Content = "Prenom : " + Convert.ToString(selectedFile.Row.ItemArray[2]);
                lab_matricule.Content = "matricule : " + Convert.ToString(selectedFile.Row.ItemArray[3]);
                lab_SalaireBrute.Content = "Salaire Brut : " + Convert.ToString(selectedFile.Row.ItemArray[4]) + " F cfa";
                lab_nbreJours.Content = "Jours : " + Convert.ToString(selectedFile.Row.ItemArray[5]);
                lab_Conjoint.Content = "Conjoint : " + Convert.ToString(selectedFile.Row.ItemArray[6]);
                lab_NbreEnfants.Content = "Enfants : " + Convert.ToString(selectedFile.Row.ItemArray[7]);

                //Autres info
                string nom = Convert.ToString(selectedFile.Row.ItemArray[1]);
                string prenom = Convert.ToString(selectedFile.Row.ItemArray[2]);
                string matricule = Convert.ToString(selectedFile.Row.ItemArray[3]);
                double salaireBrute = Convert.ToDouble(selectedFile.Row.ItemArray[44]);
                int nbreJours = Convert.ToInt32(selectedFile.Row.ItemArray[5]);
                int conjoint = Convert.ToInt32(selectedFile.Row.ItemArray[6]);
                int nbreEnfants = Convert.ToInt32(selectedFile.Row.ItemArray[7]);
                Calcul(nom, prenom, salaireBrute, nbreJours, conjoint, nbreEnfants);
            }
            catch(Exception exc)
            {
                MessageBox.Show("Choix non valide");
            }
            
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            string strConn = "database=impot; server=localhost; user id = root; pwd=";
            MySqlConnection connexion = new MySqlConnection(strConn);
            
            try
            {
                connexion.Open();
                
                Console.WriteLine("Connexion avec succès");
                //Après connexion,lecture BD
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM employes", connexion);
                //cmd.Parameters.AddWithValue(@nom, texbox1.Text)
                adaptateur = new MySqlDataAdapter(cmd);
                adaptateur.Fill(dt);

                //listEmpl_Datagrid.DataContext = dt;
                listEmpl_Datagrid.ItemsSource = dt.DefaultView;

               

                //MySqlDataReader reader = cmd.ExecuteReader();
                

                ///while(reader.Read())
                ///{
                ///    String nom = reader.GetString(1);
                 ///   lab_test.Content = nom;
                    
                ///}
                ///listEmpl_Datagrid.ItemsSource = reader;
                ///cmd.ExecuteNonQuery();


                


            }
            catch (MySqlException co)
            {

                MessageBox.Show(co.ToString());
                MessageBox.Show("Non connecté, verifiez que la base de donnée est allumée!");
            }
            

        }

        /// <summary>
        /// Retourne le minimum entre deux valeur x et y
        /// </summary>
        /// <param name="x">fonction calcul part</param>
        /// <param name="y">toujours égale à 5</param>
        /// <returns>le minimum</returns>
        public double Min(double x, double y)
        {
            if (x < y)
            {
                return x;
            }
            else
            {
                return y;
            }
        }

        public void Calcul(String nom, String prenom, double salaireBrute, int nbreJours, int conjoint, int nbreEnfants)
        {
            //Début
            string device = " F CFA";
            StreamReader sr = new StreamReader("tab_base.txt");
            sr.BaseStream.Position = 0;

            //Brute Fiscal Annuel




            //BruteFiscalAnnuel
            bruteFiscalAnnuel = salaireBrute * (365 / nbreJours);
                                Console.WriteLine("Brute fiscal annuel = " + bruteFiscalAnnuel);
                                lab_BrutFiscalAnnuel.Text = bruteFiscalAnnuel.ToString("n", f) + device;

                                //Abbatement
                                double abbatementLimit = 900000;
                                abbatement = bruteFiscalAnnuel * 0.3;
                                abbatement = (abbatement < abbatementLimit) ? abbatement : abbatementLimit;
                                Console.WriteLine("Abbatement" + abbatement.ToString("#.#", CultureInfo.InvariantCulture));
                                //lab_abbatement.Text = Convert.ToString(abbatement) + device;
                                lab_abbatement.Text = abbatement.ToString("n", f) + device;
                                //lab_abbatement.Text = abbatement.ToString("#,#", CultureInfo.InvariantCulture) + device;

                                //Brute Fiscal Après Abbatement
                                bruteFiscalApresAbbat = bruteFiscalAnnuel - abbatement;
                                Console.WriteLine("Brute Fiscal Après abbatement" + bruteFiscalApresAbbat);
                                lab_BruteFiscalApresAbat.Text = bruteFiscalApresAbbat.ToString("n", f) + device;

                                //Nombre de part
                                nbrePart = Min(((nbreEnfants + conjoint) * 0.5 + 1), 5);
                                Console.WriteLine("Nombre de part =" + nbrePart);
                                lab_nbrePart.Text = Convert.ToString(nbrePart);

                                //IRPP avant réduction
                                int i, j;
                                double[,] myArray = new double[6, 3];

                                for (int x = 0; x != 5; x++)
                                {
                                    string decalage = sr.ReadLine();
                                }
                                try
                                {

                                    string[] ligne = new string[6];
                                    for (int x = 0; x != 6; x++)
                                    {
                                        Console.WriteLine("Position = " + sr.BaseStream.Position.ToString());
                                        ligne[x] = sr.ReadLine();
                                        string[] tokens = ligne[x].Split(';');
                                        for (int y = 0; y != 3; y++)
                                        {

                                            myArray[x, y] = double.Parse(tokens[y]);
                                            Console.WriteLine("My array : " + myArray[x, y]);
                                        }
                                    }


                                }
                                catch (Exception exc)
                                {
                                    MessageBox.Show("Erreur Lecture fichier");
                                    MessageBox.Show(exc.Message);

                                }
                                Console.WriteLine("tab[2][3]" + myArray[1, 2]);


                                for (i = 5; i > 0; i--)
                                {
                                    if (bruteFiscalApresAbbat > myArray[i, 0] && bruteFiscalApresAbbat < myArray[i, 1])
                                    {
                                        resulat = 0;
                                        resulat += (bruteFiscalApresAbbat - myArray[i - 1, 1]) * myArray[i, 2];
                                        for (j = i - 1; j > 0; j--)
                                        {
                                            resulat += (myArray[j, 1] - myArray[j - 1, 1]) * myArray[j, 2];

                                        }
                                        break;
                                    }
                                    if (bruteFiscalApresAbbat > myArray[5, 1])
                                    {
                                        resulat = 0;
                                        resulat += (bruteFiscalApresAbbat - myArray[i - 1, 1]) * myArray[i, 2];
                                        for (j = 5; j > 0; j--)
                                        {
                                            resulat += (myArray[j, 1] - myArray[j - 1, 1]) * myArray[j, 2];

                                        }
                                        break;

                                    }


                                }

                                Console.WriteLine("Resultat ou irpp= " + resulat);
                                lab_IrppAvantReduction.Text = resulat.ToString("n", f) + device;


            //Reduction
            double[,] tabReduct = new double[9, 4];
            for (int x = 0; x != 2; x++)
            {
                string decalage = sr.ReadLine();
            }


            try
            {

                string[] ligne = new string[9];
                for (int x = 0; x != 9; x++)
                {
                    ligne[x] = sr.ReadLine();
                    string[] tokens = ligne[x].Split(';');
                    for (int y = 0; y != 4; y++)
                    {

                        tabReduct[x, y] = double.Parse(tokens[y]);
                        Console.WriteLine("My tabReduct : " + tabReduct[x, y]);
                    }
                }


            }
            catch (Exception exc)
            {
                MessageBox.Show("Erreur Lecture fichier");
                MessageBox.Show(exc.Message);

            }
            for (i = 5; i > 0; i--)
                                {

                                    if (nbrePart == tabReduct[i, 0])
                                    {
                                        double val = tabReduct[i, 1] * resulat;
                                        if (val > tabReduct[i, 3])
                                        {
                                            reduction = tabReduct[i, 3];
                                        }
                                        else
                                        {
                                            if (val < tabReduct[i, 2])
                                            {
                                                reduction = tabReduct[i, 2];
                                            }
                                            else
                                            {
                                                reduction = val;
                                            }
                                        }


                                        Console.WriteLine("Reduction = " + reduction);
                                        lab_reduction.Text = reduction.ToString("n", f) + device;
                                        break;
                                    }
                                }

                                Impot = resulat - reduction;
                                Console.WriteLine("Impot = " + Impot);
                                lab_Impot.Text = Impot.ToString("n", f) + device;

                                //Salaire Net
                                salaireNet = bruteFiscalAnnuel - Impot;
                                lab_salaireNet.Text = "Salaire Net : " + salaireNet.ToString("n", f) + device;

                                //flag calculated
                                //calculated = 1;


                                //Enregistrement

                                //listemployer.Add(new employer(ch_nom.Text, ch_prenom.Text, retour, nbreJour, retour1, retour2));




                            
        }



    }
}
