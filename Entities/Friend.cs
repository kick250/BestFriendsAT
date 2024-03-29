﻿using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Friend
{
    public int? Id { get; set; }
    [Required(ErrorMessage = "Um amigo precisa de um nome.")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Um amigo precisa de um sobrenome.")]
    public string? LastName { get; set; }
    [Required(ErrorMessage = "Um amigo precisa de um email.")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Um amigo precisa de um telefone.")]
    public string? Phone { get; set; }
    [Required(ErrorMessage = "Um amigo precisa de uma data de nascimento.")]
    public DateTime? Birthdate { get; set; }
    public string? PhotoUrl { get; set; }
    public int? StateId { get; set; }
    public List<Friend>? Friends { get; set; }
    public Country? Country { get; set; }
    public State? State { get; set; }

    public string GetCountryName()
    {
        if (Country == null) return "";

        return Country.Name ?? "";
    }

    public string GetStateName()
    {
        if (State == null) return "";

        return State.Name ?? "";
    }

    public bool IsFriendOf(int id)
    {
        if (Friends == null) return false;

        return FriendIds().Contains(id);
    }

    public List<int?> FriendIds()
    {
        List<int?> ids = new List<int?>();

        if (Friends == null) return ids;    

        foreach (Friend friend in Friends)
            ids.Add(friend.Id);

        return ids;
    }

    public string GetCountryFlagUrl()
    {
        if (Country == null)
            return "";

        return Country.FlagUrl ?? "";
    }

    public string GetStateFlagUrl()
    {
        if (State == null)
            return "";

        return State.FlagUrl ?? "";
    }

    public string GetFormattedBirthdate()
    {
        if (Birthdate == null) return "";

        var birthdate = DateTime.Parse(Birthdate.ToString() ?? "");

        return birthdate.ToString("MM/dd/yyyy") ?? "";
    }
}
