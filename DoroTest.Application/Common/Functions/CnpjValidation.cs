using FluentValidation;

namespace DoroTest.Application.Common.Functions;
public static class CnpjValidation
{
    public static IRuleBuilderInitial<T, string> CnpjValid<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return (IRuleBuilderInitial<T, string>)ruleBuilder.Custom((cnpj, context) =>
        {
            #region Cleaning Data

            cnpj = cnpj.Replace("-", "").Replace(".", "").Replace("/", "");

            #endregion

            #region Checking data patterns
            Span<int> cnpjArray = stackalloc int[14];
            var count = 0;
            foreach (var c in cnpj)
            {
                if (!char.IsDigit(c))
                {
                    context.AddFailure($"'{context.DisplayName}' tem que ser numérico.");
                    return;
                }

                if (char.IsDigit(c))
                {
                    if (count > 14)
                    {
                        context.AddFailure($"'{context.DisplayName}' deve possuir 14 caracteres. Foram informados " + cnpj.Length);
                        return;
                    }

                    cnpjArray[count] = c - '0';
                    count++;
                }
            }

            if (count != 14)
            {
                context.AddFailure($"'{context.DisplayName}' deve possuir 14 caracteres. Foram informados " + cnpj.Length);
                return;
            }
            if (CommonFunctions.VerifyAllCharEqual(ref cnpjArray))
            {
                context.AddFailure($"'{context.DisplayName}' Não pode conter todos os dígitos iguais.");
                return;
            }
            #endregion

            #region Application of the validation rule
            var digits = new int[14];

            var sum = new int[] { 0, 0 };

            try
            {
                var ftmt = "6543298765432";

                for (int n = 0; n < 14; n++)
                {

                    digits[n] = int.Parse(cnpj.Substring(n, 1));

                    if (n <= 11)

                        sum[0] += digits[n] *

                        int.Parse(ftmt.Substring(n + 1, 1));

                    if (n <= 12)

                        sum[1] += digits[n] *

                        int.Parse(ftmt.Substring(n, 1));
                }


                var result = new int[] { 0, 0 };

                var cnpjOk = new bool[] { false, false };

                for (int n = 0; n < 2; n++)
                {

                    result[n] = sum[n] % 11;

                    if (result[n] == 0 || result[n] == 1)
                    {
                        cnpjOk[n] = digits[12 + n] == 0;
                    }
                    else
                    {
                        cnpjOk[n] = digits[12 + n] == 11 - result[n];
                    }
                }

                if (cnpjOk[0] && cnpjOk[1])
                {
                    return;
                }
                else
                {
                    context.AddFailure($"'{context.DisplayName}' Inválido.");
                    return;
                }

            }
            catch
            {
                context.AddFailure($"'{context.DisplayName}' Inválido.");
                return;
            }
            #endregion

        });
    }

    public static bool CnpjValid(string cnpj)
    {
        #region Cleaning Data

        cnpj = cnpj.Replace("-", "").Replace(".", "").Replace("/", "");

        #endregion

        #region Checking data patterns
        Span<int> cnpjArray = stackalloc int[14];
        var count = 0;
        foreach (var c in cnpj)
        {
            if (!char.IsDigit(c))
            {
                return false;
            }

            if (char.IsDigit(c))
            {
                if (count > 14)
                {
                    return false;
                }

                cnpjArray[count] = c - '0';
                count++;
            }
        }

        if (count != 14)
        {
            return false;
        }
        #endregion

        #region Application of the validation rule
        var digits = new int[14];

        var sum = new int[] { 0, 0 };

        try
        {
            var ftmt = "6543298765432";

            for (int n = 0; n < 14; n++)
            {

                digits[n] = int.Parse(cnpj.Substring(n, 1));

                if (n <= 11)

                    sum[0] += digits[n] *

                    int.Parse(ftmt.Substring(n + 1, 1));

                if (n <= 12)

                    sum[1] += digits[n] *

                    int.Parse(ftmt.Substring(n, 1));
            }


            var result = new int[] { 0, 0 };

            var cnpjOk = new bool[] { false, false };

            for (int n = 0; n < 2; n++)
            {

                result[n] = sum[n] % 11;

                if (result[n] == 0 || result[n] == 1)
                {
                    cnpjOk[n] = digits[12 + n] == 0;
                }
                else
                {
                    cnpjOk[n] = digits[12 + n] == 11 - result[n];
                }
            }

            if (cnpjOk[0] && cnpjOk[1])
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        catch
        {
            return false;
        }
        #endregion
    }


}