﻿using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Klinikos
{
    public interface ICidadeService : IBaseService<Cidade>
    {
        Task<CustomResponse<IList<Cidade>>> GetByEstado(Guid estadoId);
        Task<CustomResponse<IList<Cidade>>> GetByName(string nomeCidade);
    }
}
