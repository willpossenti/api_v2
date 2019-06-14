using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Contexto.Klinikos;
using System;
using System.Collections.Generic;
using System.Text;
using Ecosistemas.Business.Utility;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ecosistemas.Business.Contexto.Api;
using Ecosistemas.Business.Contexto.Dominio;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class PessoaProfissionalService : BaseService<PessoaProfissional>, IPessoaProfissionalService
    {
        private readonly KlinikosDbContext _contextKlinikos;
        private readonly IPessoaHistoricoService _servicePessoaHistorico;

        public PessoaProfissionalService(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _servicePessoaHistorico = new PessoaHistoricoService(contextDominio, contextKlinikos, context);

        }


        public async Task<CustomResponse<PessoaProfissional>> AdicionarProfissional(PessoaProfissional pessoaprofissional, Guid userId)
        {
            var _response = new CustomResponse<PessoaProfissional>();

            try
            {
                Expression<Func<PessoaProfissional, bool>> _filtroNome = x => x.Cpf.Contains(pessoaprofissional.Cpf) || x.Cns.Contains(pessoaprofissional.Cns) || x.TituloEleitor.Contains(pessoaprofissional.PisPasep);
                var _cadastroEncontrado = base.ObterByExpression(_filtroNome).Result.Result.Count;

                if (_cadastroEncontrado > 0)
                {
                    _response.StatusCode = StatusCodes.Status409Conflict;
                    return _response;
                }


                var _pessoaMaster = (PessoaProfissional)_contextKlinikos.Pessoas.Where(x => x.Master).FirstOrDefault();
                pessoaprofissional.Master = false;

                await base.Adicionar(pessoaprofissional, userId);
                await _servicePessoaHistorico.AdicionarHistoricoProfissional(pessoaprofissional, _pessoaMaster);
                pessoaprofissional.LotacoesProfissional = null;
                _response.Result = pessoaprofissional;
                return _response;
            }
            catch (Exception ex)
            {

                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);

            }

            return _response;
        }

        public async Task<CustomResponse<PessoaProfissional>> ConsultaCpf(string cpf, Guid userId)
        {
            var _response = new CustomResponse<PessoaProfissional>();

            try
            {
                Expression<Func<PessoaProfissional, bool>> _filtroNome = x => x.Cpf.Equals(cpf) && x.Ativo && !x.Master;


                await Task.Run(() =>
                {

                    var _pessoaEncontrado = _contextKlinikos.PessoaProfissionais
                    .Where(_filtroNome).ToList().FirstOrDefault();

                    if (_pessoaEncontrado != null)
                    {

                        var lotacoes = _contextKlinikos.LotacoesProfissional
                        .Include(profissional => profissional.TipoProfissional)
                        .Include(profissional => profissional.OrgaoEmissorProfissional)
                        .Where(pessoa => pessoa.Pessoa.PessoaId == _pessoaEncontrado.PessoaId);

                        var newListaLotacao = new List<LotacaoProfissional>();

                    foreach (var lotacao in lotacoes)
                    {
                        lotacao.Pessoa = null;
                        newListaLotacao.Add(lotacao);
                    }

                    _pessoaEncontrado.LotacoesProfissional = newListaLotacao;

                    if (_pessoaEncontrado != null)
                    {
                        _response.Message = "Cpf encontrado";
                        _response.StatusCode = StatusCodes.Status302Found;
                        _response.Result = _pessoaEncontrado;
                    }
                    else
                    {
                        _response.Message = "Cpf não encontrado";
                        _response.StatusCode = StatusCodes.Status404NotFound;

                    }
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

        public async Task<CustomResponse<PessoaProfissional>> ConsultaCns(string cns, Guid userId)
        {
            var _response = new CustomResponse<PessoaProfissional>();

            try
            {
                Expression<Func<PessoaProfissional, bool>> _filtroNome = x => x.Cns.Equals(cns) && x.Ativo;


                await Task.Run(() =>
                {


                    var _pessoaEncontrado = _contextKlinikos.PessoaProfissionais.Where(_filtroNome).ToList().FirstOrDefault();

                    if (_pessoaEncontrado != null)
                    {
                        _response.Message = "Cns encontrado";
                        _response.StatusCode = StatusCodes.Status302Found;
                        _response.Result = _pessoaEncontrado;
                    }
                    else
                    {
                        _response.Message = "Cns não encontrado";
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

        public async Task<CustomResponse<PessoaProfissional>> ConsultaPis(string pis, Guid userId)
        {
            var _response = new CustomResponse<PessoaProfissional>();

            try
            {
                Expression<Func<PessoaProfissional, bool>> _filtroNome = x => x.PisPasep.Equals(pis) && x.Ativo;


                await Task.Run(() =>
                {


                    var _pessoaEncontrado = _contextKlinikos.PessoaProfissionais.Where(_filtroNome).ToList().FirstOrDefault();


                    if (_pessoaEncontrado != null)
                    {
                        _response.Message = "Cns encontrado";
                        _response.StatusCode = StatusCodes.Status302Found;
                        _response.Result = _pessoaEncontrado;
                    }
                    else
                    {
                        _response.Message = "Cns não encontrado";
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

        public async Task<CustomResponse<List<PessoaProfissional>>> ConsultaNome(string nome, Guid userId)
        {
            var _response = new CustomResponse<List<PessoaProfissional>>();

            try
            {
                Expression<Func<PessoaProfissional, bool>> _filtroNome = x => (x.NomeCompleto.StartsWith(nome) || x.NomeCompleto.Contains(nome) || x.NomeCompleto.EndsWith(nome)) && x.Ativo && !x.Master;


                await Task.Run(() =>
                {


                    var _listaProfissionais = _contextKlinikos.PessoaProfissionais.Where(_filtroNome).Take(5).ToList();

                    if (_listaProfissionais != null)
                    {

                        _response.Message = "Nome encontrado";
                        _response.StatusCode = StatusCodes.Status302Found;
                        _response.Result = _listaProfissionais;

                    }
                    else
                    {
                        _response.Message = "Nome não encontrado";
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

        public async Task<CustomResponse<List<PessoaProfissional>>> ConsultaNomeSocial(string nomeSocial, Guid userId)
        {
            var _response = new CustomResponse<List<PessoaProfissional>>();

            try
            {
                Expression<Func<PessoaProfissional, bool>> _filtroNome = x => (x.NomeSocial.StartsWith(nomeSocial) || x.NomeSocial.Contains(nomeSocial) || x.NomeSocial.EndsWith(nomeSocial)) && x.Ativo && !x.Master;


                await Task.Run(() =>
                {


                    var _pessoaEncontrado = _contextKlinikos.PessoaProfissionais.Where(_filtroNome).ToList();

                    if (_pessoaEncontrado != null)
                    {
                        _response.Message = "Nome social encontrado";
                        _response.StatusCode = StatusCodes.Status302Found;
                        _response.Result = _pessoaEncontrado.Take(5).ToList();
                    }
                    else
                    {
                        _response.Message = "Nome social não encontrado";
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

        public async Task<CustomResponse<PessoaProfissional>> ConsultaProfissional(Guid userId)
        {
            var _response = new CustomResponse<PessoaProfissional>();

            try
            {
                Expression<Func<PessoaProfissional, bool>> _filtroUser = x => x.UserId.Equals(userId) && x.Ativo;
                //&& !x.Master;
                await Task.Run(() =>
                {


                    var _profissionalEncontrado = _contextKlinikos.PessoaProfissionais.Where(_filtroUser).ToList().FirstOrDefault();


                    if (_profissionalEncontrado != null)
                    {
                        _response.Message = "Profissional encontrado";
                        _response.StatusCode = StatusCodes.Status302Found;
                        _response.Result = _profissionalEncontrado;
                    }
                    else
                    {
                        _response.Message = "Profissional não encontrado";
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