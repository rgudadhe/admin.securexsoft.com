using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class control : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        for (int i = 0; i < 12; i++)
            this.CboMonth.Items[i].Text = new DateTime(1, i + 1, 1).ToString("MMMM"); //MMMM Month Format

        if (!Page.IsPostBack)
        {

            //Adding the value to Combobox. [1900- thisYear]				
            for (int iYear = DateTime.Now.Year - 2; iYear <= DateTime.Now.Year + 3; iYear++)
            {
                CboYear.Items.Add(new ListItem(iYear.ToString()));
                //System.Math.Min(System.Threading.Interlocked.Increment(ref iYear), iYear - 1);

            }
            refreshControls();
            //Array.Copy(arr, arr.GetLength(1)*4, arrB, 0, arr.GetLength(1));
        }
    }


    #region "Private Methods"
    private void returnToPage(string retDate)
    {
        //"<script>window.opener." + Request.QueryString("field") + ".value ='" + ReturnDate + "';" + " " + "window.close();</script>")
        Response.Write("<script>window.opener." + Request.QueryString["field"].ToString() + ".value='" + retDate + "';window.close();</script>");
        //Response.Write("<script>alert(window.opener." + Request.QueryString["field"].ToString() + ".value);</script>");
        //Console.WriteLine(Request.QueryString["field"].ToString());
        //Response.Write("<script>alert('1dd');</script>");
    }
    private void setCalendarDate()
    {
        int day;
        int year = Int32.Parse(CboYear.SelectedValue.ToString());
        int month = Int32.Parse(CboMonth.SelectedValue.ToString());
        day = CdrDatePicker.SelectedDate.Day;
        if (day > DateTime.DaysInMonth(year, month))
        {
            day = DateTime.DaysInMonth(year, month);
        }
        CdrDatePicker.VisibleDate = new DateTime(year, month, day);
    }
    private void refreshControls()
    {
        DateTime SelectedDate;
        if (!(Request.QueryString["ShowDate"] == null))
        {
            SelectedDate = System.Convert.ToDateTime(Request.QueryString["ShowDate"]);
            CboYear.SelectedIndex = SelectedDate.Year - 1900;
            CboMonth.SelectedIndex = SelectedDate.Month - 1;
            CdrDatePicker.VisibleDate = SelectedDate.Date;
        }
        else
        {
            CboYear.SelectedIndex = DateTime.Now.Year - 1900;
            CboMonth.SelectedIndex = DateTime.Now.Month - 1;
            CdrDatePicker.VisibleDate = DateTime.Now.Date;
        }
    }
    #endregion
    protected void hlSelectDate_Click(object sender, EventArgs e)
    {

        string ReturnDate = CdrDatePicker.SelectedDate.ToString("MM/dd/yyyy");
        returnToPage(ReturnDate);
    }
    protected void CdrDatePicker_SelectionChanged(object sender, EventArgs e)
    {
        string retDate = CdrDatePicker.SelectedDate.ToString("MM/dd/yyyy");
        returnToPage(retDate);
    }
    protected void CboMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        setCalendarDate();
    }
    protected void CboYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        setCalendarDate();
    }
}
