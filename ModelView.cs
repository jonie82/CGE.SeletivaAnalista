using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CGE.SeletivaAnalista
{
    public class AnalistaInscrito
    {
        public int idanalista { get; set; }
        public string nomeAnalista { get; set; }
        public List<AnalistaExperiencia> experiencias { get; set; }
        public int pontosExperienciaReais { get; set; }
        public int pontosExperienciaConsiderados { get; set; }
        public List<AnalistaCertificacao> certificacaos { get; set; }
        public int pontosCertificacaoReais { get; set; }
        public int pontosCertificacaoConsiderados { get; set; }
        public int pontosTotais
        {
            get
            {
                return pontosExperienciaConsiderados + pontosCertificacaoConsiderados;
            }
        }
    }

    public class AnalistaExperiencia
    {
        public int idexperiencia { get; set; }
        public int idvagaperfilbarema { get; set; }
        public int vagaperfilbaremaMax { get; set; }
        public int vagaperfilbaremaMin { get; set; }
        public string descricao { get; set; }
        public string empresa { get; set; }
        public string documento_link { get; set; }
        public int anosCompletos { get; set; }
        public DateTime dtInicio { get; set; }
        public DateTime dtFim { get; set; }
        public int pontuacao { get; set; }
        public string obs { get; set; }
        public string desc_vagaperfilbarema { get; set; }
        public int pontosPorAno_vagaperfilbarema { get; set; }
    }

    public class AnalistaCertificacao
    {
        public int iddocumento { get; set; }
        public string documento_link { get; set; }
        public string nome_desc { get; set; }
        public string tipo_documento { get; set; }
        public string obs { get; set; }
        public string desc_vagaperfilbarema { get; set; }
        public int pontosPorItem_vagaperfilbarema { get; set; }
    }
}