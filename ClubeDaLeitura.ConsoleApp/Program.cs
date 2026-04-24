using ClubeDaLeitura.ConsoleApp.Apresentacao;
using ClubeDaLeitura.ConsoleApp.Apresentacao.Base;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

RepositorioCaixa repositorioCaixa = new RepositorioCaixa();
RepositorioRevista repositorioRevista = new RepositorioRevista();
RepositorioAmigo repositorioAmigo = new RepositorioAmigo();
RepositorioEmprestimo repositorioEmprestimo = new RepositorioEmprestimo();

TelaPrincipal telaPrincipal = new TelaPrincipal
(
    repositorioCaixa, 
    repositorioRevista, 
    repositorioAmigo, 
    repositorioEmprestimo
);


while (true)
{
    ITela? telaSelecionada = telaPrincipal.ApresentarMenuOpcoesPrincipal();

    if (telaSelecionada == null)
    {
        Console.Clear();
        break;
    }

    while (true)
    {        
        string? opcaoMenuInterno = telaSelecionada.ObterOpcaoMenu();

        if (opcaoMenuInterno == "S")
        {
            Console.Clear();
            break;
        }

        if (telaSelecionada is TelaBase)
        {
            TelaBase telaBase = (TelaBase)telaSelecionada;

            if (opcaoMenuInterno == "1")
            telaBase.Cadastrar();

        else if (opcaoMenuInterno == "2")
            telaBase.Editar();
            
        else if (opcaoMenuInterno == "3")
            telaBase.Excluir();

        else if (opcaoMenuInterno == "4")
            telaBase.VisualizarTodos(deveExibirCabecalho: true);
        }
        
                

        else if (telaSelecionada is TelaEmprestimo)
        {
            TelaEmprestimo telaEmprestimo = (TelaEmprestimo)telaSelecionada;

            opcaoMenuInterno = telaEmprestimo.ObterOpcaoMenu();

            if (opcaoMenuInterno == "S")
            {
                Console.Clear();
                break;
            }
            else if (opcaoMenuInterno == "1")
                telaEmprestimo.Abrir();

            else if (opcaoMenuInterno == "2")
                telaEmprestimo.Concluir();
                
            else if (opcaoMenuInterno == "3")
                telaEmprestimo.VisualizarTodos(deveExibirCabecalho: true);
        }
    }
}