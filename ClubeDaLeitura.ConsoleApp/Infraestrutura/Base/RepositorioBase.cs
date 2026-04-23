using System;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;

namespace ClubeDaLeitura.ConsoleApp.Infraestrutura.Base;

public class RepositorioBase
{
    protected EntidadeBase?[] registros = new EntidadeBase[100];

    public void Cadastrar(EntidadeBase novaEntidadeBase)
    {
        for (int i = 0; i < registros.Length; i++)
        {
            if (registros[i] == null)
            {
                registros[i] = novaEntidadeBase;
                break;
            }
        }
    }

    public bool Editar(string idSelecionado, EntidadeBase novaEntidadeBase)
    {
        EntidadeBase? registroselecionada = SelecionarPorId(idSelecionado);

        if (registroselecionada == null)
            return false;

        registroselecionada.AtualizarRegistro(novaEntidadeBase);

        return true;
    }

    public bool Excluir(string idSelecionado)
    {
        for (int i = 0; i < registros.Length; i++)
        {
            EntidadeBase? c = registros[i];

            if (c == null)
                continue;

            if (c.Id == idSelecionado)
            {
                registros[i] = null;
                return true;
            }
        }

        return false;
    }

    public EntidadeBase?[] SelecionarTodas()
    {
        return registros;
    }

    public EntidadeBase? SelecionarPorId(string idSelecionado)
    {
        EntidadeBase? registroselecionada = null;

        for (int i = 0; i < registros.Length; i++)
        {
            EntidadeBase? c = registros[i];

            if (c == null)
                continue;

            if (c.Id == idSelecionado)
            {
                registroselecionada = c;
                break;
            }
        }

        return registroselecionada;
    }
}
