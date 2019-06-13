<<<<<<< HEAD:Ecosistemas.API/Ecosistemas.Business/Interfaces/Dominio/IExameService.cs
﻿using Ecosistemas.Business.Entities.Dominio;
=======
﻿using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
>>>>>>> sprint_yl_25052019:Ecosistemas.API/Ecosistemas.Business/Interfaces/Klinikos/IExameService.cs
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