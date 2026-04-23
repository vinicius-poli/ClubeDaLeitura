using System;
using System.Collections.Specialized;
using ClubeDaLeitura.ConsoleApp.Apresentacao.Base;
using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

namespace ClubeDaLeitura.ConsoleApp.Apresentacao;

public class TelaRevista : TelaBase
{
    private RepositorioRevista repositorioRevista;
    private RepositorioCaixa repositorioCaixa;

    public TelaRevista(RepositorioRevista rR, RepositorioCaixa rC) : base("Revista", rR)
    {
        repositorioRevista = rR;
        repositorioCaixa = rC;
    }
    

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if(deveExibirCabecalho)
            ExibirCabecalho("Visualisação de Revistas");

        Console.WriteLine(
            "{0, -7} | {1, -25} | {2,-6} | {3, -17} | {4, -15} | {5, -10}",
            "id", "Título", "Edição", "Ano de Publicação", "Status", "Caixa"
        );

        EntidadeBase?[] revistas = repositorioRevista.SelecionarTodas();

        for (int i = 0; i < revistas.Length; i++)
        {
            Revista? r = (Revista?)revistas[i];

            if (r == null)
                continue;
            
            Console.Write("{0, -7} | ", r.Id);
            Console.Write("{0, -25} | ", r.Titulo);
            Console.Write("{0, -6} | ", r.NumeroEdicao);
            Console.Write("{0, -17} | ", r.AnoPublicacao);

            string status = r.Status.ToString();
                status = "Disponível";

            Console.Write("{0, -15} | ", status);

            string corSelecionada = r.Caixa.Cor;

            if (corSelecionada == "Vermelho")
                Console.ForegroundColor = ConsoleColor.Red;

            else if (corSelecionada == "Verde")
                Console.ForegroundColor = ConsoleColor.Green;

            else if (corSelecionada == "Azul")
                Console.ForegroundColor = ConsoleColor.Blue;

            Console.Write("{0, -10}", r.Caixa.Etiqueta);

            Console.WriteLine();
        }
        Console.ResetColor();
        Console.WriteLine();
        Console.Write("Digite ENTER para continuar...");
        Console.ReadLine();
    }    

    protected override EntidadeBase ObterDadosCadastrais()
    {
        Console.Write("Digite o título da revista: ");
        string? titulo = Console.ReadLine();

        Console.Write("Digite o número da edição: ");
        int numeroEdicao = Convert.ToInt32(Console.ReadLine());

        Console.Write("Digite o ano de publicação: ");
        int anoPublicacao = Convert.ToInt32(Console.ReadLine());
                
        string idSelecionado = SelecionarCaixa();

        Caixa? caixaSelecionada = (Caixa?)repositorioCaixa.SelecionarPorId(idSelecionado);

        return new Revista(titulo, numeroEdicao, anoPublicacao, caixaSelecionada);
    }

    private string SelecionarCaixa()
    {
        Console.WriteLine("---------------------------------");

        Console.WriteLine(
            "{0, -7} |  {1, -20} | {2,-10} | {3, -20}",
            "id", "Etiqueta", "Cor", "Tempo de Empréstimo"
        );

       EntidadeBase?[] caixas = repositorioCaixa.SelecionarTodas();

        for (int i = 0; i < caixas.Length; i++)
        {
            Caixa? c = (Caixa?)caixas[i];

            if (c == null)
                continue;

            string corSelecionada = c.Cor;

            if (corSelecionada == "Vermelho")
                Console.ForegroundColor = ConsoleColor.Red;

            else if (corSelecionada == "Verde")
                Console.ForegroundColor = ConsoleColor.Green;

            else if (corSelecionada == "Azul")
                Console.ForegroundColor = ConsoleColor.Blue;
            
            Console.WriteLine(
                "{0, -7} |  {1, -20} | {2,-10} | {3, -20}",
                c.Id, c.Etiqueta, c.Cor, c.DiasDeEmprestimo
            );
        }

        Console.ResetColor();

        Console.WriteLine("---------------------------------");
        
        string? idSelecionado;

        do
        {
            Console.Write("Digite o ID da caixa em que deseja guardar a revista: ");
            idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                break;
        } while (true);

        return idSelecionado;
    }
}
