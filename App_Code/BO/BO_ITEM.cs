using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BO_ITEM
/// </summary>
public class BO_ITEM
{
	public BO_ITEM()
	{
        ASSET_ID = null;
        ASSET_NAME = null;
        UOM_ID = null;
        CAT_ID = null;
        SBCAT_ID = null;
        COLOR_ID = null;
        QNTY = null;
        PRICE = null;
        DEPRICT = null;
        QRCODE = null;
        BRANCH_ID = null;
        BRANCH_FY = null;
	}
    public long? ASSET_ID { get; set; }
    public string ASSET_NAME { get; set; }
    public long? UOM_ID { get; set; }
    public long? CAT_ID { get; set; }
    public long? SBCAT_ID { get; set; }
    public long? COLOR_ID { get; set; }
    public decimal? QNTY { get; set; }
    public decimal? PRICE { get; set; }
    public decimal? DEPRICT { get; set; }
    public string QRCODE { get; set; }
    public long? BRANCH_ID { get; set; }
    public long? BRANCH_FY { get; set; }
}