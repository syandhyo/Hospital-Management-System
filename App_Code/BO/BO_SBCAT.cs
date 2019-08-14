using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BO_SBCAT
/// </summary>
public class BO_SBCAT
{
	public BO_SBCAT()
	{
		SBCAT_ID = null;
        CAT_ID = null;
        SBCAT_NAME = null;
	}
    public long? SBCAT_ID { get; set; }
    public long? CAT_ID { get; set; }
    public string SBCAT_NAME { get; set; }
}