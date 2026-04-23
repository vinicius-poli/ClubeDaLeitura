using System;
using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;

namespace ClubeDaLeitura.ConsoleApp.Infraestrutura;

public class RepositorioEmprestimo
{
    private Emprestimo?[] emprestimos = new Emprestimo[100];

    public void Cadastrar(Emprestimo emprestimo)
    {
        for (int i = 0; i < emprestimos.Length; i++)
        {
            if (emprestimos[i] == null)
            {
                emprestimos[i] = emprestimo;
                break;
            }
        }
    }

    public Emprestimo?[] SelecionarTodos()
    {
        return emprestimos;
    }
}
