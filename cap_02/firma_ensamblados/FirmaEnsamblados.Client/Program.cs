// See https://aka.ms/new-console-template for more information
using FirmaEnsamblados.Lib;
using System.Security;

ValidaEnsamblados();

static void ValidaEnsamblados()
{
    foreach (var assembly in
        AppDomain.CurrentDomain.GetAssemblies())
    {
        if (assembly.GetName().Name == "FirmaEnsamblados.Lib")
        {
            if (assembly.GetName().GetPublicKeyToken() == null
                || assembly.GetName().GetPublicKeyToken().Length == 0)
            {
                throw new SecurityException("El ensamblado no es válido");
            }
        }
    }
}



Console.WriteLine(Class1.HelloWorld());
