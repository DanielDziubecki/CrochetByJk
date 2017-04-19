using System.Web.Optimization;
using System.Web.Optimization.React;

namespace CrochetByJk
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
          
            bundles.Add(new ScriptBundle("~/bundles/react")
                .Include(
                    "~/Scripts/react/react-with-addons.js",
                    "~/Scripts/react/react-dom.js",
                    "~/Scripts/react/react-bootstrap.js",
                    "~/Scripts/unitegallery/js/unitegallery.js",
                    "~/Scripts/fileinput.js",
                    "~/Scripts/unitegallery/themes/grid/ug-theme-grid.js",
                    "~/Scripts/footable/js/footable.js"));

            bundles.Add(new BabelBundle("~/bundles/components")
                .Include("~/Content/ReactComponents/ManageProductsPanel.jsx",
                "~/Content/ReactComponents/AddNewProduct.jsx",
                "~/Content/ReactComponents/ProductForm.jsx"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/Fileinput/css/fileinput.css",
                "~/Content/aos.css",
                "~/Content/react-bootstrap-table.css",
                "~/Content/toastr.css",
                "~/Scripts/unitegallery/css/ug-theme-default.css",
                "~/Scripts/unitegallery/css/unite-gallery.css",
                "~/Scripts/footable/css/footable.bootstrap.min.css",
                "~/Content/site.css"));
        }
    }
}