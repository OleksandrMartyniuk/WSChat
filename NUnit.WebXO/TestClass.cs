using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;



namespace NUnit.WebXO
{
    [TestFixture]
    public class TestClass
    {
        IWebDriver driver = new ChromeDriver();


        [Test]

        // тест - когда пароль - это недопустимый знак пробел

        [TestCase("Anna", " ", "loka_05@mail.ru", "take away spaces!")]

        public void TestMethodValid0(string name, string pass, string mail, string result)


        {
            driver.Url = "file:///C:/Users/annag/Desktop/XO/XOWeb/html/WebClientXO.html";
            driver.FindElement(By.Id("textLogin")).SendKeys(name);
            driver.FindElement(By.Id("textPassword")).SendKeys(pass);
            driver.FindElement(By.Id("textEmail")).SendKeys(mail);
            Thread.Sleep(1000);
            driver.FindElement(By.Id("btnLogIn")).Click();

            string res = driver.SwitchTo().Alert().Text;
            Assert.AreEqual(res, result);

            Thread.Sleep(3000);
            driver.SwitchTo().Alert().Accept();

        }

        [Test]

        //тест - когда введен неправильный логин

        [TestCase("Anna", "13", "loka_05@mail.ru", "Please check that you have entered your login and password correctly")]

        public void TestMethodValid2(string name, string pass, string mail, string result)


        {
            driver.Url = "file:///C:/Users/annag/Desktop/XO/XOWeb/html/WebClientXO.html";
            driver.FindElement(By.Id("textLogin")).SendKeys(name);
            driver.FindElement(By.Id("textPassword")).SendKeys(pass);
            driver.FindElement(By.Id("textEmail")).SendKeys(mail);
            Thread.Sleep(1000);
            driver.FindElement(By.Id("btnLogIn")).Click();

            string res = driver.SwitchTo().Alert().Text;
            Assert.AreEqual(res, result);

            Thread.Sleep(3000);
            driver.SwitchTo().Alert().Accept();

        }

        [Test]

        // тест - регистрация имени юзера, которе уже используется

        [TestCase("Anna", "13", "loka_05@mail.ru", "Пользователь существует")]

        public void TestMethodValid3(string name, string pass, string mail, string result)


        {
            driver.Url = "file:///C:/Users/annag/Desktop/XO/XOWeb/html/WebClientXO.html";
            driver.FindElement(By.Id("textLogin")).SendKeys(name);
            driver.FindElement(By.Id("textPassword")).SendKeys(pass);
            driver.FindElement(By.Id("textEmail")).SendKeys(mail);
            Thread.Sleep(1000);
            driver.FindElement(By.Id("btnReg")).Click();

            string res = driver.SwitchTo().Alert().Text;
            Assert.AreEqual(result, res);

            Thread.Sleep(3000);
            driver.SwitchTo().Alert().Accept();

           driver.Quit();
        }


        IWebDriver driver2 = new ChromeDriver();

        [Test]
        public void TestMethod_Play()
        {
            //
            // регистрация 2-х играков + игра, где X - победитель
            //

            driver.Url = "file:///C:/Users/annag/Desktop/XO/XOWeb/html/WebClientXO.html";
            Thread.Sleep(7000);

            driver.FindElement(By.Id("textLogin")).SendKeys("Anna");
            driver.FindElement(By.Id("textPassword")).SendKeys("12");
            driver.FindElement(By.Id("textEmail")).SendKeys("loka_05@mail.ru");
            Thread.Sleep(700);
            //driver.FindElement(By.Id("btnReg")).Click();
            //driver.SwitchTo().Alert().Accept();
            //Thread.Sleep(700);
            driver.FindElement(By.Id("btnLogIn")).Click();

            driver2.Url = "file:///C:/Users/annag/Desktop/XO/XOWeb/html/WebClientXO.html";
            driver2.FindElement(By.Id("textLogin")).SendKeys("Olga");
            driver2.FindElement(By.Id("textPassword")).SendKeys("12");
            driver2.FindElement(By.Id("textEmail")).SendKeys("loka_05@mail.ru");
            Thread.Sleep(1000);
            //driver2.FindElement(By.Id("btnReg")).Click();
            //driver2.SwitchTo().Alert().Accept();
            Thread.Sleep(700);
            driver2.FindElement(By.Id("btnLogIn")).Click();
            Thread.Sleep(700);
            driver.FindElement(By.Id("btnRefresh")).Click();
            Thread.Sleep(700);
            driver2.FindElement(By.Id("btnRefresh")).Click();

            driver.FindElement(By.Id("Olga")).Click();

            driver.FindElement(By.Id("btnInvite")).Click();
            Thread.Sleep(700);

            driver2.SwitchTo().Alert().Accept();
            driver.SwitchTo().Alert().Accept();

            driver.FindElement(By.Id("b1")).Click();
            Thread.Sleep(700);
            driver2.FindElement(By.Id("b2")).Click();
            Thread.Sleep(700);
            driver.FindElement(By.Id("b4")).Click();
            Thread.Sleep(700);
            driver2.FindElement(By.Id("b5")).Click();
            Thread.Sleep(700);
            driver.FindElement(By.Id("b7")).Click();

            Thread.Sleep(2000);
            string msg = driver.SwitchTo().Alert().Text;
            Assert.AreEqual("X win!", msg);
            Thread.Sleep(1500);
            driver.SwitchTo().Alert().Accept();

            Thread.Sleep(100);
            string msg2 = driver2.SwitchTo().Alert().Text;
            Assert.AreEqual("X win!", msg2);
            Thread.Sleep(200);
            driver2.SwitchTo().Alert().Accept();

            //
            //новая игра, где победитель - O
            //

            driver.FindElement(By.Id("btnRefresh")).Click();
            driver.FindElement(By.Id("Olga")).Click();

            driver.FindElement(By.Id("btnInvite")).Click();
            Thread.Sleep(700);

            driver2.SwitchTo().Alert().Accept();
            driver.SwitchTo().Alert().Accept();

            driver.FindElement(By.Id("b0")).Click();
            Thread.Sleep(700);
            driver2.FindElement(By.Id("b4")).Click();
            Thread.Sleep(700);
            driver.FindElement(By.Id("b3")).Click();
            Thread.Sleep(700);
            driver2.FindElement(By.Id("b6")).Click();
            Thread.Sleep(700);
            driver.FindElement(By.Id("b1")).Click();
            Thread.Sleep(700);
            driver2.FindElement(By.Id("b2")).Click();

            Thread.Sleep(2000);
            string msg3 = driver.SwitchTo().Alert().Text;
            Assert.AreEqual("O win!", msg3);
            Thread.Sleep(700);
            driver.SwitchTo().Alert().Accept();

            Thread.Sleep(200);
            string msg4 = driver2.SwitchTo().Alert().Text;
            Assert.AreEqual("O win!", msg4);
            Thread.Sleep(700);
            driver2.SwitchTo().Alert().Accept();

            //
            //новая игра, где результат игры - НИЧЬЯ
            //

            driver.FindElement(By.Id("btnRefresh")).Click();
            driver.FindElement(By.Id("Olga")).Click();

            driver.FindElement(By.Id("btnInvite")).Click();
            Thread.Sleep(700);

            driver2.SwitchTo().Alert().Accept();
            driver.SwitchTo().Alert().Accept();

            driver.FindElement(By.Id("b0")).Click();
            Thread.Sleep(700);
            driver2.FindElement(By.Id("b4")).Click();
            Thread.Sleep(700);
            driver.FindElement(By.Id("b3")).Click();
            Thread.Sleep(700);
            driver2.FindElement(By.Id("b6")).Click();
            Thread.Sleep(700);
            driver.FindElement(By.Id("b2")).Click();
            Thread.Sleep(700);
            driver2.FindElement(By.Id("b1")).Click();
            Thread.Sleep(700);
            driver.FindElement(By.Id("b7")).Click();
            Thread.Sleep(700);
            driver2.FindElement(By.Id("b5")).Click();
            Thread.Sleep(700);
            driver.FindElement(By.Id("b8")).Click();

            Thread.Sleep(2000);
            string msg5 = driver.SwitchTo().Alert().Text;
            Assert.AreEqual("Draw!", msg5);
            Thread.Sleep(700);
            driver.SwitchTo().Alert().Accept();

            Thread.Sleep(200);
            string msg6 = driver2.SwitchTo().Alert().Text;
            Assert.AreEqual("Draw!", msg6);
            Thread.Sleep(700);
            driver2.SwitchTo().Alert().Accept();

            //
            // выход игроков из игры - LOG_OUT
            //

            Thread.Sleep(700);
            driver.FindElement(By.Id("btnLogOut")).Click();
            Thread.Sleep(700);
            driver2.FindElement(By.Id("btnLogOut")).Click();

            driver2.Quit();


        }



    }
}
