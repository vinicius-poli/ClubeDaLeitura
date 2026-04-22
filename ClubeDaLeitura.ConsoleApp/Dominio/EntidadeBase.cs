using System.Security.Cryptography;

namespace ClubeDaLeitura.ConsoleApp.Dominio;

//classe abstrata não pode ser isntanciada
//só vai definir comportamentos e propriedades dentro do sistema
public abstract class EntidadeBase
{
    public string Id { get; set; } = string.Empty;

    public EntidadeBase()
    {
        Id = Convert.ToHexString(RandomNumberGenerator.GetBytes(20)).ToLower().Substring(0, 7);
    }

    public abstract string[] Validar();

    public abstract void AtualizarRegistro(EntidadeBase novaEntidade);
    
}
