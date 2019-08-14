using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BO_CAT
/// </summary>
public class BO_CAT
{
	public BO_CAT()
	{
        CAT_ID = null;
        CAT_NAME = null;
	}
    public long? CAT_ID { get; set; }
    public string CAT_NAME { get; set; }
}