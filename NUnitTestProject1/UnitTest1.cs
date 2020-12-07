using NUnit.Framework;

using WebApplication4.Models;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

using System;

namespace Tests
{
    public class Tests
    {                          
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            // Chrome driver. Normally we want this path not hardcoded here
            // But we get better error messages like this.
            // Make sure you download the ChromeDriver compatible with your
            // currently installed Chrome and OS.
            // Website: https://chromedriver.chromium.org/downloads
            driver = new ChromeDriver("D:\\Projects\\ChromeDriver\\86.0.4240.22");
        }

        [TearDown]
        public void Cleanup()
        {
            // Closes the Chrome instance after all tests are done.
            driver.Close();
        }

        [Test]
        public void RemovePersonTest()
        {
            // Open the browser, navigate to URL, change to whatever URL your ISS is
            // running.
            driver.Navigate().GoToUrl("https://localhost:44346/");

            // Wait 10s or until the element with the id "#person_row_3" in our page
            // is loaded
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => driver.FindElement(By.Id("person_list")));

            // Find the Remove link, try to click it.            
            IWebElement person_row_3 = driver.FindElement(By.Id("person_row_3"));
            person_row_3.FindElement(By.TagName("a")).Click();

            // Wait 10s or until the element person_row_3 is removed from DOM.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver =>
            {
                try {
                    driver.FindElement(By.Id("person_row_3"));
                    return false;
                } catch (NoSuchElementException e) {
                    return true;
                }
            });

            // If no timeouts have happened so far, this test succeeds.
        }
    }
}