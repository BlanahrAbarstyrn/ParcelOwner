using acDbServ = Autodesk.AutoCAD.DatabaseServices;

public static class AutoCADFunctions
{
    /// <summary>
    /// Find the ObjectID of a Handle text
    /// </summary>
    public static acDbServ.ObjectId GetObjectIdFromHandleString(string handleString)
    {
        acDbServ.ObjectId returnValue = acDbServ.ObjectId.Null;

        try
        {
            using (acDbServ.Database db = acDbServ.HostApplicationServices.WorkingDatabase)
            {
                returnValue = db.GetObjectId(false, new acDbServ.Handle(long.Parse(handleString, System.Globalization.NumberStyles.AllowHexSpecifier)), 0);
                if (returnValue.IsErased == true)
                {
                    returnValue = acDbServ.ObjectId.Null;
                }
            }
        } catch (System.Exception) { }

        return returnValue;
    }
}