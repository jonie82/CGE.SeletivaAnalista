using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CGE.SeletivaAnalista
{
    public partial class Detalhes : System.Web.UI.Page
    {
        #region props

        int IDInscrito
        {
            get
            {
                return Convert.ToInt32(Request.QueryString["id"]);
            }
        }

        #endregion props

        #region methods

        void CarregarDados()
        {
            var Dados = BizBarema.GetResultadoBarema(false, IDInscrito).First();
            litNome.Text = Dados.nomeAnalista;

            rptExperiencia.DataSource = Dados.experiencias;
            rptExperiencia.DataBind();
            lblExperienciaTOTAL.Text = Dados.pontosExperienciaConsiderados.ToString();

            rptCertificacao.DataSource = Dados.certificacaos;
            rptCertificacao.DataBind();
            lblCertificacaoTOTAL.Text = Dados.pontosCertificacaoConsiderados.ToString();

            litTotal.Text = Dados.pontosTotais.ToString();
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

        #endregion events
    }
}