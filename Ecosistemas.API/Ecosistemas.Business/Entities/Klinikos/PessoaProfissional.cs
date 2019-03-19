using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class PessoaProfissional: Pessoa
    {
        public virtual TipoProfissional TipoProfissional { get; set; }

        [StringLength(10, ErrorMessage = "{0} Precisa ter no máximo 10")]
        [DataType(DataType.Text)]
        public string NumeroConselho { get; set; }

        [StringLength(2, ErrorMessage = "{0} Precisa ter no máximo 2")]
        [DataType(DataType.Text)]
        public string UfProfissional { get; set; }

        public virtual OrgaoEmissor OrgaoEmissorProfissional { get; set; }

        [StringLength(5, ErrorMessage = "{0} Precisa ter no máximo 5")]
        [DataType(DataType.Text)]
        public string CodigoLogin { get; set; }

        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string Login { get; set; }

    }
}
