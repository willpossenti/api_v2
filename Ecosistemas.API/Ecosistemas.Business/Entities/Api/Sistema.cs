﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Api
{
    public class Sistema
    {
        [Key]
        public Guid SistemaId { get; set; }

        [Required]
        public string Nome { get; set; }
        public bool Ativo { get; set; } = true;

        public virtual Unidade Unidade { get; set; }

        public List<SistemaUser> SistemasUser { get; set; }
    }
}
