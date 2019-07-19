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
    public class FilaAtendimentoEventoService : BaseService<FilaAtendimentoEvento>, IFilaAtendimentoEventoService
    {

        private readonly KlinikosDbContext _contextKlinikos;
        private readonly DominioDbContext _contextDominio;

        public FilaAtendimentoEventoService(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _contextDominio = contextDominio;
        }

        public async Task<CustomResponse<FilaAtendimentoEvento>> ConsultarRegistrosNovos(FilaAtendimentoEvento filaAtendimentoEvento, Guid userId)
        {

            var _response = new CustomResponse<FilaAtendimentoEvento>();

            try
            {
                await Task.Run(() =>
                {
                    var _codigoNovoRegistro = _contextDominio.Eventos.Where(x => x.Sigla == "A").FirstOrDefault();

                    Expression<Func<FilaAtendimentoEvento, bool>> _filtrofila;


                    if (filaAtendimentoEvento.FilaAtendimento != null)
                        _filtrofila = x =>
                        x.FilaAtendimento.FilaAtendimentoId != filaAtendimentoEvento.FilaAtendimento.FilaAtendimentoId &&
                        x.DataFilaAtendimentoEvento > filaAtendimentoEvento.FilaAtendimento.DataEntradaFilaAtendimento &&
                        x.EventoId == _codigoNovoRegistro.EventoId && x.Ativo && x.FilaAtendimento.Ativo;

                    else
                        _filtrofila = x => x.EventoId == _codigoNovoRegistro.EventoId && x.FilaAtendimento.Ativo;


                    var _novoRegistroEncontrado = _contextKlinikos.FilaAtendimentoEvento.Include(fila => fila.FilaAtendimento).Where(_filtrofila).ToList().FirstOrDefault();

                    if (_novoRegistroEncontrado != null)
                    {

                        var _novoRegistroFila = _contextKlinikos.FilaAtendimento.Include(fila => fila.ClassificacaoRisco).ThenInclude(pessoa => pessoa.PessoaPaciente)
                                    .Where(x => x.FilaAtendimentoId == _novoRegistroEncontrado.FilaAtendimento.FilaAtendimentoId).FirstOrDefault();

                        var evento = new FilaAtendimentoEvento { FilaAtendimento = _novoRegistroFila };

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
        public async Task<CustomResponse<FilaAtendimentoEvento>> ConsultarRegistrosRetirados(FilaAtendimentoEvento filaAtendimentoEvento, Guid userId)
        {

            var _response = new CustomResponse<FilaAtendimentoEvento>();

            try
            {
                await Task.Run(() =>
                {

                    var _codigoRemoverRegistro = _contextDominio.Eventos.Where(x => x.Sigla == "R").FirstOrDefault();

                    Expression<Func<FilaAtendimentoEvento, bool>> _filtrofila = x =>
                    x.FilaAtendimento.DataEntradaFilaAtendimento >= filaAtendimentoEvento.DataFilaAtendimentoEvento && x.EventoId == _codigoRemoverRegistro.EventoId &&
                    !x.FilaAtendimento.Ativo;


                    var _removerRegistroEncontrado = _contextKlinikos.FilaAtendimentoEvento.Include(fila => fila.FilaAtendimento).Where(_filtrofila).ToList().FirstOrDefault();

                    if (_removerRegistroEncontrado != null)
                    {

                        var _novoRegistroFila = _contextKlinikos.FilaAtendimento.Include(fila => fila.ClassificacaoRisco).ThenInclude(pessoa => pessoa.PessoaPaciente)
                                    .Where(x => x.FilaAtendimentoId == _removerRegistroEncontrado.FilaAtendimento.FilaAtendimentoId).FirstOrDefault();

                        var evento = new FilaAtendimentoEvento { FilaAtendimento = _novoRegistroFila, DataFilaAtendimentoEvento = _removerRegistroEncontrado.DataFilaAtendimentoEvento };

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

        public async Task<CustomResponse<FilaAtendimentoEvento>> ConsultarRegistrosChamadosAoPainel(FilaAtendimentoEvento filaAtendimentoEvento, Guid userId)
        {

            var _response = new CustomResponse<FilaAtendimentoEvento>();

            try
            {
                await Task.Run(() =>
                {
                    var _codigoNovoRegistro = _contextDominio.Eventos.Where(x => x.Sigla == "P").FirstOrDefault();

                    Expression<Func<FilaAtendimentoEvento, bool>> _filtrofila;


                    if (filaAtendimentoEvento.FilaAtendimentoEventoId != Guid.Empty)
                        _filtrofila = x =>
                        x.FilaAtendimentoEventoId != filaAtendimentoEvento.FilaAtendimentoEventoId &&
                        x.DataFilaAtendimentoEvento > filaAtendimentoEvento.DataFilaAtendimentoEvento &&
                        x.EventoId == _codigoNovoRegistro.EventoId && x.Ativo;

                    else
                        _filtrofila = x => x.EventoId == _codigoNovoRegistro.EventoId && x.FilaAtendimento.Ativo;


                    var _novoRegistroEncontrado = _contextKlinikos.FilaAtendimentoEvento.Include(fila => fila.FilaAtendimento).ThenInclude(classificacaorisco => classificacaorisco.ClassificacaoRisco)
                                                    .ThenInclude(pessoaprofissional => pessoaprofissional.PessoaProfissional).Where(_filtrofila).ToList().OrderByDescending(x => x.DataFilaAtendimentoEvento).FirstOrDefault();

                    if (_novoRegistroEncontrado != null)
                    {

                        var _novoRegistroFila = _contextKlinikos.FilaAtendimento.Include(fila => fila.ClassificacaoRisco).ThenInclude(pessoa => pessoa.PessoaPaciente)
                                    .Where(x => x.FilaAtendimentoId == _novoRegistroEncontrado.FilaAtendimento.FilaAtendimentoId).FirstOrDefault();

                        var evento = new FilaAtendimentoEvento
                        {
                            FilaAtendimento = _novoRegistroFila,
                            DataFilaAtendimentoEvento = _novoRegistroEncontrado.DataFilaAtendimentoEvento,
                            FilaAtendimentoEventoId = _novoRegistroEncontrado.FilaAtendimentoEventoId
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

        public async Task<CustomResponse<FilaAtendimentoEvento>> ConsultarRegistrosCancelados(FilaAtendimentoEvento filaAtendimentoEvento, Guid userId)
        {

            var _response = new CustomResponse<FilaAtendimentoEvento>();

            try
            {
                await Task.Run(() =>
                {
                    var _codigoNovoRegistro = _contextDominio.Eventos.Where(x => x.Sigla == "C").FirstOrDefault();

                    Expression<Func<FilaAtendimentoEvento, bool>> _filtrofila;


                    if (filaAtendimentoEvento.FilaAtendimentoEventoId != Guid.Empty)
                        _filtrofila = x =>
                        x.FilaAtendimentoEventoId != filaAtendimentoEvento.FilaAtendimentoEventoId &&
                        x.DataFilaAtendimentoEvento > filaAtendimentoEvento.DataFilaAtendimentoEvento &&
                        x.EventoId == _codigoNovoRegistro.EventoId && x.Ativo;

                    else
                        _filtrofila = x => x.EventoId == _codigoNovoRegistro.EventoId && x.FilaAtendimento.Ativo;


                    var _novoRegistroEncontrado = _contextKlinikos.FilaAtendimentoEvento.Include(fila => fila.FilaAtendimento).ThenInclude(classificacaorisco => classificacaorisco.ClassificacaoRisco)
                                                    .ThenInclude(pessoaprofissional => pessoaprofissional.PessoaProfissional).Where(_filtrofila).ToList().OrderByDescending(x => x.DataFilaAtendimentoEvento).FirstOrDefault();

                    if (_novoRegistroEncontrado != null)
                    {

                        var _novoRegistroFila = _contextKlinikos.FilaAtendimento.Include(fila => fila.ClassificacaoRisco).ThenInclude(pessoa => pessoa.PessoaPaciente)
                                    .Where(x => x.FilaAtendimentoId == _novoRegistroEncontrado.FilaAtendimento.FilaAtendimentoId).FirstOrDefault();

                        var evento = new FilaAtendimentoEvento
                        {
                            FilaAtendimento = _novoRegistroFila,
                            DataFilaAtendimentoEvento = _novoRegistroEncontrado.DataFilaAtendimentoEvento,
                            FilaAtendimentoEventoId = _novoRegistroEncontrado.FilaAtendimentoEventoId
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

        public async Task<CustomResponse<FilaAtendimentoEvento>> ConsultarRegistrosConfirmados(FilaAtendimentoEvento filaAtendimentoEvento, Guid userId)
        {

            var _response = new CustomResponse<FilaAtendimentoEvento>();

            try
            {
                await Task.Run(() =>
                {
                    var _codigoNovoRegistro = _contextDominio.Eventos.Where(x => x.Sigla == "O").FirstOrDefault();

                    Expression<Func<FilaAtendimentoEvento, bool>> _filtrofila;


                    if (filaAtendimentoEvento.FilaAtendimentoEventoId != Guid.Empty)
                        _filtrofila = x =>
                        x.FilaAtendimentoEventoId != filaAtendimentoEvento.FilaAtendimentoEventoId &&
                        x.DataFilaAtendimentoEvento > filaAtendimentoEvento.DataFilaAtendimentoEvento &&
                        x.EventoId == _codigoNovoRegistro.EventoId && x.Ativo;

                    else
                        _filtrofila = x => x.EventoId == _codigoNovoRegistro.EventoId && x.FilaAtendimento.Ativo;


                    var _novoRegistroEncontrado = _contextKlinikos.FilaAtendimentoEvento.Include(fila => fila.FilaAtendimento).ThenInclude(classificacaorisco => classificacaorisco.ClassificacaoRisco)
                                                    .ThenInclude(pessoaprofissional => pessoaprofissional.PessoaProfissional).Where(_filtrofila).ToList().OrderByDescending(x => x.DataFilaAtendimentoEvento).FirstOrDefault();

                    if (_novoRegistroEncontrado != null)
                    {

                        var _novoRegistroFila = _contextKlinikos.FilaAtendimento.Include(fila => fila.ClassificacaoRisco).ThenInclude(pessoa => pessoa.PessoaPaciente)
                                    .Where(x => x.FilaAtendimentoId == _novoRegistroEncontrado.FilaAtendimento.FilaAtendimentoId).FirstOrDefault();

                        var evento = new FilaAtendimentoEvento
                        {
                            FilaAtendimento = _novoRegistroFila,
                            DataFilaAtendimentoEvento = _novoRegistroEncontrado.DataFilaAtendimentoEvento,
                            FilaAtendimentoEventoId = _novoRegistroEncontrado.FilaAtendimentoEventoId
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