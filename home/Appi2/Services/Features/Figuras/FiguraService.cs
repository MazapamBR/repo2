using JaveragesLibrary.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace JaveragesLibrary.Services.Features.Figuras;

public class FiguraService
{
    private readonly List<Figura> _figuras;
    private int _nextId = 1;

    public FiguraService()
    {
        _figuras = new List<Figura>();
    }

    public IEnumerable<Figura> GetAll() => _figuras;

    public Figura? GetById(int id) => _figuras.FirstOrDefault(f => f.Id == id);

    public Figura Add(Figura figura)
    {
        figura.Id = _nextId++;
        Calcular(figura);
        _figuras.Add(figura);
        return figura;
    }

    public bool Update(Figura figuraToUpdate)
    {
        var existing = _figuras.FirstOrDefault(f => f.Id == figuraToUpdate.Id);
        if (existing == null) return false;

        figuraToUpdate.Id = existing.Id;
        Calcular(figuraToUpdate);

        // Reemplaza la figura
        _figuras[_figuras.IndexOf(existing)] = figuraToUpdate;
        return true;
    }

    public bool Delete(int id)
    {
        var figura = _figuras.FirstOrDefault(f => f.Id == id);
        if (figura == null) return false;
        _figuras.Remove(figura);
        return true;
    }

    private void Calcular(Figura figura)
    {
        switch (figura.Tipo.ToLower())
        {
            case "cuadrado":
                figura.Area = figura.Lado * figura.Lado;
                figura.Perimetro = 4 * figura.Lado;
                figura.Volumen = 0;
                break;

            case "rectangulo":
                figura.Area = figura.Base * figura.Altura;
                figura.Perimetro = 2 * (figura.Base + figura.Altura);
                figura.Volumen = 0;
                break;

            case "circulo":
                figura.Area = Math.PI * Math.Pow(figura.Radio, 2);
                figura.Perimetro = 2 * Math.PI * figura.Radio;
                figura.Volumen = 0;
                break;

            case "cilindro":
                figura.Area = 2 * Math.PI * figura.Radio * (figura.Radio + figura.Altura);
                figura.Volumen = Math.PI * Math.Pow(figura.Radio, 2) * figura.Altura;
                figura.Perimetro = 2 * Math.PI * figura.Radio;
                break;

            default:
                figura.Area = 0;
                figura.Perimetro = 0;
                figura.Volumen = 0;
                break;
        }
    }
}
