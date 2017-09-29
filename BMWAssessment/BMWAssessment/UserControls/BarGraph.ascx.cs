using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMWAssessment.UserControls
{
    public partial class BarGraph : System.Web.UI.UserControl
    {
        private int _progress = 0;
        public int Progress {
            get
            {
                return _progress;
            }
            set
            {
                _progress = value;
                pnlSlider.Width = _progress * 2;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}