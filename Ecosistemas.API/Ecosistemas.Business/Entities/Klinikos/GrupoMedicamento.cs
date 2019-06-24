using Ecosistemas.Business.Entities.Dominio;
using Ecosistemas.Business.Entities.Klinikos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class GrupoMedicamento
    {

        [Key]
        public Guid GrupoMedicamentoId { get; set; }

        public virtual GrupoMedicamentoDetalhe GrupoMedicamentoDetalhe { get; set; }

        public Guid MedicamentoId { get; set; }

        public bool Ativo { get; set; }

    }
}