using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;


namespace Bulletin_impot
{
    /// <summary>
    /// Nom du fichier à utiliser
    /// </summary>


    public partial class bareme : Window
    {
        private string nomFichier = String.Empty;
        private string CheminCompletNomFichier = String.Empty;
        private string ExtensionFichier = String.Empty;

        public bareme()
        {
            InitializeComponent();
            if (editeur.Text.Equals(""))

                boutonEnregistrer.IsEnabled = false;
        }

        /// <summary>
        /// Ouvrir le fichier après la sélection par l'utilisateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public string LireNomFichier(string cheminFichier)
        {
            if (cheminFichier.Equals(""))
                return "";
            return System.IO.Path.GetFileName(cheminFichier);
        }
        public string LireExtensionFichier(string cheminFichier)
        {
            if (cheminFichier.Equals(""))
                return "";
            return System.IO.Path.GetExtension(cheminFichier);
        }
        private void BoutonOuvrir_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.FileName = "tab_base";
            dlg.InitialDirectory = "C:\\";
            dlg.Filter = "Text documents  files (*.txt)|*.txt";
            dlg.FilterIndex = 2;


            dlg.DefaultExt = ".txt";



            if (dlg.ShowDialog() == true)
            {

                try
                {
                    nomFichier = LireNomFichier(dlg.FileName);
                    if (!nomFichier.Equals(""))
                    {
                        Stream myStream = dlg.OpenFile();

                        if (myStream != null)
                        {
                            using (myStream)
                            {
                                using (StreamReader reader = new StreamReader(myStream, Encoding.UTF8))
                                {

                                    var value = reader.ReadToEnd();
                                    value = new System.Xml.Linq.XText(value).ToString();

                                    editeur.Text = value;
                                    ExtensionFichier = LireExtensionFichier(dlg.FileName);
                                    this.Title = "Editeur Fichier : " + dlg.FileName + " | " + ExtensionFichier;
                                    CheminCompletNomFichier = dlg.FileName;
                                    this.FontSize = 14;
                                    boutonEnregistrer.IsEnabled = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Le fichier n'existe pas.");
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Une erreur est survenue lors de l'ouverture du fichier : '" + ee.Message + "' ");
                }
            }

        }

        // TODO - Implémenter une méthode pour lire le nom du fichier 
        // Ajouter une méthode LireNomFichier 


        // Enregistrer les données à nouveau dans le fichier
        private void BoutonEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (!CheminCompletNomFichier.Equals(""))
            {
                try
                {


                    SaveFileDialog sel = new SaveFileDialog();
                    sel.FileName = CheminCompletNomFichier;
                    sel.DefaultExt = "txt";
                    sel.OverwritePrompt = true;
                    sel.ShowDialog();
                    string selec = sel.FileName;



                    if (!selec.Equals(""))
                    {
                        File.WriteAllText(selec, editeur.Text, Encoding.UTF8);
                        this.Title = "Editeur Fichier : " + CheminCompletNomFichier + "  ( " + ExtensionFichier + " )";
                        MessageBox.Show("Enregistrer avec succes ");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }



        private void Editeur_KeyDown(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

            if (!CheminCompletNomFichier.Equals(""))
                this.Title = "Editeur : " + CheminCompletNomFichier + " * ";
            else
                this.Title = "Editeur  : Nouveau Document * ";
            boutonEnregistrer.IsEnabled = true;

        }
    }
}
