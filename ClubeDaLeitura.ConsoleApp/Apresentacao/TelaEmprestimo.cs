using System.IO.Compression;
using ClubeDaLeitura.ConsoleApp.Dominio;

namespace ClubeDaLeitura.ConsoleApp.Apresentacao;

public class TelaEmprestimo
{
    public void Abrir()
    {
        Emprestimo emprestimo = ObterDadosCadastrais();

        string[] erros = emprestimo.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine("---------------------------------");

            Console.ForegroundColor = ConsoleColor.Red;

            for (int i =0; i < erros.Length; i++)
            {
                string erro = erros[i];

                Console.WriteLine(erro);
            }

            Console.ResetColor();
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");        
            Console.ReadLine();

            Abrir();
            return;
        } 

        emprestimo.Abrir();       
    }

    private Emprestimo ObterDadosCadastrais()
    {
        return null;
    }
}
