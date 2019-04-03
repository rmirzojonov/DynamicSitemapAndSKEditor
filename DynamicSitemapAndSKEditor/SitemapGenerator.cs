﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DynamicSitemapAndSKEditor
{
    public class SitemapGenerator
    {
        private readonly PostRepository postRepository;
        public SitemapGenerator()
        {
            postRepository = new PostRepository();
        }

        public IReadOnlyCollection<string> GetSitemapNodes(IUrlHelper urlHelper)
        {
            List<string> nodes = new List<string>();

            nodes.Add(urlHelper.AbsoluteRouteUrl("Default", new { controller = "Home", action = "Index" }));
            nodes.Add(urlHelper.AbsoluteRouteUrl("Default", new { controller = "Home", action = "About" }));
            nodes.Add(urlHelper.AbsoluteRouteUrl("Default", new { controller = "Home", action = "Contact" }));

            foreach (int postId in postRepository.GetPosts())
            {
                nodes.Add(urlHelper.AbsoluteRouteUrl("Default", new { controller = "Home", action = "Post", id = postId }));
            }

            return nodes;
        }

        public string GetSitemapDocument(IEnumerable<string> sitemapNodes)
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement root = new XElement(xmlns + "urlset");

            foreach (string sitemapNode in sitemapNodes)
            {
                XElement urlElement = new XElement(
                    xmlns + "url",
                    new XElement(xmlns + "loc", Uri.EscapeUriString(sitemapNode)));
                root.Add(urlElement);
            }

            XDocument document = new XDocument(root);
            return document.ToString();
        }
    }
    public static class UrlHelperExtensions
    {
        public static string AbsoluteRouteUrl(this IUrlHelper urlHelper,
            string routeName, object routeValues = null)
        {
            string scheme = urlHelper
                .ActionContext
                .HttpContext
                .Request
                .Scheme;
            return urlHelper.RouteUrl(routeName, routeValues, scheme);
        }
    }
}
