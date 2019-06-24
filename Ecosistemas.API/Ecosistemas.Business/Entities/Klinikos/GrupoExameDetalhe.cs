using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class GrupoExameDetalhe
    {

        [Key]
        public Guid GrupoExameDetalheId { get; set; }

        public Guid GrupoExameId { get; set; }

        public Guid ExameId { get; set; }

        public bool Ativo { get; set; } = true;

    }
}