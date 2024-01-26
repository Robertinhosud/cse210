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
        Console.WriteLine($"{Referencia.ToString()} - Escritura:");

        foreach (Palavra palavra in palavras)
        {
            // Se a palavra estiver oculta, exibe sublinhado, caso contrário, exibe a palavra
            Console.Write(palavra.Oculta ? "_ " : $"{palavra.Texto} ");
        }

        Console.WriteLine("\nPressione Enter para continuar...");
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
        // Criar uma escritura de exemplo
        Referencia referencia = new Referencia("João", 3, 16, 16);
        Escritura escritura = new Escritura(referencia, "Porque Deus amou o mundo de tal maneira que deu o seu Filho unigênito, para que todo aquele que nele crê não pereça, mas tenha a vida eterna.");

        do
        {
            escritura.ExibirEscritura();
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                break;
            }

            escritura.OcultarPalavrasAleatorias();

        } while (!escritura.TodasPalavrasOcultas());
    }
}
