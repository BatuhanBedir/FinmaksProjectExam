using ExamProject.API.Application.DTOs;
using ExamProject.API.Application.IInterfaces;
using HtmlAgilityPack;
using System.ComponentModel;
using System.Net;
using System.Xml.Linq;

namespace ExamProject.API.Application.Services;

public class RssService : IRssService
{
    const string rssUrl = "https://www.wired.com/feed/rss";

    private readonly HttpClient _httpClient;

    public RssService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<CustomResponseDto<List<ArticleDto>>> GetWiredArticle()
    {
        try
        {
            var response = await _httpClient.GetStringAsync(rssUrl);
            var xdoc = XDocument.Parse(response);

            var latestArticles = (from item in xdoc.Descendants("item")
                                  select new ArticleDto
                                  {
                                      Title = item.Element("title")?.Value,
                                      Link = item.Element("link")?.Value,
                                      Content = GetArticleContent(item.Element("link")?.Value)
                                  }).Take(5).ToList();

            return CustomResponseDto<List<ArticleDto>>.Success(latestArticles, HttpStatusCode.OK.GetHashCode());
        }
        catch (Exception ex)
        {
            return CustomResponseDto<List<ArticleDto>>.Fail(new List<string> { $"An error occurred: {ex.Message}" }, (int)HttpStatusCode.InternalServerError);
        }
    }

    private string GetArticleContent(string articleLink)
    {
        var articleResponse = _httpClient.GetStringAsync(articleLink).Result;
        var articleDoc = new HtmlDocument(); //içeriği html olarak ayrıştırır
        articleDoc.LoadHtml(articleResponse);

        var contentElement = articleDoc.DocumentNode.SelectSingleNode("//div[@class='body__inner-container']");

        return contentElement?.InnerText; //html özel karakterlerini düzeltir.
    }
    //private string GetArticleContent2(string articleLink)
    //{
    //    var articleResponse = _httpClient.GetStringAsync(articleLink).Result;
    //    var articleDoc = new HtmlDocument();
    //    articleDoc.LoadHtml(articleResponse);

    //    var contentElement = articleDoc.DocumentNode.SelectSingleNode("//div[@class='body__inner-container']");

    //    if (contentElement is not null)
    //    {
    //        var cleanContent = System.Net.WebUtility.HtmlDecode(contentElement.InnerText);

    //        return cleanContent;
    //    }

    //    return null;
    //}
}
