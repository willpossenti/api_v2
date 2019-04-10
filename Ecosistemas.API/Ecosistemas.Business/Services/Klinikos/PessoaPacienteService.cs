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
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class PessoaPacienteService : BaseService<PessoaPaciente>, IPessoaPacienteService
    {
        private readonly KlinikosDbContext _context;
        private IPessoaHistoricoService _servicePessoaHistorico;

        public PessoaPacienteService(KlinikosDbContext context) : base(context)
        {
            _context = context;
            _servicePessoaHistorico = new PessoaHistoricoService(context);
        }

        public async Task<CustomResponse<PessoaPaciente>> AdicionarPaciente(PessoaPaciente pessoaPaciente, Guid userId)
        {
            var _response = new CustomResponse<PessoaPaciente>();


            try
            {


                var _pessoaMaster = (PessoaProfissional)_context.Pessoas.Where(x => x.Master).FirstOrDefault();
                await base.Adicionar(pessoaPaciente, userId);
                await _servicePessoaHistorico.AdicionarHistoricoPaciente(pessoaPaciente, _pessoaMaster);
                pessoaPaciente.PessoaContatos = null;
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

        public async Task<CustomResponse<PessoaPaciente>> ConsultaCpf(string cpf)
        {
            var _response = new CustomResponse<PessoaPaciente>();
            var somenteNumeros = string.Empty;
            Expression<Func<PessoaPaciente, bool>> ppaciente = null;

            if (cpf.Length > 14)
                _response.Message = "Informe um CPF válido";
            else
            {
                if (cpf.Length == 14)
                {
                    var primeiroponto = cpf.Substring(3, 1);
                    var segundoponto = cpf.Substring(7, 1);
                    var terceiroponto = cpf.Substring(11, 1);

                    if ((!primeiroponto.Equals(".")
                        || !segundoponto.Equals(".")
                        || !terceiroponto.Equals("-")))
                        _response.Message = "Informe um CPF válido";


                    somenteNumeros = cpf.Replace(".", "").Replace("-", "");

                    Int64 cpfvalidado; Int64.TryParse(somenteNumeros, out cpfvalidado);

                    if (cpfvalidado == 0)
                        _response.Message = "Informe apenas números";

                       ppaciente = x => x.Cpf.Equals(somenteNumeros);
                }
                else
                {

                    if (cpf.Length == 11)
                    {
                        Int64 cpfvalidado; Int64.TryParse(cpf, out cpfvalidado);

                        if (cpfvalidado == 0)
                            _response.Message = "Favor apenas números";

                        //validações
                        else if (string.IsNullOrEmpty(cpf))
                            _response.Message = "Favor informar o cpf";



                    }
                    else
                        _response.Message = "Informe um CPF válido";

                    ppaciente = x => x.Cpf.Equals(cpf);
                }
            }


            if (!string.IsNullOrEmpty(_response.Message))
                return _response;


            try
            {

                //var cpfvalidado = String.Join("", System.Text.RegularExpressions.Regex.Split(cpf, @"[^\d]"));
            
                var resultado = await base.ObterByExpression(ppaciente);

                if (resultado.Result.Count == 0)
                    _response.Message = "Paciente não encontrado";
                else
                {
                    _response.Message = "Sucesso";
                    _response.Result = resultado.Result.FirstOrDefault();
                }

                return _response;




                //if (!string.IsNullOrEmpty(cpf))
                //{
                //    try
                //    {
                //        if (cpf.Contains(".") || cpf.Contains("-") && cpf.Contains("[0-9]"))
                //        {
                //            var cpfvalidado = String.Join("", System.Text.RegularExpressions.Regex.Split(cpf, @"[^\d]"));
                //            Expression<Func<PessoaPaciente, bool>> ppaciente = x => x.Cpf.Contains(cpfvalidado);
                //            var resultado = await base.ObterByExpression(ppaciente);
                //            if (resultado.Result.Count == 0 || string.IsNullOrEmpty(cpf))
                //                _response.Message = "Paciente não encontrado";
                //            else
                //            {
                //                _response.Message = "Sucesso";
                //                _response.Result = resultado.Result.FirstOrDefault();
                //            }

                //            return _response;
                //        }

                //        else
                //        {
                //            Expression<Func<PessoaPaciente, bool>> ppaciente = x => x.Cpf.Contains(cpf);
                //        var resultado = await base.ObterByExpression(ppaciente);

                //        if (resultado.Result.Count == 0 || string.IsNullOrEmpty(cpf))
                //            _response.Message = "Paciente não encontrado";
                //        else
                //        {
                //            _response.Message = "Sucesso";
                //            _response.Result = resultado.Result.FirstOrDefault();
                //        }

                //        return _response;
                //    }
            }

            catch (Exception ex)
            {

                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);

            }


            return _response;


        }

        public async Task<CustomResponse<List<PessoaPaciente>>> ConsultarNome(string Nome)
        {
            var _response = new CustomResponse<List<PessoaPaciente>>();
           
            var newnome = Nome.ToUpper().Replace(" ","");
            try
            {
                Expression<Func<PessoaPaciente, bool>> ppaciente = x => x.NomeCompleto.Contains(newnome);
                var resultado = await base.ObterByExpression(ppaciente);
                if (resultado.Result.Count == 0 || string.IsNullOrEmpty(newnome))
                {
                    _response.Message = "Paciente não encontrado";
                }
                else
                {
                    _response.Message = "Sucesso";
                    _response.Result = resultado.Result.ToList();
                }
            }
            catch(Exception ex)
            {

                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);

            }
            return _response;

        }

        public async Task<CustomResponse<List<PessoaPaciente>>> ConsultarNomeComRaca(string Nome)
        {
            var _response = new CustomResponse<List<PessoaPaciente>>();
            var newraca = new List<Raca>();
            
            var newnome = Nome.ToUpper().Replace(" ", "");
            try
            {
                Expression<Func<PessoaPaciente, bool>> ppaciente = x => x.NomeCompleto.Contains(newnome);

                await Task.Run(() =>
                {
                    
                    var resultado = _context.PessoaPacientes.
                    Include(pessoa=>pessoa.Raca).
                    Include(pessoa=>pessoa.Etnia).
                    Where(ppaciente).ToList();
                   


                    if (resultado.Count == 0 || string.IsNullOrEmpty(newnome))
                    {
                        _response.Message = "Paciente não encontrado";
                    }
                    else
                    {
                        _response.Message = "Sucesso";
                        _response.Result = resultado;
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