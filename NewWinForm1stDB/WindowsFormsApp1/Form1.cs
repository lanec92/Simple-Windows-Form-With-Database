using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frmTitles : Form
    {
        OleDbConnection conn; //access database connection object
        OleDbCommand titlesCommand; // command object
        OleDbDataAdapter titlesAdapter; // data adpater object
        DataTable titlesTable; // DataTable object - one table of in memory data
        CurrencyManager titlesManager; // manages list of binding objects
        public frmTitles()
        {
            InitializeComponent();
        }

        private void frmTitles_Load(object sender, EventArgs e)
        {
            var connString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\Lanec\Desktop\C# Udemy wDatabase\DB\Books.accdb;
                                    Persist Security Info = False;"; //connection string standard security OLEDB 12.0

            conn = new OleDbConnection(connString); //create new connection object - pass connection string(passes provider, data source, and security info into oledbconn object)
            conn.Open(); //opens connection
            titlesCommand = new OleDbCommand("Select * from Titles", conn);
            titlesAdapter = new OleDbDataAdapter();
            titlesAdapter.SelectCommand = titlesCommand;
            titlesTable = new DataTable();
            titlesAdapter.Fill(titlesTable);

            //bind controls
            txtTitle.DataBindings.Add("Text", titlesTable, "Title");
            txtYear.DataBindings.Add("Text", titlesTable, "Year_Published");
            txtISBN.DataBindings.Add("Text", titlesTable, "ISBN");
            txtPubID.DataBindings.Add("Text", titlesTable, "PubId");

            // establish currency manager
            titlesManager = (CurrencyManager) BindingContext[titlesTable]; //cast bound data to currency manager object



            conn.Close(); //closes connection
            conn.Dispose(); //removes from memory
            titlesAdapter.Dispose();
            titlesTable.Dispose();

        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            titlesManager.Position = 0; //On click first row data displays

        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            titlesManager.Position--; //goes to the previous data entry
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            titlesManager.Position++; //goes to the next data entry
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            titlesManager.Position = titlesManager.Count - 1; //goes tot the last data entry
        }
    }
}
