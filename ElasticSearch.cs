using Elasticsearch.Net;
using Nest;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

// https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/nest-getting-started.html
// https://miroslavpopovic.com/posts/2018/07/elasticsearch-with-aspnet-core-and-docker

//https://github.com/miroslavpopovic/miniblog-elasticsearch

public static class ElasticsearchExtensions
{
    public static void AddElasticsearch(
        this IServiceCollection services, IConfiguration configuration)
    {


        var url = configuration["elasticsearch:url"];
        var defaultIndex = configuration["elasticsearch:index"];

        var settings = new ConnectionSettings(new Uri(url))
            .DefaultIndex(defaultIndex);

        AddDefaultMappings(settings);

        var client = new ElasticClient(settings);

        services.AddSingleton<IElasticClient>(client);
    }

    private static void AddDefaultMappings(ConnectionSettings settings)
    {
        settings
            .DefaultMappingFor<Person>(m => m
                .PropertyName(p => p.id, "id")
            );
            // .DefaultMappingFor<Comment>(m => m
            //     .Ignore(c => c.Email)
            //     .Ignore(c => c.IsAdmin)
            //     .PropertyName(c => c.ID, "id")
            // );
    }
}

public class Comment
{
    [Required]
    public string ID { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string Author { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Content { get; set; }

    [Required]
    public DateTime PubDate { get; set; } = DateTime.UtcNow;

    public bool IsAdmin { get; set; }
}

public class Post
{
    [Required]
    public string ID { get; set; } = DateTime.UtcNow.Ticks.ToString();

    [Required]
    public string Title { get; set; }

    public string Slug { get; set; }

    [Required]
    public string Excerpt { get; set; }

    [Required]
    public string Content { get; set; }

    public DateTime PubDate { get; set; } = DateTime.UtcNow;

    public DateTime LastModified { get; set; } = DateTime.UtcNow;

    public bool IsPublished { get; set; } = true;
}



public class Person
{
    public int id { get; set; }

    public string first_name { get; set; }

    public string last_name { get; set; }

    public string gender { get; set; }

    public string ip_address { get; set; }

    public string cartype { get; set; }

    public string state { get; set; }

    public string zipcode { get; set; }
}

