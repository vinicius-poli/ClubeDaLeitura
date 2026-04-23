using System;

namespace ClubeDaLeitura.ConsoleApp.Apresentacao;

public abstract class TelaBase
{
    private string nomeEntidade = string.Empty;

    protected TelaBase(string nomeEntidade)
    {
        this.nomeEntidade = nomeEntidade;
    }

    public string? ObterOpcaoMenu()
    {
        string nomeMinusculo = nomeEntidade.ToLower();
        
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de {nomeEntidade}s");
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"1 - Cadastrar {nomeMinusculo}");
        Console.WriteLine($"2 - Editar {nomeMinusculo}");
        Console.WriteLine($"3 - Excluir {nomeMinusculo}");
        Console.WriteLine($"4 - Visualizar {nomeMinusculo}s");
        Console.WriteLine("S - Voltar para o início");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenu = Console.ReadLine()?.ToUpper();

        return opcaoMenu;
    }

    protected void ExibirCabecalho(string titulo)
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de {nomeEntidade}s");
        Console.WriteLine("---------------------------------");
        Console.WriteLine(titulo);        
        Console.WriteLine("---------------------------------");
    }

    protected void ExibirMensagem(string mensagem)
    {        
        Console.WriteLine("---------------------------------");
        Console.WriteLine(mensagem);
        Console.WriteLine("---------------------------------");
        Console.Write("Digite ENTER para continuar...");        
        Console.ReadLine();
    }
}
