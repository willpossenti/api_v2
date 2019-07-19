using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Contexto.Klinikos;
using Ecosistemas.Business.Contexto.Dominio;
using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;


namespace Ecosistemas.Business.Services.Klinikos
{
    public class FilaRegistroEventoService : BaseService<FilaRegistroEvento>, IFilaRegistroEventoService
    {

        private readonly KlinikosDbContext _contextKlinikos;
        private readonly DominioDbContext _contextDominio;

        public FilaRegistroEventoService(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _contextDominio = contextDominio;
        }

        public async Task<CustomResponse<FilaRegistroEvento>> ConsultarRegistrosNovos(FilaRegistroEvento filaRegistroEvento, Guid userId)
        {

            var _response = new CustomResponse<FilaRegistroEvento>();

            try
            {
                await Task.Run(() =>
                {
                    var _codigoNovoRegistro = _contextDominio.Eventos.Where(x => x.Sigla == "A").FirstOrDefault();

                    Expression<Func<FilaRegistroEvento, bool>> _filtrofila;


                    if (filaRegistroEvento.FilaRegistro != null)
                        _filtrofila = x =>
                        x.FilaRegistro.FilaRegistroId != filaRegistroEvento.FilaRegistro.FilaRegistroId &&
                        x.DataFilaRegistroEvento > filaRegistroEvento.FilaRegistro.DataEntradaFilaRegistro &&
                        x.EventoId == _codigoNovoRegistro.EventoId && x.Ativo && x.FilaRegistro.Ativo;

                    else
                        _filtrofila = x => x.EventoId == _codigoNovoRegistro.EventoId && x.FilaRegistro.Ativo;


                    var _novoRegistroEncontrado = _contextKlinikos.FilaRegistroEvento.Include(fila => fila.FilaRegistro).Where(_filtrofila).ToList().FirstOrDefault();

                    if (_novoRegistroEncontrado != null)
                    {

                        var _novoRegistroFila = _contextKlinikos.FilaRegistro.Include(fila => fila.Acolhimento).ThenInclude(pessoa => pessoa.PessoaPaciente)
                                    .Where(x => x.FilaRegistroId == _novoRegistroEncontrado.FilaRegistro.FilaRegistroId).FirstOrDefault();

                        var evento = new FilaRegistroEvento { FilaRegistro = _novoRegistroFila };

                        _response.Message = "novo registro encontrado";
                        _response.StatusCode = StatusCodes.Status302Found;
                        _response.Result = evento;
                    }
                    else
                    {
                        _response.Message = "não encontrado";
                        _response.StatusCode = StatusCodes.Status404NotFound;

                    }



                });
            }
            catch (Exception ex)
            {

                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);

            }

            return _response;

        }
        public async Task<CustomResponse<FilaRegistroEvento>> ConsultarRegistrosRetirados(FilaRegistroEvento filaRegistroEvento, Guid userId)
        {

            var _response = new CustomResponse<FilaRegistroEvento>();

            try
            {
                await Task.Run(() =>
                {

                    var _codigoRemoverRegistro = _contextDominio.Eventos.Where(x => x.Sigla == "R").FirstOrDefault();

                    Expression<Func<FilaRegistroEvento, bool>> _filtrofila = x =>
                    x.FilaRegistro.DataEntradaFilaRegistro >= filaRegistroEvento.DataFilaRegistroEvento && x.EventoId == _codigoRemoverRegistro.EventoId &&
                    !x.FilaRegistro.Ativo;


                    var _removerRegistroEncontrado = _contextKlinikos.FilaRegistroEvento.Include(fila => fila.FilaRegistro).Where(_filtrofila).ToList().FirstOrDefault();

                    if (_removerRegistroEncontrado != null)
                    {

                        var _novoRegistroFila = _contextKlinikos.FilaRegistro.Include(fila => fila.Acolhimento).ThenInclude(pessoa => pessoa.PessoaPaciente)
                                    .Where(x => x.FilaRegistroId == _removerRegistroEncontrado.FilaRegistro.FilaRegistroId).FirstOrDefault();

                        var evento = new FilaRegistroEvento { FilaRegistro = _novoRegistroFila, DataFilaRegistroEvento = _removerRegistroEncontrado.DataFilaRegistroEvento };

                        _response.Message = "novo registro encontrado";
                        _response.StatusCode = StatusCodes.Status302Found;
                        _response.Result = evento;
                    }
                    else
                    {
                        _response.Message = "não encontrado";
                        _response.StatusCode = StatusCodes.Status404NotFound;

                    }

                });
            }
            catch (Exception ex)
            {

                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);

            }

            return _response;

        }

        public async Task<CustomResponse<FilaRegistroEvento>> ConsultarRegistrosChamadosAoPainel(FilaRegistroEvento filaRegistroEvento, Guid userId)
        {

            var _response = new CustomResponse<FilaRegistroEvento>();

            try
            {
                await Task.Run(() =>
                {
                    var _codigoNovoRegistro = _contextDominio.Eventos.Where(x => x.Sigla == "P").FirstOrDefault();

                    Expression<Func<FilaRegistroEvento, bool>> _filtrofila;


                    if (filaRegistroEvento.FilaRegistroEventosId != Guid.Empty)
                        _filtrofila = x =>
                        x.FilaRegistroEventosId != filaRegistroEvento.FilaRegistroEventosId &&
                        x.DataFilaRegistroEvento > filaRegistroEvento.DataFilaRegistroEvento &&
                        x.EventoId == _codigoNovoRegistro.EventoId && x.Ativo;

                    else
                        _filtrofila = x => x.EventoId == _codigoNovoRegistro.EventoId && x.FilaRegistro.Ativo;


                    var _novoRegistroEncontrado = _contextKlinikos.FilaRegistroEvento.Include(fila => fila.FilaRegistro).ThenInclude(acolhimento => acolhimento.Acolhimento)
                                                    .ThenInclude(pessoaprofissional => pessoaprofissional.PessoaProfissional).Where(_filtrofila).ToList().OrderByDescending(x => x.DataFilaRegistroEvento).FirstOrDefault();

                    if (_novoRegistroEncontrado != null)
                    {

                        var _novoRegistroFila = _contextKlinikos.FilaRegistro.Include(fila => fila.Acolhimento).ThenInclude(pessoa => pessoa.PessoaPaciente)
                                    .Where(x => x.FilaRegistroId == _novoRegistroEncontrado.FilaRegistro.FilaRegistroId).FirstOrDefault();

                        var evento = new FilaRegistroEvento { FilaRegistro = _novoRegistroFila, DataFilaRegistroEvento = _novoRegistroEncontrado.DataFilaRegistroEvento,
                            FilaRegistroEventosId = _novoRegistroEncontrado.FilaRegistroEventosId};

                        _response.Message = "novo registro encontrado";
                        _response.StatusCode = StatusCodes.Status302Found;
                        _response.Result = evento;
                    }
                    else
                    {
                        _response.Message = "não encontrado";
                        _response.StatusCode = StatusCodes.Status404NotFound;

                    }



                });
            }
            catch (Exception ex)
            {

                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);

            }

            return _response;
        }

        public async Task<CustomResponse<FilaRegistroEvento>> ConsultarRegistrosCancelados(FilaRegistroEvento filaRegistroEvento, Guid userId)
        {

            var _response = new CustomResponse<FilaRegistroEvento>();

            try
            {
                await Task.Run(() =>
                {
                    var _codigoNovoRegistro = _contextDominio.Eventos.Where(x => x.Sigla == "C").FirstOrDefault();

                    Expression<Func<FilaRegistroEvento, bool>> _filtrofila;


                    if (filaRegistroEvento.FilaRegistroEventosId != Guid.Empty)
                        _filtrofila = x =>
                        x.FilaRegistroEventosId != filaRegistroEvento.FilaRegistroEventosId &&
                        x.DataFilaRegistroEvento > filaRegistroEvento.DataFilaRegistroEvento &&
                        x.EventoId == _codigoNovoRegistro.EventoId && x.Ativo;

                    else
                        _filtrofila = x => x.EventoId == _codigoNovoRegistro.EventoId && x.FilaRegistro.Ativo;


                    var _novoRegistroEncontrado = _contextKlinikos.FilaRegistroEvento.Include(fila => fila.FilaRegistro).ThenInclude(acolhimento => acolhimento.Acolhimento)
                                                    .ThenInclude(pessoaprofissional => pessoaprofissional.PessoaProfissional).Where(_filtrofila).ToList().OrderByDescending(x => x.DataFilaRegistroEvento).FirstOrDefault();

                    if (_novoRegistroEncontrado != null)
                    {

                        var _novoRegistroFila = _contextKlinikos.FilaRegistro.Include(fila => fila.Acolhimento).ThenInclude(pessoa => pessoa.PessoaPaciente)
                                    .Where(x => x.FilaRegistroId == _novoRegistroEncontrado.FilaRegistro.FilaRegistroId).FirstOrDefault();

                        var evento = new FilaRegistroEvento
                        {
                            FilaRegistro = _novoRegistroFila,
                            DataFilaRegistroEvento = _novoRegistroEncontrado.DataFilaRegistroEvento,
                            FilaRegistroEventosId = _novoRegistroEncontrado.FilaRegistroEventosId
                        };

                        _response.Message = "novo registro encontrado";
                        _response.StatusCode = StatusCodes.Status302Found;
                        _response.Result = evento;
                    }
                    else
                    {
                        _response.Message = "não encontrado";
                        _response.StatusCode = StatusCodes.Status404NotFound;

                    }



                });
            }
            catch (Exception ex)
            {

                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);

            }

            return _response;
        }

        public async Task<CustomResponse<FilaRegistroEvento>> ConsultarRegistrosConfirmados(FilaRegistroEvento filaRegistroEvento, Guid userId)
        {

            var _response = new CustomResponse<FilaRegistroEvento>();

            try
            {
                await Task.Run(() =>
                {
                    var _codigoNovoRegistro = _contextDominio.Eventos.Where(x => x.Sigla == "O").FirstOrDefault();

                    Expression<Func<FilaRegistroEvento, bool>> _filtrofila;


                    if (filaRegistroEvento.FilaRegistroEventosId != Guid.Empty)
                        _filtrofila = x =>
                        x.FilaRegistroEventosId != filaRegistroEvento.FilaRegistroEventosId &&
                        x.DataFilaRegistroEvento > filaRegistroEvento.DataFilaRegistroEvento &&
                        x.EventoId == _codigoNovoRegistro.EventoId && x.Ativo;

                    else
                        _filtrofila = x => x.EventoId == _codigoNovoRegistro.EventoId && x.FilaRegistro.Ativo;


                    var _novoRegistroEncontrado = _contextKlinikos.FilaRegistroEvento.Include(fila => fila.FilaRegistro).ThenInclude(acolhimento => acolhimento.Acolhimento)
                                                    .ThenInclude(pessoaprofissional => pessoaprofissional.PessoaProfissional).Where(_filtrofila).ToList().OrderByDescending(x => x.DataFilaRegistroEvento).FirstOrDefault();

                    if (_novoRegistroEncontrado != null)
                    {

                        var _novoRegistroFila = _contextKlinikos.FilaRegistro.Include(fila => fila.Acolhimento).ThenInclude(pessoa => pessoa.PessoaPaciente)
                                    .Where(x => x.FilaRegistroId == _novoRegistroEncontrado.FilaRegistro.FilaRegistroId).FirstOrDefault();

                        var evento = new FilaRegistroEvento
                        {
                            FilaRegistro = _novoRegistroFila,
                            DataFilaRegistroEvento = _novoRegistroEncontrado.DataFilaRegistroEvento,
                            FilaRegistroEventosId = _novoRegistroEncontrado.FilaRegistroEventosId
                        };

                        _response.Message = "novo registro encontrado";
                        _response.StatusCode = StatusCodes.Status302Found;
                        _response.Result = evento;
                    }
                    else
                    {
                        _response.Message = "não encontrado";
                        _response.StatusCode = StatusCodes.Status404NotFound;

                    }



                });
            }
            catch (Exception ex)
            {

                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);

            }

            return _response;
        }

    }
}