using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class PessoaPaciente: Pessoa
    {


        [StringLength(10, ErrorMessage = "{0} Precisa ter no máximo 10")]
        [DataType(DataType.Text)]
        public string NumeroProntuario { get; set; }


        [StringLength(500, ErrorMessage = "{0} Precisa ter no máximo 500")]
        [DataType(DataType.Text)]
        public string DescricaoNaoIdentificado { get; set; }

        public bool Recemnascido { get; set; }


    


    }
}
