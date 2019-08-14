using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BO_UNIT
/// </summary>
public class BO_UNIT
{
	public BO_UNIT()
	{
        UOM_ID = null;
        UOM_Name = null;
	}
    public long? UOM_ID { get; set; }
    public string UOM_Name { get; set; }
}