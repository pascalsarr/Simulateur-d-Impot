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
using System.Collections;
using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;
using System.IO;

namespace Bulletin_impot
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ArrayList listemployer = new ArrayList();
        double bruteFiscalAnnuel = 0;
        double abbatement = 0;
        double bruteFiscalApresAbbat = 0;
        double nbrePart = 0;
        double Impot = 0;
        double resulat = 0;
        double reduction = 0;
        double salaireNet = 0;
        int calculated = 0;
        bool existe = false;
        NumberFormatInfo f = new NumberFormatInfo { NumberGroupSeparator = " " };
        

        static string strConn = "database=impot; server=localhost; user id = root; pwd=";
        MySqlConnection connexion = new MySqlConnection(strConn);

        public MainWindow()
        {

            InitializeComponent();
            DateTime thisDay = DateTime.Now;
            dateAujourdhui.Content += thisDay.ToString();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (VerifChamp() == 1)
            {
                Calcul();               
            }

        }

        private int VerifChamp()
        {
            if (ch_nom.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir un Nom!");
                return 0;
            }
            else
            {
                if (ch_prenom.Text == string.Empty)
                {
                    MessageBox.Show("Veuillez saisir un prénom!");
                    return 0;
                }
                else
                {
                    return 1;
                }
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

        public void Calcul()
        {
            StreamReader sr = new StreamReader("tab_base.txt");
            //Début
            string device = " F CFA";
            calculated = 0;
            sr.BaseStream.Position = 0;





            //Brute Fiscal Annuel


            int nbreJour;
            if (!int.TryParse(nbrJour.Text, out nbreJour) || nbreJour <= 0 || nbreJour > 360)
            {
                MessageBox.Show("Veuillez saisir un nombre de jour valide!");
            }
            else
            {

                if (!double.TryParse(salaireBrut.Text, out double retour))
                {


                    MessageBox.Show("Veuillez saisir un salaire valide!");
                }
                else
                {
                    if (retour < (50000 * nbreJour / 30))
                    {
                        MessageBox.Show("Veuillez saisir un salaire qui respecte le SMIC (" + (50000 * nbreJour / 30) + ")");
                    }
                    else
                    {


                        if (!int.TryParse(conjoint.Text, out int retour1) || retour1 < 0 || retour1 > 1)
                        {
                            MessageBox.Show("Veuillez saisir un nombre de conjoint valide!");

                        }
                        else
                        {
                            if (!int.TryParse(nbreEnfant.Text, out int retour2) || retour2 < 0)
                            {
                                MessageBox.Show("Veuillez saisir un nombre d'enfant valide!");

                            }
                            else
                            {

                                //BruteFiscalAnnuel
                                bruteFiscalAnnuel = retour * (365 / nbreJour);
                                //Console.WriteLine("Brute fiscal annuel = "+ bruteFiscalAnnuel);
                                //MessageBox.Show("Brute fiscal annuel = "+ bruteFiscalAnnuel);
                                lab_BrutFiscalAnnuel.Text = bruteFiscalAnnuel.ToString("n", f) + device;

                                //Abbatement
                                double abbatementLimit = 900000;
                                abbatement = bruteFiscalAnnuel * 0.3;
                                abbatement = (abbatement < abbatementLimit) ? abbatement : abbatementLimit;
                               // MessageBox.Show("Abbatement" + abbatement.ToString("#.#", CultureInfo.InvariantCulture));
                                //lab_abbatement.Text = Convert.ToString(abbatement) + device;
                                lab_abbatement.Text = abbatement.ToString("n", f) + device;
                                //lab_abbatement.Text = abbatement.ToString("#,#", CultureInfo.InvariantCulture) + device;

                                //Brute Fiscal Après Abbatement
                                bruteFiscalApresAbbat = bruteFiscalAnnuel - abbatement;
                                Console.WriteLine("Brute Fiscal Après abbatement" + bruteFiscalApresAbbat);
                                lab_BruteFiscalApresAbat.Text = bruteFiscalApresAbbat.ToString("n", f) + device;

                                //Nombre de part
                                nbrePart = Min((((Int32.Parse(nbreEnfant.Text)) + (Int32.Parse(conjoint.Text))) * 0.5 + 1), 5);
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
                                    for (int x=0;x!=6;x++)
                                    {
                                        Console.WriteLine("Position = "+ sr.BaseStream.Position.ToString());
                                        ligne[x] = sr.ReadLine();
                                        string[] tokens = ligne[x].Split(';');
                                        for (int y=0; y!=3;y++)
                                        {
                                            
                                            myArray[x, y] = double.Parse(tokens[y]);
                                            Console.WriteLine("My array : " + myArray[x, y]);
                                        }
                                    }


                                }
                                catch(Exception exc)
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

                               // MessageBox.Show("Resultat ou irpp= " + resulat);
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


                                        Console.WriteLine("Réduction = " + reduction);
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
                                calculated = 1;


                                //Enregistrement

                                listemployer.Add(new employer(ch_nom.Text, ch_prenom.Text, ch_matricule.Text, retour, nbreJour, retour1, retour2));

                                sr.Close();


                            }
                        }
                    }
                }
            }
        }

        class employer
        {
            public string nom;
            public string prenom;
            public string matricule;
            public double salairebrute;
            public int nbreJour;
            public int conjoint;
            public int nbreEnfant;
           

            public employer(string nom, string prenom, string matricule, double sal, int nbreJour, int conjoint, int nbreEnfant)
            {

                this.nom = nom;
                this.prenom = prenom;
                this.matricule = matricule;
                this.salairebrute = sal;
                this.nbreJour = nbreJour;
                this.nbreEnfant = nbreEnfant;
                this.conjoint = conjoint;

            }

            public string toString()
            {
                string chaine = "Nom : " + nom + "\t Prénom : " + prenom + "Matricule : " +matricule+ "\t Salaire Brute : " + salairebrute + "\t Jour : " + nbreJour + "\t conjoint : " + conjoint + "\t Nombre Enfant: " + nbreEnfant + "\n";
                return chaine;
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var v = MessageBox.Show("Voulez-vous vraiment annuler les données saisies ?", "Confirmer", button: MessageBoxButton.YesNo);
            if (v == MessageBoxResult.Yes) { 
                ch_nom.Text = "";
            ch_prenom.Text = "";
            ch_matricule.Text = "";
            salaireBrut.Text = "";
            nbrJour.Text = "";
            conjoint.Text = "";
            nbreEnfant.Text = "";
            lab_salaireNet.Text = "Salaire net : ";
            lab_BrutFiscalAnnuel.Text = "";
            lab_abbatement.Text = "";
            lab_BruteFiscalApresAbat.Text = "";
            lab_nbrePart.Text = "";
            lab_IrppAvantReduction.Text = "";
            lab_reduction.Text = "";
            lab_Impot.Text = "";
            }


        }

        // Boutton Fermeture du programme
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var v = MessageBox.Show("Voulez-vous vraiment quitter l'application ?", "Confirmer", button: MessageBoxButton.YesNo);
            if (v == MessageBoxResult.Yes)
                App.Current.Windows[0].Close();
        }

        // Affichage liste des employés dans un box
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ListEmployes fm = new ListEmployes();
            fm.Show();

            
            string chaine = "";
            foreach (employer emp in listemployer)
            {
                chaine += emp.toString();



            }
            //MessageBox.Show(chaine);
        }

        private void but_Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            existe = false;
            if (VerifChamp() == 1)
            {
                Calcul();
            }
            if (calculated == 1)
            {

                //Verification existance Employé dans la BD
                string sql1 = "SELECT * FROM employes WHERE (Prenom = '" + ch_prenom.Text + "' AND Nom = '" + ch_nom.Text + "')";
                MySqlCommand cmd1 = new MySqlCommand(sql1, connexion);
                try
                {
                    connexion.Open();
                    MySqlDataReader reader = cmd1.ExecuteReader();
                    if (reader.HasRows)
                    {
                        existe = true;
                    }
                    connexion.Close();
                }
                catch (MySqlException co)
                {

                    MessageBox.Show(co.ToString());
                    MessageBox.Show("Désolé, ajout impossible");
                }
               

                //Enregistrement dans la BD (avec condition existance)
                if (existe==false)
                {
                    //Ecriture dans la BD
                    String sql = "INSERT into employes (Nom,Prenom,Matricule,SalaireBrut,NbreJours,Conjoint,NbreEnfant) " +
                    "VALUES ('" + ch_nom.Text + "','" + ch_prenom.Text + "','" + ch_matricule.Text + "','" + double.Parse(salaireBrut.Text) + "','" + int.Parse(nbrJour.Text) + "','" + int.Parse(conjoint.Text) + "','" + int.Parse(nbreEnfant.Text) + "')";
                    MySqlCommand cmd = new MySqlCommand(sql, connexion);

                    try
                    {
                        connexion.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Employé Ajouté !");
                        }
                        connexion.Close();
                    }
                    catch (MySqlException co)
                    {

                        MessageBox.Show(co.ToString());
                        MessageBox.Show("Désolé, erreur dans l'ajout ! Veuillez réessayer");
                    }
                }else
                {
                    MessageBox.Show("Cet employé existe déjà. Réessayez svp !");
                }
            
                    
            }
            

            
        }

        /*
        private void but_bareme_Click(object sender, RoutedEventArgs e)
        {
            bareme fm = new bareme();
            fm.Show();
        }*/
    }
}
