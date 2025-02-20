﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Binke.Models
{
    public class SitemapGenerator : ISitemapGenerator
    {
        private static readonly XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        private static readonly XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";

        public XDocument GenerateSiteMap(IEnumerable<ISitemapItem> items)
        {
            //Ensure.Argument.NotNull(items, "items");

            var sitemap = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement(xmlns + "urlset",
                      new XAttribute("xmlns", xmlns),
                      new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                      new XAttribute(xsi + "schemaLocation", "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd"),
                      from item in items
                      select CreateItemElement(item)
                      )
                 );

            return sitemap;
        }

        public XElement CreateItemElement(ISitemapItem item)
        {
            var itemElement = new XElement(xmlns + "url", new XElement(xmlns + "loc", item.Url));

            // all other elements are optional

            if (item.LastModified.HasValue)
                itemElement.Add(new XElement(xmlns + "lastmod", item.LastModified.Value.ToString("yyyy-MM-dd")));

            if (item.ChangeFrequency.HasValue)
                itemElement.Add(new XElement(xmlns + "changefreq", item.ChangeFrequency.Value.ToString().ToLower()));

            if (item.Priority.HasValue)
                itemElement.Add(new XElement(xmlns + "priority", item.Priority.Value.ToString("F1", CultureInfo.InvariantCulture)));

            return itemElement;
        }

        //Added by Reena
        public XDocument GenerateSIteMapIndex(IEnumerable<ISitemapItem> items)
        {
            var sitemap = new XDocument(
                new XDeclaration("1.0", "utf-8", "no"),
                    new XElement(xmlns + "sitemapindex",                                          
                      from item in items
                      select CreateIndexItemElement(item)
                      )
                 );

            return sitemap;
        }

        //Added by Reena
        public XElement CreateIndexItemElement(ISitemapItem item)
        {
            var itemElement = new XElement("sitemap", new XElement("loc", "https://www.doctyme.com/Sitemaps/" + item.Url));
            itemElement.Add(new XElement("lastmod", item.LastModified.Value.ToString("yyyy-MM-dd")));         
            return itemElement;
        }
    }
}
