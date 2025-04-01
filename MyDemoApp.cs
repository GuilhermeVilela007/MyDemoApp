using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;


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
        options.AddAdditionalCapability(MobileCapabilityType.App, "storage:filename=mda-2.0.0-21.apk");
        options.AddAdditionalCapability("appPackage", "com.saucelabs.mydemoapp.android");
        options.AddAdditionalCapability("appActivity", "com.saucelabs.mydemoapp.android.view.activities.SplashActivity");
        options.AddAdditionalCapability("newCommandTimeout", 90); 
 
        driver = new AndroidDriver<AndroidElement>(remoteAddres:URI, driverOptions: options, commandTimeout: timeSpan.FromSeconds(180));
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
   driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().resourceId(\"com.saucelabs.mydemoapp.android:id/productIV\").instance(0)"));.Click(); 

   String tituloProduto = driver.FindElement(MobileBy.Id("com.saucelabs.mydemoapp.android:id/productTV")).Text;
   Assert.That(tituloProduto, Is.EqualTo("Sauce Labs Backpack"));

   String precoProduto = driver.FindElement(MobileBy.Id("com.saucelabs.mydemoapp.android:id/priceTV")).Text;
   Assert.That(precoProduto, Is.EqualTo("$ 29.99"));

    ITouchAction touchAction = new ITouchAction(driver); 
        touchAction.Press(469, 1964);
        touchAction.MoveTo(494, 788);
        touchAction.Release();
        touchAction.Perform();

    driver.FindElement(MobileBy.AccessibilityId("Tap to add product to cart")).Click();
    driver.FindElement(MobileBy.AccessibilityId("Tap to add product to cart")).Click();

    tituloProduto = driver.FindElement(MobileBy.Id("com.saucelabs.mydemoapp.android:id/cartIV")).Text;
    Assert.That(tituloProduto, Is.EqualTo("Sauce Labs Backpack"));

    quantidadeCarrinho = driver.FindElement(MobileBy.Id("com.saucelabs.mydemoapp.android:id/itemsTV")).Text;
    Assert.That(quantidadeCarrinho, IsEqualTo(2));

    precoProduto = driver.FindElement(MobileBy.Id("com.saucelabs.mydemoapp.android:id/totalPriceTV")).Text;
     Assert.That(precoProduto, Is.EqualTo("$ 59.98"));
 }



}