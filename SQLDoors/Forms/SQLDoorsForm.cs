using System;
using System.Collections.Generic;
using System.ComponentModel;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB.Architecture;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Form = System.Windows.Forms.Form;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SQLDoors.Forms
{
    public partial class SQLDoorsForm : System.Windows.Forms.Form
    {

        Document Doc;
        SQLDBConnect sqlConnection = new SQLDBConnect();

        public SQLDoorsForm(Document doc)
        {
            InitializeComponent();

            Doc = doc;
        }

        private void SQLDoorsForm_Load(object sender, EventArgs e)
        {
            sqlConnection.ConnectDB();
        }

        /// <summary>
        /// Method to export door values to SQL database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_save_Click(object sender, EventArgs e)
        {
            //Get all doors which aren't element types
            IList<Element> doors = new FilteredElementCollector(Doc).OfCategory(BuiltInCategory.OST_Doors)
                .WhereElementIsNotElementType().ToElements();
         
            //SQL query to save door data
            string setQuery = "INSERT INTO Doors(UniqueId, FamilyType, Mark, DoorFinish) " +
                "VALUES(@param1, @param2, @param3, @param4)";


            foreach (Element ele in doors)
            {
                //Get door finish value
                Parameter doorFinish = ele.get_Parameter(BuiltInParameter.DOOR_FINISH);
                string dFinish;

                if (doorFinish.HasValue == true)
                {
                    dFinish = doorFinish.AsString();
                }
                else
                {
                    dFinish = "";
                }
                 
                //Get door mark value
                Parameter doorMark = ele.LookupParameter("Mark");
                string dMark = doorMark.AsString();

                //Save door data using AddWithValue paramater
                using (SqlCommand command = sqlConnection.Query(setQuery))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@param1", ele.UniqueId);
                        command.Parameters.AddWithValue("@param2", ele.Name);
                        command.Parameters.AddWithValue("@param3", dMark);
                        command.Parameters.AddWithValue("@param4", dFinish);
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        TaskDialog.Show("SQL Insert Error", ex.ToString());
                    }
                }

            }

            TaskDialog.Show("Door Values Export", "Door values added");

        }

        /// <summary>
        /// Method to import Door values from SQL database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_import_Click(object sender, EventArgs e)
        {
            //SQL query to import door data
            string getQuery = "SELECT * FROM Doors";

            SqlCommand command = sqlConnection.Query(getQuery);
            SqlDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                string uID = reader["UniqueId"].ToString();
                string dFinish = reader["DoorFinish"].ToString();

                Element ele = Doc.GetElement(uID);
                Parameter doorFinish = ele.get_Parameter(BuiltInParameter.DOOR_FINISH);

                //Set door finish parameter
                using (Transaction t = new Transaction(Doc, "Set Data"))
                {
                    t.Start();

                    doorFinish.Set(dFinish);

                    t.Commit();
                }

            }

            reader.Close();

            TaskDialog.Show("Update Door Finish Values", "Door values updated");

            //Set the Dialog Result so that the calling Method resturns the correct result and the changes stick
            DialogResult = DialogResult.OK;
            //Close the form
            Close();

        }

        /// <summary>
        /// Method to create SQL table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_tableCreate_Click(object sender, EventArgs e)
        {
            //Check if Doors table exists 
            bool doesExist = TableExists("yourDatabase", "Doors");

            if (doesExist)
            {
                TaskDialog.Show("SQL Table Error", "SQL table already exists");
            }
            else
            {
                try
                {
                    //SQL query to create door table
                    string tableQuery = "CREATE TABLE Doors" +
                       "(UniqueId varchar(255) NOT NULL PRIMARY KEY, FamilyType varchar(255)," +
                       "Mark varchar(255), DoorFinish varchar(255))";

                    SqlCommand command = sqlConnection.Query(tableQuery);
                    command.ExecuteNonQuery();

                    TaskDialog.Show("Create SQL Table", "Doors table created");
                }
                catch (Exception ex)
                {
                    TaskDialog.Show("SQL Error", ex.ToString());
                }

            }
  
        }

        /// <summary>
        /// Check if Doors table exists in database
        /// </summary>
        /// <param name="database"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool TableExists(string database, string name)
        {
            try
            {       
                //SQL query to check if doors table exists
                string existsQuery = "select case when exists((select * FROM [" + database + "].sys.tables " +
                    "WHERE name = '" + name + "')) then 1 else 0 end";

                SqlCommand command = sqlConnection.Query(existsQuery);

                //If value is 1 table exists if 0 , table doesnt exist
                return (int)command.ExecuteScalar() == 1;
            }
            catch(Exception err) 
            { 

                TaskDialog.Show("Error", err.ToString());
                return true;
            }
        }
    }
}
