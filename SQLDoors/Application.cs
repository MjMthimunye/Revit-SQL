using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Autodesk.Revit.UI;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.IO;


namespace SQLDoors
{
    /// <summary>
    /// Implements the Revit add-in interface IExternalApplication
    /// </summary> 
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class Application : IExternalApplication
    {
        /// <summary>
        /// Implements the OnShutdown event
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        /// <summary>
        /// Implements the OnStartup event
        /// 
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Result OnStartup(UIControlledApplication application)
        {
            RibbonPanel panel = RibbonPanel(application);
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

            if (panel.AddItem(new PushButtonData("SQLDoors", "SQLDoors", thisAssemblyPath, "SQLDoors.Command"))
                is PushButton button)
            {
                button.ToolTip = "Export door data to SQL database";

                Uri uri = new Uri(Path.Combine(Path.GetDirectoryName(thisAssemblyPath), "Resources", "jacobian.ico"));
                BitmapImage bitmap = new BitmapImage(uri);
                button.LargeImage = bitmap;

            }


            return Result.Succeeded;
        }

        /// <summary>
        /// Function creates Jacobian Dev tab and Jacobian ribbon panel
        /// </summary>
        /// <param name="a"></param>
        /// <returns name="ribbonPanel"> </returns>
        public RibbonPanel RibbonPanel(UIControlledApplication a)
        {
            string tab = "Jacobian Dev";
            RibbonPanel ribbonPanel = null;

            try
            {
                a.CreateRibbonTab(tab);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            try
            {
                a.CreateRibbonPanel(tab, "Jacobian");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }


            List<RibbonPanel> panels = a.GetRibbonPanels(tab);
            foreach (RibbonPanel p in panels.Where(p => p.Name == "Jacobian"))
            {
                ribbonPanel = p;
            }

            return ribbonPanel;

        }
    }
}
