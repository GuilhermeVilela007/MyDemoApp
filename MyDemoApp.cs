using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;


namespace appium;


public class MyDemoApp
{

  public static string SAUCE_USERNAME = Environment.GetEnvironmentVariable("SAUCE_USERNAME");
  public static string SAUCE_ACCES_KEY = Environment.GetEnvironmentVariable("SAUCE_ACCES_KEY");
  public Uri URI = new Uri($"https://{SAUCE_USERNAME}:{SAUCE_ACCES_KEY}@ondemand.us-west-1.saucelabs.com:443/wd/hub");
  public AndroidDriver<AndroidElement> driver {get; set; }

  [SetUp]
   public void mobileSetup()
  {
    var options = new AppiumOptions(); 
        options.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
        options.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "9.0");
        options.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Samsung Galaxy S9 FHD GoogleAPI Emulator");
        options.AddAdditionalCapability(MobileCapabilityType.App, "storage:filename=mda-2.2.0-25.apk");
        options.AddAdditionalCapability("appPackage", "com.saucelabs.mydemoapp.android");
        options.AddAdditionalCapability("appActivity", "com.saucelabs.mydemoapp.android.view.activities.SplashActivity");
        options.AddAdditionalCapability("newCommandTimeout", 90); 
 
        driver = new AndroidDriver<AndroidElement>(remoteAddress:URI, driverOptions: options, commandTimeout: TimeSpan.FromSeconds(180));
  }

   [TearDown] 
    public void Finalizar()
    {
        if (driver == null) return; 
        driver.Quit();
    }

  [Test]
public void AddBackPack()
  {
   driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().resourceId(\"com.saucelabs.mydemoapp.android:id/productIV\").instance(0)")).Click(); // seleciona a mochila

   String tituloProduto = driver.FindElement(MobileBy.Id("com.saucelabs.mydemoapp.android:id/productTV")).Text;
   Assert.That(tituloProduto, Is.EqualTo("Sauce Labs Backpack")); //valida se o titulo do produto ao seleciona - lo esta correto

   String precoProduto = driver.FindElement(MobileBy.Id("com.saucelabs.mydemoapp.android:id/priceTV")).Text;
   Assert.That(precoProduto, Is.EqualTo("$ 29.99")); // valida se o valor do item esta correto

    TouchAction touchAction = new TouchAction(driver); // arrastar tela para cima/baixo
    touchAction.Press(469, 1964);
    touchAction.MoveTo(494, 788);
    touchAction.Release();
    touchAction.Perform();

    driver.FindElement(MobileBy.AccessibilityId("Increase item quantity")).Click(); // seleciona a quantidade de itens que seje colocar no carrinho(obs: ja vem 1 pré selecionado)
    
    driver.FindElement(MobileBy.AccessibilityId("Tap to add product to cart")).Click(); // inclui o item no carrinho

    driver.FindElement(MobileBy.Id("com.saucelabs.mydemoapp.android:id/cartTV")).Click(); // abre o carrinho


    tituloProduto = driver.FindElement(MobileBy.Id("com.saucelabs.mydemoapp.android:id/titleTV")).Text;
    Assert.That(tituloProduto, Is.EqualTo("Sauce Labs Backpack")); // verifica se o nome do item dentro do carrinho

    precoProduto = driver.FindElement(MobileBy.Id("com.saucelabs.mydemoapp.android:id/totalPriceTV")).Text;
     Assert.That(precoProduto, Is.EqualTo("$ 59.98")); // verifica a soma dos itens de acordo com a quantidade desejada
   

    string quantidadeCarrinho = driver.FindElement(MobileBy.Id("com.saucelabs.mydemoapp.android:id/itemsTV")).Text;
    Assert.That(quantidadeCarrinho, Is.EqualTo("2 Items")); // verifica se a quantidade selecionada esta correta

    
 }



}