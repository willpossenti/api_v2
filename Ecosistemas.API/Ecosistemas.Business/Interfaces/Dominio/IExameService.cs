
﻿using Ecosistemas.Business.Entities.Dominio;
﻿using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Dominio
{
    public interface IExameService : IBaseService<Exame>
    {
        Task<CustomResponse<List<Exame>>> ConsultaExame(string nome, Guid userId);
    }
}