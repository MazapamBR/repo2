namespace JaveragesLibrary.Domain.Entities;

public class Figura
{
    public int Id { get; set; } // Identificador único
    public required string Tipo { get; set; } // Cuadrado, círculo, cilindro, etc.
    public double Base { get; set; }
    public double Altura { get; set; }
    public double Radio { get; set; }
    public double Lado { get; set; }

    // Resultados calculados
    public double Area { get; set; }
    public double Perimetro { get; set; }
    public double Volumen { get; set; }
}
