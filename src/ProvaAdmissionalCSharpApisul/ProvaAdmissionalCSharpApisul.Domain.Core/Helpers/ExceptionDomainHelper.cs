namespace ProvaAdmissionalCSharpApisul.Domain.Core.Helpers
{
    public sealed class ExceptionDomainHelper
    {
        public static void Validar(bool regraInvalida, string mensagem)
        {
            if (regraInvalida)
                throw new ApplicationException(mensagem);
        }
    }
}