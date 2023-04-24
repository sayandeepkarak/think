using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace think
{
    public class Validation
    {
        private bool isValid;

        public Validation() {
            this.isValid = true;
        }

        public bool isOk() {
            return this.isValid;
        }

        public void validateField(bool condition,WebControl input) {
            string color = condition ? "--border" : "--error-border";
            input.Style.Add("border", "1px solid var(" + color + ")");
            this.isValid &= condition;
        }

        public static void alertInJs(UpdatePanel panel, bool isOk, string msg)
        {
            string isError = isOk ? "true" : "false";
            ScriptManager.RegisterStartupScript(panel, panel.GetType(), "message", "showAlert(" + isError + ",'" + msg + "');", true);
        }
    }
}