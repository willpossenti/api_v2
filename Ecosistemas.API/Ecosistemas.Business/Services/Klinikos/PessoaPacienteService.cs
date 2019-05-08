using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Contexto.Klinikos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecosistemas.Business.Utility;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Ecosistemas.Business.Contexto.Api;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class PessoaPacienteService : BaseService<PessoaPaciente>, IPessoaPacienteService
    {
        private readonly KlinikosDbContext _contextKlinikos;
        private readonly ApiDbContext _context;
        private IPessoaHistoricoService _servicePessoaHistorico;

        public PessoaPacienteService(KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _context = context;
            _servicePessoaHistorico = new PessoaHistoricoService(contextKlinikos, context);
        }

        public async Task<CustomResponse<PessoaPaciente>> AdicionarPaciente(PessoaPaciente pessoaPaciente, Guid userId)
        {
            var _response = new CustomResponse<PessoaPaciente>();


            try
            {

                Expression<Func<PessoaPaciente, bool>> _filtroNome = x => x.Cpf.Contains(pessoaPaciente.Cpf) || x.Cns.Contains(pessoaPaciente.Cns) || x.PisPasep.Contains(pessoaPaciente.PisPasep);
                var _cadastroEncontrado = base.ObterByExpression(_filtroNome).Result.Result.Count;

                if (_cadastroEncontrado > 0)
                {
                    _response.StatusCode = StatusCodes.Status409Conflict;
                    return _response;
                }

                var _pessoaMaster = (PessoaProfissional)_contextKlinikos.Pessoas.Where(x => x.Master).FirstOrDefault();
                await base.Adicionar(pessoaPaciente, userId);
                await _servicePessoaHistorico.AdicionarHistoricoPaciente(pessoaPaciente, _pessoaMaster);
                _response.Result = pessoaPaciente;
                return _response;
            }
            catch (Exception ex)
            {

                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);

            }

            return _response;
        }

        public async Task<CustomResponse<PessoaPaciente>> AtualizarPaciente(PessoaPaciente pessoaPaciente, Guid userId)
        {
            var _response = new CustomResponse<PessoaPaciente>();


            try
            {


                var _pessoaMaster = (PessoaProfissional)_contextKlinikos.Pessoas.Where(x => x.Master).FirstOrDefault();
                await base.Atualizar(pessoaPaciente, userId);
                await _servicePessoaHistorico.AdicionarHistoricoPaciente(pessoaPaciente, _pessoaMaster);
                _response.Result = pessoaPaciente;
                return _response;
            }
            catch (Exception ex)
            {

                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);

            }

            return _response;
        }

        public async Task<CustomResponse<PessoaPaciente>> ConsultaCpf(string cpf, Guid userId)
        {
            var _response = new CustomResponse<PessoaPaciente>();

            try
            {
                Expression<Func<PessoaPaciente, bool>> _filtroNome = x => x.Cpf.Equals(cpf) && x.Ativo;


                await Task.Run(() =>
               {

                   var _pessoaEncontrado = Paciente.Where(_filtroNome).ToList().FirstOrDefault();


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
               });


            }
            catch (Exception ex)
            {
                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);
            }

            return _response;
        }

        public async Task<CustomResponse<PessoaPaciente>> ConsultaCns(string cns, Guid userId)
        {
            var _response = new CustomResponse<PessoaPaciente>();

            try
            {
                Expression<Func<PessoaPaciente, bool>> _filtroNome = x => x.Cns.Equals(cns) && x.Ativo;


                await Task.Run(() =>
                {


                    var _pessoaEncontrado = Paciente.Where(_filtroNome).ToList().FirstOrDefault();


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

        public async Task<CustomResponse<PessoaPaciente>> ConsultaPis(string pis, Guid userId)
        {
            var _response = new CustomResponse<PessoaPaciente>();

            try
            {
                Expression<Func<PessoaPaciente, bool>> _filtroNome = x => x.PisPasep.Equals(pis) && x.Ativo;


                await Task.Run(() =>
                {


                    var _pessoaEncontrado = Paciente.Where(_filtroNome).ToList().FirstOrDefault();


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

        public async Task<CustomResponse<List<PessoaPaciente>>> ConsultaNome(string nome, Guid userId)
        {
            var _response = new CustomResponse<List<PessoaPaciente>>();

            try
            {
                Expression<Func<PessoaPaciente, bool>> _filtroNome = x => (x.NomeCompleto.StartsWith(nome) || x.NomeCompleto.Contains(nome) || x.NomeCompleto.EndsWith(nome)) && x.Ativo;


                await Task.Run(() =>
                {

                    var _listaPacientes = Paciente.Where(_filtroNome).Take(5).ToList();

                    if (_listaPacientes != null)
                    {

                        _response.Message = "Nome encontrado";
                        _response.StatusCode = StatusCodes.Status302Found;
                        _response.Result = _listaPacientes;

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

        public async Task<CustomResponse<List<PessoaPaciente>>> ConsultaNomeSocial(string nomeSocial, Guid userId)
        {
            var _response = new CustomResponse<List<PessoaPaciente>>();

            try
            {
                Expression<Func<PessoaPaciente, bool>> _filtroNome = x => (x.NomeSocial.StartsWith(nomeSocial) || x.NomeSocial.Contains(nomeSocial) || x.NomeSocial.EndsWith(nomeSocial)) && x.Ativo;


                await Task.Run(() =>
                {

                    var _listaPacientes = Paciente.Where(_filtroNome).Take(5).ToList();

                    if (_listaPacientes != null)
                    {


                        _response.Message = "Nome social encontrado";
                        _response.StatusCode = StatusCodes.Status302Found;
                        _response.Result = _listaPacientes;

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

        protected internal IQueryable<PessoaPaciente> Paciente
        {


            get
            {
                return _contextKlinikos.PessoaPacientes
                   .Include(pessoa => pessoa.Raca)
                   .Include(pessoa => pessoa.Etnia)
                   .Include(pessoa => pessoa.Justificativa)
                   .Include(pessoa => pessoa.Nacionalidade)
                   .Include(pessoa => pessoa.Naturalidade).ThenInclude(estado => estado.Estado)
                   .Include(pessoa => pessoa.OrgaoEmissor)
                   .Include(pessoa => pessoa.Estado)
                   .Include(pessoa => pessoa.Cidade)
                   .Include(pessoa => pessoa.Ocupacao)
                   .Include(pessoa => pessoa.PaisOrigem)
                   .Include(pessoa => pessoa.TipoCertidao)
                   .Include(pessoa => pessoa.Escolaridade)
                   .Include(pessoa => pessoa.SituacaoFamiliarConjugal);
            }
        }

    }



}