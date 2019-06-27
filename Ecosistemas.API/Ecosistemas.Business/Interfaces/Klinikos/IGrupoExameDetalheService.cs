using Ecosistemas.Business.Entities.Dominio;
using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Klinikos
{
    public interface IGrupoExameDetalheService : IBaseService<GrupoExameDetalhe>
    {
        //Task<CustomResponse<List<GrupoExameDetalhe>>> AdicionarCargaGrupoExameDetalhe(List<Exame> exame, GrupoExame grupo, Guid userId);
        CustomResponse<GrupoExameDetalhe> AdicionarCargaGrupoExameDetalhe(List<Exame> listaExame, GrupoExame grupo, Guid userId);
    }
}