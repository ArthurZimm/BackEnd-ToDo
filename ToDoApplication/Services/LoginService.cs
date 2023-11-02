using Microsoft.AspNetCore.Mvc;
using ToDoApplication.Models;
using ToDoApplication.Repositories;

namespace ToDoApplication.Services;

public class LoginService
{
    private LoginRepository _repository;
    public LoginService(LoginRepository repository)
    {
        _repository = repository;
    }

    public bool ValidaLogin(LoginModel login)
    {
        if (_repository.InsereUsuarioBanco(login))
            return true;
        return false;
    }

    public List<LoginModel> GetUsuarios()
    {
        return _repository.GetUsuarios();
    }

    /*
    public List<LoginModel> GetUsuario(string email)
    {
        return _repository.GetUsuario(email);
    }
    */
    public bool ValidaUsuarios(List<LoginModel> lista)
    {
        if (lista != null)
        {
            return true;
        }
        return false;
    }

    public bool ValidaUsuario(List<LoginModel> usuario)
    {
        if (usuario != null)
        {
            return true;
        }

        return false;
    }

    public bool AtualizaSenha(string cpf, string senha, string senhaAtual)
    {
        if(_repository.AtualizaSenha(cpf, CriptografiaDeSenha(senha),senhaAtual))
            return true;
        return false;
    }

    public bool DeletaUsuario(string cpf, string senha)
    {
        if (_repository.DeletaUsuario(senha, cpf))
            return true;
        return false;
    }
    
    public string CriptografiaDeSenha(string senha)
    {
        List<char> chars = new List<char>();
        List<int> criptInt = new List<int>();
        chars.AddRange(senha);

        foreach (char item in chars)
        {
            int valor = (int)item;
            if (valor >= 65 && valor <= 67)
                valor += 23;
            else if (valor >= 88 && valor <= 90)
                valor -= 23;
            else
                valor -= 3;
            criptInt.Add(valor);
        }
        chars = criptInt.Select(asciiValue => (char)asciiValue).ToList();
        string palavra = new string(chars.ToArray());
        return palavra;
    }
    
    public string DescriptografiaDeSenha(string senha)
    {
        List<char> chars = new List<char>();
        List<int> criptInt = new List<int>();
        chars.AddRange(senha);

        foreach (char item in chars)
        {
            int valor = (int)item;
            if (valor >= 88 && valor <= 90)
                valor -= 23;
            else
                valor += 3;
            criptInt.Add(valor);
        }
        chars = criptInt.Select(asciiValue => (char)asciiValue).ToList();
        string palavra = new string(chars.ToArray());
        return palavra;
    }
    

}