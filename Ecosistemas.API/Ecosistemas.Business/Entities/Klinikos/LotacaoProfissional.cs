using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class LotacaoProfissional
    {
        [Key]
        public Guid LotacaoProfissionalId { get; set; }

        public virtual PessoaProfissional Pessoa { get; set; }

        public Guid TipoProfissional { get; set; }

        [StringLength(10, ErrorMessage = "{0} Precisa ter no máximo 10")]
        [DataType(DataType.Text)]
        public string NumeroConselho { get; set; }

        [StringLength(2, ErrorMessage = "{0} Precisa ter no máximo 2")]
        [DataType(DataType.Text)]
        public string UfProfissional { get; set; }

        public Guid OrgaoEmissorProfissional { get; set; }

        public bool Coordenador { get; set; }

        public bool Ativo { get; set; } = true;


    }
}
