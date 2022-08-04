using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using System.Windows.Forms;

namespace SQLDoors
{
    /// <summary>
    /// Implements the Revit add-in interface IExternalCommand
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            //Get the Current Session / Project from Revit
            UIApplication uiapp = commandData.Application;

            //Get the Current Document from the Current Session
            Document doc = uiapp.ActiveUIDocument.Document;

            //Create a new instance of the ViewScheduleCopyForm form and pass the current document as a variable
            using (System.Windows.Forms.Form form = new SQLDoors.Forms.SQLDoorsForm(doc))
            {
                //Checks to see if the DialogResult of the form is OK and resturn the correct result as needed.
                if (form.ShowDialog() == DialogResult.OK)
                {
                    //Let Revit know it executed successfully. This is also how you can roll back the entire feature.
                    return Result.Succeeded;
                }
                else
                {
                    //Let Revit know the Execute method did not finish successfully. All modifications to the Document will be rolled back based on the TransactionMode
                    return Result.Cancelled;
                }
            }
        }

    }
}
