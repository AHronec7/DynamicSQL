using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace DynamicSQL
{
    public partial class Form1 : Form
    {
        //establishing a connection

        const string connstring = @"server=DTPLAPTOP11;Database=Adventureworks2012; 
                                 Trusted_Connection=True";
        SqlConnection sqlconnection = new SqlConnection(connstring);



        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            SqlCommand sqlcmd = new SqlCommand("select * From sys.databases", sqlconnection);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();

            da.Fill(dt);



            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string DBName = dt.Rows[i][0].ToString();
                DBcombobox.Items.Add(DBName);
            }
        }



        private void DBcombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string connectDB = (@"Data Source=DTPLAPTOP11;Initial Catalog=" + DBcombobox.SelectedItem + ";Trusted_Connection=True");
            SqlConnection sql = new SqlConnection(connectDB);

            SqlCommand sqlcmd = new SqlCommand((@"select Table_Schema, Table_name FROM INFORMATION_SCHEMA.TABLES UNION select Specific_Schema , Specific_Name FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE='PROCEDURE'"), sql);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();

            sqlda.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string db = dt.Rows[i][0].ToString() + "." + dt.Rows[i][1].ToString();
                listBox1.Items.Add(db);

            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string connectdb = (@"Data Source=DTPLAPTOP11;Initial Catalog=" + listBox1.SelectedItem + ";Trusted_Connection=True");
            SqlConnection sqlconnnect = new SqlConnection(connectdb);

            SqlCommand sqlcmd = new SqlCommand((@"EXEC sp_depends @objectname= N '" + listBox1.SelectedItem + " ';"), sqlconnnect);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
            //commmand type stored procedure
            DataSet ds = new DataSet();
            sqlda.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listBox2.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listBox2.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }



        }
    }
}

     
    

