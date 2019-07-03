using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using BookStore.WebUI.HtmlHelper;
using BookStore.WebUI.Models;

namespace BookStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CanGeneratePageLinks()
        {
            // Arrange
            HtmlHelper helper = null;
            PagingInfo pi = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 14,
                ItemsPerPage = 5
            };

            Func<int, string> pageUrldelegate = i => "Page" + i;
            string expectedResult = "<a class=\"btn btn-default\" href=\"Page1\">1</a>"
                                   + "<a class=\"btn btn-default btn-primary selected\" href=\"Page2\">2</a>"
                                   + "<a class=\"btn btn-default\" href=\"Page3\">3</a>";

            // Act
            MvcHtmlString result = helper.PageLinks(pi, pageUrldelegate);

            // Assert
            Assert.AreEqual(expectedResult, result.ToString());
        }

    }
}
