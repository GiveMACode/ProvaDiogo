using System;

public class Logicas
{
    public static double CalcularSalarioBruto(double horasTrabalhadas, double valorHora)
    {
        return horasTrabalhadas * valorHora;
    }

    public static double CalcularImpostoRenda(double salarioBruto)
    {
        double imposto = 0.0;

        if (salarioBruto <= 1903.98)
        {
            imposto = 0.0;
        }
        else if (salarioBruto > 1903.98 && salarioBruto <= 2826.65)
        {
            imposto = (salarioBruto - 1903.98) * 0.075 - 142.80;
        }
        else if (salarioBruto > 2826.65 && salarioBruto <= 3751.05)
        {
            imposto = (salarioBruto - 2826.65) * 0.15 - 354.80;
        }
        else if (salarioBruto > 3751.05 && salarioBruto <= 4664.68)
        {
            imposto = (salarioBruto - 3751.05) * 0.225 - 636.13;
        }
        else
        {
            imposto = (salarioBruto - 4664.68) * 0.275 - 869.36;
        }

        return imposto;
    }

    public static double CalcularINSS(double salarioBruto)
    {
        double desconto = 0.0;

        if (salarioBruto <= 1693.72)
        {
            desconto = salarioBruto * 0.08;
        }
        else if (salarioBruto > 1693.72 && salarioBruto <= 2822.90)
        {
            desconto = salarioBruto * 0.09;
        }
        else if (salarioBruto > 2822.90 && salarioBruto <= 5645.80)
        {
            desconto = salarioBruto * 0.11;
        }
        else
        {
            desconto = 621.03;
        }

        return desconto;
    }

    public static double CalcularFGTS(double salarioBruto)
    {
        return salarioBruto * 0.08;
    }

    public static double CalcularSalarioLiquido(double salarioBruto, double descontoIR, double descontoINSS)
    {
        return salarioBruto - descontoIR - descontoINSS;
    }
}