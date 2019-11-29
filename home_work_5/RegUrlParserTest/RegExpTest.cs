using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using home_work_5;
using System.IO;

namespace RegUrlParserTest
{
    [TestFixture]
    public class RegExpTest
    {
        [Test]
        public void CorrectInputTest()
        {
            string testData = "qwerasdfzxcv";
            string expected = "an invalid request uri was provided";
            string[] param = { testData };
            string data = Program.GetHtml(testData);
            Assert.AreEqual(null, data, "Программа съела неправильный URL");
            testData = @"http://webcode.me";
            data = Program.GetHtml(testData);
            bool d = data ==null;
            Console.WriteLine(Program.GetHtml(testData));
            Assert.AreEqual(true, (data is string) , "Программа не обработала правильный URL");
        }
        [Test]
        public void CorrectOutputTest()
        {
            List<string> expected = new List<string>();

            List<string> list = Program.ProcessResponse(Data.DataToCheck);
            foreach (var item in list)
                Console.WriteLine(item);
            int expectedN = 9;
            Assert.AreEqual(expectedN, list.Count, "Не совпадает количество!");
        }
    }
    public class ConsoleOutput : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleOutput()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOuput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }
    public class Data
    {
        public static string DataToCheck = @"<!DOCTYPE html>
<html lang='en'>
  <head><meta charset='UTF-8'/>
<meta http-equiv='X-UA-Compatible' content='IE=edge' />
<meta name='viewport' content='width=device-width, initial-scale=1, maximum-scale=1'/>


<meta http-equiv='Cache-Control' content='no-transform' />
<meta http-equiv='Cache-Control' content='no-siteapp' />

<meta name='theme-color' content='#f8f5ec' />
<meta name='msapplication-navbutton-color' content='#f8f5ec'>
<meta name='apple-mobile-web-app-capable' content='yes'>
<meta name='apple-mobile-web-app-status-bar-style' content='#f8f5ec'>

<meta name='description' content='Showdown: MsTest Vs NUnit'/><meta name='keywords' content='Opinion, Test Driven Development, Shizzle.io' /><link rel='alternate' href='/atom.xml' title='Shizzle.io'><link rel='shortcut icon' type='image/x-icon' href='/favicon.ico?v=2.11.0' />
<link rel='canonical' href='http://tommarien.github.io/blog/2012/04/16/showdown-mstest-vs-nunit/'/>

<link rel='stylesheet' type='text/css' href='/lib/nprogress/nprogress.min.css' />
<link rel='stylesheet' type='text/css' href='/css/style.css?v=2.11.0' />

<!-- Global site tag (gtag.js) - Google Analytics -->
<script async src='https://www.googletagmanager.com/gtag/js?id=UA-50637133-1'></script>
<script>
  window.dataLayer = window.dataLayer || [];
  function gtag(){dataLayer.push(arguments);}
  gtag('js', new Date());

  gtag('config', 'UA-50637133-1');
</script><script>
  window.config = {'leancloud':{'app_id':null,'app_key':null},'toc':true,'fancybox':false,'pjax':true,'latex':false};
</script>

    <title>Showdown: MsTest Vs NUnit - Shizzle.io</title>
  </head>

  <body><div id='mobile-navbar' class='mobile-navbar'>
  <div class='mobile-header-logo'>
    <a href='/.' class='logo'>Shizzle.io</a>
  </div>
  <div class='mobile-navbar-icon'>
    <span></span>
    <span></span>
    <span></span>
  </div>
</div>

<nav id='mobile-menu' class='mobile-menu slideout-menu'>
  <ul class='mobile-menu-list'><a href='/'>
        <li class='mobile-menu-item'>Home
          </li>
      </a><a href='/archives/'>
        <li class='mobile-menu-item'>Archives
          </li>
      </a><a href='/about'>
        <li class='mobile-menu-item'>About
          </li>
      </a></ul>
</nav>
<div class='container' id='mobile-panel'>
      <header id='header' class='header'><div class='logo-wrapper'>
  <a href='/.' class='logo'>Shizzle.io</a>
</div>

<nav class='site-navbar'><ul id='menu' class='menu'><li class='menu-item'>
          <a class='menu-item-link' href='/'>
            Home
            </a>
        </li>
      <li class='menu-item'>
          <a class='menu-item-link' href='/archives/'>
            Archives
            </a>
        </li>
      <li class='menu-item'>
          <a class='menu-item-link' href='/about'>
            About
            </a>
        </li>
      </ul></nav>
</header>

      <main id='main' class='main'>
        <div class='content-wrapper'>
          <div id='content' class='content'><article class='post'>
    <header class='post-header'>
      <h1 class='post-title'>Showdown: MsTest Vs NUnit
        </h1>

      <div class='post-meta'>
        <span class='post-time'>
          Apr 16, 2012
        </span><span class='post-reading-time'>
          4 min read
        </span>
      </div>
    </header>

    <div class='post-toc' id='post-toc'>
    <h2 class='post-toc-title'>Contents</h2>
    <div class='post-toc-content'>
      <ol class='toc'><li class='toc-item toc-level-3'><a class='toc-link' href='#Startup'><span class='toc-text'>Startup</span></a><ol class='toc-child'><li class='toc-item toc-level-4'><a class='toc-link' href='#MsTest'><span class='toc-text'>MsTest</span></a></li><li class='toc-item toc-level-4'><a class='toc-link' href='#NUnit'><span class='toc-text'>NUnit</span></a></li></ol></li><li class='toc-item toc-level-3'><a class='toc-link' href='#Attributing-classes-and-methods'><span class='toc-text'>Attributing classes and methods</span></a><ol class='toc-child'><li class='toc-item toc-level-4'><a class='toc-link' href='#MsTest-1'><span class='toc-text'>MsTest</span></a></li><li class='toc-item toc-level-4'><a class='toc-link' href='#NUnit-1'><span class='toc-text'>NUnit</span></a></li></ol></li><li class='toc-item toc-level-3'><a class='toc-link' href='#Categorize-Ignore-…'><span class='toc-text'>Categorize, Ignore, …</span></a><ol class='toc-child'><li class='toc-item toc-level-4'><a class='toc-link' href='#MsTest-2'><span class='toc-text'>MsTest</span></a></li><li class='toc-item toc-level-4'><a class='toc-link' href='#NUnit-2'><span class='toc-text'>NUnit</span></a></li></ol></li><li class='toc-item toc-level-3'><a class='toc-link' href='#Assert'><span class='toc-text'>Assert</span></a><ol class='toc-child'><li class='toc-item toc-level-4'><a class='toc-link' href='#MsTest-3'><span class='toc-text'>MsTest</span></a></li><li class='toc-item toc-level-4'><a class='toc-link' href='#NUnit-3'><span class='toc-text'>NUnit</span></a></li></ol></li><li class='toc-item toc-level-3'><a class='toc-link' href='#Extensibility'><span class='toc-text'>Extensibility</span></a></li><li class='toc-item toc-level-3'><a class='toc-link' href='#Data-driven-tests'><span class='toc-text'>Data driven tests</span></a><ol class='toc-child'><li class='toc-item toc-level-4'><a class='toc-link' href='#MsTest-4'><span class='toc-text'>MsTest</span></a></li><li class='toc-item toc-level-4'><a class='toc-link' href='#NUnit-4'><span class='toc-text'>NUnit</span></a></li></ol></li></ol>
    </div>
  </div><div class='post-content'><p>More and more <strong>businesses</strong> these days seem to understand the <strong>value</strong> of <strong>unit-tests</strong>. They want a safety net to protect their software investment.</p>
<p>But do they know that <strong>choosing a test framework</strong> is a choice that sticks for the entire project lifetime? As no one is going to rewrite all the tests for a specific project, just because the testing framework changed. The cost of doing that would be too high. So choosing a testing framework is really important at the startup of a project.</p>
<p>By the time Team Foundation Server was marketed and integration for MsTest came out of the box, more and more companies thought MsTest is/was the best choice. But is it?</p>
<p>I’ve been using NUnit for the largest part of my career, but the last two years I’ve been seeing more and more businesses having MsTest as test framework.</p>
<p>The test framework choice might come up again with the launch of the new TFS version as <strong>test framework openness</strong> is one of the features.</p>
<p>More and more people i know seem to think that MsTest is equivalent to NUnit or even better. But is it?</p>
<h3 id='Startup'><a href='#Startup' class='headerlink' title='Startup'></a>Startup</h3><h4 id='MsTest'><a href='#MsTest' class='headerlink' title='MsTest'></a>MsTest</h4><p>Needs a specific project type, ‘Test Project’, which means you will automatic have the right reference and the correct project type guid.</p>
<h4 id='NUnit'><a href='#NUnit' class='headerlink' title='NUnit'></a>NUnit</h4><p>Does not require any specific project type, but most of the time people will add a class library to separate their code from their unittests. You need to reference the nunit.framework.dll yourself. But if you use nuget this will be done for you automatic while installing NUnit.</p>
<p><strong>Conclusion</strong>: If you go for out of the box experience i would say MsTest wins this section, otherwise i would say a draw between MsTest and NUnit.</p>
<h3 id='Attributing-classes-and-methods'><a href='#Attributing-classes-and-methods' class='headerlink' title='Attributing classes and methods'></a>Attributing classes and methods</h3><p>Both frameworks separate the apples from the pears trough attributes.</p>
<h4 id='MsTest-1'><a href='#MsTest-1' class='headerlink' title='MsTest'></a>MsTest</h4><ul>
<li>AssemblyInitialize, AssemblyCleanup are two special attributes that can be used to bootstrap or teardown your test assembly</li>
<li>TestClass with ClassInitialize -&gt; TestInitialize -&gt; TestMethod -&gt; TestCleanup -&gt; ClassCleanup</li>
</ul>
<h4 id='NUnit-1'><a href='#NUnit-1' class='headerlink' title='NUnit'></a>NUnit</h4><ul>
<li>TestFixture with TestFixtureSetup -&gt; Setup -&gt; Test -&gt; TearDown -&gt; TestFixtureTearDown</li>
</ul>
<p><strong>Conclusion</strong>: Apart for the missing AssemblyInitialize and Cleanup both frameworks are on par with each other, i must admit MsTest wins this section also, but i would like to point out the fact that having to bootstrap or teardown your test assembly is probably a smell that you are doing something wrong.</p>
<h3 id='Categorize-Ignore-…'><a href='#Categorize-Ignore-…' class='headerlink' title='Categorize, Ignore, …'></a>Categorize, Ignore, …</h3><h4 id='MsTest-2'><a href='#MsTest-2' class='headerlink' title='MsTest'></a>MsTest</h4><ul>
<li>TestCategory</li>
<li>Ignore</li>
<li>Timeout</li>
<li>ExpectedException</li>
</ul>
<h4 id='NUnit-2'><a href='#NUnit-2' class='headerlink' title='NUnit'></a>NUnit</h4><ul>
<li>Category</li>
<li>Ignore</li>
<li>Timeout</li>
<li>ExpectedException : Although this attribute has been deprecated and replaced by Assert.Throws it has more options than the MsTest version</li>
</ul>
<p><strong>Conclusion</strong>: I think both frameworks are on par with each other if you look at the basic attributes, but NUnit still has some gems, like Explicit, which will make the test run only when explicitly told so, but there are many more like SetCulture and SetUiCulture. In terms of language completeness i think NUnit wins this section.</p>
<h3 id='Assert'><a href='#Assert' class='headerlink' title='Assert'></a>Assert</h3><h4 id='MsTest-3'><a href='#MsTest-3' class='headerlink' title='MsTest'></a>MsTest</h4><ul>
<li>Assert, StringAssert and CollectionAssert</li>
</ul>
<h4 id='NUnit-3'><a href='#NUnit-3' class='headerlink' title='NUnit'></a>NUnit</h4><ul>
<li>Assert, StringAssert and CollectionAssert, Exception Asserts ( Assert.Throws, Assert.Catch etc), File Asserts, Directory Asserts</li>
</ul>
<p><strong>Conclusion</strong>: Again the basics are present in both frameworks but the language richness of NUnit makes it to win this section.</p>
<h3 id='Extensibility'><a href='#Extensibility' class='headerlink' title='Extensibility'></a>Extensibility</h3><p>Clearly NUnit wins this section see <a href='http://www.nunit.org/index.php?p=extensibility&amp;r=2.5.10' target='_blank' rel='noopener'>http://www.nunit.org/index.php?p=extensibility&amp;r=2.5.10</a></p>
<h3 id='Data-driven-tests'><a href='#Data-driven-tests' class='headerlink' title='Data driven tests'></a>Data driven tests</h3><h4 id='MsTest-4'><a href='#MsTest-4' class='headerlink' title='MsTest'></a>MsTest</h4><ul>
<li>Takes a file based approach with DataSource to provide testing values, which is nice but you will always have to need to add csv, excel or xml data file</li>
</ul>
<h4 id='NUnit-4'><a href='#NUnit-4' class='headerlink' title='NUnit'></a>NUnit</h4><ul>
<li>Does not have a build in support for filebased datasources but it does have many attributes that can be used to provide values in your test code directly like TestCase and TestCaseSource</li>
</ul>
<p><strong>Conclusion</strong>: This section depends on personal preference. For me i prefer the easy way of NUnit because i see no real point in using a file based approach</p>

      </div>
      
      <footer class='post-footer'>
        <div class='post-tags'>
            <a href='/tags/opinion/'>Opinion</a>
            <a href='/tags/test-driven-development/'>Test Driven Development</a>
            </div>
        
        <nav class='post-nav'><a class='prev' href='/blog/2012/04/21/castle-windsor-avoid-memory-leaks-by-learning-the-underlying-mechanics/'>
        <i class='iconfont icon-left'></i>
        <span class='prev-text nav-default'>Castle Windsor: Avoid memory leaks by learning the underlying mechanics</span>
        <span class='prev-text nav-mobile'>Prev</span>
      </a>
    <a class='next' href='/blog/2012/04/12/take-control-of-your-configuration/'>
        <span class='next-text nav-default'>Take control of your configuration</span>
        <span class='prev-text nav-mobile'>Next</span>
        <i class='iconfont icon-right'></i>
      </a>
    </nav></footer>
    </article></div><div class='comments' id='comments'></div></div>
      </main>

      <footer id='footer' class='footer'><div class='social-links'><a href='mailto:tommarien@gmail.com' target='_blank' rel='noopener' class='iconfont icon-email' title='email'></a>
        <a href='http://twitter.com/Tom_Marien' target='_blank' rel='noopener' class='iconfont icon-twitter' title='twitter'></a>
        <a href='http://linkedin.com/in/tommarien' target='_blank' rel='noopener' class='iconfont icon-linkedin' title='linkedin'></a>
        <a href='https://github.com/tommarien' target='_blank' rel='noopener' class='iconfont icon-github' title='github'></a>
        <a href='/atom.xml' class='iconfont icon-rss' title='rss'></a>
    </div><div class='copyright'>
  <span class='power-by'>
    Powered by <a class='hexo-link' href='https://hexo.io/' target='_blank' rel='noopener'>Hexo</a>
  </span>
  <span class='division'>|</span>
  <span class='theme-info'>
    Theme - 
    <a class='theme-link' href='https://github.com/ahonn/hexo-theme-even' target='_blank' rel='noopener'>Even</a>
  </span>

  <span class='copyright-year'>&copy;2015 - 2019<span class='heart'>
      <i class='iconfont icon-heart'></i>
    </span>
    <span class='author'>Tom Marien</span>
  </span>
</div>
</footer>

      <div class='back-to-top' id='back-to-top'>
        <i class='iconfont icon-up'></i>
      </div>
    </div><script type='text/javascript' src='/lib/jquery/jquery.min.js'></script>
  <script type='text/javascript' src='/lib/slideout/slideout.js'></script>
  <script type='text/javascript' src='/lib/pjax/jquery.pjax.min.js'></script>
  <script type='text/javascript' src='/lib/nprogress/nprogress.min.js'></script>
  <script type='text/javascript' src='/js/src/even.js?v=2.11.0'></script>
</body>
</html>
";
    }
}
