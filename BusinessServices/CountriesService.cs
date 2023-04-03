﻿using Entities;
using Repository;

namespace BusinessServices;

public class CountriesService
{
    private CountriesRepository CountriesRepository { get; set; }

    public CountriesService(CountriesRepository countriesRepository)
    {
        CountriesRepository = countriesRepository;
    }

    public List<Country> GetAll()
    {
        return CountriesRepository.GetAll();
    }

    public Country GetById(int id)
    {
        return CountriesRepository.GetById(id);
    }

    public void DeleteById(int id)
    {
        CountriesRepository.DeleteById(id);
    }
}