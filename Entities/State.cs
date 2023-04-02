﻿namespace Entities;

public class State
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? FlagUrl { get; set; }
    public Country? Country { get; set; }
}