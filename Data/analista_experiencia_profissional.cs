//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CGE.SeletivaAnalista.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class analista_experiencia_profissional
    {
        public int id_experiencia_profissional { get; set; }
        public int id_inscricao { get; set; }
        public int id_documento { get; set; }
        public string empresa { get; set; }
        public string cargo { get; set; }
        public string atividades_desenvolvidas { get; set; }
        public System.DateTime perminencia_inicio { get; set; }
        public System.DateTime perminencia_final { get; set; }
        public string vinculo_trabalho { get; set; }
        public string outro_desc { get; set; }
        public string motivo_saida { get; set; }
    }
}
