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
    public class FilaClassificacaoEventoService : BaseService<FilaClassificacaoEvento>, IFilaClassificacaoEventoService
    {

        private readonly KlinikosDbContext _contextKlinikos;
        private readonly DominioDbContext _contextDominio;

        public FilaClassificacaoEventoService(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _contextDominio = contextDominio;
        }


        public async Task<CustomResponse<FilaClassificacaoEvento>> ConsultarRegistrosNovos(FilaClassificacaoEvento filaClassificacaoEvento, Guid userId)
        {

            var _response = new CustomResponse<FilaClassificacaoEvento>();

            try
            {
                await Task.Run(() =>
                {
                    var _codigoNovoRegistro = _contextDominio.Eventos.Where(x => x.Sigla == "A").FirstOrDefault();

                    Expression<Func<FilaClassificacaoEvento, bool>> _filtrofila;


                    if (filaClassificacaoEvento.FilaClassificacao != null)
                        _filtrofila = x =>
                        x.FilaClassificacao.FilaClassificacaoId != filaClassificacaoEvento.FilaClassificacao.FilaClassificacaoId &&
                        x.DataFilaClassificacaoEvento > filaClassificacaoEvento.FilaClassificacao.DataEntradaFilaClassificacao &&
                        x.EventoId == _codigoNovoRegistro.EventoId && x.Ativo && x.FilaClassificacao.Ativo;

                    else
                        _filtrofila = x => x.EventoId == _codigoNovoRegistro.EventoId && x.FilaClassificacao.Ativo;


                    var _novoRegistroEncontrado = _contextKlinikos.FilaClassificacaoEvento.Include(fila => fila.FilaClassificacao).Where(_filtrofila).ToList().FirstOrDefault();

                    if (_novoRegistroEncontrado != null)
                    {

                        var _novoRegistroFila = _contextKlinikos.FilaClassificacao.Include(fila => fila.RegistroBoletim).ThenInclude(pessoa => pessoa.PessoaPaciente)
                                    .Where(x => x.FilaClassificacaoId == _novoRegistroEncontrado.FilaClassificacao.FilaClassificacaoId).FirstOrDefault();

                        var evento = new FilaClassificacaoEvento { FilaClassificacao = _novoRegistroFila };

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
        public async Task<CustomResponse<FilaClassificacaoEvento>> ConsultarRegistrosRetirados(FilaClassificacaoEvento filaClassificacaoEvento, Guid userId)
        {

            var _response = new CustomResponse<FilaClassificacaoEvento>();

            try
            {
                await Task.Run(() =>
                {

                    var _codigoRemoverRegistro = _contextDominio.Eventos.Where(x => x.Sigla == "R").FirstOrDefault();

                    Expression<Func<FilaClassificacaoEvento, bool>> _filtrofila = x =>
                    x.FilaClassificacao.DataEntradaFilaClassificacao >= filaClassificacaoEvento.DataFilaClassificacaoEvento && x.EventoId == _codigoRemoverRegistro.EventoId &&
                    !x.FilaClassificacao.Ativo;


                    var _removerRegistroEncontrado = _contextKlinikos.FilaClassificacaoEvento.Include(fila => fila.FilaClassificacao).Where(_filtrofila).ToList().FirstOrDefault();

                    if (_removerRegistroEncontrado != null)
                    {

                        var _novoRegistroFila = _contextKlinikos.FilaClassificacao.Include(fila => fila.RegistroBoletim).ThenInclude(pessoa => pessoa.PessoaPaciente)
                                    .Where(x => x.FilaClassificacaoId == _removerRegistroEncontrado.FilaClassificacao.FilaClassificacaoId).FirstOrDefault();

                        var evento = new FilaClassificacaoEvento { FilaClassificacao = _novoRegistroFila, DataFilaClassificacaoEvento = _removerRegistroEncontrado.DataFilaClassificacaoEvento };

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

        public async Task<CustomResponse<FilaClassificacaoEvento>> ConsultarRegistrosChamadosAoPainel(FilaClassificacaoEvento filaClassificacaoEvento, Guid userId)
        {

            var _response = new CustomResponse<FilaClassificacaoEvento>();

            try
            {
                await Task.Run(() =>
                {
                    var _codigoNovoRegistro = _contextDominio.Eventos.Where(x => x.Sigla == "P").FirstOrDefault();

                    Expression<Func<FilaClassificacaoEvento, bool>> _filtrofila;


                    if (filaClassificacaoEvento.FilaClassificacaoEventoId != Guid.Empty)
                        _filtrofila = x =>
                        x.FilaClassificacaoEventoId != filaClassificacaoEvento.FilaClassificacaoEventoId &&
                        x.DataFilaClassificacaoEvento > filaClassificacaoEvento.DataFilaClassificacaoEvento &&
                        x.EventoId == _codigoNovoRegistro.EventoId && x.Ativo;

                    else
                        _filtrofila = x => x.EventoId == _codigoNovoRegistro.EventoId && x.FilaClassificacao.Ativo;


                    var _novoRegistroEncontrado = _contextKlinikos.FilaClassificacaoEvento.Include(fila => fila.FilaClassificacao).ThenInclude(registro => registro.RegistroBoletim)
                                                    .ThenInclude(pessoaprofissional => pessoaprofissional.PessoaProfissional).Where(_filtrofila).ToList().OrderByDescending(x => x.DataFilaClassificacaoEvento).FirstOrDefault();

                    if (_novoRegistroEncontrado != null)
                    {

                        var _novoRegistroFila = _contextKlinikos.FilaClassificacao.Include(fila => fila.RegistroBoletim).ThenInclude(pessoa => pessoa.PessoaPaciente)
                                    .Where(x => x.FilaClassificacaoId == _novoRegistroEncontrado.FilaClassificacao.FilaClassificacaoId).FirstOrDefault();

                        var evento = new FilaClassificacaoEvento
                        {
                            FilaClassificacao = _novoRegistroFila,
                            DataFilaClassificacaoEvento = _novoRegistroEncontrado.DataFilaClassificacaoEvento,
                            FilaClassificacaoEventoId = _novoRegistroEncontrado.FilaClassificacaoEventoId
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

        public async Task<CustomResponse<FilaClassificacaoEvento>> ConsultarRegistrosCancelados(FilaClassificacaoEvento filaClassificacaoEvento, Guid userId)
        {

            var _response = new CustomResponse<FilaClassificacaoEvento>();

            try
            {
                await Task.Run(() =>
                {
                    var _codigoNovoRegistro = _contextDominio.Eventos.Where(x => x.Sigla == "C").FirstOrDefault();

                    Expression<Func<FilaClassificacaoEvento, bool>> _filtrofila;


                    if (filaClassificacaoEvento.FilaClassificacaoEventoId != Guid.Empty)
                        _filtrofila = x =>
                        x.FilaClassificacaoEventoId != filaClassificacaoEvento.FilaClassificacaoEventoId &&
                        x.DataFilaClassificacaoEvento > filaClassificacaoEvento.DataFilaClassificacaoEvento &&
                        x.EventoId == _codigoNovoRegistro.EventoId && x.Ativo;

                    else
                        _filtrofila = x => x.EventoId == _codigoNovoRegistro.EventoId && x.FilaClassificacao.Ativo;


                    var _novoRegistroEncontrado = _contextKlinikos.FilaClassificacaoEvento.Include(fila => fila.FilaClassificacao).ThenInclude(registro => registro.RegistroBoletim)
                                                    .ThenInclude(pessoaprofissional => pessoaprofissional.PessoaProfissional).Where(_filtrofila).ToList().OrderByDescending(x => x.DataFilaClassificacaoEvento).FirstOrDefault();

                    if (_novoRegistroEncontrado != null)
                    {

                        var _novoRegistroFila = _contextKlinikos.FilaClassificacao.Include(fila => fila.RegistroBoletim).ThenInclude(pessoa => pessoa.PessoaPaciente)
                                    .Where(x => x.FilaClassificacaoId == _novoRegistroEncontrado.FilaClassificacao.FilaClassificacaoId).FirstOrDefault();

                        var evento = new FilaClassificacaoEvento
                        {
                            FilaClassificacao = _novoRegistroFila,
                            DataFilaClassificacaoEvento = _novoRegistroEncontrado.DataFilaClassificacaoEvento,
                            FilaClassificacaoEventoId = _novoRegistroEncontrado.FilaClassificacaoEventoId
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

        public async Task<CustomResponse<FilaClassificacaoEvento>> ConsultarRegistrosConfirmados(FilaClassificacaoEvento filaClassificacaoEvento, Guid userId)
        {

            var _response = new CustomResponse<FilaClassificacaoEvento>();

            try
            {
                await Task.Run(() =>
                {
                    var _codigoNovoRegistro = _contextDominio.Eventos.Where(x => x.Sigla == "O").FirstOrDefault();

                    Expression<Func<FilaClassificacaoEvento, bool>> _filtrofila;


                    if (filaClassificacaoEvento.FilaClassificacaoEventoId != Guid.Empty)
                        _filtrofila = x =>
                        x.FilaClassificacaoEventoId != filaClassificacaoEvento.FilaClassificacaoEventoId &&
                        x.DataFilaClassificacaoEvento > filaClassificacaoEvento.DataFilaClassificacaoEvento &&
                        x.EventoId == _codigoNovoRegistro.EventoId && x.Ativo;

                    else
                        _filtrofila = x => x.EventoId == _codigoNovoRegistro.EventoId && x.FilaClassificacao.Ativo;


                    var _novoRegistroEncontrado = _contextKlinikos.FilaClassificacaoEvento.Include(fila => fila.FilaClassificacao).ThenInclude(registro => registro.RegistroBoletim)
                                                    .ThenInclude(pessoaprofissional => pessoaprofissional.PessoaProfissional).Where(_filtrofila).ToList().OrderByDescending(x => x.DataFilaClassificacaoEvento).FirstOrDefault();

                    if (_novoRegistroEncontrado != null)
                    {

                        var _novoRegistroFila = _contextKlinikos.FilaClassificacao.Include(fila => fila.RegistroBoletim).ThenInclude(pessoa => pessoa.PessoaPaciente)
                                    .Where(x => x.FilaClassificacaoId == _novoRegistroEncontrado.FilaClassificacao.FilaClassificacaoId).FirstOrDefault();

                        var evento = new FilaClassificacaoEvento
                        {
                            FilaClassificacao = _novoRegistroFila,
                            DataFilaClassificacaoEvento = _novoRegistroEncontrado.DataFilaClassificacaoEvento,
                            FilaClassificacaoEventoId = _novoRegistroEncontrado.FilaClassificacaoEventoId
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