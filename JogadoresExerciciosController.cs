using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using copaHAS.Models;
using copaHAS.Models.Enum;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace copaHAS.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class JogadoresExerciciosController : ControllerBase
    {
        public static List<Jogador> listaJogadores = new List<Jogador>
    {
        // GOLEIRO
        new Jogador(){ Id=1, Nome="Hugo Souza", NumeroCamisa=1, Posicao="Goleiro", Status=Models.Enum.StatusJogador.Titular },

        // DEFESA
        new Jogador(){ Id=2, Nome="Matheuzinho", NumeroCamisa=2, Posicao="Lateral Direito", Status=Models.Enum.StatusJogador.Titular },
        new Jogador(){ Id=3, Nome="Felix Torres", NumeroCamisa=3, Posicao="Zagueiro", Status=Models.Enum.StatusJogador.Titular },
        new Jogador(){ Id=4, Nome="Andre Ramalho", NumeroCamisa=5, Posicao="Zagueiro", Status=Models.Enum.StatusJogador.Titular },
        new Jogador(){ Id=5, Nome="Matheus Bidu", NumeroCamisa=21, Posicao="Lateral Esquerdo", Status=Models.Enum.StatusJogador.Titular },

        // MEIO CAMPO
        new Jogador(){ Id=6, Nome="Raniele", NumeroCamisa=14, Posicao="Volante", Status=Models.Enum.StatusJogador.Titular },
        new Jogador(){ Id=7, Nome="Breno Bidon", NumeroCamisa=27, Posicao="Meia", Status=Models.Enum.StatusJogador.Titular },
        new Jogador(){ Id=8, Nome="Rodrigo Garro", NumeroCamisa=10, Posicao="Meia", Status=Models.Enum.StatusJogador.Titular },

        // ATAQUE
        new Jogador(){ Id=9, Nome="Memphis Depay", NumeroCamisa=94, Posicao="Atacante", Status=Models.Enum.StatusJogador.Titular },
        new Jogador(){ Id=10, Nome="Yuri Alberto", NumeroCamisa=9, Posicao="Atacante", Status=Models.Enum.StatusJogador.Titular },
        new Jogador(){ Id=11, Nome="Vitinho", NumeroCamisa=11, Posicao="Atacante", Status=Models.Enum.StatusJogador.Titular },

        // RESERVAS IMPORTANTES
        new Jogador(){ Id=12, Nome="Matheus Donelli", NumeroCamisa=32, Posicao="Goleiro", Status=Models.Enum.StatusJogador.Reserva },
        new Jogador(){ Id=13, Nome="Caca", NumeroCamisa=25, Posicao="Zagueiro", Status=Models.Enum.StatusJogador.Reserva },
        new Jogador(){ Id=14, Nome="Charles", NumeroCamisa=35, Posicao="Volante", Status=Models.Enum.StatusJogador.Reserva },
        new Jogador(){ Id=15, Nome="Andre Carrillo", NumeroCamisa=19, Posicao="Meia", Status=Models.Enum.StatusJogador.Reserva },

        // BASE
        new Jogador(){ Id=16, Nome="Gui Negao", NumeroCamisa=99, Posicao="Atacante", Status=Models.Enum.StatusJogador.Titular },
        new Jogador(){ Id=17, Nome="Kayke", NumeroCamisa=37, Posicao="Atacante", Status=Models.Enum.StatusJogador.Reserva }
    };

            
            [HttpGet("Get/{nome}")]
           public IActionResult GetByNome(string nome)
           {
            var lista = listaJogadores.Where(j => j.Nome.ToLower().StartsWith(nome.ToLower())).ToList();
                if(lista.Count == 0)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(lista);
                }
           }

            [HttpGet("GetTitulares")]

            public IActionResult GetTitulares()
            {  

                var titulares = listaJogadores.Where(j => j.Status == Models.Enum.StatusJogador.Titular)
                .OrderByDescending(j => j.NumeroCamisa).ToList();
                return Ok(titulares);
            }

            [HttpGet("GetEstatisticas")]
            public IActionResult GetEstatisticas()
            {
                var quantidade = listaJogadores.Count;
                var somaCamisas = listaJogadores.Sum(j => j.NumeroCamisa);

                return Ok(new
                {
                    QuantidadeJogadores = quantidade, SomaNumerosCamisa = somaCamisas
                });
            }

            [HttpPost("PostValidacao")]
            public IActionResult PostValidacao([FromBody] Jogador jogador)
            {
                if (jogador.NumeroCamisa > 100)
                {
                    return BadRequest("Número da camisa não pode ser maior que 100");
                }

                listaJogadores.Add(jogador);

                return Ok(jogador);
            }

  [HttpPost("PostValidacaoNome")]
            public IActionResult PostValidacaoNome([FromBody] Jogador jogador)
            {
                if (jogador.Posicao.ToLower() != "goleiro" && jogador.NumeroCamisa == 1)
                {
                    return BadRequest("Número da camisa não pode ser maior que 100");
                }

                listaJogadores.Add(jogador);

                return Ok(jogador);
            }

[HttpGet("GetByStatus/{status}")]
            public IActionResult GetByStatus(Models.Enum.StatusJogador status)
            {
                var jogadores = listaJogadores.Where(j => j.Status == status).ToList();
                return Ok(jogadores);
            }

  }
}
