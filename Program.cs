using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;

namespace SeleniumTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var service = ChromeDriverService.CreateDefaultService(AppDomain.CurrentDomain.BaseDirectory);
                IWebDriver driver = new ChromeDriver(service);
                driver.Navigate().GoToUrl("https://www.n11.com/giris-yap");
                driver.Manage().Window.Maximize();

                IWebElement usernameInput = driver.FindElement(By.Id("email"));
                IWebElement passwordInput = driver.FindElement(By.Id("password"));

                string userName = ConfigurationManager.AppSettings["username"].ToString();
                string password = ConfigurationManager.AppSettings["password"].ToString();

                usernameInput.SendKeys(userName);
                passwordInput.SendKeys(password);

                IWebElement loginButton = driver.FindElement(By.Id("loginButton"));
                loginButton.Submit();

                //driver.Navigate().GoToUrl("https://www.n11.com/hesabim/kuponlarim");
                driver.Navigate().GoToUrl("https://www.n11.com/static/new-design/static/output/couponCenter/index.html");

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
                IWebElement myElement = wait.Until<IWebElement>(d =>
                {
                    IWebElement couponList = driver.FindElement(By.ClassName("couponWrapper"));
                    return couponList;
                });

                var couponCards = driver.FindElements(By.ClassName("couponCard"));
                string message = $"{couponCards.Count} adet kuponunuz bulunmaktadır.";
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(message);
                driver.Quit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        private static void Senaryo1()
        {
            var service = ChromeDriverService.CreateDefaultService(AppDomain.CurrentDomain.BaseDirectory);
            IWebDriver driver = new ChromeDriver(service);
            // Web sayfasını aç
            driver.Navigate().GoToUrl("https://www.n11.com/");
            // Sayfanın başlığını al
            string pageTitle = driver.Title;
            // Başlığı kontrol et
            if (pageTitle.Contains("Alışveriş"))
            {
                Console.WriteLine("Test Başarılı!");
            }
            else
            {
                Console.WriteLine("Test Başarısız!");
            }
            // Tarayıcıyı kapat
            driver.Quit();
            Console.ReadLine();
        }
    }
}