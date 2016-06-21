using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/**
 * @author: Nick Rowlandson
 * @date: June 8 2016
 * @version: 0.0.2 - updated SetActivePage method to include new links.
 */
namespace COMP2007_Lesson6
{
    public partial class Navbar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            addActiveClass();
        }

        /**
         * This method sets the class 'active' to list items 
         * in navigation links
         * 
         * @private
         * @method addActiveClass
         * @return {string}
         */
        private string addActiveClass()
        {
            Object activeNode = (System.Web.UI.HtmlControls.HtmlGenericControl)FindControl(Page.Title.ToString().ToLower().Replace(" ", String.Empty));
            if (activeNode != null)
            {
                ((System.Web.UI.HtmlControls.HtmlGenericControl)activeNode).Attributes.Add("class", "active");
            }
            return (Page.Title.ToString().ToLower().Replace(" ", String.Empty));
        }   
    }
}