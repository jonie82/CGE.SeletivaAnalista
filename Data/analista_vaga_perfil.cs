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
    
    public partial class analista_vaga_perfil
    {
        public analista_vaga_perfil()
        {
            this.analista_vaga_perfil_barema = new HashSet<analista_vaga_perfil_barema>();
        }
    
        public int id { get; set; }
        public int idvaga { get; set; }
        public int idperfil { get; set; }
        public int quantidade { get; set; }
        public Nullable<int> pleno { get; set; }
        public Nullable<int> senior { get; set; }
        public int processo_seletivo { get; set; }
    
        public virtual ICollection<analista_vaga_perfil_barema> analista_vaga_perfil_barema { get; set; }
    }
}
