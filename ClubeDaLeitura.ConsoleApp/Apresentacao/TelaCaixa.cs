using System;
using System.Runtime.CompilerServices;
using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

namespace ClubeDaLeitura.ConsoleApp.Apresentacao;

public class TelaCaixa
{
    private RepositorioCaixa repositorioCaixa;

    public TelaCaixa(RepositorioCaixa rC)
    {
        repositorioCaixa = rC;
    }
    public string? ObterOpcaoMenu()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Gestão de Caixas");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Cadastrar caixa");
        Console.WriteLine("2 - Editar caixa");
        Console.WriteLine("3 - Excluir caixa");
        Console.WriteLine("4 - Visualizar caixas");
        Console.WriteLine("S - Voltar para o início");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenu = Console.ReadLine()?.ToUpper();

        return opcaoMenu;
    }

    public void Cadastrar()
    {
        ExibirCabecalho("Cadastro de Caixa");

        Caixa novaCaixa = ObterDadosCadastrais();

        repositorioCaixa.Cadastrar(novaCaixa);

        Console.WriteLine("---------------------------------");
        Console.WriteLine($"O registro \"{novaCaixa.Id}\" foi cadastrado com sucesso!");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Digite ENTER para continuar...");        
        Console.WriteLine("---------------------------------");        
        
    }    

    internal void Editar()
    {
        ExibirCabecalho("Edição de Caixa");

        Console.WriteLine(
            "{0, -7} |  {1, -20} | {2,-10} | {3, -20}",
            "id", "Etiqueta", "Cor", "Tempo de Empréstimo"
        );

        Caixa?[] caixas = repositorioCaixa.SelecionarTodas();

        for (int i = 0; i < caixas.Length; i++)
        {
            Caixa? c = caixas[i];

            if (c == null)
                continue;
            
            Console.WriteLine(
                "{0, -7} |  {1, -20} | {2,-10} | {3, -20}",
                c.Id, c.Etiqueta, c.Cor, c.DiasDeEmprestimo
            );
        }
        
        Console.WriteLine("---------------------------------");
        
        string? idSelecionado;

        do
        {
            Console.Write("Digite o id do registro que deseja editar: ");
            idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                break;
        } while (true);

        Caixa novaCaixa = ObterDadosCadastrais();

        bool conseguiuEditar = repositorioCaixa.Editar(idSelecionado, novaCaixa);

        if (!conseguiuEditar)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Não possível encontrar o registro requisitado!");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Digite ENTER para continuar...");        
            Console.WriteLine("---------------------------------");
            Console.ReadLine();
            return;   
        }

        Console.WriteLine("---------------------------------");
        Console.WriteLine($"O registro \"{idSelecionado}\" foi editado com sucesso!");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Digite ENTER para continuar...");        
        Console.WriteLine("---------------------------------");
        Console.ReadLine();
        
    }    

    internal void Excluir()
    {
        throw new NotImplementedException();
    }

    internal void VisualizarTodos()
    {
        throw new NotImplementedException();
    }
    

    public void ExibirCabecalho(string titulo)
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Gestão de Caixas");
        Console.WriteLine("---------------------------------");
        Console.WriteLine(titulo);        
        Console.WriteLine("---------------------------------");
    }

    private Caixa ObterDadosCadastrais()
    {
        Console.Write("Informe a etiqueta da caixa: ");
        string? etiqueta = Console.ReadLine();

        Console.WriteLine("---------------------------------");
        Console.WriteLine("Selecione uma das cores válidas:");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("1 - Vermelho");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("2 - Verde");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("3 - Azul");
        Console.ResetColor();
        Console.WriteLine("4 - Branco (Padrão)");
        Console.WriteLine("---------------------------------");

        Console.Write("Informe a cor da caixa: ");
        string? codigoCor = Console.ReadLine();

        string cor;

        if (codigoCor == "1")
            cor = "Vermelho";        
        else if (codigoCor == "2")
            cor = "Verde";        
        else if (codigoCor == "")
            cor = "Azul";
        else
            cor = "Branco";

        Console.Write("Informe o tempo de empréstimo das revistas da caixa: ");
        int diasDeEmprestimo = Convert.ToInt32(Console.ReadLine());

        Caixa novaCaixa = new Caixa(etiqueta, cor, diasDeEmprestimo);

        return novaCaixa;
    }
}
