using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CGE.SeletivaAnalista
{
    public partial class Default : System.Web.UI.Page
    {
        #region props

        bool OrdenarPontos
        {
            get
            {
                return ddlOrdenar.SelectedIndex == 1;
            }
        }

        #endregion

        #region methods

        void CarregarDados()
        {
            var _analistasa = BizBarema.GetResultadoBarema(OrdenarPontos, null);
            gvDados.DataSource = _analistasa;
            gvDados.DataBind();

            lblTotal.Text = _analistasa.Count.ToString();
        }

        #endregion methods

        #region events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarDados();
            }
        }

        protected void gvDados_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect($"Detalhes.aspx?id={gvDados.SelectedValue}");
        }

        protected void ddlOrdenar_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarDados();
        }

        #endregion events

    }
}