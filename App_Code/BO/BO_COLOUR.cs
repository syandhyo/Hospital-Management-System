using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BO_COLOUR
/// </summary>
public class BO_COLOUR
{
	public BO_COLOUR()
	{
        COLOR_ID = null;
        COLOR_Name = null;
	}
    public long? COLOR_ID { get; set; }
    public string COLOR_Name { get; set; }
}