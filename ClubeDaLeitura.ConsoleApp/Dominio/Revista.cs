using System;
using System.Dynamic;
using ClubeDaLeitura.ConsoleApp.Apresentacao;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

namespace ClubeDaLeitura.ConsoleApp.Dominio;

public class Revista : EntidadeBase
{    
    public string Titulo { get; set; }
    public int NumeroEdicao { get; set; }
    public int AnoPublicacao { get; set; }
    public Caixa Caixa { get; set; }

    private RepositorioRevista repositorioRevista;
    private RepositorioBase repositorioBase;

    private Revista?[] revistas = new Revista[100];

    public Revista(string titulo, int numeroEdicao, int anoPublicacao, Caixa caixa)
    {        
        Titulo = titulo;
        NumeroEdicao = numeroEdicao;
        AnoPublicacao = anoPublicacao;
        Caixa = caixa;        
    }

    public override string[] Validar()
    {
        string erros = string.Empty;          

        if (string.IsNullOrWhiteSpace(Titulo))
        {
            erros += "O campo \"Titulo\" é obrigatório;";            
        }

        else if (Titulo.Length < 2 || Titulo.Length > 100)
        {
            erros += "O campo \"Titulo\" deve conter entre 2 e 100 caracteres;";            
        }

        if (NumeroEdicao < 0)
        {
           erros += "O campo \"Número da Edição\" deve conter um valor igual ou maior que 0;";            
        }
        
        int anoAtual = DateTime.Now.Year;

        if (AnoPublicacao < 1 || AnoPublicacao > anoAtual)
        {
           erros += "O campo \"Ano de Publicação\" deve conter uma data válida;";            
        }

        if (Caixa == null)
            erros += "O campo \"Caixa\" deve conter uma caixa válida;"; 

        return erros.Split(';', StringSplitOptions.RemoveEmptyEntries);       
    }    

    public override void AtualizarRegistro(EntidadeBase novaEntidade)
    {
        Revista novaRevista = (Revista)novaEntidade;

        Titulo = novaRevista.Titulo;
        NumeroEdicao = novaRevista.NumeroEdicao;
        AnoPublicacao = novaRevista.AnoPublicacao;
        Caixa = novaRevista.Caixa;
    }
}
