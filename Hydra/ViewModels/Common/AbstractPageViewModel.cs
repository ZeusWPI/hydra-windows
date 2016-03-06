using Prism.Windows.AppModel;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.ViewModels.Common {
    /// <summary>
    /// A base class for all viewmodels responsible for a Page
    /// </summary>
    public abstract class AbstractPageViewModel : ViewModelBase {

        private readonly IResourceLoader resourceLoader;
        public string Title { get { return LoadResource("Title"); } }

        public AbstractPageViewModel(IResourceLoader resourceLoader) {
            this.resourceLoader = resourceLoader;
        }

        /// <summary>
        /// Returns the prefix for the current view model's resources.
        /// </summary>
        /// <returns>The view model's prefix</returns>
        protected string GetResourcePrefix() {
            return this.GetType().Name + "_";
        }

        /// <summary>
        /// Returns the resource value (loaded with the correct prefix).
        /// </summary>
        /// <param name="property"></param>
        /// <returns>The resource value</returns>
        protected string LoadResource(string property) {
            return resourceLoader.GetString(GetResourcePrefix() + property);
        }
    }
}
