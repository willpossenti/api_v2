<<<<<<< HEAD:Ecosistemas.API/Ecosistemas.Business/Interfaces/Dominio/IMedicamentoService.cs
﻿using Ecosistemas.Business.Entities.Dominio;
=======
﻿using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
>>>>>>> sprint_yl_25052019:Ecosistemas.API/Ecosistemas.Business/Interfaces/Klinikos/IMedicamentoService.cs
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