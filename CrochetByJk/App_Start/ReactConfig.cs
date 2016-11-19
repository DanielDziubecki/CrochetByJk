using React;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CrochetByJk.ReactConfig), "Configure")]

namespace CrochetByJk
{
	public static class ReactConfig
	{
		public static void Configure()
		{
		    ReactSiteConfiguration.Configuration
		        .AddScript("~/Content/ReactComponents/Navbar.jsx")
		        .AddScript("~/Content/ReactComponents/Footer.jsx")
		        .AddScript("~/Content/ReactComponents/ItemContainer.jsx")
		        .AddScript("~/Content/ReactComponents/Baner.jsx");
		    // If you want to use server-side rendering of React components, 
		    // add all the necessary JavaScript files here. This includes 
		    // your components as well as all of their dependencies.
		    // See http://reactjs.net/ for more information. Example:
		    //ReactSiteConfiguration.Configuration
		    //	.AddScript("~/Scripts/First.jsx")
		    //	.AddScript("~/Scripts/Second.jsx");

		    // If you use an external build too (for example, Babel, Webpack,
		    // Browserify or Gulp), you can improve performance by disabling 
		    // ReactJS.NET's version of Babel and loading the pre-transpiled 
		    // scripts. Example:
		    //ReactSiteConfiguration.Configuration
		    //	.SetLoadBabel(false)
		    //	.AddScriptWithoutTransform("~/Scripts/bundle.server.js")
		}
	}
}