﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DeWaste.Logging;
using DeWaste.Models.DataModels;

namespace DeWaste.WebServices
{
    public class DatabaseApi : WebApiBase
    {
        IServiceProvider container;
        ILogger logger;

        string url = "http://localhost:5000/";

        public DatabaseApi(IServiceProvider container)
        {
            this.container = container;
            logger = container?.GetService(typeof(ILogger)) as ILogger;
        }

        public async Task<ObservableCollection<Item>> GetSimilar(string name)
        {
            try
            {
                var result = await this.GetAsync(url + "SimilarItems/" + name.ToLower());

                if (result != null)
                {
                    return JsonSerializer.Deserialize<ObservableCollection<Item>>(result);
                }
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
            }
            return new ObservableCollection<Item>();
        }
        
        public async Task<ObservableCollection<Suggestion>> GetSuggestions()
        {
            try
            {
                var result = await this.GetAsync(url + "SimilarItems");

                if (result != null)
                {
                    return JsonSerializer.Deserialize<ObservableCollection<Suggestion>>(result);
                }
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
            }
            return new ObservableCollection<Suggestion>();
        }

        public async Task<Item> GetItem(int id)
        {
            try
            {
                var result = await GetAsync(url + "Items/" + id);

                if (result != null)
                {
                    return JsonSerializer.Deserialize<Item>(result);
                }
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
            }

            return null;
        }

        public async Task<List<Category>> GetCategories(int id)
        {
            try
            {
                var result = await GetAsync(url + "Categories/" + id);

                if (result != null)
                {
                    Guid.NewGuid();
                    return JsonSerializer.Deserialize<List<Category>>(result);
                }
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
            }

            return new List<Category>();
        }
    }
}
