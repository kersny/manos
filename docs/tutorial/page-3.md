Getting Started with Mango
==========================

This is page three of the Getting Started with Mango tutorial.  It assumes you already have
Mango installed and have created the Hello World App from page one and the templates on page two.


Testing Mango Applications
--------------------------

Mango was designed in a way to make testing every aspect of an application dead simple. We can
easily test our routing tables, application logic and templates using nunit mocks and Mango's
interfaced based infrastructure.

Before we get started testing our application though, we're going to need to restructure our
routing code though.


Using Attributes for Routing
----------------------------

Using the Get method in a our application constructor is an easy way to hook up to an HTTP method
but it makes it difficult to test our code. To make our code easily testable we will want to
create a seperate method for our action:

    public static void Hello (IMangoContext ctx)
    {
        IndexHtml.Render (ctx, new {
	    Name = "Mango"
	});
    }

Mango will automatically route your Hello method to the "Hello" URI, so if you rebuild your
application and navigate too http://localhost:8080/Hello we'll see our Hello World page again.

If we want to map our action to a different URL, we can use the HTTP Attributes to map any (or all)
HTTP methods to our method:

    [Get ("$")]
    public static void Hello (IMangoContext ctx)
    {
        IndexHtml.Render (ctx, new {
	    Name = "Mango"
	});
    }


Testing Actions with MangoTestContext
-------------------------------------

Mango includes a number of built in nunit mocking objects that we can use to build tests.  Here
is a simple test to check the output of our Hello Action.

    [Test]
    public void TestHello ()
    {
        var ctx = new MangoTestContext ();

        HelloWorldApp.Hello (ctx);

        Assert.Assert (ctx.PropertySet ("Name"), "is prop set");
        Assert.AreEqual ("Mango", ctx.PropertyValue ("Name"));
    }


Testing Routes with MangoTestRoute
----------------------------------

To ensure your routes are setup correctly Mango offers some convenience functions for testing routing
outcomes.


    [Test]
    public void TestHelloRoute ()
    {
        Assert.AreEqual (MangoTestRoute.Put ("Hello"), HelloWorldApp.Hello);
    }


Testing Templates with MangoTestContext
----------------------------------------

We can also write tests for our template output using MangoTestContext.

    [Test]
    public void TestHelloTemplate ()
    {
        var ctx = new MangoTestContext ();

        IndexHtml.Render (ctx, new {
	    Name = "Mango"
	});

        Assert.AreEqual (ctx.Output, ".....");
    }
