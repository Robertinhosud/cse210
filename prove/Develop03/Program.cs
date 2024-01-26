using System;
using System.Collections.Generic;
using System.Linq;

// Classe que representa uma palavra na escritura
class Palavra
{
    public string Texto { get; set; }
    public bool Oculta { get; set; }

    public Palavra(string texto)
    {
        Texto = texto;
        Oculta = false;
    }
}

// Classe que representa uma referência de escritura
class Referencia
{
    public string Livro { get; set; }
    public int Capitulo { get; set; }
    public int VersiculoInicio { get; set; }
    public int VersiculoFim { get; set; }

    public Referencia(string livro, int capitulo, int versiculoInicio, int versiculoFim)
    {
        Livro = livro;
        Capitulo = capitulo;
        VersiculoInicio = versiculoInicio;
        VersiculoFim = versiculoFim;
    }

    // Método para obter a representação da referência em string
    public override string ToString()
    {
        if (VersiculoInicio == VersiculoFim)
            return $"{Livro} {Capitulo}:{VersiculoInicio}";
        else
            return $"{Livro} {Capitulo}:{VersiculoInicio}-{VersiculoFim}";
    }
}

// Classe que representa uma escritura
class Escritura
{
    private List<Palavra> palavras;

    public Referencia Referencia { get; set; }

    public Escritura(Referencia referencia, string texto)
    {
        Referencia = referencia;
        InicializarPalavras(texto);
    }

    private void InicializarPalavras(string texto)
    {
        string[] palavrasArray = texto.Split(' ');
        palavras = palavrasArray.Select(palavra => new Palavra(palavra)).ToList();
    }

    // Método para ocultar palavras aleatórias
    public void OcultarPalavrasAleatorias()
    {
        Random random = new Random();

        foreach (Palavra palavra in palavras)
        {
            // Se a palavra não estiver oculta, há uma chance aleatória de ocultá-la
            if (!palavra.Oculta && random.Next(2) == 0)
            {
                palavra.Oculta = true;
            }
        }
    }

    // Método para exibir a escritura
    public void ExibirEscritura()
    {

        Console.Clear();
        Console.WriteLine($"{Referencia.ToString()} - Scripture:");

        foreach (Palavra palavra in palavras)
        {
            // Se a palavra estiver oculta, exibe sublinhado, caso contrário, exibe a palavra
            Console.Write(palavra.Oculta ? " _ " : $"{palavra.Texto} ");
        }

        Console.WriteLine("\nPress Enter to continue or write quit to stop the program.");
    }

    // Método para verificar se todas as palavras estão ocultas
    public bool TodasPalavrasOcultas()
    {
        return palavras.All(palavra => palavra.Oculta);
    }
}

class Program
{
    static void Main()
    {
        // Exibe a mensagem de boas-vindas
        ExibirMensagemDeBoasVindas();

        // Aguarda a entrada do usuário antes de criar a escritura de exemplo
        Console.ReadLine();

        // Exibe o menu de escolha de escrituras
        int escolhaEscritura = ExibirMenuDeEscrituras();

        // Criar uma escritura com base na escolha do usuário
        Escritura escritura;

        switch (escolhaEscritura)
        {
            case 1:
                escritura = CriarEscrituraExemplo("John", 3, 16, 16, "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life.");
                break;
            case 2:
                escritura = CriarEscrituraExemplo(" 1 Nephi", 3, 7, 7, "And it came to pass that I, Nephi, said unto my father: I will go and do the things which the Lord hath commanded, for I know that the Lord giveth no commandments unto the children of men, save he shall prepare a way for them that they may accomplish the thing which he commandeth them.");
                break;
            case 3:
                escritura = CriarEscrituraExemplo("James", 1, 5, 6, "5 If any of you lack wisdom, let him ask of God, that giveth to all men liberally, and upbraideth not; and it shall be given him. 6 But let him ask in faith, nothing wavering. For he that wavereth is like a wave of the sea driven with the wind and tossed.");
                break;
            default:
                Console.WriteLine("Invalid choice. Exiting the program.");
                return;
        }

        do
        {
            // Exibe a escritura
            escritura.ExibirEscritura();

            // Aguarda a entrada do usuário
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                break;
            }

            // Oculta palavras aleatórias e exibe a escritura novamente
            escritura.OcultarPalavrasAleatorias();

        } while (!escritura.TodasPalavrasOcultas());
    }

    static void ExibirMensagemDeBoasVindas()
    {
        Console.WriteLine("Welcome to Scripture Memorizer App!");
        Console.WriteLine("This program will help you memorize a scripture by hiding words with each press of Enter.");
        Console.WriteLine("Type 'quit' at any time to exit.");
        Console.WriteLine();
    }

    static int ExibirMenuDeEscrituras()
    {
        Console.WriteLine("Choose a scripture to practice:");
        Console.WriteLine("1. John 3:16");
        Console.WriteLine("2. 1 Nephi 3:7");
        Console.WriteLine("3. James 1:5-6");

        int escolha;
        while (!int.TryParse(Console.ReadLine(), out escolha) || escolha < 1 || escolha > 3)
        {
            Console.WriteLine("Invalid choice. Try again:");
        }

        return escolha;
    }

    static Escritura CriarEscrituraExemplo(string livro, int capitulo, int versiculoInicio, int versiculoFim, string texto)
    {
        Referencia referencia = new Referencia(livro, capitulo, versiculoInicio, versiculoFim);
        return new Escritura(referencia, texto);
    }
}
