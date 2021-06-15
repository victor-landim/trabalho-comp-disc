using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace TrabalhoComp.Utils
{
    public static class Utils
    {
        public static bool CpfValido(this string cpf)
        {
            var expressao = new Regex(@"\D");
            var cpfSequencial = new Regex("^(.)\\1+$");

            cpf = expressao.Replace(cpf, "");

            if (cpf.Length != 11)
            {
                return false;
            }

            if (cpfSequencial.Match(cpf).Success)
            {
               return false;
            }

            var soma = 0;
            var cpfArray = cpf.ToArray();
            for (int i = 8, j = 2; i >= 0; i--, j++)
            {
                soma += Convert.ToInt32(cpfArray[i].ToString()) * j;
            }

            var resto = soma * 10 % 11;
            if (resto == 10)
            {
                resto = 0;
            }

            if (resto != Convert.ToInt32(cpfArray[9].ToString()))
            {
                return false;
            }

            soma = 0;
            resto = 0;
            for (int i = 9, j = 2; i >= 0; i--, j++)
            {
                soma += Convert.ToInt32(cpfArray[i].ToString()) * j;
            }

            resto = soma * 10 % 11;
            if (resto == 10)
            {
                resto = 0;
            }

            if (resto != Convert.ToInt32(cpfArray[10].ToString()))
            {
                return false;
            }

            return true;
        }
    }
}