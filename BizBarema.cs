using CGE.SeletivaAnalista.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CGE.SeletivaAnalista
{
    /// <summary>
    /// Classe reponsável por realizar os cálculos e processamentos das informações a serem exibidas
    /// como resultado final dos candidatos, considerando a tabela de Barema disponibilazada no edital
    /// </summary>
    public static class BizBarema
    {
        #region props
        private static int CertificacaoPontuacaoMax { get; set; }
        private static int CertificacaoPontuacaoTotal { get; set; }

        #endregion

        /// <summary>
        /// Retorna lista de informações e cálculos para serem exibidas na página principal desta aplicação
        /// </summary>
        /// <returns></returns>
        public static List<AnalistaInscrito> GetResultadoBarema(bool ordenarPontos, int? id_inscrito)
        {
            using (seletiva_analistas_002Entities dc = new seletiva_analistas_002Entities())
            {
                List<AnalistaInscrito> _analistas = new List<AnalistaInscrito>();

                List<analista_inscrito> _analistasInscritos = new List<analista_inscrito>();
                if (id_inscrito.HasValue)
                    _analistasInscritos = dc.analista_inscrito.Where(p=>p.id == id_inscrito).ToList();
                else
                    _analistasInscritos = dc.analista_inscrito.ToList();

                foreach (var item in _analistasInscritos)
                {
                    AnalistaInscrito _analista = new AnalistaInscrito();
                        _analista.idanalista = item.id;
                        _analista.nomeAnalista = item.nome;

                    //calcula os pontos das experiencias proficionais listadas
                    _analista.experiencias = GetExperiencias(dc, item.id);
                    _analista.pontosExperienciaReais = _analista.experiencias.Sum(p => p.pontuacao);
                    _analista.pontosExperienciaConsiderados = GetPontuacaoExperiencia(_analista.experiencias);

                    //calcula os pontos das certificacoes
                    _analista.certificacaos = GetCertificacaos(dc, item.id);
                    _analista.pontosCertificacaoConsiderados = CertificacaoPontuacaoTotal;

                    if (CertificacaoPontuacaoTotal > CertificacaoPontuacaoMax)
                        _analista.pontosCertificacaoReais = CertificacaoPontuacaoMax;
                    else
                        _analista.pontosCertificacaoReais = CertificacaoPontuacaoTotal;


                    _analistas.Add(_analista);
                }

                if (ordenarPontos)
                    return _analistas.OrderByDescending(p => p.pontosTotais).ThenBy(p => p.nomeAnalista).ToList();
                else
                    return _analistas.OrderBy(p => p.nomeAnalista).ToList();
            }
        }

        /// <summary>
        /// Retorna a pontuacao total das experiencias, considerando valores minimos e maximos permitidos
        /// </summary>
        /// <param name="_experiencias"></param>
        /// <returns></returns>
        private static int GetPontuacaoExperiencia(List<AnalistaExperiencia> _experiencias)
        {
            int total = 0;

            //lista de forma distinct as categorias de experiencia para o qual o candidato afirmou possuir
            List<int> idsPerfilBarem = _experiencias.Select(p => p.idvagaperfilbarema).Distinct().ToList();

            //para cada categoria listada acima faz-se o calculo considerado as pontuaçoes minimas e maximas
            foreach (var item in idsPerfilBarem)
            {
                var _perfilBarema = _experiencias.First(p => p.idvagaperfilbarema == item);

                int _pontosParaEsseID = _experiencias.Where(p => p.idvagaperfilbarema == item).Sum(p => p.pontuacao);

                if (_pontosParaEsseID > _perfilBarema.vagaperfilbaremaMax)
                    _pontosParaEsseID = _perfilBarema.vagaperfilbaremaMax;

                if (_pontosParaEsseID < _perfilBarema.vagaperfilbaremaMin)
                    _pontosParaEsseID = 0;

                total += _pontosParaEsseID;
            }

            return total;
        }

        /// <summary>
        /// Retorna todas as experiencias informadas pelo candidato e organiza o objeto com os dados
        /// necessários para o cálculo de barema posteriormente
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="idInscrito"></param>
        /// <returns></returns>
        private static List<AnalistaExperiencia> GetExperiencias(seletiva_analistas_002Entities dc, int idInscrito)
        {
            List<AnalistaExperiencia> _analistaExperiencias = new List<AnalistaExperiencia>();

            //inscricao
            analista_inscricao _inscricao = dc.analista_inscricao
                .FirstOrDefault(p => p.idinscrito == idInscrito);

            if (_inscricao == null)
                return _analistaExperiencias;

            //todas as experiencias cadastradas para a inscricao
            var _experiencias = dc.analista_experiencia_profissional
                .Where(p => p.id_inscricao == _inscricao.id_inscricao).ToList();

            //para cada experiência encontrada:
            foreach (var item in _experiencias)
            {
                AnalistaExperiencia _novaExperiencia = new AnalistaExperiencia()
                {
                    idexperiencia = item.id_experiencia_profissional,
                    descricao = item.atividades_desenvolvidas,
                    empresa = item.empresa,
                    dtInicio = item.perminencia_inicio,
                    dtFim = item.perminencia_final
                };

                //1- descobre o documento
                var _documento = dc.analista_inscrito_documento.Find(item.id_documento);
                _novaExperiencia.documento_link = _documento.link;

                //2- descobre perfilbarema
                var _vagaPerfilBarema = dc.analista_vaga_perfil_barema.Find(_documento.idvagaperfilbarema);
                if(_vagaPerfilBarema == null)
                {
                    _novaExperiencia.obs = "O campo idperfilbarema não estava preenchido";
                    return _analistaExperiencias;
                }

                //3- define valores de calculo baseado na tb vaga_perfil_barema
                _novaExperiencia.idvagaperfilbarema = _vagaPerfilBarema.id;
                _novaExperiencia.vagaperfilbaremaMax = _vagaPerfilBarema.maximo;
                _novaExperiencia.vagaperfilbaremaMin = _vagaPerfilBarema.minimo;
                _novaExperiencia.desc_vagaperfilbarema = _vagaPerfilBarema.descricao;

                //3- calcula a quant de anos inteiros
                int anosInteiros = item.perminencia_final.Year - item.perminencia_inicio.Year;
                if (item.perminencia_final.DayOfYear < item.perminencia_inicio.DayOfYear)
                    anosInteiros = anosInteiros - 1;

                _novaExperiencia.anosCompletos = anosInteiros;
                _novaExperiencia.pontuacao = _vagaPerfilBarema.pontos * anosInteiros;
                _novaExperiencia.pontosPorAno_vagaperfilbarema = _vagaPerfilBarema.pontos;

                _analistaExperiencias.Add(_novaExperiencia);
            }

            return _analistaExperiencias;
        }

        /// <summary>
        /// Retorna lista de certificacoes/titulos informados pelo candidato
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="idInscrito"></param>
        /// <returns></returns>
        private static List<AnalistaCertificacao> GetCertificacaos(seletiva_analistas_002Entities dc, int idInscrito)
        {
            List<AnalistaCertificacao> _certificacaoes = new List<AnalistaCertificacao>();

            //inscricao
            analista_inscricao _inscricao = dc.analista_inscricao
                .FirstOrDefault(p => p.idinscrito == idInscrito);

            if (_inscricao == null)
                return _certificacaoes;

            //retorna todos os documentos dos tipos certificacao e titulo
            var _documentos = dc.analista_inscrito_documento.Where(p =>
            p.id_inscricao == _inscricao.id_inscricao && p.idvagaperfilbarema.HasValue &&
            (p.tipo_documento == "1º CERTIFICADO" || p.tipo_documento == "2º CERTIFICADO" || p.tipo_documento == "ESP. DE F. ACADEMICA")).ToList();


            CertificacaoPontuacaoMax = 0;
            CertificacaoPontuacaoTotal = 0;

            foreach (var item in _documentos)
            {
                AnalistaCertificacao _novaCertificacao = new AnalistaCertificacao()
                {
                    iddocumento = item.id,
                    documento_link = item.link,
                    nome_desc = item.nome_desc,
                    tipo_documento = item.tipo_documento
                };

                //verifica se a inscricao requer
                if(_novaCertificacao.tipo_documento == "ESP. DE F. ACADEMICA")
                {
                    if (item.idvagaperfilbarema == 19) //para titulo, esse é o id.
                    {
                        var _vagaPerfilBarema = dc.analista_vaga_perfil_barema.Find(item.idvagaperfilbarema);
                        _novaCertificacao.desc_vagaperfilbarema = _vagaPerfilBarema.descricao;
                        _novaCertificacao.pontosPorItem_vagaperfilbarema = _vagaPerfilBarema.pontos;

                        CertificacaoPontuacaoTotal += _vagaPerfilBarema.pontos;

                        CertificacaoPontuacaoMax = _vagaPerfilBarema.maximo;
                    }
                    else
                        _novaCertificacao.obs = "O campo idperfilbarema não estava preenchido";
                }
                else
                {
                    if(item.idvagaperfilbarema != 0) //para certificacao nao pode ser 0
                    {
                        var _vagaPerfilBarema = dc.analista_vaga_perfil_barema.Find(item.idvagaperfilbarema);
                        _novaCertificacao.desc_vagaperfilbarema = _vagaPerfilBarema.descricao;
                        _novaCertificacao.pontosPorItem_vagaperfilbarema = _vagaPerfilBarema.pontos;

                        CertificacaoPontuacaoTotal += _vagaPerfilBarema.pontos;

                        CertificacaoPontuacaoMax = _vagaPerfilBarema.maximo;
                    }
                    else
                        _novaCertificacao.obs = "O campo idperfilbarema não estava preenchido";
                }

                _certificacaoes.Add(_novaCertificacao);
            }

            return _certificacaoes;
        }
    }
}