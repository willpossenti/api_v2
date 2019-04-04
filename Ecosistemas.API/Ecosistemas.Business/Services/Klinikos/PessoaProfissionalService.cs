﻿using Ecosistemas.Business.Entities.Klinikos;
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

namespace Ecosistemas.Business.Services.Klinikos
{
    public class PessoaProfissionalService : BaseService<PessoaProfissional>, IPessoaProfissionalService
    {
        private readonly KlinikosDbContext _context;
        private IPessoaHistoricoService _servicePessoaHistorico;

        public PessoaProfissionalService(KlinikosDbContext context) : base(context)
        {
            _context = context;
            _servicePessoaHistorico = new PessoaHistoricoService(context);
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


                var _pessoaMaster = (PessoaProfissional)_context.Pessoas.Where(x => x.Master).FirstOrDefault();
                pessoaprofissional.Master = false;

                if (!string.IsNullOrWhiteSpace(pessoaprofissional.Login))
                {
                    var login = _context.Pessoas.Max(x => x.CodigoLogin);

                    if (login != null)
                    {
                        var novoCodigo = int.Parse(login);
                        novoCodigo++;
                        pessoaprofissional.CodigoLogin = novoCodigo.ToString("000000");
                    }
                    else
                        pessoaprofissional.CodigoLogin = "000001";
                }

                await base.Adicionar(pessoaprofissional, userId);
                await _servicePessoaHistorico.AdicionarHistoricoProfissional(pessoaprofissional, _pessoaMaster);
                pessoaprofissional.PessoaContatos = null;
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

                    var _pessoaEncontrado = Profissional
                    .Where(_filtroNome).ToList().FirstOrDefault();

                if (_pessoaEncontrado != null)
                {

                    var lotacoes = _context.LotacoesProfissional
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

                        if (_pessoaEncontrado.PessoaContatos != null)
                        {
                            var newListaContato = new List<PessoaContato>();

                            foreach (var contato in _pessoaEncontrado.PessoaContatos)
                            {
                                contato.Pessoa = null;
                                newListaContato.Add(contato);
                            }

                            _pessoaEncontrado.PessoaContatos = newListaContato;
                        }

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


        public async Task<CustomResponse<List<PessoaProfissional>>> ConsultaNome(string nome, Guid userId)
        {
            var _response = new CustomResponse<List<PessoaProfissional>>();

            try
            {
                Expression<Func<PessoaProfissional, bool>> _filtroNome = x => (x.NomeCompleto.StartsWith(nome) || x.NomeCompleto.Contains(nome) || x.NomeCompleto.EndsWith(nome)) && x.Ativo && !x.Master;


                await Task.Run(() =>
                {


                    var _pessoaEncontrado = Profissional.Where(_filtroNome).ToList();

                    //if (_pessoaEncontrado != null)
                    //{

                    //    var newListaContato = new List<PessoaContato>();

                    //    foreach (var contato in _pessoaEncontrado.PessoaContatos)
                    //    {
                    //        contato.Pessoa = null;
                    //        newListaContato.Add(contato);
                    //    }

                    //    _pessoaEncontrado.PessoaContatos = newListaContato;


                    //}

                    if (_pessoaEncontrado != null)
                    {
                        _response.Message = "Nome encontrado";
                        _response.StatusCode = StatusCodes.Status302Found;
                        _response.Result = _pessoaEncontrado.Take(5).ToList();
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


        protected internal IQueryable<PessoaProfissional> Profissional
        {


            get
            {
                return _context.PessoaProfissionais
                   .Include(pessoa => pessoa.Raca)
                   .Include(pessoa => pessoa.Etnia)
                   .Include(pessoa => pessoa.Justificativa)
                   .Include(pessoa => pessoa.Nacionalidade)
                   .Include(pessoa => pessoa.Naturalidade).ThenInclude(estado => estado.Estado)
                   .Include(pessoa => pessoa.OrgaoEmissor)
                   .Include(pessoa => pessoa.PessoaContatos)
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