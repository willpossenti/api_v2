
﻿using Ecosistemas.Business.Entities.Dominio;
﻿using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Dominio
{
    public interface IMedicamentoService : IBaseService<Medicamento>
    {
        Task<CustomResponse<List<Medicamento>>> ConsultaMedicamento(string nome, Guid userId);
    }
}