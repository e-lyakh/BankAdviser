using BankAdviser.DAL.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankAdviser.BLL.WPO
{
    public abstract class WebPage
    {
        public WebPage(IWebDriver driver)
        {
            this.driver = driver;            
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.WaitElement));
            action = new Actions(driver);
        }

        protected string pageUrl;

        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected Actions action;

        public string PageUrl
        {
            get { return pageUrl; }
        }

        public bool IsDriverRunning { get; set; }

        private void SwitchToNewTab()
        {
            foreach (var window in driver.WindowHandles)
            {
                driver.SwitchTo().Window(window);
                if (driver.Title == "New Tab" || driver.Title == "Новая вкладка")
                    break;
            }
        }                

        protected IWebElement FindElement(By by)
        {
            IWebElement element;
            try
            {
                element = driver.FindElement(by);
                return element;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
        protected IList<IWebElement> FindElements(By by)
        {
            IList<IWebElement> elements = null;
            try
            {
                elements = driver.FindElements(by);
                return elements;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
        protected IWebElement FindChildElement(IWebElement parentElement, By by)
        {
            IWebElement childElement = null;
            try
            {
                childElement = parentElement.FindElement(by);
                return childElement;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
        protected IList<IWebElement> FindChildElements(IWebElement parentElement, By by)
        {
            IList<IWebElement> childElements = null;
            try
            {
                childElements = parentElement.FindElements(by);
                return childElements;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
        protected IWebElement WaitElementIfExists(By by)
        {
            IWebElement element;
            try
            {
                element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
                return element;
            }
            catch
            {
                return null;
            }
        }
        protected IWebElement WaitUntilElementVisible(By by)
        {
            IWebElement element;
            try
            {
                element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
                return element;
            }
            catch
            {
                return null;
            }
        }
        protected IWebElement WaitUntilElementClickable(By by)
        {
            IWebElement element;
            try
            {
                element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
                return element;
            }
            catch
            {
                return null;
            }
        }
        protected IWebElement WaitUntilElementDisplayed(By by)
        {
            IWebElement element = null;
            while (element == null || !element.Displayed)
                driver.FindElement(by);
            return element;
        }
        protected IWebElement WaitUntilElementNotNull(By by)
        {
            IWebElement element = null;
            while (element == null)
                driver.FindElement(by);
            return element;
        }
        protected IWebElement WaitElementForSec(By by, int seconds = 15)
        {
            IWebElement webElement = null;
            int secCounter = 0;
            while (secCounter <= seconds)
            {
                webElement = FindElement(by);
                if (webElement != null)
                    break;
                Task.Delay(1000).Wait();
                secCounter++;
            }
            return webElement;
        }
        protected Task<IWebElement> WaitElementForSecAsync(By by, int seconds)
        {
            return Task.Run(() =>
            {
                IWebElement webElement = null;
                int secCounter = 0;
                while (secCounter <= seconds)
                {
                    webElement = FindElement(by);
                    if (webElement != null)
                        break;
                    Task.Delay(1000).Wait();
                    secCounter++;
                }
                return webElement;
            });
        }

        protected object ExecuteJsScript(string script, object[] arguments = null)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            object scriptResult = js.ExecuteScript(script, arguments);
            return scriptResult;
        }
        protected object ExecuteJsScript(string script, IWebElement argument)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            object scriptResult = js.ExecuteScript(script, argument);
            return scriptResult;
        }
        protected void AddJsScriptToHead(string scriptContent)
        {
            string script = "var script = document.createElement('script');" +
                            "script.innerHTML = " + scriptContent + ";" +
                            "document.head.appendChild(script);";
            ExecuteJsScript(script);
        }

        protected void OpenUrlInNewTab(string url)
        {
            driver.FindElement(By.CssSelector("body")).SendKeys(Keys.Control + "t");
            SwitchToNewTab();
            while (!(driver.Title == "New Tab" || driver.Title == "Новая вкладка"))
                SwitchToNewTab();
            driver.Navigate().GoToUrl(url);
        }
        protected bool GoToTabWithTextInTitle(string titlePartialText)
        {
            bool isTabFound = false;
            foreach (var tab in driver.WindowHandles)
            {
                driver.SwitchTo().Window(tab);
                if (driver.Title.Contains(titlePartialText))
                {
                    isTabFound = true;
                    break;
                }
            }
            return isTabFound;
        }

        protected void Wait(int sec = 1)
        {
            Task.Delay(sec * 1000).Wait();
        }

        public void GoToPage(WebPage page)
        {
            driver.Navigate().GoToUrl(page.PageUrl);
        }
        public void GoToUrl(string url)
        {
            driver.Navigate().GoToUrl(url);
        }        

        public virtual void Reload()
        {
            if (driver.CurrentWindowHandle != null)
                driver.Navigate().Refresh();
        }

        public void QuitDriver()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }
    }
}