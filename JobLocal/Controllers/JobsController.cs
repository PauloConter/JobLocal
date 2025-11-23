using JobLocal.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace JobLocal.Controllers
{
    public class JobsController : Controller
    {
        private static List<Vaga> _vagas = new List<Vaga>
        {
            new Vaga { Id = 1, Titulo = "Limpeza Residencial Semanal", Descricao = "Preciso de diarista para limpeza completa da casa 1 vez por semana. Área de 80m², 2 quartos, 1 banheiro.", Cidade = "São Paulo", TipoServico = "Limpeza", Valor = 120.00m, UsuarioId = 101, DataPublicacao = DateTime.Now.AddDays(-2), Ativa = true },
            new Vaga { Id = 2, Titulo = "Jardinagem para Condomínio", Descricao = "Manutenção mensal do jardim do condomínio. Podar grama, regar plantas e cuidar das flores.", Cidade = "Rio de Janeiro", TipoServico = "Jardinagem", Valor = 350.00m, UsuarioId = 102, DataPublicacao = DateTime.Now.AddDays(-5), Ativa = true },
            new Vaga { Id = 3, Titulo = "Pintura de Apartamento", Descricao = "Pintura completa de apartamento de 2 quartos. Mão de obra experiente necessária. Forneço material.", Cidade = "Belo Horizonte", TipoServico = "Pintura", Valor = 800.00m, UsuarioId = 103, DataPublicacao = DateTime.Now.AddDays(-1), Ativa = true },
            new Vaga { Id = 4, Titulo = "Babá para Criança de 3 anos", Descricao = "Babá para cuidar de criança de 3 anos no período da tarde. Experiência com crianças necessária.", Cidade = "São Paulo", TipoServico = "Babá", Valor = 25.00m, UsuarioId = 104, DataPublicacao = DateTime.Now.AddDays(-3), Ativa = true },
            new Vaga { Id = 5, Titulo = "Cuidador de Idoso", Descricao = "Cuidador para senhor de 78 anos. Acompanhamento durante o dia, auxílio com medicação e alimentação.", Cidade = "Porto Alegre", TipoServico = "Cuidador de Idosos", Valor = 45.00m, UsuarioId = 105, DataPublicacao = DateTime.Now.AddDays(-7), Ativa = true },
            new Vaga { Id = 6, Titulo = "Encanador para Vazamento", Descricao = "Reparo em vazamento na cozinha. Troca de registro e verificação da tubulação.", Cidade = "Brasília", TipoServico = "Encanador", Valor = 150.00m, UsuarioId = 106, DataPublicacao = DateTime.Now.AddDays(-4), Ativa = true },
            new Vaga { Id = 7, Titulo = "Instalação de Lustre", Descricao = "Instalação de lustre na sala. Preciso de eletricista para instalação segura e profissional.", Cidade = "Salvador", TipoServico = "Eletricista", Valor = 100.00m, UsuarioId = 107, DataPublicacao = DateTime.Now.AddDays(-6), Ativa = true },
            new Vaga { Id = 8, Titulo = "Montagem de Móveis", Descricao = "Montagem de guarda-roupas e estante recém-comprados. Experiência com montagem de móveis necessária.", Cidade = "Fortaleza", TipoServico = "Marceneiro", Valor = 200.00m, UsuarioId = 108, DataPublicacao = DateTime.Now.AddDays(-2), Ativa = true },
            new Vaga { Id = 9, Titulo = "Reparo no Telhado", Descricao = "Reparo em pequeno vazamento no telhado. Troca de telhas e vedação adequada.", Cidade = "Curitiba", TipoServico = "Pedreiro", Valor = 300.00m, UsuarioId = 109, DataPublicacao = DateTime.Now.AddDays(-8), Ativa = true },
            new Vaga { Id = 10, Titulo = "Limpeza Pós-Obra", Descricao = "Limpeza completa após reforma. Remoção de resíduos, limpeza de pisos e paredes.", Cidade = "Recife", TipoServico = "Limpeza", Valor = 250.00m, UsuarioId = 110, DataPublicacao = DateTime.Now.AddDays(-1), Ativa = true }
        };

        private static List<Candidatura> _candidaturas = new List<Candidatura>();
        private static List<Avaliacao> _avaliacoes = new List<Avaliacao>();
        private static int _nextVagaId = 11;
        private static int _nextAvaliacaoId = 1;
        private static int _nextCandidaturaId = 1;
        private static List<Servico> _servicos = new List<Servico>();
        private static int _nextServicoId = 1;
        private static List<ServicoPrestador> _servicosPrestador = new List<ServicoPrestador>();
        private static int _nextServicoPrestadorId = 1;

        public IActionResult OferecerServicos()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                TempData["Erro"] = "Você precisa estar logado para oferecer serviços.";
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // ⭐⭐ MÉTODO: CADASTRAR SERVIÇO PRESTADOR (GET) ⭐⭐
        public IActionResult CadastrarServicoPrestador()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                TempData["Erro"] = "Você precisa estar logado para cadastrar um serviço.";
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // ⭐⭐ MÉTODO: CADASTRAR SERVIÇO PRESTADOR (POST) ⭐⭐
        [HttpPost]
        public IActionResult CadastrarServicoPrestador(ServicoPrestador servico)
        {
            try
            {
                var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
                if (usuarioId == null)
                {
                    TempData["Erro"] = "Você precisa estar logado para cadastrar um serviço.";
                    return RedirectToAction("Login", "Account");
                }

                if (ModelState.IsValid)
                {
                    servico.Id = _nextServicoPrestadorId++;
                    servico.UsuarioId = usuarioId.Value;
                    servico.DataCadastro = DateTime.Now;
                    servico.Ativo = true;

                    _servicosPrestador.Add(servico);

                    TempData["Sucesso"] = "Serviço cadastrado com sucesso! Agora você aparecerá para clientes locais.";
                    return RedirectToAction("MeusServicos");
                }

                // Se houver erros de validação
                TempData["Erro"] = "Por favor, preencha todos os campos obrigatórios.";
                return View(servico);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao cadastrar serviço. Tente novamente.";
                Console.WriteLine($"Erro em CadastrarServicoPrestador: {ex.Message}");
                return View(servico);
            }
        }

        // ⭐⭐ MÉTODO: MEUS SERVIÇOS ⭐⭐
        public IActionResult MeusServicos()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                TempData["Erro"] = "Você precisa estar logado para ver seus serviços.";
                return RedirectToAction("Login", "Account");
            }

            var meusServicos = _servicosPrestador
                .Where(s => s.UsuarioId == usuarioId.Value)
                .OrderByDescending(s => s.DataCadastro)
                .ToList();

            ViewBag.MeusServicos = meusServicos;
            return View();
        }

        // ⭐⭐ MÉTODO: EDITAR SERVIÇO ⭐⭐
        public IActionResult EditarServico(int id)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                TempData["Erro"] = "Você precisa estar logado para editar serviços.";
                return RedirectToAction("Login", "Account");
            }

            var servico = _servicosPrestador.FirstOrDefault(s => s.Id == id && s.UsuarioId == usuarioId.Value);
            if (servico == null)
            {
                TempData["Erro"] = "Serviço não encontrado.";
                return RedirectToAction("MeusServicos");
            }

            return View(servico);
        }

        [HttpPost]
        public IActionResult EditarServico(ServicoPrestador servicoAtualizado)
        {
            try
            {
                var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
                if (usuarioId == null)
                {
                    TempData["Erro"] = "Você precisa estar logado para editar serviços.";
                    return RedirectToAction("Login", "Account");
                }

                var servicoExistente = _servicosPrestador.FirstOrDefault(s => s.Id == servicoAtualizado.Id && s.UsuarioId == usuarioId.Value);
                if (servicoExistente == null)
                {
                    TempData["Erro"] = "Serviço não encontrado.";
                    return RedirectToAction("MeusServicos");
                }

                // Atualizar os dados
                servicoExistente.Titulo = servicoAtualizado.Titulo;
                servicoExistente.Descricao = servicoAtualizado.Descricao;
                servicoExistente.Categoria = servicoAtualizado.Categoria;
                servicoExistente.Valor = servicoAtualizado.Valor;
                servicoExistente.Cidade = servicoAtualizado.Cidade;
                servicoExistente.Telefone = servicoAtualizado.Telefone;
                servicoExistente.Ativo = servicoAtualizado.Ativo;

                TempData["Sucesso"] = "Serviço atualizado com sucesso!";
                return RedirectToAction("MeusServicos");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao atualizar serviço. Tente novamente.";
                Console.WriteLine($"Erro em EditarServico: {ex.Message}");
                return View(servicoAtualizado);
            }
        }

        // ⭐⭐ MÉTODO: EXCLUIR SERVIÇO ⭐⭐
        [HttpPost]
        public IActionResult ExcluirServico(int id)
        {
            try
            {
                var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
                if (usuarioId == null)
                {
                    TempData["Erro"] = "Você precisa estar logado para excluir serviços.";
                    return RedirectToAction("Login", "Account");
                }

                var servico = _servicosPrestador.FirstOrDefault(s => s.Id == id && s.UsuarioId == usuarioId.Value);
                if (servico != null)
                {
                    _servicosPrestador.Remove(servico);
                    TempData["Sucesso"] = "Serviço excluído com sucesso!";
                }
                else
                {
                    TempData["Erro"] = "Serviço não encontrado.";
                }

                return RedirectToAction("MeusServicos");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao excluir serviço. Tente novamente.";
                Console.WriteLine($"Erro em ExcluirServico: {ex.Message}");
                return RedirectToAction("MeusServicos");
            }
        }


        public IActionResult Vagas(string cidade, string tipoServico)
        {
            var vagasFiltradas = _vagas.Where(v => v.Ativa);

            if (!string.IsNullOrEmpty(cidade))
                vagasFiltradas = vagasFiltradas.Where(v => v.Cidade.Contains(cidade, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(tipoServico))
                vagasFiltradas = vagasFiltradas.Where(v => v.TipoServico == tipoServico);

            ViewBag.Vagas = vagasFiltradas.ToList();
            ViewBag.CidadeFiltro = cidade;
            ViewBag.ServicoFiltro = tipoServico;

            return View();
        }

        // ⭐⭐ MÉTODO COMPLETO: CANDIDATAR-SE À VAGA ⭐⭐
        [HttpPost]
        public IActionResult CandidatarVaga(int vagaId, string mensagemCandidatura)
        {
            try
            {
                var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
                if (usuarioId == null)
                {
                    TempData["Erro"] = "Você precisa estar logado para se candidatar a uma vaga.";
                    return RedirectToAction("Login", "Account");
                }

                var vaga = _vagas.FirstOrDefault(v => v.Id == vagaId && v.Ativa);
                if (vaga == null)
                {
                    TempData["Erro"] = "Vaga não encontrada ou indisponível.";
                    return RedirectToAction("Vagas");
                }

                // Verificar se já existe candidatura
                var candidaturaExistente = _candidaturas.FirstOrDefault(c => c.VagaId == vagaId && c.CandidatoId == usuarioId.Value);
                if (candidaturaExistente != null)
                {
                    TempData["Aviso"] = "Você já se candidatou a esta vaga anteriormente.";
                    return RedirectToAction("Vagas");
                }

                // Criar nova candidatura
                var candidatura = new Candidatura
                {
                    Id = _nextCandidaturaId++,
                    VagaId = vagaId,
                    CandidatoId = usuarioId.Value,
                    Mensagem = mensagemCandidatura,
                    DataCandidatura = DateTime.Now,
                    Status = "Pendente"
                };

                _candidaturas.Add(candidatura);

                TempData["Sucesso"] = $"✅ Candidatura enviada com sucesso para: {vaga.Titulo}!";
                return RedirectToAction("MinhasCandidaturas");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao processar candidatura. Tente novamente.";
                return RedirectToAction("Vagas");
            }
        }

        // ⭐⭐ MÉTODO: MINHAS CANDIDATURAS ⭐⭐
        public IActionResult MinhasCandidaturas()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null) return RedirectToAction("Login", "Account");

            var minhasCandidaturas = _candidaturas
                .Where(c => c.CandidatoId == usuarioId.Value)
                .Join(_vagas,
                      candidatura => candidatura.VagaId,
                      vaga => vaga.Id,
                      (candidatura, vaga) => new CandidaturaViewModel
                      {
                          Candidatura = candidatura,
                          Vaga = vaga
                      })
                .OrderByDescending(x => x.Candidatura.DataCandidatura)
                .ToList();

            ViewBag.MinhasCandidaturas = minhasCandidaturas;
            return View();
        }

        // ⭐⭐ MÉTODO: DETALHES DA VAGA COM FORMULÁRIO DE CANDIDATURA ⭐⭐
        public IActionResult DetalhesVaga(int id)
        {
            try
            {
                var vaga = _vagas.FirstOrDefault(v => v.Id == id && v.Ativa);
                if (vaga == null)
                {
                    TempData["Erro"] = "Vaga não encontrada ou indisponível.";
                    return RedirectToAction("Vagas");
                }

                return View(vaga); // ⭐⭐ Passar o model diretamente ⭐⭐
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao carregar detalhes da vaga.";
                return RedirectToAction("Vagas");
            }
        }

        // ⭐⭐ MÉTODO: ENTRAR EM CONTATO ⭐⭐
        public IActionResult EntrarContato(int vagaId)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null) return RedirectToAction("Login", "Account");

            var vaga = _vagas.FirstOrDefault(v => v.Id == vagaId);
            if (vaga == null)
            {
                TempData["Erro"] = "Vaga não encontrada.";
                return RedirectToAction("Vagas");
            }

            TempData["Info"] = $"📞 Entre em contato com o contratante através do telefone: (11) 9XXXX-XXXX<br>" +
                              "💬 Ou envie uma mensagem pelo chat da plataforma!";

            return RedirectToAction("Vagas");
        }

        // ... outros métodos existentes ...
        public IActionResult CadastrarVaga()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarVaga(Servico servico)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null) return RedirectToAction("Login", "Account");

            servico.Id = _nextServicoId++;
            servico.UsuarioId = usuarioId.Value;
            _servicos.Add(servico);

            TempData["Sucesso"] = "Serviço cadastrado com sucesso!";
            return RedirectToAction("MinhasVagas");
        }

        public IActionResult MinhasVagas()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null) return RedirectToAction("Login", "Account");

            var minhasVagas = _servicos.Where(s => s.UsuarioId == usuarioId.Value).ToList();
            ViewBag.MinhasVagas = minhasVagas;

            return View();
        }

        public IActionResult Perfil()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null) return RedirectToAction("Login", "Account");

            var minhasAvaliacoes = _avaliacoes.Where(a => a.TrabalhadorId == usuarioId.Value).ToList();

            double mediaNotas = 0;
            if (minhasAvaliacoes.Any())
            {
                mediaNotas = minhasAvaliacoes.Average(a => a.Nota);
            }

            ViewBag.Avaliacoes = minhasAvaliacoes;
            ViewBag.TotalAvaliacoes = minhasAvaliacoes.Count;
            ViewBag.MediaNotas = mediaNotas;

            return View();
        }
    }

    // ⭐⭐ VIEW MODEL PARA CANDIDATURAS ⭐⭐
    public class CandidaturaViewModel
    {
        public Candidatura Candidatura { get; set; }
        public Vaga Vaga { get; set; }
    }
}