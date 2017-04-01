using React;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CrochetByJk.ReactConfig), "Configure")]

namespace CrochetByJk
{
    public static class ReactConfig
    {
        public static void Configure()
        {
            ReactSiteConfiguration.Configuration
                .AddScript("~/Scripts/react/react-with-addons.js")
                .AddScript("~/Scripts/react/react-dom.js")
                .AddScript("~/Scripts/react/react-bootstrap.js")
                .AddScript("~/Content/ReactComponents/CategoryProducts.jsx")
                .AddScript("~/Content/ReactComponents/ProductDetails.jsx")
                .AddScript("~/Content/ReactComponents/DeleteFromNewsletter.jsx");
        }
    }
}