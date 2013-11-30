using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SystemAsset.WebUI;
using SystemAsset.WebUI.Controllers;

namespace SystemAsset.WebUI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // 정렬
            HomeController controller = new HomeController();

            // 동작
            ViewResult result = controller.Index() as ViewResult;

            // 어설션
            Assert.AreEqual("ASP.NET MVC 응용 프로그램을 바로 시작하려면 이 템플릿을 수정하십시오.", result.ViewBag.Message);
        }

        [TestMethod]
        public void About()
        {
            // 정렬
            HomeController controller = new HomeController();

            // 동작
            ViewResult result = controller.About() as ViewResult;

            // 어설션
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Contact()
        {
            // 정렬
            HomeController controller = new HomeController();

            // 동작
            ViewResult result = controller.Contact() as ViewResult;

            // 어설션
            Assert.IsNotNull(result);
        }
    }
}
