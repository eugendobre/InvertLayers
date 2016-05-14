using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;

namespace InvertLayers
{
    public class Class1
    {

        [CommandMethod("InvertLayers")]
        public void InvertLayers()
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acDB = acDoc.Database;
            try
            {
                using (Transaction acTrans = acDB.TransactionManager.StartTransaction())
                {
                    LayerTable acLayTbl = (LayerTable)acTrans.GetObject(acDB.LayerTableId, OpenMode.ForRead);

                    foreach (ObjectId objLayer in acLayTbl)
                    {
                        LayerTableRecord acLayTblRec = (LayerTableRecord)acTrans.GetObject(objLayer, OpenMode.ForWrite);
                        //string LayerName= acLayTblRec.Name;
                        if(acLayTblRec.IsOff)
                        {
                            acLayTblRec.IsOff = false;
                        }
                        else
                        {
                            acLayTblRec.IsOff = true;
                        }
                    }
                    acTrans.Commit();
                }
            }
            catch (System.Exception ex)
            {
                Application.ShowAlertDialog(ex.Message);

            }
        }
    }
}
