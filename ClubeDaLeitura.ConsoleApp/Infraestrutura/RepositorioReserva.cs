using ClubeDaLeitura.ConsoleApp.Dominio;

namespace ClubeDaLeitura.ConsoleApp.Infraestrutura;

public class RepositorioReserva
{
    private Reserva?[] reservas = new Reserva[100];

    public void Cadastrar(Reserva reserva)
    {
        for (int i = 0; i < reservas.Length; i++)
        {
            if (reservas[i] == null)
            {
                reservas[i] = reserva;
                break;
            }
        }
    }

    public bool Excluir(string idSelecionado)
    {
        for (int i = 0; i < reservas.Length; i++)
        {
            Reserva? r = reservas[i];

            if (r == null)
                continue;

            if (r.Id == idSelecionado)
            {
                reservas[i] = null;
                return true;
            }
        }

        return false;
    }

    public Reserva?[] SelecionarTodos()
    {
        return reservas;
    }

    internal Reserva? SelecionarPorId(string idSelecionado)
    {
        for (int i = 0; i < reservas.Length; i++)
        {
            Reserva? r = reservas[i];

            if (r == null)
                continue;

            if (r.Id == idSelecionado)
            {
                return r;                
            }
        }

        return null;
    }
}
